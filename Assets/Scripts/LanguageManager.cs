using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
    private string CANCEL_EN = "Cancel";
    private string CANCEL_KO = " 취 소";
    private string CONFIRM_EN = "Confirm";
    private string CONFIRM_KO = " 확 인";

    public static LanguageManager instance;
    public Language language;

    public GameObject languageSet;
    public Text[] texts;

/*    public delegate void OnChangeLanguage();
    public OnChangeLanguage onChangeLanguage;*/

    private GameObject buttonKorean;
    private GameObject buttonEnglish;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        try
        {
            language = GameManager.instance.GetPlayerData().language;
        }
        catch (NullReferenceException)
        {
            GameManager.instance.GetPlayerData().language = Language.KOREAN;
            language = Language.KOREAN;
        }

        buttonEnglish = languageSet.transform.GetChild(0).gameObject;
        buttonKorean = languageSet.transform.GetChild(1).gameObject;

        languageSet.SetActive(false);

        try
        {
            ChangeGachaLanguage();
        }
        catch
        {
            ChangeMainLanguage();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLanguage(int index)
    {
        switch (index)
        {
            case 0:
                language = Language.ENGLISH;
                break;
            case 1:
                language = Language.KOREAN;
                break;
        }
    }

    public void ChangeGachaLanguage()
    {
        if (language == Language.KOREAN)
        {
            texts[0].text = "기원 10회";
            texts[1].text = "상세";
            texts[2].text = "뽑기 기록";
            texts[3].text = "인벤토리";
            texts[4].text = "충전소";
            texts[5].text = "Language";
            texts[6].text = CONFIRM_KO; // details
            texts[7].text = CANCEL_KO; // inventory notice
            texts[8].text = CONFIRM_KO;
            texts[9].text = CONFIRM_KO; // inventory information
            texts[10].text = CANCEL_KO; // exit notice
            texts[11].text = CONFIRM_KO;
            texts[12].text = CANCEL_KO; // ad notice
            texts[13].text = CONFIRM_KO;
            texts[14].text = CONFIRM_KO; // information
            texts[15].text = "주인없는\n스타라이트";
            texts[16].text = "주인없는\n스타더스트";
            texts[17].text = "획득";
            texts[18].text = "추가 획득";
            texts[19].text = "아무 키나 눌러 계속하기";
            texts[20].text = CANCEL_KO;
            texts[21].text = CONFIRM_KO; // notice
            texts[22].text = "캐릭터";
            texts[23].text = "무기";
            texts[24].text = "성유물";
            texts[25].text = "요리";
            texts[26].text = "재료";
            texts[27].text = "퀘스트 아이템";
            texts[28].text = "모험 아이템";
            texts[29].text = "처음으로";
            texts[30].text = "마지막으로";
            texts[31].text = "3성 모두 파괴";
            texts[32].text = "4성 모두 파괴";
            texts[33].text = "처음으로";
            texts[34].text = "마지막으로";
            texts[35].text = "주인없는\n스타라이트";
            texts[36].text = "주인없는\n스타더스트";
            texts[37].text = "획득";
            texts[38].text = "추가 획득";
            texts[39].text = "아무 키나 눌러 계속하기";
            texts[40].text = "파괴";
            texts[41].text = "스킬 레벨 UP";
            texts[42].text = "스킬 레벨 Down";
            texts[43].text = "돌파 재료";
            texts[44].text = "스킬 정보";
            texts[45].text = "운명의 자리";
            texts[46].text = "초기화";
        }
        else
        {
            texts[0].text = "Wish X 10";
            texts[1].text = "Details";
            texts[2].text = "History";
            texts[3].text = "Inventory";
            texts[4].text = "GET FATE";
            texts[5].text = "언어";
            texts[6].text = CONFIRM_EN; // details
            texts[7].text = CANCEL_EN; // inventory notice
            texts[8].text = CONFIRM_EN;
            texts[9].text = CONFIRM_EN; // inventory information
            texts[10].text = CANCEL_EN; // exit notice
            texts[11].text = CONFIRM_EN;
            texts[12].text = CANCEL_EN; // ad notice
            texts[13].text = CONFIRM_EN;
            texts[14].text = CONFIRM_EN; // information
            texts[15].text = "Masterless\nStarlight";
            texts[16].text = "Masterless\nStardust";
            texts[17].text = "Obtained";
            texts[18].text = "Extra";
            texts[19].text = "Click anywhere in the blank area to continue";
            texts[20].text = CANCEL_EN;
            texts[21].text = CONFIRM_EN; // notice
            texts[22].text = "Character";
            texts[23].text = "Weapon";
            texts[24].text = "Artifact";
            texts[25].text = "Food";
            texts[26].text = "Material";
            texts[27].text = "Quest Item";
            texts[28].text = "ETC Item";
            texts[29].text = "Go to the beginning";
            texts[30].text = "Go to the last";
            texts[31].text = "Destroy all 3s";
            texts[32].text = "Destroy all 4s";
            texts[33].text = "Go to the beginning";
            texts[34].text = "Go to the last";
            texts[35].text = "Masterless\nStarlight"; // inventory stardust
            texts[36].text = "Masterless\nStardust";
            texts[37].text = "Obtained";
            texts[38].text = "Extra";
            texts[39].text = "Click anywhere in the blank area to continue";
            texts[40].text = "Destroy";
            texts[41].text = "Skill Lv UP";
            texts[42].text = "Skill Lv Down";
            texts[43].text = "Required Material";
            texts[44].text = "Talents";
            texts[45].text = "Constellation";
            texts[46].text = "Reset";
        }

        BannerManager.instance.SetBannerImageByLanguage(language);
        GameManager.instance.GetPlayerData().language = language;
        GameManager.instance.SavePlayerDataToJson();
    }

    public void ChangeMainLanguage()
    {
        if (language == Language.KOREAN)
        {
            texts[0].text = "Language";
            texts[1].text = "기원 페이지\n";
            texts[2].text = "맵 페이지\n";
            texts[3].text = "종료하시겠습니까?\n";
            texts[4].text = " 취 소\n";
            texts[5].text = " 확 인\n";
        }
        else
        {
            texts[0].text = "언어";
            texts[1].text = "Wish Page\n";
            texts[2].text = "Map Page\n";
            texts[3].text = "Are you sure you want to exit?\n";
            texts[4].text = " Cancel\n";
            texts[5].text = " Confirm\n";
        }

        GameManager.instance.GetPlayerData().language = language;
        GameManager.instance.SavePlayerDataToJson();
    }

    public void ButtonLanguage()
    {
        SoundManager.instance.PlayOneShotEffectSound(1);

        languageSet.SetActive(true);
    }

    public void OffLanguage()
    {
        SoundManager.instance.PlayOneShotEffectSound(3);

        try
        {
            ChangeGachaLanguage();
        }
        catch
        {
            ChangeMainLanguage();
        }

        languageSet.SetActive(false);
    }

    public void ButtonKorean()
    {
        SoundManager.instance.PlayOneShotEffectSound(1);
        buttonEnglish.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 1f);

        buttonKorean.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
        language = Language.KOREAN;
    }

    public void ButtonEnglish()
    {
        SoundManager.instance.PlayOneShotEffectSound(1);
        buttonKorean.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 1f);

        buttonEnglish.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
        language = Language.ENGLISH;
    }
}

public enum Language
{
    ENGLISH,
    KOREAN
}
