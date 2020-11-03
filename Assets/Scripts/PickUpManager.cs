using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class PickUpManager : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        resultPage.SetActive(false);
        playerData = GameManager.instance.playerData;

        hasFiveStar = false;
        result = new Item[10];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PickUpButton()
    {
        int buttonType = BannerManager.instance.onBannerIndex;

        Grade[] grades;

        SoundManager.instance.PlayOneShotEffectSound(1);

        if (buttonType == 1)
        {
            if (playerData.intertwinedFateCount > 9)
            {
                grades = setPlayerDataAfterGacha(false);

                for (int i = 0; i < grades.Length; i++)
                {
                    result[i] = getRandomItem(grades[i], PickUpType.CHARACTER, playerData.isPickUpCharacterAlways);
                    playerData.characterHistory.Add(result[i]);
                }

                playerData.characterTotalTryCount += 10;
            }
            else
            {
                return;
            }

        }
        if (buttonType == 2)
        {
            if (playerData.intertwinedFateCount > 9)
            {
                grades = setPlayerDataAfterGacha(false);

                for (int i = 0; i < grades.Length; i++)
                {
                    result[i] = getRandomItem(grades[i], PickUpType.WEAPON, playerData.isPickUpWeaponAlways, playerData.isPickUpWeapon4Always);
                    playerData.weaponHistory.Add(result[i]);
                }

                playerData.weaponTotalTryCount += 10;
            }
            else
            {
                return;
            }
        }
        if (buttonType == 3)
        {
            if (playerData.acquantFateCount > 9)
            {
                grades = setPlayerDataAfterGacha();

                for (int i = 0; i < grades.Length; i++)
                {
                    result[i] = getRandomItem(grades[i], PickUpType.NORMAL, playerData.isPickUpNormalAlways, playerData.isPickUpCharacter4Always);
                    playerData.normalHistory.Add(result[i]);
                }

                playerData.acquantFateTotalTryCount += 10;
            }
            else
            {
                return;
            }
        }
        if (buttonType == 0)
        {
            if (playerData.acquantFateCount > 7)
            {
                grades = setPlayerDataAfterGacha(true, 10, 8);

                for (int i = 0; i < grades.Length; i++)
                {
                    result[i] = getRandomItem(grades[i], PickUpType.NORMAL, playerData.isPickUpNormalAlways);

                    if (i == 0 && !playerData.hasFirstTimeNoelle && grades[0] != Grade.LEGEND)
                    {
                        result[0] = ItemDatabase.instance.findItemByName("노엘");
                        playerData.hasFirstTimeNoelle = true;
                    }

                    playerData.noelleHistory.Add(result[i]);
                }

                playerData.acquantFateTotalTryCount += 10;
            }
            else
            {
                return;
            }

            playerData.acquantFateTotalTryCount += 10;
        }

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
        videos[0].Play();

        Invoke("OnSkipButton", 2.5f);
        Invoke("OffPanel", 6.3f);
    }

    public void OnSkipButton()
    {
        skipButton.SetActive(true);
    }

    public void ButtonSkip()
    {
        SoundManager.instance.PlayOneShotEffectSound(1);
        OffPanel();
    }

    public void OffPanel()
    {
        videos[0].Stop();

        panel.SetActive(false);
        videoPanel.gameObject.SetActive(false);
        skipButton.SetActive(false);

        resultPage.SetActive(true);
        resultPage.transform.GetChild(1).GetComponent<Animator>().SetBool("isClean", true);
        resultPage.transform.GetChild(2).GetComponent<Animator>().SetBool("isShow", true);

        CancelInvoke("OffPanel");
    }

    public void OffResultPage()
    {
        SoundManager.instance.PlayOneShotEffectSound(0);
        resultPage.transform.GetChild(1).GetComponent<Animator>().SetBool("isClean", false);
        resultPage.transform.GetChild(2).GetComponent<Animator>().SetBool("isShow", false);
        OffGachaItemImage();

        OffResultDetails();

        resultPage.SetActive(false);
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

            if (result[i].type == ItemType.HERO)
            {
                resultImageParent.transform.GetChild(i).GetChild(4).GetChild((int) result[i].element).gameObject.SetActive(true); // 속성 켜기
                resultImageParent.transform.GetChild(i).GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 500);
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

            if (result[i].type == ItemType.HERO)
            {
                resultImageParent.transform.GetChild(i).GetChild(4).GetChild((int)result[i].element).gameObject.SetActive(false);
            }
        }
    }

    public Grade GetRandomGrade(int fourStarCount, int fiveStarCount)
    {
        if (fiveStarCount >= 89)
        {
            return Grade.LEGEND;
        }

        if (fourStarCount >= 9)
        {
            return Grade.UNIQUE;
        }

        int value = (int)((0.6f + (fiveStarCount * 0.02f)) * 100);
        int result = Random.Range(0, 10000);

        if (value > result)
        {
            return Grade.LEGEND;
        }

        value = (int)((5.1f + (fiveStarCount * 0.08f)) * 100);

        if (value > result || fourStarCount == 10)
        {
            return Grade.UNIQUE;
        }

        return Grade.EPIC;
    }

    public Grade[] setPlayerDataAfterGacha(bool isAcquantFate = true, int repeatTime = 10, int removeCount = 10)
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

            if (BannerManager.instance.onBannerIndex == 1)
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
                    grades[i] = GetRandomGrade(playerData.weaponFourStarCount++, playerData.weaponFiveStarCount++);

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

    public Item getRandomItem(Grade grade, PickUpType pickUpType, bool isPickUpAlways = false, bool isPickUp4Always = false)
    {
        if (grade == Grade.LEGEND)
        {
            if (pickUpType == PickUpType.NORMAL)
            {
                int r = Random.Range(0, 2);

                if (r == 0)
                {
                    return ItemDatabase.instance.itemDB[Random.Range(0, 5)];
                }
                else
                {
                    return ItemDatabase.instance.itemDB[Random.Range(22, 32)];
                }
            }
            if (pickUpType == PickUpType.WEAPON)
            {
                int r = Random.Range(0, 2);

                if (r == 0 || isPickUpAlways)
                {
                    playerData.isPickUpWeaponAlways = false;

                    if (Random.Range(0, 2) == 0)
                    {
                        return ItemDatabase.instance.itemDB[24];
                    }
                    else
                    {
                        return ItemDatabase.instance.itemDB[28];
                    }
                }
                else
                {
                    playerData.isPickUpWeaponAlways = true;
                    return ItemDatabase.instance.itemDB[Random.Range(22, 32)];
                }
            }
            if (pickUpType == PickUpType.CHARACTER)
            {
                int r = Random.Range(0, 2);

                if (r == 0 || isPickUpAlways)
                {
                    playerData.isPickUpCharacterAlways = false;

                    return ItemDatabase.instance.itemDB[6];
                }
                else
                {
                    playerData.isPickUpCharacterAlways = true;
                    return ItemDatabase.instance.itemDB[Random.Range(0, 5)];
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
                    return ItemDatabase.instance.itemDB[Random.Range(8, 22)];
                }
                else
                {
                    return ItemDatabase.instance.itemDB[Random.Range(32, 73)];
                }
            }
            if (pickUpType == PickUpType.WEAPON)
            {
                int r = Random.Range(0, 2);

                if (r == 0 || isPickUp4Always)
                {
                    playerData.isPickUpWeaponAlways = false;

                    int value = Random.Range(0, 5);

                    switch (value)
                    {
                        case 0:
                            return ItemDatabase.instance.findItemByName("제례검");
                        case 1:
                            return ItemDatabase.instance.findItemByName("제례 대검");
                        case 2:
                            return ItemDatabase.instance.findItemByName("제례활");
                        case 3:
                            return ItemDatabase.instance.findItemByName("제례의 악장");
                        case 4:
                            return ItemDatabase.instance.findItemByName("용학살창");
                    }
                }
                else
                {
                    int range = Random.Range(0, 55); // 무기 + 캐릭터 개수

                    if (range < 14) // 캐릭터 개수
                    {
                        return ItemDatabase.instance.itemDB[Random.Range(8, 22)];
                    }
                    else
                    {
                        playerData.isPickUpWeapon4Always = true;
                        return ItemDatabase.instance.itemDB[Random.Range(32, 73)];
                    }
                }
            }
            if (pickUpType == PickUpType.CHARACTER)
            {
                int r = Random.Range(0, 2);

                if (r == 0 || isPickUp4Always)
                {
                    playerData.isPickUpCharacterAlways = false;

                    int value = Random.Range(0, 3);

                    switch (value)
                    {
                        case 0:
                            return ItemDatabase.instance.findItemByName("노엘");
                        case 1:
                            return ItemDatabase.instance.findItemByName("설탕");
                        case 2:
                            return ItemDatabase.instance.findItemByName("행추");
                    }

                    return ItemDatabase.instance.itemDB[Random.Range(8, 22)];
                }
                else
                {
                    int range = Random.Range(0, 55); // 무기 + 캐릭터 개수

                    if (range < 14) // 캐릭터 개수
                    {
                        playerData.isPickUpCharacter4Always = true;
                        return ItemDatabase.instance.itemDB[Random.Range(8, 22)];
                    }
                    else
                    {
                        return ItemDatabase.instance.itemDB[Random.Range(32, 73)];
                    }
                }
            }
        }

        return ItemDatabase.instance.itemDB[Random.Range(73, 98)];
    }
}

public enum PickUpType
{
    NORMAL,
    CHARACTER,
    WEAPON
}
