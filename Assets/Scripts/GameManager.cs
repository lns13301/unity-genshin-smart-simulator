using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    public string END_DATE = "테스트 기간이 종료되었습니다.";
    public string NEW_LINE = "\n";
    public string ORANGE_COLOR = "e59e00";
    public string RED_COLOR = "d50707";
    public string WARNING;

    public GameObject information;
    public Text informationText;

    public GameObject exitNotice;

    public GameObject detail;
    public Transform detailContent;

    public string LACK_OF_WISH_EN = "You don't have enough wish.";
    public string UPDATE_YET_EN = "This feature has not been added yet.\n\nPlease wait for further updates!";
    private string WANT_SKIP_EN = "Would you like to skip it?";
    public string END_DATE_EN = "Your trial period has ended.";
    public string WARNING_EN;

    public bool isTestVersion;
    /* 1. isTestVersion 값 바꾸기, 2. ggsdatat로 세이브파일 바꾸기, 3. 광고보기를 초기화로 바꾸기*/

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        isTestVersion = false; // 게임이 테스트 버전인지 설정하기, 플레이어 데이터 로드에서 생성하는 부분도 수정하기

        LoadPlayerDataFromJson();

        SetResources();

        try
        {
            noticeText = notice.transform.GetChild(5).GetComponent<Text>();
            noticeText.supportRichText = true;
            notice.SetActive(false);

            informationText = information.transform.GetChild(3).GetComponent<Text>();
            informationText.supportRichText = true;
            information.SetActive(false);

            Invoke("isValidTimeOver", 1f);

            WARNING = GetColorText("! 경고 !", RED_COLOR);
            WARNING_EN = GetColorText("! WARNING !", RED_COLOR);

            SetDetailText();
        }
        catch
        {
            Invoke("isValidTimeOver", 1f);

            WARNING = GetColorText("! 경고 !", RED_COLOR);
            WARNING_EN = GetColorText("! WARNING !", RED_COLOR);
        }

