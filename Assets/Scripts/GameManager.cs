using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private PlayerData playerData;

    public bool isMobile = true;

    public Text intertwinedFateText;
    public Text acquantFateText;
    public Text primogemText;
    public Text starLightText;
    public Text starDustText;

    public GameObject notice;
    public Text noticeText;
    public string LACK_OF_WISH = "보유한 재화가 부족합니다.";
    public string UPDATE_YET = "아직 추가되지 않은 기능입니다.\n\n추후 업데이트를 기다려주세요!";
    public string WANT_SKIP = "건너뛰시겠습니까?";
    public string END_DATE = "체험 기간이 종료되었습니다.";
    public string NEW_LINE = "\n";
    public string ORANGE_COLOR = "e59e00";
    public string RED_COLOR = "d50707";
    public string WARNING;

    public GameObject information;
    public Text informationText;

    public GameObject exitNotice;

    public GameObject detail;
    public Transform detailContent;

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

        WARNING = GetColorText("! 경고 !", RED_COLOR);

        SetDetailText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDetailText()
    {
        for (int i = 0; i < detailContent.childCount; i++)
        {
            detailContent.GetChild(0).GetComponent<Text>().supportRichText = true;
        }

        detailContent.GetChild(0).GetComponent<Text>().text = "확률 정보입니다.";
        detailContent.GetChild(1).GetComponent<Text>().text = 
            GetColorText("번쩍이는 화염 이벤트 기원", "9958b3") + " : " + GetColorText("75회차", RED_COLOR) + "까지 " + GetColorText("0.6%", RED_COLOR) 
            + "이후부터 " + GetColorText("32.323%", RED_COLOR) + "확률로 " + GetColorText("5성", ORANGE_COLOR) + "이 등장합니다.\n"
            + "(기본 " + GetColorText("0.6%", RED_COLOR) + "에서 최대 " + GetColorText("1.6%", RED_COLOR) 
            + "로 최대 " + GetColorText("90", RED_COLOR) + "회차에 확정적으로 " + GetColorText("5성 획득", ORANGE_COLOR) + "이 가능합니다.)";
        detailContent.GetChild(2).GetComponent<Text>().text =
            GetColorText("번쩍이는 화염 이벤트 기원", "9958b3") + " : 기본 " + GetColorText("5.1%", RED_COLOR) + ", " 
            + GetColorText("4회차", RED_COLOR) + " 이후부터 " + GetColorText("1회", RED_COLOR) + "당 " + GetColorText("1.58%", RED_COLOR) + "씩 증가\n" +
            "(최대 " + GetColorText("13%", RED_COLOR) + " 로 최대 " + GetColorText("10회차", RED_COLOR) + "에 확정적으로 " 
            + GetColorText("4성 획득", ORANGE_COLOR) + "이 가능합니다.)";
        detailContent.GetChild(3).GetComponent<Text>().text =
            "기원에서 " + GetColorText("5성", ORANGE_COLOR) + " 캐릭터를 획득 시 " 
            + GetColorText("50%", RED_COLOR) + "확률로 " + GetColorText("픽업 캐릭터", ORANGE_COLOR) + "를 획득합니다.\n" +
            "만약 이벤트에서 획득한 캐릭터가 " + GetColorText("픽업캐릭터가 아닐경우 다음 5성 캐릭터는 무조건 픽업 캐릭터", ORANGE_COLOR) + "입니다.";
        detailContent.GetChild(4).GetComponent<Text>().text =
            "기원에서 " + GetColorText("4성", ORANGE_COLOR) + " 캐릭터를 획득 시 "
            + GetColorText("50%", RED_COLOR) + "확률로 " + GetColorText("픽업 캐릭터", ORANGE_COLOR) + "를 획득합니다.\n" +
            "만약 이벤트에서 획득한 캐릭터가 " + GetColorText("픽업캐릭터가 아닐경우 다음 4성 캐릭터는 무조건 픽업 캐릭터", ORANGE_COLOR) + "입니다.";
        detailContent.GetChild(5).GetComponent<Text>().text =
            GetColorText("신의 주조 이벤트 기원", "51cfcb") + " : " + GetColorText("65회차", RED_COLOR) + "까지 " + GetColorText("0.7%", RED_COLOR)
            + "이후부터 " + GetColorText("32.323%", RED_COLOR) + "확률로 " + GetColorText("5성", ORANGE_COLOR) + "이 등장합니다.\n"
            + "(기본 " + GetColorText("0.7%", RED_COLOR) + "에서 최대 " + GetColorText("1.85%", RED_COLOR)
            + "로 최대 " + GetColorText("80", RED_COLOR) + "회차에 확정적으로 " + GetColorText("5성 획득", ORANGE_COLOR) + "이 가능합니다.)";
        detailContent.GetChild(6).GetComponent<Text>().text =
            GetColorText("신의 주조 이벤트 기원", "51cfcb") + " : 기본 " + GetColorText("6%", RED_COLOR) + ", "
            + GetColorText("4회차", RED_COLOR) + " 이후부터 " + GetColorText("1회", RED_COLOR) + "당 " + GetColorText("1.7%", RED_COLOR) + "씩 증가\n" +
            "(최대 " + GetColorText("14.5%", RED_COLOR) + " 로 최대 " + GetColorText("10회차", RED_COLOR) + "에 확정적으로 "
            + GetColorText("4성 획득", ORANGE_COLOR) + "이 가능합니다.)";
        detailContent.GetChild(7).GetComponent<Text>().text =
            "기원에서 " + GetColorText("5성", ORANGE_COLOR) + " 무기를 획득 시 "
            + GetColorText("50%", RED_COLOR) + "확률로 " + GetColorText("픽업 무기", ORANGE_COLOR) + "를 획득합니다.\n" +
            "만약 이벤트에서 획득한 무기가 " + GetColorText("픽업무기가 아닐경우 다음 5성 무기는 무조건 픽업 무기", ORANGE_COLOR) + "입니다.";
        detailContent.GetChild(8).GetComponent<Text>().text =
            "기원에서 " + GetColorText("4성", ORANGE_COLOR) + " 무기를 획득 시 "
            + GetColorText("50%", RED_COLOR) + "확률로 " + GetColorText("픽업 무기", ORANGE_COLOR) + "를 획득합니다.\n" +
            "만약 이벤트에서 획득한 무기가 " + GetColorText("픽업무기가 아닐경우 다음 4성 무기는 무조건 픽업 무기", ORANGE_COLOR) + "입니다.";
        detailContent.GetChild(9).GetComponent<Text>().text =
            GetColorText("세상 여행 일반 기원", "497a4d") + " : " + GetColorText("75회차", RED_COLOR) + "까지 " + GetColorText("0.6%", RED_COLOR)
            + "이후부터 " + GetColorText("32.323%", RED_COLOR) + "확률로 " + GetColorText("5성", ORANGE_COLOR) + "이 등장합니다.\n"
            + "(기본 " + GetColorText("0.6%", RED_COLOR) + "에서 최대 " + GetColorText("1.6%", RED_COLOR)
            + "로 최대 " + GetColorText("90", RED_COLOR) + "회차에 확정적으로 " + GetColorText("5성 획득", ORANGE_COLOR) + "이 가능합니다.)";
        detailContent.GetChild(10).GetComponent<Text>().text =
            GetColorText("세상 여행 일반 기원", "497a4d") + " : 기본 " + GetColorText("5.1%", RED_COLOR) + ", "
            + GetColorText("4회차", RED_COLOR) + " 이후부터 " + GetColorText("1회", RED_COLOR) + "당 " + GetColorText("1.58%", RED_COLOR) + "씩 증가\n" +
            "(최대 " + GetColorText("13%", RED_COLOR) + " 로 최대 " + GetColorText("10회차", RED_COLOR) + "에 확정적으로 "
            + GetColorText("4성 획득", ORANGE_COLOR) + "이 가능합니다.)";
    }

    public void ButtonDetail()
    {
        SoundManager.instance.PlayOneShotEffectSound(1);
        detail.SetActive(true);
    }

    public void OffDetail()
    {
        SoundManager.instance.PlayOneShotEffectSound(3);
        detail.SetActive(false);
    }

    public bool isValidTimeOver()
    {
        return false;
        int[] timeData = TimeManager.sharedInstance.GetKoreaCurrentTime();

        Debug.Log(timeData[0] + "년" + timeData[1] + "월" + timeData[2] + "일" + timeData[3] + "시" + timeData[4] + "분");

        if ( ((timeData[0] >= 2020 && timeData[1] >= 11) || timeData[0] > 2020)
            && ((timeData[2] > 6) || (timeData[2] <= 6 && timeData[3] >= 23 && timeData[4] >= 59))
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
        File.WriteAllText(SaveOrLoad(isMobile, true, "ggsdata"), AESCrypto.AESEncrypt128(jsonData));
    }

    [ContextMenu("From Json Data")]
    public void LoadPlayerDataFromJson()
    {
        try
        {
            Debug.Log("플레이어 정보 로드 성공");
            string jsonData = File.ReadAllText(SaveOrLoad(isMobile, false, "ggsdata"));
            playerData = JsonUtility.FromJson<PlayerData>(AESCrypto.AESDecrypt128(jsonData));

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

            File.WriteAllText(SaveOrLoad(isMobile, false, "ggsdata"), AESCrypto.AESEncrypt128(jsonData));
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

    public void SaveAndLoad()
    {
        SavePlayerDataToJson();
        LoadPlayerDataFromJson();
    }

    public void SetResources()
    {
        acquantFateText.text = "" + playerData.acquantFateCount;
        intertwinedFateText.text = "" + playerData.intertwinedFateCount;
        starLightText.text = "" + playerData.starLightCount;
        starDustText.text = "" + playerData.starDustCount;
        primogemText.text = "" + playerData.primogemCount;
    }

    public bool AddWishes(int acquantFateCount, int intertwinedFateCount)
    {
        try
        {
            playerData.acquantFateCount += acquantFateCount;
            playerData.intertwinedFateCount += intertwinedFateCount;

            SetResources();

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
        OffNoticeAll();

        SoundManager.instance.PlayOneShotEffectSound(1);
        notice.SetActive(true);
        noticeText.text = WANT_SKIP;
    }

    public void OnInformationNotYet()
    {
        OffNoticeAll();

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
        OffNoticeAll();

        information.SetActive(true);

        informationText.text = item.koName;

        string[] character = new string[2];

        if (item.type == ItemType.CHARACTER)
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

    public PlayerData GetPlayerData()
    {
        return playerData;
    }

    public void OffNoticeAll()
    {
        notice.SetActive(false);
        information.SetActive(false);
        AdMobReward.instance.notice.SetActive(false);
        // HistoryManager.instance.historySet.SetActive(false);
        //InventoryManager.instance.inventorySet.SetActive(false);
    }

    public void OnNoticeWeaponInventoryFull()
    {
        SoundManager.instance.PlayOneShotEffectSound(1);
        information.SetActive(true);
        informationText.text = WARNING + "\n\n장비 인벤토리의 최대 보유 수량은 " + GetColorText("200", ORANGE_COLOR) + "개 입니다.\n\n"
            + "인벤토리 최대수량을 초과하여 장비아이템을 수령할 경우\n일부 장비아이템이 " + GetColorText("사라지게", RED_COLOR) + " 됩니다.\n\n"
            + "인벤토리의 " + GetColorText("공간을 확보한 후", RED_COLOR) + " 재시도 해주세요.";
    }
}
