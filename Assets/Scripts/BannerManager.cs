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

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        isBannerChange = false;

        bannerImage[0].GetComponent<Animator>().SetBool("isBannerOn", true);

        for (int i = 1; i < bannerImage.Length; i++)
        {
            bannerImage[i].GetComponent<Animator>().SetBool("isBannerOn", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        OnBanner();
        OffBanner(onBannerIndex);
    }

    public void OnBanner(int bannerIndex)
    {
        onBannerIndex = bannerIndex;
        isBannerChange = true;
        SoundManager.instance.PlayOneShotEffectSound(0);

        GameManager.instance.SetResources();

        switch (bannerIndex)
        {
            case 0:
                pickupButton.sprite = buttonImage[8];
                pickupButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, buttonSize1.x);
                pickupButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, buttonSize1.y);
                break;
            case 1:
                pickupButton.sprite = buttonImage[1];
                pickupButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, buttonSize2.x);
                pickupButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, buttonSize2.y);
                break;
            case 2:
                pickupButton.sprite = buttonImage[1];
                pickupButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, buttonSize2.x);
                pickupButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, buttonSize2.y);
                break;
            case 3:
                pickupButton.sprite = buttonImage[5];
                pickupButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, buttonSize2.x);
                pickupButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, buttonSize2.y);
                break;
        }
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
}
