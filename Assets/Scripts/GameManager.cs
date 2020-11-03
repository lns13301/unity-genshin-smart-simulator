using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerData playerData;

    public bool isMobile = true;

    public Text intertwinedFateText;
    public Text acquantFateText;

    public GameObject notice;
    public Text noticeText;
    public string AD_REWORD_TEXT = "광고시청 보상을 획득하였습니다.\n\n(만남의 인연 500개, 뒤얽힌 인연 500개)";
    public string LACK_OF_WISH = "보유한 재화가 부족합니다.";
    public string UPDATE_YET = "아직 구현되지 않은 기능입니다.";
    public string WANT_SKIP = "건너뛰시겠습니까?";
    public string END_DATE = "체험 기간이 종료되었습니다.";

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        LoadPlayerDataFromJson();

        SetResources();

        Invoke("AddResources", 1f);

        noticeText = notice.transform.GetChild(5).GetComponent<Text>();
        notice.SetActive(false);

        Invoke("CheckValidTime", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckValidTime()
    {
        if (TimeManager.sharedInstance.GetYear() >= 2020 && TimeManager.sharedInstance.GetMonth() >= 11 && TimeManager.sharedInstance.GetDay() >= 4)
        {
            playerData.acquantFateCount = 0;
            playerData.intertwinedFateCount = 0;

            notice.SetActive(true);
            noticeText.text = END_DATE;

            GameManager.instance.SavePlayerDataToJson();
        }
    }

    public void AddResources()
    {
        playerData.acquantFateCount = 50000;
        playerData.intertwinedFateCount = 50000;
        SetResources();
    }

    [ContextMenu("To Json Data")]
    public void SavePlayerDataToJson()
    {
        Debug.Log("저장 성공");

        string jsonData = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(SaveOrLoad(isMobile, true, "playerData"), jsonData);
    }

    [ContextMenu("From Json Data")]
    public void LoadPlayerDataFromJson()
    {
        try
        {
            Debug.Log("플레이어 정보 로드 성공");
            string jsonData = File.ReadAllText(SaveOrLoad(isMobile, false, "playerData"));
            playerData = JsonUtility.FromJson<PlayerData>(jsonData);

            // 버전 변경 시 스프라이트 이미지 코드가 변경되는 현상 막기
            for (int i = 0; i < playerData.items.Count; i++)
            {
                playerData.items[i].spritePath = ItemDatabase.instance.findItemByCode(playerData.items[i].code).spritePath;
                playerData.items[i].sprite = playerData.items[i].loadSprite(playerData.items[i].spritePath);
            }
        }
        catch (FileNotFoundException)
        {
            Debug.Log("로드 오류");
            playerData.acquantFateCount = 100;
            playerData.intertwinedFateCount = 100;

            string jsonData = JsonUtility.ToJson(playerData, true);

            File.WriteAllText(SaveOrLoad(isMobile, false, "playerData"), jsonData);
            LoadPlayerDataFromJson();
        }
    }

    public string SaveOrLoad(bool isMobile, bool isSave, string fileName)
    {
        if (isSave)
        {
            if (isMobile)
            {
                // 모바일 저장
                return Path.Combine(Application.persistentDataPath, fileName + ".json");
            }
            else
            {
                // pc 저장
                return Path.Combine(Application.dataPath, fileName + ".json");
            }
        }
        else
        {
            if (isMobile)
            {
                // 모바일 로드
                return Path.Combine(Application.persistentDataPath, fileName + ".json");
            }
            else
            {
                // pc 로드
                return Path.Combine(Application.dataPath, fileName + ".json");
            }
        }
    }

    public void SetResources()
    {
        acquantFateText.text = "" + playerData.acquantFateCount;
        intertwinedFateText.text = "" + playerData.intertwinedFateCount;
    }

    public bool AddWishes(int acquantFateCount, int intertwinedFateCount)
    {
        try
        {
            playerData.acquantFateCount += acquantFateCount;
            playerData.intertwinedFateCount += intertwinedFateCount;

            return true;
        }
        catch (System.Exception)
        {
            return false;
        }
    }

    public void OffNotice(bool isSoundPlay)
    {
        if (isSoundPlay)
        {
            SoundManager.instance.PlayOneShotEffectSound(3);
        }

        notice.SetActive(false);
    }

    public void SetNoticeSkip()
    {
        SoundManager.instance.PlayOneShotEffectSound(1);
        notice.SetActive(true);
        noticeText.text = WANT_SKIP;
    }
}