/*        if (!isTestVersion && playerData.isTestVersion)
        {
            playerData.acquantFateCount = -10000;
            playerData.intertwinedFateCount = -10000;
            playerData.starDustCount = -100000;
            playerData.starLightCount = -100000;
        }*/
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

        if (LanguageManager.instance.language == Language.KOREAN)
        {
            detailContent.GetChild(0).GetComponent<Text>().text = "확률 정보입니다.";
            detailContent.GetChild(1).GetComponent<Text>().text =
                GetColorText("산야의 시조 이벤트 기원", "9958b3") + " : " + GetColorText("74회차", RED_COLOR) + "까지 " + GetColorText("0.6%", RED_COLOR)
                + "이후부터 " + GetColorText("32.323%", RED_COLOR) + "확률로 " + GetColorText("5성", ORANGE_COLOR) + "이 등장합니다.\n"
                + "(기본 " + GetColorText("0.6%", RED_COLOR) + "에서 최대 " + GetColorText("1.6%", RED_COLOR)
                + "로 최대 " + GetColorText("90", RED_COLOR) + "회차에 확정적으로 " + GetColorText("5성 획득", ORANGE_COLOR) + "이 가능합니다.)";
            detailContent.GetChild(2).GetComponent<Text>().text =
                GetColorText("산야의 시조 이벤트 기원", "9958b3") + " : 기본 " + GetColorText("5.1%", RED_COLOR) + ", "
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
                GetColorText("신의 주조 이벤트 기원", "51cfcb") + " : " + GetColorText("64회차", RED_COLOR) + "까지 " + GetColorText("0.7%", RED_COLOR)
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
                + GetColorText("75%", RED_COLOR) + "확률로 " + GetColorText("픽업 무기", ORANGE_COLOR) + "를 획득합니다.\n" +
                "만약 이벤트에서 획득한 무기가 " + GetColorText("픽업무기가 아닐경우 다음 5성 무기는 무조건 픽업 무기", ORANGE_COLOR) + "입니다.";
            detailContent.GetChild(8).GetComponent<Text>().text =
                "기원에서 " + GetColorText("4성", ORANGE_COLOR) + " 무기를 획득 시 "
                + GetColorText("75%", RED_COLOR) + "확률로 " + GetColorText("픽업 무기", ORANGE_COLOR) + "를 획득합니다.\n" +
                "만약 이벤트에서 획득한 무기가 " + GetColorText("픽업무기가 아닐경우 다음 4성 무기는 무조건 픽업 무기", ORANGE_COLOR) + "입니다.";
            detailContent.GetChild(9).GetComponent<Text>().text =
                GetColorText("세상 여행 일반 기원", "497a4d") + " : " + GetColorText("74회차", RED_COLOR) + "까지 " + GetColorText("0.6%", RED_COLOR)
                + "이후부터 " + GetColorText("32.323%", RED_COLOR) + "확률로 " + GetColorText("5성", ORANGE_COLOR) + "이 등장합니다.\n"
                + "(기본 " + GetColorText("0.6%", RED_COLOR) + "에서 최대 " + GetColorText("1.6%", RED_COLOR)
                + "로 최대 " + GetColorText("90", RED_COLOR) + "회차에 확정적으로 " + GetColorText("5성 획득", ORANGE_COLOR) + "이 가능합니다.)";
            detailContent.GetChild(10).GetComponent<Text>().text =
                GetColorText("세상 여행 일반 기원", "497a4d") + " : 기본 " + GetColorText("5.1%", RED_COLOR) + ", "
                + GetColorText("4회차", RED_COLOR) + " 이후부터 " + GetColorText("1회", RED_COLOR) + "당 " + GetColorText("1.58%", RED_COLOR) + "씩 증가\n" +
                "(최대 " + GetColorText("13%", RED_COLOR) + " 로 최대 " + GetColorText("10회차", RED_COLOR) + "에 확정적으로 "
                + GetColorText("4성 획득", ORANGE_COLOR) + "이 가능합니다.)";
        }
        else
        {
            detailContent.GetChild(0).GetComponent<Text>().text = "Probability Information";
            detailContent.GetChild(1).GetComponent<Text>().text =
                GetColorText("Event Wish \"Gentry of Hermitage\"", "9958b3") + " : " + "It has a " + GetColorText("0.6%", RED_COLOR) + " probability up to " + GetColorText("74 times", RED_COLOR) + "."
                + "Since then, " + GetColorText("5-star", ORANGE_COLOR) + " appear with a " + GetColorText("32.323%", RED_COLOR) + " probability" + ".\n"
                + "(Base probability of winning " + GetColorText("5-star item", ORANGE_COLOR) + " = " + GetColorText("0.6%", RED_COLOR) + ", consolidated probability of winning " 
                + GetColorText("5-star item", ORANGE_COLOR) + " = " + GetColorText("1.6%", RED_COLOR)
                + "guaranteed to win " + GetColorText("5-star item", ORANGE_COLOR) + "at least once per " + GetColorText("90", RED_COLOR) + " attempts";
            detailContent.GetChild(2).GetComponent<Text>().text =
            GetColorText("Event Wish \"Gentry of Hermitage\"", "9958b3") + " : " + "It has a " + GetColorText("5.1%", RED_COLOR) + " probability up to " + GetColorText("4 times", RED_COLOR) + "."
                + "Since then, " + GetColorText("4-star", ORANGE_COLOR) + " increases by " + GetColorText("1.58%", RED_COLOR) + " each time." + ".\n"
                + "(Base probability of winning " + GetColorText("4-star item", ORANGE_COLOR) + " = " + GetColorText("5.1%", RED_COLOR) + ", consolidated probability of winning "
                + GetColorText("4-star item", ORANGE_COLOR) + " = " + GetColorText("13%", RED_COLOR)
                + "guaranteed to win " + GetColorText("4-star item", ORANGE_COLOR) + "at least once per " + GetColorText("10", RED_COLOR) + " attempts";
            detailContent.GetChild(3).GetComponent<Text>().text =
                "The first time you win a " + GetColorText("5-star item", ORANGE_COLOR) + " in this event wish, there is a "
                + GetColorText("50% chance", RED_COLOR) + " it will be the " + GetColorText("promotional character", ORANGE_COLOR) + ".\n" +
                "If the first 5-star character you win in this event wish is not " + GetColorText("the promotional character, then the next 5-star character you win is guaranteed to be the promotional character", ORANGE_COLOR) + ".";
            detailContent.GetChild(4).GetComponent<Text>().text =
                "The first time you win a " + GetColorText("4-star item", ORANGE_COLOR) + " in this event wish, there is a "
                + GetColorText("50% chance", RED_COLOR) + " it will be the " + GetColorText("promotional character", ORANGE_COLOR) + ".\n" +
                "If the first 4-star character you win in this event wish is not " + GetColorText("the promotional character, then the next 4-star character you win is guaranteed to be the promotional character", ORANGE_COLOR) + ".";
            detailContent.GetChild(5).GetComponent<Text>().text =
            GetColorText("Event Wish \"Epitome Invocation\"", "51cfcb") + " : " + "It has a " + GetColorText("0.7%", RED_COLOR) + " probability up to " + GetColorText("64 times", RED_COLOR) + "."
                + "Since then, " + GetColorText("5-star", ORANGE_COLOR) + " appear with a " + GetColorText("32.323%", RED_COLOR) + " probability" + ".\n"
                + "(Base probability of winning " + GetColorText("5-star item", ORANGE_COLOR) + " = " + GetColorText("0.7%", RED_COLOR) + ", consolidated probability of winning "
                + GetColorText("5-star item", ORANGE_COLOR) + " = " + GetColorText("1.85%", RED_COLOR)
                + "guaranteed to win " + GetColorText("5-star item", ORANGE_COLOR) + "at least once per " + GetColorText("90", RED_COLOR) + " attempts";
            detailContent.GetChild(6).GetComponent<Text>().text =
            GetColorText("Event Wish \"Epitome Invocation\"", "51cfcb") + " : " + "It has a " + GetColorText("5.1%", RED_COLOR) + " probability up to " + GetColorText("4 times", RED_COLOR) + "."
                + "Since then, " + GetColorText("4-star", ORANGE_COLOR) + " increases by " + GetColorText("1.58%", RED_COLOR) + " each time." + ".\n"
                + "(Base probability of winning " + GetColorText("4-star item", ORANGE_COLOR) + " = " + GetColorText("6%", RED_COLOR) + ", consolidated probability of winning "
                + GetColorText("4-star item", ORANGE_COLOR) + " = " + GetColorText("14.5%", RED_COLOR)
                + "guaranteed to win " + GetColorText("4-star item", ORANGE_COLOR) + "at least once per " + GetColorText("10", RED_COLOR) + " attempts";
            detailContent.GetChild(7).GetComponent<Text>().text =
                "The first time you win a " + GetColorText("5-star item", ORANGE_COLOR) + " in this event wish, there is a "
                + GetColorText("75% chance", RED_COLOR) + " it will be the " + GetColorText("promotional weapon", ORANGE_COLOR) + ".\n" +
                "If the first 5-star weapon you win in this event wish is not " + GetColorText("the promotional weapon, then the next 5-star weapon you win is guaranteed to be the promotional weapon", ORANGE_COLOR) + ".";
            detailContent.GetChild(8).GetComponent<Text>().text =
                "The first time you win a " + GetColorText("4-star item", ORANGE_COLOR) + " in this event wish, there is a "
                + GetColorText("75% chance", RED_COLOR) + " it will be the " + GetColorText("promotional weapon", ORANGE_COLOR) + ".\n" +
                "If the first 4-star weapon you win in this event wish is not " + GetColorText("the promotional weapon, then the next 4-star weapon you win is guaranteed to be the promotional weapon", ORANGE_COLOR) + ".";
            detailContent.GetChild(9).GetComponent<Text>().text =
            GetColorText("Standard Wish \"Wanderlust Invocation\"", "497a4d") + " : " + "It has a " + GetColorText("0.6%", RED_COLOR) + " probability up to " + GetColorText("74 times", RED_COLOR) + "."
                + "Since then, " + GetColorText("5-star", ORANGE_COLOR) + " appear with a " + GetColorText("32.323%", RED_COLOR) + " probability" + ".\n"
                + "(Base probability of winning " + GetColorText("5-star item", ORANGE_COLOR) + " = " + GetColorText("0.6%", RED_COLOR) + ", consolidated probability of winning "
                + GetColorText("5-star item", ORANGE_COLOR) + " = " + GetColorText("1.6%", RED_COLOR)
                + "guaranteed to win " + GetColorText("5-star item", ORANGE_COLOR) + "at least once per " + GetColorText("90", RED_COLOR) + " attempts";
            detailContent.GetChild(10).GetComponent<Text>().text =
            GetColorText("Standard Wish \"Wanderlust Invocation\"", "497a4d") + " : " + "It has a " + GetColorText("5.1%", RED_COLOR) + " probability up to " + GetColorText("4 times", RED_COLOR) + "."
                + "Since then, " + GetColorText("4-star", ORANGE_COLOR) + " increases by " + GetColorText("1.58%", RED_COLOR) + " each time." + ".\n"
                + "(Base probability of winning " + GetColorText("4-star item", ORANGE_COLOR) + " = " + GetColorText("5.1%", RED_COLOR) + ", consolidated probability of winning "
                + GetColorText("4-star item", ORANGE_COLOR) + " = " + GetColorText("13%", RED_COLOR)
                + "guaranteed to win " + GetColorText("4-star item", ORANGE_COLOR) + "at least once per " + GetColorText("10", RED_COLOR) + " attempts";
        }
    }

    public void ButtonDetail()
    {
        SoundManager.instance.PlayOneShotEffectSound(1);

        if (HistoryManager.instance.historySet.activeSelf)
        {
            HistoryManager.instance.historySet.SetActive(false);
        }

        if (InventoryManager.instance.inventorySet.activeSelf)
        {
            InventoryManager.instance.inventorySet.SetActive(false);
        }

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

        if ( ((timeData[0] >= 2020 && timeData[1] >= 12) || timeData[0] > 2020)
            && ((timeData[2] > 6) || (timeData[2] <= 6 && timeData[3] >= 23 && timeData[4] >= 59))
            )
        {
            playerData.acquantFateCount = 0;
            playerData.intertwinedFateCount = 0;
            playerData.starLightCount = 0;
            playerData.starDustCount = 0;

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
        // Debug.Log("저장 성공");

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
            Debug.Log("플레이어 세이브파일 로드 오류");

            if (isTestVersion)
            {
                playerData.acquantFateCount = 2000;
                playerData.intertwinedFateCount = 2000;
                playerData.starDustCount = 1000;
                playerData.starLightCount = 100;
            }
            else
            {
                playerData.acquantFateCount = 100;
                playerData.intertwinedFateCount = 100;
                playerData.starDustCount = 300;
                playerData.starLightCount = 10;
            }

            // playerData.isTestVersion = isTestVersion;

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

    // refresh
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

        SetNoticeText();
        notice.SetActive(true);
    }

    // 도저히 어디서 건너뛰시겠습니까를 변경시키는지 모르겠음...
    private void SetNoticeText()
    {
        if (LanguageManager.instance.language == Language.KOREAN)
        {
            noticeText.text = WANT_SKIP;
        }
        else
        {
            noticeText.text = WANT_SKIP_EN;
        }
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
        
        if (LanguageManager.instance.language == Language.KOREAN)
        {
            informationText.text = item.koName;
        }
        else
        {
            informationText.text = item.enName;
        }

        string[] character = new string[2];
        Language language = LanguageManager.instance.language;

        if (item.type == ItemType.CHARACTER)
        {
            if (language == Language.KOREAN)
            {
                character = item.GetCharacterNameWithColorKorean();
            }
            else
            {
                character = item.GetCharacterNameWithColorEnglish();
            }

            informationText.text += NEW_LINE + NEW_LINE + GetColorText(character[1], character[0]);
        }
        else
        {
            if (language == Language.KOREAN)
            {
                informationText.text += NEW_LINE + NEW_LINE + GetColorText(item.GetItemTypeToKorean(), "37946e");
            }
            else
            {
                informationText.text += NEW_LINE + NEW_LINE + GetColorText(item.GetItemTypeToEnglish(), "37946e");
            }
        }

        if (language == Language.KOREAN)
        {
            informationText.text += NEW_LINE + NEW_LINE + GetColorText(item.GetItemGradeToKorean(), "d95763");
        }
        else
        {
            informationText.text += NEW_LINE + NEW_LINE + GetColorText(item.GetItemGradeToEnglish(), "d95763");
        }
    }

    public void ShowLackOfWish(int limit = 10, bool isAcquantFateWish = false)
    {
        information.SetActive(true);

        if (isAcquantFateWish)
        {
            if (LanguageManager.instance.language == Language.KOREAN)
            {
                informationText.text = "만남의 인연이 " + GetColorText("" + (limit - playerData.acquantFateCount), ORANGE_COLOR) + "개 부족합니다.";
            }
            else
            {
                informationText.text = "You need " + GetColorText("" + (limit - playerData.acquantFateCount), ORANGE_COLOR) + " more acquant fate.";
            }
        }
        else
        {
            if (LanguageManager.instance.language == Language.KOREAN)
            {
                informationText.text = "뒤얽힌 인연이 " + GetColorText("" + (limit - playerData.intertwinedFateCount), ORANGE_COLOR) + "개 부족합니다.";
            }
            else
            {
                informationText.text = "You need " + GetColorText("" + (limit - playerData.intertwinedFateCount), ORANGE_COLOR) + " more intertwined fate.";
            }
        }

        AdMobReward.instance.ButtonAd();
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

        if (LanguageManager.instance.language == Language.KOREAN)
        {
            informationText.text = WARNING + "\n\n장비 인벤토리의 최대 보유 수량은 " + GetColorText("200", ORANGE_COLOR) + "개 입니다.\n\n"
                + "인벤토리 최대수량을 초과하여 장비아이템을 수령할 경우\n일부 장비아이템이 " + GetColorText("사라지게", RED_COLOR) + " 됩니다.\n\n"
                + "인벤토리의 " + GetColorText("공간을 확보한 후", RED_COLOR) + " 재시도 해주세요.";
        }
        else
        {
            informationText.text = WARNING_EN + "\n\nThe maximum quantity of equipment inventory is " + GetColorText("200", ORANGE_COLOR) + ".\n\n"
                + "Some items that will receive equipment items \nin excess of the maximum quantity of inventory will " + GetColorText("disappear", RED_COLOR) + ".\n\n"
                + "Please try again " + GetColorText("after securing space in the inventory", RED_COLOR) + ".";
        }
    }

    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ResetPlayerData()
    {
        PlayerData playerData = new PlayerData();

        if (isTestVersion)
        {
            playerData.acquantFateCount = 3000;
            playerData.intertwinedFateCount = 3000;
            playerData.starDustCount = 1000;
            playerData.starLightCount = 100;
        }
        else
        {
            playerData.acquantFateCount = 100;
            playerData.intertwinedFateCount = 100;
            playerData.starDustCount = 300;
            playerData.starLightCount = 10;
        }

        // playerData.isTestVersion = isTestVersion;

        string jsonData = JsonUtility.ToJson(playerData, true);

        File.WriteAllText(SaveOrLoad(isMobile, false, "ggsdatat"), AESCrypto.AESEncrypt128(jsonData));
        LoadPlayerDataFromJson();
        SetResources();
        ChangeScene(0);
    }
}
