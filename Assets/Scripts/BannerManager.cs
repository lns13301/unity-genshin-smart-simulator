﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BannerManager : MonoBehaviour
{
    public static BannerManager instance;
    public GameObject[] bannerButton;
    public GameObject[] bannerImage;

    public bool isBannerChange;
    public Color alpha;
    public float alphaSpeed = 0.001f;
    public int onBannerIndex;

    public Image pickupButton;
    public Image pickupButtonOne;
    public Sprite[] buttonImage;
    public Vector2 buttonSize1 = new Vector2(342, 93);
    public Vector2 buttonSize2 = new Vector2(342, 76);

    public RectTransform pickupButtonText;
    public RectTransform pickupButtonOneText;

    public GameObject bannerImageParent;
    public GameObject bannerButtonImageParent;
    public Image[] extraBannerImages;
    public Image[] extraBannerButtonImage;

    public BannerButtonCharacter bannerButtonCharacter;

    public GameObject lastPickUpBannerChangeText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        isBannerChange = false;
        bannerButtonCharacter = BannerButtonCharacter.VENTI;

        bannerImage[0].GetComponent<Animator>().SetBool("isBannerOn", true);

        for (int i = 1; i < bannerImage.Length; i++)
        {
            bannerImage[i].GetComponent<Animator>().SetBool("isBannerOn", false);
        }

        for (int i = 5; i < 10; i++)
        {
            bannerButton[i].SetActive(false);
        }

        OnBannerFirst();

        extraBannerImages = new Image[bannerImageParent.transform.childCount];

        for (int i = 0; i < bannerImageParent.transform.childCount; i++)
        {
            extraBannerImages[i] = bannerImageParent.transform.GetChild(i).GetComponent<Image>();
        }

        extraBannerButtonImage = new Image[bannerButtonImageParent.transform.childCount];

        for (int i = 0; i < bannerButtonImageParent.transform.childCount; i++)
        {
            extraBannerButtonImage[i] = bannerButtonImageParent.transform.GetChild(i).GetComponent<Image>();
        }

        lastPickUpBannerChangeText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        OnBanner();
        OffBanner(onBannerIndex);
    }

    private void OnBannerFirst()
    {
        bannerButton[0].SetActive(false);
        bannerButton[5].SetActive(true);

        pickupButton.sprite = buttonImage[8];
        pickupButtonOne.gameObject.SetActive(false);
        pickupButtonOneText.gameObject.SetActive(false);

        pickupButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, buttonSize1.x);
        pickupButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, buttonSize1.y);
        pickupButtonOne.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, buttonSize2.x);
        pickupButtonOne.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, buttonSize2.y);
    }

    public void OnBanner(int bannerIndex)
    {
        bool isLastBannerIndexExtraPickUp = false;

        // 지나간 픽업을 이전에 선택한 상태면 다음 픽업으로 변경
        if (bannerIndex == 4 && onBannerIndex == 4)
        {
            isLastBannerIndexExtraPickUp = true;
        }

        onBannerIndex = bannerIndex;
        isBannerChange = true;
        SoundManager.instance.PlayOneShotEffectSound(0);

        GameManager.instance.SetResources();

        OffAllBannerButton();
        pickupButtonOne.gameObject.SetActive(true);
        pickupButtonOneText.gameObject.SetActive(true);
        lastPickUpBannerChangeText.SetActive(false);

        switch (bannerIndex)
        {
            case 0:
                bannerButton[0].SetActive(false);
                bannerButton[5].SetActive(true);
                pickupButton.sprite = buttonImage[8];
                pickupButtonOne.gameObject.SetActive(false);
                pickupButtonOneText.gameObject.SetActive(false);
                pickupButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, buttonSize1.x);
                pickupButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, buttonSize1.y);
                break;
            case 1:
                bannerButton[1].SetActive(false);
                bannerButton[6].SetActive(true);
                SetPickUPButtonImage();
                pickupButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, buttonSize2.x);
                pickupButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, buttonSize2.y);
                break;
            case 2:
                bannerButton[2].SetActive(false);
                bannerButton[7].SetActive(true);
                SetPickUPButtonImage();
                pickupButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, buttonSize2.x);
                pickupButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, buttonSize2.y);
                break;
            case 3:
                bannerButton[3].SetActive(false);
                bannerButton[8].SetActive(true);
                SetPickUPButtonImage(true);
                pickupButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, buttonSize2.x);
                pickupButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, buttonSize2.y);
                break;
            case 4:
                bannerButton[4].SetActive(false);
                bannerButton[9].SetActive(true);
                lastPickUpBannerChangeText.SetActive(true);

                SetPickUPButtonImage();
                pickupButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, buttonSize2.x);
                pickupButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, buttonSize2.y);

                if (isLastBannerIndexExtraPickUp)
                {
                    bannerButtonCharacter = GetNextBannerButtonCharacter(bannerButtonCharacter);
                    bannerButton[4].GetComponent<Image>().sprite = extraBannerButtonImage[GetBannerButtonCharacterIndex(bannerButtonCharacter)].sprite;
                    bannerButton[9].GetComponent<Image>().sprite = extraBannerButtonImage[GetBannerButtonCharacterIndex(bannerButtonCharacter) + 1].sprite;

                    if (LanguageManager.instance.language == Language.KOREAN)
                    {
                        bannerImage[4].GetComponent<Image>().sprite = extraBannerImages[GetBannerButtonCharacterIndex(bannerButtonCharacter) + 9].sprite;
                    }
                    else
                    {
                        bannerImage[4].GetComponent<Image>().sprite = extraBannerImages[GetBannerButtonCharacterIndex(bannerButtonCharacter) + 8].sprite;
                    }
                }

                break;
        }

        pickupButtonText.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, buttonSize2.x);
        pickupButtonText.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, buttonSize2.y);

        pickupButtonOneText.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, buttonSize2.x);
        pickupButtonOneText.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, buttonSize2.y);
    }

    public void SetPickUPButtonImage(bool isAcquantFactWish = false)
    {
        PlayerData playerData = GameManager.instance.GetPlayerData();

        if (isAcquantFactWish)
        {
            if (playerData.acquantFateCount < 10)
            {
                pickupButton.sprite = buttonImage[7];
            }
            else
            {
                pickupButton.sprite = buttonImage[5];
            }

            if (playerData.acquantFateCount < 1)
            {
                pickupButtonOne.sprite = buttonImage[6];
            }
            else
            {
                pickupButtonOne.sprite = buttonImage[4];
            }
        }
        else
        {
            if (playerData.intertwinedFateCount < 10)
            {
                pickupButton.sprite = buttonImage[3];
            }
            else
            {
                pickupButton.sprite = buttonImage[1];
            }

            if (playerData.intertwinedFateCount < 1)
            {
                pickupButtonOne.sprite = buttonImage[2];
            }
            else
            {
                pickupButtonOne.sprite = buttonImage[0];
            }
        }
    }

    public void OffAllBannerButton()
    {
        bannerButton[0].SetActive(true);
        bannerButton[1].SetActive(true);
        bannerButton[2].SetActive(true);
        bannerButton[3].SetActive(true);
        bannerButton[4].SetActive(true);
        bannerButton[5].SetActive(false);
        bannerButton[6].SetActive(false);
        bannerButton[7].SetActive(false);
        bannerButton[8].SetActive(false);
        bannerButton[9].SetActive(false);
    }

    public void OnBanner()
    {
        if (!isBannerChange)
        {
            return;
        }

        bannerImage[onBannerIndex].GetComponent<Animator>().SetBool("isBannerOn", true);
    }

    public void OffBanner(int onBannerIndex)
    {
        if (!isBannerChange)
        {
            return;
        }

        for (int i = 0; i < bannerImage.Length; i++)
        {
            if (i != onBannerIndex)
            {
                bannerImage[i].GetComponent<Animator>().SetBool("isBannerOn", false);
            }
        }

        isBannerChange = false;
    }

    public void SetBannerImageByLanguage(Language language)
    {
        if (language == Language.KOREAN)
        {
            for (int i = 0; i < bannerImage.Length; i++)
            {
                bannerImage[i].GetComponent<Image>().sprite = extraBannerImages[i + 4].sprite;
            }

            bannerImage[4].GetComponent<Image>().sprite = extraBannerImages[9].sprite;
        }
        else
        {
            for (int i = 0; i < bannerImage.Length; i++)
            {
                bannerImage[i].GetComponent<Image>().sprite = extraBannerImages[i].sprite;
            }

            bannerImage[4].GetComponent<Image>().sprite = extraBannerImages[8].sprite;
        }
    }

    public BannerButtonCharacter GetNextBannerButtonCharacter(BannerButtonCharacter bbc)
    {
        switch (bbc)
        {
            case BannerButtonCharacter.VENTI:
                return BannerButtonCharacter.KLEE;
            case BannerButtonCharacter.KLEE:
                return BannerButtonCharacter.TARTAGLIA;
            case BannerButtonCharacter.TARTAGLIA:
                return BannerButtonCharacter.ZHONGLI;
            case BannerButtonCharacter.ZHONGLI:
                return BannerButtonCharacter.ALBEDO;
            case BannerButtonCharacter.ALBEDO:
                return BannerButtonCharacter.GANYU;
            case BannerButtonCharacter.GANYU:
                return BannerButtonCharacter.XIAO;
            case BannerButtonCharacter.XIAO:
                return BannerButtonCharacter.KEQING;
            case BannerButtonCharacter.KEQING:
                return BannerButtonCharacter.HUTAO;
            case BannerButtonCharacter.HUTAO:
                return BannerButtonCharacter.VENTI_RETURN;
            case BannerButtonCharacter.VENTI_RETURN:
                return BannerButtonCharacter.ZHONGLI_RETURN;
            case BannerButtonCharacter.ZHONGLI_RETURN:
                return BannerButtonCharacter.TARTAGLIA_RETURN;
            case BannerButtonCharacter.TARTAGLIA_RETURN:
                PickUpManager.instance.lastPickUpBannerType = PickUpType.WEAPON;
                return BannerButtonCharacter.KLEE_WEAPON;
            case BannerButtonCharacter.KLEE_WEAPON:
                return BannerButtonCharacter.TARTAGLIA_WEAPON;
            case BannerButtonCharacter.TARTAGLIA_WEAPON:
                return BannerButtonCharacter.ZHONGLI_WEAPON;
            case BannerButtonCharacter.ZHONGLI_WEAPON:
                return BannerButtonCharacter.ALBEDO_WEAPON;
            case BannerButtonCharacter.ALBEDO_WEAPON:
                return BannerButtonCharacter.GANYU_WEAPON;
            case BannerButtonCharacter.GANYU_WEAPON:
                return BannerButtonCharacter.XIAO_WEAPON;
            case BannerButtonCharacter.XIAO_WEAPON:
                return BannerButtonCharacter.HUTAO_WEAPON;
            case BannerButtonCharacter.HUTAO_WEAPON:
                return BannerButtonCharacter.VENTI_RETURN_WEAPON;
            case BannerButtonCharacter.VENTI_RETURN_WEAPON:
                return BannerButtonCharacter.ZHONGLI_RETURN_WEAPON;
            case BannerButtonCharacter.ZHONGLI_RETURN_WEAPON:
                return BannerButtonCharacter.TARTAGLIA_RETURN_WEAPON;
            case BannerButtonCharacter.TARTAGLIA_RETURN_WEAPON:
                PickUpManager.instance.lastPickUpBannerType = PickUpType.CHARACTER;
                return BannerButtonCharacter.VENTI;
        }

        return BannerButtonCharacter.VENTI;
    }

    public int GetBannerButtonCharacterIndex(BannerButtonCharacter bbc)
    {
        switch (bbc)
        {
            case BannerButtonCharacter.VENTI:
                return 0;
            case BannerButtonCharacter.KLEE:
                return 2;
            case BannerButtonCharacter.TARTAGLIA:
                return 4;
            case BannerButtonCharacter.ZHONGLI:
                return 6;
            case BannerButtonCharacter.ALBEDO:
                return 8;
            case BannerButtonCharacter.GANYU:
                return 10;
            case BannerButtonCharacter.XIAO:
                return 12;
            case BannerButtonCharacter.KEQING:
                return 14;
            case BannerButtonCharacter.HUTAO:
                return 16;
            case BannerButtonCharacter.VENTI_RETURN:
                return 18;
            case BannerButtonCharacter.ZHONGLI_RETURN:
                return 20;
            case BannerButtonCharacter.TARTAGLIA_RETURN:
                return 22;
            case BannerButtonCharacter.KLEE_WEAPON:
                return 24;
            case BannerButtonCharacter.TARTAGLIA_WEAPON:
                return 26;
            case BannerButtonCharacter.ZHONGLI_WEAPON:
                return 28;
            case BannerButtonCharacter.ALBEDO_WEAPON:
                return 30;
            case BannerButtonCharacter.GANYU_WEAPON:
                return 32;
            case BannerButtonCharacter.XIAO_WEAPON:
                return 34;
            case BannerButtonCharacter.HUTAO_WEAPON:
                return 36;
            case BannerButtonCharacter.VENTI_RETURN_WEAPON:
                return 38;
            case BannerButtonCharacter.ZHONGLI_RETURN_WEAPON:
                return 40;
            case BannerButtonCharacter.TARTAGLIA_RETURN_WEAPON:
                return 42;
            default:
                return 0;
        }
    }
}

public enum BannerButtonCharacter
{
    VENTI,
    KLEE,
    TARTAGLIA,
    ZHONGLI,
    ALBEDO,
    GANYU,
    XIAO,
    KEQING,
    HUTAO,
    VENTI_RETURN,
    ZHONGLI_RETURN,
    TARTAGLIA_RETURN,
    EULA,
    VENTI_WEAPON,
    KLEE_WEAPON,
    TARTAGLIA_WEAPON,
    ZHONGLI_WEAPON,
    ALBEDO_WEAPON,
    GANYU_WEAPON,
    XIAO_WEAPON,
    KEQING_WEAPON,
    HUTAO_WEAPON,
    VENTI_RETURN_WEAPON,
    ZHONGLI_RETURN_WEAPON,
    TARTAGLIA_RETURN_WEAPON,
    EULA_WEAPON
}
