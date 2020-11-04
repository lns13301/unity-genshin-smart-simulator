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
    public string LACK_OF_WISH = "보유한 재화가 부족합니다.";
    public string UPDATE_YET = "아직 추가되지 않은 기능입니다.\n\n추후 업데이트를 기다려주세요!";
    public string WANT_SKIP = "건너뛰시겠습니까?";
    public string END_DATE = "체험 기간이 종료되었습니다.";
    public string NEW_LINE = "\n";
    public string ORANGE_COLOR = "e59e00";

    public GameObject information;
    public Text informationText;

    public GameObject exitNotice;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        LoadPlayerDataFromJson();

        SetResources();

        noticeText = notice.transform.GetChild(5).GetComponent<Text>();
        noticeText.supportRichText = true;
        notice.SetActive(false);

        informationText = information.transform.GetChild(3).GetComponent<Text>();
        informationText.supportRichText = true;
        information.SetActive(false);

        Invoke("isValidTimeOver", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isValidTimeOver()
    {
        int[] timeData = TimeManager.sharedInstance.GetKoreaCurrentTime();

        Debug.Log(timeData[0] + "년" + timeData[1] + "월" + timeData[2] + "일" + timeData[3] + "시" + timeData[4] + "분");

        if ( ((timeData[0] >= 2020 && timeData[1] >= 11) || timeData[0] > 2020)
            && ((timeData[2] > 5) || (timeData[2] <= 5 && timeData[3] >= 23 && timeData[4] >= 59))
            )
        {
            playerData.acquantFateCount = 0;
            playerData.intertwinedFateCount = 0;

            information.SetActive(true);
            informationText.text = END_DATE;

            SavePlayerDataToJson();

            return true;
        }

        return false;
    }

    public void AddResources()
    {
        playerData.acquantFateCount = 500;
        playerData.intertwinedFateCount = 500;
        SetResources();
    }

    [ContextMenu("To Json Data")]
    public void SavePlayerDataToJson()
    {
        Debug.Log("저장 성공");

        string jsonData = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(SaveOrLoad(isMobile, true, "PlayerData"), jsonData);
    }

    [ContextMenu("From Json Data")]
    public void LoadPlayerDataFromJson()
    {
        try
        {
            Debug.Log("플레이어 정보 로드 성공");
            string jsonData = File.ReadAllText(SaveOrLoad(isMobile, false, "PlayerData"));
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
            playerData.acquantFateCount = 0;
            playerData.intertwinedFateCount = 0;

            string jsonData = JsonUtility.ToJson(playerData, true);

            File.WriteAllText(SaveOrLoad(isMobile, false, "PlayerData"), jsonData);
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

    public void OffNotice()
    {
        SoundManager.instance.PlayOneShotEffectSound(3);
        notice.SetActive(false);
    }

    public void SetNoticeSkip()
    {
        SoundManager.instance.PlayOneShotEffectSound(1);
        notice.SetActive(true);
        noticeText.text = WANT_SKIP;
    }

    public void OnInformationNotYet()
    {
        SoundManager.instance.PlayOneShotEffectSound(1);
        information.SetActive(true);
        informationText.text = UPDATE_YET;
    }

    public void OffInformation()
    {
        SoundManager.instance.PlayOneShotEffectSound(3);
        information.SetActive(false);
    }

    public string GetColorText(string text, string colorValue)
    {
        return "<color=#" + colorValue + ">" + text + "</color>";
    }

    public void OnItemInformation(Item item)
    {
        information.SetActive(true);

        informationText.text = item.koName;

        string[] character = new string[2];

        if (item.type == ItemType.HERO)
        {
            character = item.GetCharacterNameWithColor();
            informationText.text += NEW_LINE + NEW_LINE + GetColorText(character[1], character[0]);
        }
        else
        {
            informationText.text += NEW_LINE + NEW_LINE + GetColorText(item.GetItemTypeToKorean(), "37946e");
        }

        informationText.text += NEW_LINE + NEW_LINE + GetColorText(item.GetItemGradeToKorean(), "d95763");
    }

    public void ShowLackOfWish(int limit = 10, bool isAcquantFateWish = false)
    {
        information.SetActive(true);

        if (isAcquantFateWish)
        {
            informationText.text = "만남의 인연이 " + GetColorText("" + (limit - playerData.acquantFateCount), ORANGE_COLOR) + "개 부족합니다.";
        }
        else
        {
            informationText.text = "뒤얽힌 인연이 " + GetColorText("" + (limit - playerData.intertwinedFateCount), ORANGE_COLOR) + "개 부족합니다.";
        }

        AdMobReward.instance.ButtonAd();
    }

    public void SetHistoryRecent()
    {
        while (playerData.normalHistory.Count > 90)
        {
            playerData.normalHistory.RemoveAt(0);
        }

        while (playerData.characterHistory.Count > 90)
        {
            playerData.characterHistory.RemoveAt(0);
        }

        while (playerData.weaponHistory.Count > 90)
        {
            playerData.weaponHistory.RemoveAt(0);
        }

        while (playerData.noelleHistory.Count > 90)
        {
            playerData.noelleHistory.RemoveAt(0);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ButtonExit()
    {
        SoundManager.instance.PlayOneShotEffectSound(1);
        exitNotice.SetActive(true);
    }

    public void OffExitNotice()
    {
        SoundManager.instance.PlayOneShotEffectSound(3);
        exitNotice.SetActive(false);
    }
}
