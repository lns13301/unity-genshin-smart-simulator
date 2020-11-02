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

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        loadPlayerDataFromJson();

        SetResources();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("To Json Data")]
    public void savePlayerDataToJson()
    {
        Debug.Log("저장 성공");

        string jsonData = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(saveOrLoad(isMobile, true, "playerData"), jsonData);
    }

    [ContextMenu("From Json Data")]
    public void loadPlayerDataFromJson()
    {
        try
        {
            Debug.Log("플레이어 정보 로드 성공");
            string jsonData = File.ReadAllText(saveOrLoad(isMobile, false, "playerData"));
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

            File.WriteAllText(saveOrLoad(isMobile, false, "playerData"), jsonData);
            loadPlayerDataFromJson();
        }
    }

    public string saveOrLoad(bool isMobile, bool isSave, string fileName)
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
}
