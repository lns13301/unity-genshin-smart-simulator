using System.Collections;
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
    public Sprite[] buttonImage;
    public Vector2 buttonSize1 = new Vector2(342, 93);
    public Vector2 buttonSize2 = new Vector2(342, 76);

    public RectTransform pickupButtonText;

    public GameObject bannerImageParent;
    public Image[] extraBannerImages;
    public Image[] extraBannerButtonImage;

    public BannerButtonCharacter bannerButtonCharacter;

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
        pickupButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, buttonSize1.x);
        pickupButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, buttonSize1.y);
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

        switch (bannerIndex)
        {
            case 0:
                bannerButton[0].SetActive(false);
                bannerButton[5].SetActive(true);
                pickupButton.sprite = buttonImage[8];
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
            default:
                return 0;
        }
    }
}

public enum BannerButtonCharacter
{
    VENTI,
    KLEE,
    TARTAGLIA
}
