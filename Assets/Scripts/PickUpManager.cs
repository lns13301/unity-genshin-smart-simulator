using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class PickUpManager : MonoBehaviour
{
    private static int WEAPON_COUNT_UNDER_FOUR_STAR = 41;
    private static int CHARACTER_COUNT_FOUR_STAR = 13; // 픽업 4성은 제외 했음

    // MAX 값들은 실제 인덱스에 + 1 해주었음
    private static int MIN_EPIC_WEAPON = 26;
    private static int MAX_EPIC_WEAPON = 47 + 1;
    private static int MIN_UNIQUE_WEAPON = MAX_EPIC_WEAPON;
    private static int MAX_UNIQUE_WEAPON = 65 + 1;
    private static int MIN_LEGEND_WEAPON = MAX_UNIQUE_WEAPON;
    private static int MAX_LEGEND_WEAPON = 76 + 1;

    private static int MIN_UNIQUE_CHARACTER = 0;
    private static int MAX_UNIQUE_CHARACTER = CHARACTER_COUNT_FOUR_STAR + 1;
    private static int MIN_LEGEND_CHARACTER = 16; // 다이루크 시작 코드
    private static int MAX_LEGEND_CHARACTER = 20 + 1;

    private static int CHARACTER_AND_WEAPON_FOUR_STAR_TOTAL = CHARACTER_COUNT_FOUR_STAR + MAX_EPIC_WEAPON - MIN_EPIC_WEAPON;

    public static PickUpManager instance;
    public GameObject panel;
    public RawImage videoPanel;
    public VideoPlayer[] videos;

    public GameObject resultPage;

    public PlayerData playerData;

    public bool hasFiveStar;
    public Item[] result;

    public GameObject resultImageParent;
    public GameObject skipButton;

    public GameObject stardustPage;

    public GameObject buttonInformation;

    public GameObject gachaIllustSet;
    public GameObject gachaIllust;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        resultPage.SetActive(false);
        playerData = GameManager.instance.GetPlayerData();

        hasFiveStar = false;
        result = new Item[10];

        stardustPage.SetActive(false);

        InitializeVideo();

        gachaIllust = gachaIllustSet.transform.GetChild(1).gameObject;
        gachaIllustSet.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PickUpButton()
    {
        // 체험 기간 확인 코드
        if (GameManager.instance.isValidTimeOver())
        {
            return;
        }

        // 인벤토리 수령가능 공간 확인
        if (GameManager.instance.GetPlayerData().weapons.Count > 190)
        {
            GameManager.instance.OnNoticeWeaponInventoryFull();
            return;
        }

        int buttonType = BannerManager.instance.onBannerIndex;

        Grade[] grades;

        SoundManager.instance.PlayOneShotEffectSound(1);

        if (buttonType == 1 || buttonType == 4) // 픽업, 지나간 픽업 처리
        {
            if (playerData.intertwinedFateCount > 9)
            {
                grades = SetPlayerDataAfterGacha(false);

                for (int i = 0; i < grades.Length; i++)
                {
                    result[i] = GetRandomItem(grades[i], PickUpType.CHARACTER, playerData.isPickUpCharacterAlways, playerData.isPickUpCharacter4Always);
                    playerData.AddItemAndHistory(result[i], buttonType);
                }

                playerData.characterTotalTryCount += 10;
            }
            else
            {
                GameManager.instance.ShowLackOfWish();
                return;
            }

        }
        if (buttonType == 2)
        {
            if (playerData.intertwinedFateCount > 9)
            {
                grades = SetPlayerDataAfterGacha(false);

                for (int i = 0; i < grades.Length; i++)
                {
                    result[i] = GetRandomItem(grades[i], PickUpType.WEAPON, playerData.isPickUpWeaponAlways, playerData.isPickUpWeapon4Always);
                    playerData.AddItemAndHistory(result[i], buttonType);
                }

                playerData.weaponTotalTryCount += 10;
            }
            else
            {
                GameManager.instance.ShowLackOfWish();
                return;
            }
        }
        if (buttonType == 3)
        {
            if (playerData.acquantFateCount > 9)
            {
                grades = SetPlayerDataAfterGacha();

                for (int i = 0; i < grades.Length; i++)
                {
                    result[i] = GetRandomItem(grades[i], PickUpType.NORMAL, playerData.isPickUpNormalAlways);
                    playerData.AddItemAndHistory(result[i], buttonType);
                }

                playerData.acquantFateTotalTryCount += 10;
            }
            else
            {
                GameManager.instance.ShowLackOfWish(10, true);
                return;
            }
        }
        if (buttonType == 0)
        {
            if (playerData.acquantFateCount > 7)
            {
                grades = SetPlayerDataAfterGacha(true, 10, 8);

                for (int i = 0; i < grades.Length; i++)
                {
                    result[i] = GetRandomItem(grades[i], PickUpType.NORMAL, playerData.isPickUpNormalAlways);

                    if (i == 0 && !playerData.hasFirstTimeNoelle && grades[0] != Grade.LEGEND)
                    {
                        result[0] = ItemDatabase.instance.findItemByName("노엘");
                        playerData.hasFirstTimeNoelle = true;
                    }

                    playerData.AddItemAndHistory(result[i], buttonType);
                }

                playerData.acquantFateTotalTryCount += 10;
            }
            else
            {
                GameManager.instance.ShowLackOfWish(8, true);
                return;
            }

            playerData.acquantFateTotalTryCount += 10;
        }

        SortResultItems();

        // 이미지 등록
        for (int i = 0; i < 10; i++)
        {
            resultImageParent.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>().sprite = result[i].sprite;
        }

        if (hasFiveStar)
        {
            PlayPickUpVideo(5);
        }
        else
        {
            PlayPickUpVideo(4);
        }

        GameManager.instance.SetResources();
    }

    public void PickUpButtonOne()
    {
        // 체험 기간 확인 코드
        if (GameManager.instance.isValidTimeOver())
        {
            return;
        }

        // 인벤토리 수령가능 공간 확인
        if (GameManager.instance.GetPlayerData().weapons.Count > 190)
        {
            GameManager.instance.OnNoticeWeaponInventoryFull();
            return;
        }

        int buttonType = BannerManager.instance.onBannerIndex;

        Grade[] grades;

        SoundManager.instance.PlayOneShotEffectSound(1);

        if (buttonType == 1 || buttonType == 4) // 픽업, 지나간 픽업 처리
        {
            if (playerData.intertwinedFateCount > 0)
            {
                grades = SetPlayerDataAfterGacha(false);

                for (int i = 0; i < grades.Length; i++)
                {
                    result[i] = GetRandomItem(grades[i], PickUpType.CHARACTER, playerData.isPickUpCharacterAlways, playerData.isPickUpCharacter4Always);
                    playerData.AddItemAndHistory(result[i], buttonType);
                }

                playerData.characterTotalTryCount += 1;
            }
            else
            {
                GameManager.instance.ShowLackOfWish();
                return;
            }

        }
        if (buttonType == 2)
        {
            if (playerData.intertwinedFateCount > 0)
            {
                grades = SetPlayerDataAfterGacha(false);

                for (int i = 0; i < grades.Length; i++)
                {
                    result[i] = GetRandomItem(grades[i], PickUpType.WEAPON, playerData.isPickUpWeaponAlways, playerData.isPickUpWeapon4Always);
                    playerData.AddItemAndHistory(result[i], buttonType);
                }

                playerData.weaponTotalTryCount += 1;
            }
            else
            {
                GameManager.instance.ShowLackOfWish();
                return;
            }
        }
        if (buttonType == 3)
        {
            if (playerData.acquantFateCount > 0)
            {
                grades = SetPlayerDataAfterGacha();

                for (int i = 0; i < grades.Length; i++)
                {
                    result[i] = GetRandomItem(grades[i], PickUpType.NORMAL, playerData.isPickUpNormalAlways);
                    playerData.AddItemAndHistory(result[i], buttonType);
                }

                playerData.acquantFateTotalTryCount += 1;
            }
            else
            {
                GameManager.instance.ShowLackOfWish(10, true);
                return;
            }
        }

        SortResultItems();

        // 이미지 등록
        for (int i = 0; i < 10; i++)
        {
            resultImageParent.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>().sprite = result[i].sprite;
        }

        if (hasFiveStar)
        {
            PlayPickUpVideo(5);
        }
        else
        {
            PlayPickUpVideo(4);
        }

        GameManager.instance.SetResources();
    }

    public void PlayPickUpVideo(int star = 4, int wishCount = 10)
    {
        panel.SetActive(true);

        if (star == 5 && wishCount == 10)
        {
            videos[0].clip = videos[1].clip;
        }
        else
        {
            videos[0].clip = videos[2].clip;
        }

        videoPanel.gameObject.SetActive(true);
        OnResultDetails();
        OffResultDetailsColor();

        GameManager.instance.SavePlayerDataToJson();

        videos[0].Play();

        Invoke("OnSkipButton", 1.0f);
        Invoke("OffPanel", 6.5f);
    }

    public void SortResultItems()
    {
        List<Item> temp = new List<Item>();

        for (int i = 0; i < 10; i++)
        {
            if (result[i].grade == Grade.LEGEND && result[i].type == ItemType.CHARACTER)
            {
                temp.Add(result[i]);
            }
        }

        for (int i = 0; i < 10; i++)
        {
            if (result[i].grade == Grade.LEGEND && result[i].type != ItemType.CHARACTER)
            {
                temp.Add(result[i]);
            }
        }

        for (int i = 0; i < 10; i++)
        {
            if (result[i].grade == Grade.UNIQUE && result[i].type == ItemType.CHARACTER)
            {
                temp.Add(result[i]);
            }
        }

        for (int i = 0; i < 10; i++)
        {
            if (result[i].grade == Grade.UNIQUE && result[i].type != ItemType.CHARACTER)
            {
                temp.Add(result[i]);
            }
        }

        for (int i = 0; i < 10; i++)
        {
            if (result[i].grade == Grade.EPIC && result[i].type == ItemType.CHARACTER)
            {
                temp.Add(result[i]);
            }
        }

        for (int i = 0; i < 10; i++)
        {
            if (result[i].grade == Grade.EPIC && result[i].type != ItemType.CHARACTER)
            {
                temp.Add(result[i]);
            }
        }

        result = temp.ToArray();
    }

    public void OnSkipButton()
    {
        skipButton.SetActive(true);
    }

    public void ButtonSkip()
    {
        SoundManager.instance.PlayOneShotEffectSound(1);

        DoSkip();

        //GameManager.instance.SetNoticeSkip();
    }
    
    public void DoSkip()
    {
        GameManager.instance.OffNotice();
        SoundManager.instance.PlayOneShotEffectSound(1);

        StopCoroutine("PlayItemVideo");
        CancelInvoke("OffPanel");
        OffPanelAndSetting();
    }

    // resultPage On
    public void OffPanel()
    {
        videos[0].Stop();

        StartCoroutine("PlayItemVideo");
    }

    public void OffPanelAndSetting()
    {
        videos[0].Stop();

        panel.SetActive(false);
        videoPanel.gameObject.SetActive(false);
        skipButton.SetActive(false);

        resultPage.SetActive(true);
        SoundManager.instance.PlayOneShotEffectSound(4);
        resultPage.transform.GetChild(1).GetComponent<Animator>().SetBool("isClean", true);
        resultPage.transform.GetChild(2).GetComponent<Animator>().SetBool("isShow", true);

        SetGachaResultParticles();

        GameManager.instance.OffNotice();
        SoundManager.instance.StopEffectSound(3);

        CancelInvoke("OffPanel");
    }

    public void OffResultPage()
    {
        resultPage.transform.GetChild(1).GetComponent<Animator>().SetBool("isClean", false);
        resultPage.transform.GetChild(2).GetComponent<Animator>().SetBool("isShow", false);
        OffGachaItemImage();

        OnStardustPage(result);

        OffResultDetails();

        OffButtonInformation();

        resultPage.SetActive(false);
    }

    public void SetGachaResultParticles()
    {
        Transform parent = resultPage.transform.GetChild(2);

        for (int i = 0; i < parent.childCount; i++)
        {
            parent.GetChild(i).GetChild(5).gameObject.SetActive(false);
            parent.GetChild(i).GetChild(6).gameObject.SetActive(false);
            parent.GetChild(i).GetChild(7).gameObject.SetActive(false);
        }

        Invoke("OnGachaResultParticle", 2f);
    }

    public void OnGachaResultParticle()
    {
        Transform parent = resultPage.transform.GetChild(2);

        for (int i = 0; i < parent.childCount; i++)
        {
            if (result[i].grade == Grade.LEGEND)
            {
                parent.GetChild(i).GetChild(7).gameObject.SetActive(true);
            }
            else if (result[i].grade == Grade.UNIQUE)
            {
                parent.GetChild(i).GetChild(6).gameObject.SetActive(true);
            }
            else
            {
                parent.GetChild(i).GetChild(5).gameObject.SetActive(true);
            }
        }

        // information buttons
        buttonInformation.SetActive(true);
    }

    private void OffButtonInformation()
    {
        buttonInformation.SetActive(false);
    }

    public void OnStardustPage(Item[] items)
    {
        SoundManager.instance.PlayOneShotEffectSound(5);

        stardustPage.SetActive(true);
        stardustPage.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "" + GetStarlightCount(items);
        stardustPage.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "" + GetStardustCount(items);
    }

    public void OffStardustPage()
    {
        SoundManager.instance.PlayOneShotEffectSound(2);
        stardustPage.SetActive(false);
    }

    public int GetStarlightCount(Item[] items)
    {
        int starlightCount = 0;

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].type != ItemType.CHARACTER) // 무기
            {
                if (items[i].grade == Grade.LEGEND)
                {
                    starlightCount += 10;
                    playerData.starLightCount += 10;
                }
                else if (items[i].grade == Grade.UNIQUE)
                {
                    starlightCount += 2;
                    playerData.starLightCount += 2;
                }
            }
            else // 캐릭터
            {
                // 추후에 중복 캐릭터 7개 이후부터는 25, 5개씩 얻도록 해야함
                if (items[i].grade == Grade.LEGEND)
                {
                    starlightCount += 10;
                    playerData.starLightCount += 10;
                }
                else if (items[i].grade == Grade.UNIQUE)
                {
                    starlightCount += 2;
                    playerData.starLightCount += 2;
                }
            }
        }

        return starlightCount;
    }

    public int GetStardustCount(Item[] items)
    {
        int stardustCount = 0;

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].type != ItemType.CHARACTER) // 무기
            {
                if (items[i].grade == Grade.EPIC)
                {
                    stardustCount += 15;
                    playerData.starDustCount += 15;
                }
            }
        }

        return stardustCount;
    }

    public void OffGachaItemImage()
    {
        Transform parent = resultPage.transform.GetChild(2);

        for (int i = 0; i < parent.childCount; i++)
        {
            parent.GetChild(i).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
        }
    }

    public void OffResultDetailsColor()
    {
        for (int i = 0; i < 10; i++)
        {
            resultImageParent.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);

            if (result[i].grade == Grade.EPIC)
            {
                for (int j = 0; j < 3; j++)
                {
                    resultImageParent.transform.GetChild(i).GetChild(1).GetChild(j).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                }
            }
            if (result[i].grade == Grade.UNIQUE)
            {
                for (int j = 0; j < 4; j++)
                {
                    resultImageParent.transform.GetChild(i).GetChild(2).GetChild(j).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                }
            }
            if (result[i].grade == Grade.LEGEND)
            {
                for (int j = 0; j < 5; j++)
                {
                    resultImageParent.transform.GetChild(i).GetChild(3).GetChild(j).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                }
            }

            for (int j = 0; j < 7; j++)
            {
                resultImageParent.transform.GetChild(i).GetChild(4).GetChild(j).GetComponent<Image>().color = new Color(1, 1, 1, 0);
            }
        }

        OffGachaItemImage();
    }

    public void OnResultDetails()
    {
        for (int i = 0; i < 10; i++)
        {
            resultImageParent.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);

            if (result[i].grade == Grade.EPIC)
            {
                resultImageParent.transform.GetChild(i).GetChild(1).gameObject.SetActive(true);
            }
            if (result[i].grade == Grade.UNIQUE)
            {
                resultImageParent.transform.GetChild(i).GetChild(2).gameObject.SetActive(true);
            }
            if (result[i].grade == Grade.LEGEND)
            {
                resultImageParent.transform.GetChild(i).GetChild(3).gameObject.SetActive(true);
            }

            if (result[i].type == ItemType.CHARACTER)
            {
                resultImageParent.transform.GetChild(i).GetChild(4).GetChild((int) result[i].character.element).gameObject.SetActive(true); // 속성 켜기
                resultImageParent.transform.GetChild(i).GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 133.3f);
                resultImageParent.transform.GetChild(i).GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 306.4f);
            }
            else
            {
                resultImageParent.transform.GetChild(i).GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 130);
                resultImageParent.transform.GetChild(i).GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 130);
            }
        }
    }

    public void OffResultDetails()
    {
        for (int i = 0; i < 10; i++)
        {
            resultImageParent.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);

            if (result[i].grade == Grade.EPIC)
            {
                resultImageParent.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
            }
            if (result[i].grade == Grade.UNIQUE)
            {
                resultImageParent.transform.GetChild(i).GetChild(2).gameObject.SetActive(false);
            }
            if (result[i].grade == Grade.LEGEND)
            {
                resultImageParent.transform.GetChild(i).GetChild(3).gameObject.SetActive(false);
            }

            if (result[i].type == ItemType.CHARACTER)
            {
                resultImageParent.transform.GetChild(i).GetChild(4).GetChild((int)result[i].character.element).gameObject.SetActive(false);
            }
        }
    }

    public Grade GetRandomGrade(int fourStarCount, int fiveStarCount, bool isWeaponPickUP = false)
    {
        if (isWeaponPickUP && fiveStarCount >= 79)
        {
            return Grade.LEGEND;
        }

        if (fiveStarCount >= 89)
        {
            return Grade.LEGEND;
        }

        if (fourStarCount >= 9)
        {
            return Grade.UNIQUE;
        }

        int value = 600;

        if (isWeaponPickUP)
        {
            value = 700;
        }

        if (fiveStarCount > 64 && isWeaponPickUP)
        {
            value = 32323;
        }
        if (fiveStarCount > 74 && !isWeaponPickUP)
        {
            value = 32323;
        }

        int result = Random.Range(0, 100000);

        // Debug.Log("뽑기 확률 >>  사용자 : " + value + "   결과 값 : " + result);

        if (value > result)
        {
            return Grade.LEGEND;
        }

        value = 5100;

        if (isWeaponPickUP)
        {
            value = 6000;
        }

        if (fourStarCount > 4 && isWeaponPickUP)
        {
            value = (int)((6f + ((fourStarCount - 4) * 1.7f)) * 1000);
        }
        if (fourStarCount > 4 && !isWeaponPickUP)
        {
            value = (int)((5.1f + ((fourStarCount - 4) * 1.58f)) * 1000);
        }

        if (value > result || fourStarCount == 10)
        {
            return Grade.UNIQUE;
        }

        return Grade.EPIC;
    }

    public Grade[] SetPlayerDataAfterGacha(bool isAcquantFate = true, int repeatTime = 10, int removeCount = 10)
    {
        Grade[] grades = new Grade[10];
        hasFiveStar = false;

        if (isAcquantFate)
        {
            playerData.acquantFateCount -= removeCount;

            for (int i = 0; i < repeatTime; i++)
            {
                grades[i] = GetRandomGrade(playerData.acquantFateFourStarCount++, playerData.acquantFateFiveStarCount++);

                if (grades[i] == Grade.LEGEND)
                {
                    playerData.acquantFateFiveStarCount = 0;
                    hasFiveStar = true;
                }

                if (grades[i] == Grade.UNIQUE)
                {
                    playerData.acquantFateFourStarCount = 0;
                }
            }
        }
        else
        {
            playerData.intertwinedFateCount -= removeCount;

            if (BannerManager.instance.onBannerIndex == 1 || BannerManager.instance.onBannerIndex == 4)
            {
                for (int i = 0; i < repeatTime; i++)
                {
                    grades[i] = GetRandomGrade(playerData.characterFourStarCount++, playerData.characterFiveStarCount++);

                    if (grades[i] == Grade.LEGEND)
                    {
                        playerData.characterFiveStarCount = 0;
                        hasFiveStar = true;
                    }

                    if (grades[i] == Grade.UNIQUE)
                    {
                        playerData.characterFourStarCount = 0;
                    }
                }
            }
            else
            {
                for (int i = 0; i < repeatTime; i++)
                {
                    grades[i] = GetRandomGrade(playerData.weaponFourStarCount++, playerData.weaponFiveStarCount++, true);

                    if (grades[i] == Grade.LEGEND)
                    {
                        playerData.weaponFiveStarCount = 0;
                        hasFiveStar = true;
                    }

                    if (grades[i] == Grade.UNIQUE)
                    {
                        playerData.weaponFourStarCount = 0;
                    }
                }
            }
        }

        return grades;
    }

    public Item GetRandomItem(Grade grade, PickUpType pickUpType, bool isPickUpAlways = false, bool isPickUp4Always = false)
    {
        if (grade == Grade.LEGEND)
        {
            if (pickUpType == PickUpType.NORMAL)
            {
                int r = Random.Range(0, 2);

                if (r == 0)
                {
                    return ItemDatabase.instance.itemDB[Random.Range(MIN_LEGEND_CHARACTER, MAX_LEGEND_CHARACTER)];
                }
                else
                {
                    return ItemDatabase.instance.itemDB[Random.Range(MIN_LEGEND_WEAPON, MAX_LEGEND_WEAPON)];
                }
            }
            if (pickUpType == PickUpType.WEAPON)
            {
                int r = Random.Range(0, 75);

                if (r < 75 || isPickUpAlways)
                {
                    playerData.isPickUpWeaponAlways = false;

                    if (Random.Range(0, 2) == 0)
                    {
                        return ItemDatabase.instance.findItemByName("참봉의 칼날");
                    }
                    else
                    {
                        return ItemDatabase.instance.findItemByName("천공의 두루마리");
                    }
                }
                else
                {
                    playerData.isPickUpWeaponAlways = true;
                    return ItemDatabase.instance.itemDB[Random.Range(MIN_LEGEND_WEAPON, MAX_LEGEND_WEAPON)];
                }
            }
            if (pickUpType == PickUpType.CHARACTER)
            {
                // 현재 픽업중인 캐릭터
                if (BannerManager.instance.onBannerIndex == 1)
                {
                    return GetPickUpResultLegendGrade("알베도", isPickUpAlways);
                }

                if (BannerManager.instance.onBannerIndex == 4)
                {
                    // 지나간 픽업 캐릭터
                    switch (BannerManager.instance.bannerButtonCharacter)
                    {
                        case BannerButtonCharacter.VENTI:
                            return GetPickUpResultLegendGrade("벤티", isPickUpAlways);
                        case BannerButtonCharacter.KLEE:
                            return GetPickUpResultLegendGrade("클레", isPickUpAlways);
                        case BannerButtonCharacter.TARTAGLIA:
                            return GetPickUpResultLegendGrade("타르탈리아", isPickUpAlways);
                        case BannerButtonCharacter.ZHONGLI:
                            return GetPickUpResultLegendGrade("종려", isPickUpAlways);
                    }
                }
            }
        }

        if (grade == Grade.UNIQUE)
        {
            if (pickUpType == PickUpType.NORMAL)
            {
                int r = Random.Range(0, 2);

                if (r == 0)
                {
/*                    int value = Random.Range(MIN_UNIQUE_CHARACTER, MAX_UNIQUE_CHARACTER);
                    Debug.Log("랜덤 값 : " + MIN_UNIQUE_CHARACTER + ", : " + MAX_UNIQUE_CHARACTER);*/
                    return ItemDatabase.instance.itemDB[Random.Range(MIN_UNIQUE_CHARACTER, MAX_UNIQUE_CHARACTER)];
                }
                else
                {
                    return ItemDatabase.instance.itemDB[Random.Range(MIN_UNIQUE_WEAPON, MAX_UNIQUE_WEAPON)];
                }
            }
            if (pickUpType == PickUpType.WEAPON)
            {
                int r = Random.Range(0, 2);

                if (r == 0 || isPickUp4Always)
                {
                    playerData.isPickUpWeapon4Always = false;

                    int value = Random.Range(0, 5);

                    switch (value)
                    {
                        case 0:
                            return ItemDatabase.instance.findItemByName("페보니우스 검");
                        case 1:
                            return ItemDatabase.instance.findItemByName("페보니우스 장창");
                        case 2:
                            return ItemDatabase.instance.findItemByName("페보니우스 대검");
                        case 3:
                            return ItemDatabase.instance.findItemByName("절현");
                        case 4:
                            return ItemDatabase.instance.findItemByName("제례의 악장");
                    }
                }
                else
                {
                    int range = Random.Range(0, CHARACTER_COUNT_FOUR_STAR + WEAPON_COUNT_UNDER_FOUR_STAR + 1); // 무기 + 캐릭터 개수

                    if (range < CHARACTER_COUNT_FOUR_STAR + 1) // 캐릭터 개수
                    {
                        return ItemDatabase.instance.itemDB[Random.Range(MIN_UNIQUE_CHARACTER, MAX_UNIQUE_CHARACTER)];
                    }
                    else
                    {
                        playerData.isPickUpWeapon4Always = true;
                        return ItemDatabase.instance.itemDB[Random.Range(MIN_UNIQUE_WEAPON, MAX_UNIQUE_WEAPON)];
                    }
                }
            }
            if (pickUpType == PickUpType.CHARACTER)
            {
                int r = Random.Range(0, 2);
                string[] names = new string[3];

                if (r == 0 || isPickUp4Always)
                {
                    if (BannerManager.instance.onBannerIndex == 1)
                    {
                        names[0] = "피슬";
                        names[1] = "설탕";
                        names[2] = "베넷";
                        return GetPickUpResultUniqueGrade(names, isPickUp4Always);
                    }
                    else if(BannerManager.instance.onBannerIndex == 4)
                    {
                        switch (BannerManager.instance.bannerButtonCharacter)
                        {
                            case BannerButtonCharacter.VENTI:
                                names[0] = "향릉";
                                names[1] = "피슬";
                                names[2] = "바바라";
                                return GetPickUpResultUniqueGrade(names, isPickUpAlways);
                            case BannerButtonCharacter.KLEE:
                                names[0] = "행추";
                                names[1] = "설탕";
                                names[2] = "노엘";
                                return GetPickUpResultUniqueGrade(names, isPickUpAlways);
                            case BannerButtonCharacter.TARTAGLIA:
                                names[0] = "응광";
                                names[1] = "북두";
                                names[2] = "디오나";
                                return GetPickUpResultUniqueGrade(names, isPickUpAlways);
                            case BannerButtonCharacter.ZHONGLI:
                                names[0] = "신염";
                                names[1] = "중운";
                                names[2] = "레이저";
                                return GetPickUpResultUniqueGrade(names, isPickUpAlways);
                        }
                    }
                }
                else
                {
                    int range = Random.Range(0, CHARACTER_AND_WEAPON_FOUR_STAR_TOTAL); // 무기 + 캐릭터 개수

                    if (range < MAX_UNIQUE_CHARACTER) // 캐릭터 개수
                    {
                        playerData.isPickUpCharacter4Always = true;
                        return ItemDatabase.instance.itemDB[Random.Range(MIN_UNIQUE_CHARACTER, MAX_UNIQUE_CHARACTER)];
                    }
                    else
                    {
                        return ItemDatabase.instance.itemDB[Random.Range(MIN_UNIQUE_WEAPON, MAX_UNIQUE_WEAPON)];
                    }
                }
            }
        }

        return ItemDatabase.instance.itemDB[Random.Range(MIN_EPIC_WEAPON, MAX_EPIC_WEAPON)];
    }

    public void ButtonShowItemInformation(int index)
    {
        SoundManager.instance.PlayOneShotEffectSound(1);
        GameManager.instance.OnItemInformation(result[index]);
    }

    private Item GetPickUpResultLegendGrade(string pickUpCharacterKoName, bool isPickUpAlways)
    {
        int r = Random.Range(0, 2);

        if (r == 0 || isPickUpAlways)
        {
            playerData.isPickUpCharacterAlways = false;

            return ItemDatabase.instance.findItemByName(pickUpCharacterKoName);
        }
        else
        {
            playerData.isPickUpCharacterAlways = true;
            return ItemDatabase.instance.itemDB[Random.Range(MIN_LEGEND_CHARACTER, MAX_LEGEND_CHARACTER)];
        }
    }

    private Item GetPickUpResultUniqueGrade(string[] pickUpCharacterKoNames, bool isPickUpAlways)
    {
        playerData.isPickUpCharacter4Always = false;

        int value = Random.Range(0, 3);

        switch (value)
        {
            case 0:
                return ItemDatabase.instance.findItemByName(pickUpCharacterKoNames[0]);
            case 1:
                return ItemDatabase.instance.findItemByName(pickUpCharacterKoNames[1]);
            case 2:
                return ItemDatabase.instance.findItemByName(pickUpCharacterKoNames[2]);
        }

        return ItemDatabase.instance.itemDB[Random.Range(MIN_UNIQUE_CHARACTER, MAX_UNIQUE_CHARACTER)];
    }

    private void InitializeVideo()
    {
        videos = new VideoPlayer[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            videos[i] = transform.GetChild(i).GetComponent<VideoPlayer>();
        }
    }

    IEnumerator PlayItemVideo()
    {
        bool isPlayVideo = false;
        bool isFiveStar = false;

        for (int i = 0; i < result.Length; i++)
        {
            isPlayVideo = true;

            switch(result[i].koName)
            {
                case "진":
                    videos[0].clip = videos[3].clip;
                    isFiveStar = true;
                    break;
                case "타르탈리아":
                    videos[0].clip = videos[4].clip;
                    isFiveStar = true;
                    break;
                case "속세의 자물쇠":
                    videos[0].clip = videos[5].clip;
                    isFiveStar = true;
                    break;
                case "종려":
                    videos[0].clip = videos[6].clip;
                    isFiveStar = true;
                    break;
                case "북두":
                    videos[0].clip = videos[7].clip;
                    break;
                case "중운":
                    videos[0].clip = videos[8].clip;
                    break;
                case "디오나":
                    videos[0].clip = videos[9].clip;
                    break;
                case "리사":
                    videos[0].clip = videos[10].clip;
                    break;
                case "노엘":
                    videos[0].clip = videos[11].clip;
                    break;
                case "레이저":
                    videos[0].clip = videos[12].clip;
                    break;
                case "설탕":
                    videos[0].clip = videos[13].clip;
                    break;
                case "향릉":
                    videos[0].clip = videos[14].clip;
                    break;
                case "신염":
                    videos[0].clip = videos[15].clip;
                    break;
                case "용학살창":
                    videos[0].clip = videos[16].clip;
                    break;
                case "소심":
                    videos[0].clip = videos[17].clip;
                    break;
                case "페보니우스 비전":
                    videos[0].clip = videos[18].clip;
                    break;
                case "페보니우스 대검":
                    videos[0].clip = videos[19].clip;
                    break;
                case "페보니우스 장창":
                    videos[0].clip = videos[20].clip;
                    break;
                case "페보니우스 활":
                    videos[0].clip = videos[21].clip;
                    break;
                case "용의 포효":
                    videos[0].clip = videos[22].clip;
                    break;
                case "빗물베기":
                    videos[0].clip = videos[23].clip;
                    break;
                case "제례의 악장":
                    videos[0].clip = videos[24].clip;
                    break;
                case "제례 대검":
                    videos[0].clip = videos[25].clip;
                    break;
                case "음유시인의 악장":
                    videos[0].clip = videos[26].clip;
                    break;
                case "응광":
                    videos[0].clip = videos[27].clip;
                    break;
                case "엠버":
                    videos[0].clip = videos[28].clip;
                    break;
                case "바바라":
                    videos[0].clip = videos[29].clip;
                    break;
                case "페보니우스 검":
                    videos[0].clip = videos[30].clip;
                    break;
                case "절현":
                    videos[0].clip = videos[31].clip;
                    break;
                case "제례활":
                    videos[0].clip = videos[32].clip;
                    break;
                case "제례검":
                    videos[0].clip = videos[33].clip;
                    break;
                case "천공의 마루":
                    videos[0].clip = videos[34].clip;
                    isFiveStar = true;
                    break;
                case "베넷":
                    videos[0].clip = videos[35].clip;
                    break;
                case "피슬":
                    videos[0].clip = videos[36].clip;
                    break;
                case "케이아":
                    videos[0].clip = videos[37].clip;
                    break;
                case "천공의 검":
                    videos[0].clip = videos[38].clip;
                    isFiveStar = true;
                    break;
                case "천공의 날개":
                    videos[0].clip = videos[39].clip;
                    isFiveStar = true;
                    break;
                case "피리검":
                    videos[0].clip = videos[40].clip;
                    break;
                case "늑대의 말로":
                    videos[0].clip = videos[41].clip;
                    isFiveStar = true;
                    break;
                case "다이루크":
                    videos[0].clip = videos[42].clip;
                    isFiveStar = true;
                    break;
                case "알베도":
                    videos[0].clip = videos[43].clip;
                    isFiveStar = true;
                    break;
                default:
                    isPlayVideo = false;

                    if (result[i].type != ItemType.CHARACTER)
                    {
                        break;
                    }

                    SetCachaIllust(result[i]);
                    gachaIllustSet.SetActive(true);

                    break;
            }

            if (isPlayVideo)
            {
                videos[0].Play();

                if (isFiveStar)
                {
                    yield return new WaitForSeconds(4.0f);
                }
                else
                {
                    yield return new WaitForSeconds(3.6f);
                }

                isFiveStar = false;

                videos[0].Stop();
            }
            else if (result[i].type == ItemType.CHARACTER)
            {
                yield return new WaitForSeconds(3.5f);
                gachaIllustSet.SetActive(false);
            }
        }

        OffPanelAndSetting();
    }

    private void SetCachaIllust(Item item)
    {
        //gachaIllustSet.GetComponent<Animator>().SetTrigger("doIllustOn");

        gachaIllust.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Character/Illust/" + item.enName);
        gachaIllust.transform.GetChild(0).GetComponent<Image>().sprite = 
            gachaIllustSet.transform.GetChild(6).GetChild(item.GetElementIndex()).GetComponent<Image>().sprite;

        if (LanguageManager.instance.language == Language.KOREAN)
        {
            gachaIllust.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = item.koName;
        }
        else
        {
            gachaIllust.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = item.enName;
        }

        gachaIllustSet.transform.GetChild(2).GetComponent<ParticleSystem>().Play();

        if (item.grade == Grade.LEGEND)
        {
            gachaIllust.transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            gachaIllustSet.transform.GetChild(3).gameObject.SetActive(false);
            gachaIllustSet.transform.GetChild(4).gameObject.SetActive(true);
            gachaIllustSet.transform.GetChild(4).GetComponent<ParticleSystem>().Play();
            SoundManager.instance.PlayOneShotEffectSound(7);

        }
        else
        {
            gachaIllust.transform.GetChild(0).GetChild(5).gameObject.SetActive(false);
            gachaIllustSet.transform.GetChild(3).gameObject.SetActive(true);
            gachaIllustSet.transform.GetChild(4).gameObject.SetActive(false);
            gachaIllustSet.transform.GetChild(3).GetComponent<ParticleSystem>().Play();
            SoundManager.instance.PlayOneShotEffectSound(6);
        }
    }
}

public enum PickUpType
{
    NORMAL,
    CHARACTER,
    WEAPON
}
