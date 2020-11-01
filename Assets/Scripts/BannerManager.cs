using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BannerManager : MonoBehaviour
{
    public BannerManager instance;
    public GameObject[] bannerButton;
    public GameObject[] bannerImage;

    public bool isBannerChange;
    public Color alpha;
    public float alphaSpeed = 0.001f;
    public int onBannerIndex;

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
