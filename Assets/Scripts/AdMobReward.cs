using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;

public class AdMobReward : MonoBehaviour
{
    public static AdMobReward instance;
    static bool isAdVideoLoaded = false;

    private RewardedAd videoAd;
    public static bool ShowAd = false;
    string videoID;

    public GameObject notice;
    public Text noticeText;

    public void Start()
    {
        instance = this;

        //Test ID : "ca-app-pub-3940256099942544/5224354917";
        //광고 ID : "ca-app-pub-5596979448837149/3283466862";
        videoID = "ca-app-pub-5596979448837149/3283466862";
        videoAd = new RewardedAd(videoID);
        Handle(videoAd);
        Load();

        noticeText.supportRichText = true;
        notice.SetActive(false);
    }

    private void Handle(RewardedAd videoAd)
    {
        videoAd.OnAdLoaded += HandleOnAdLoaded;
        videoAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        videoAd.OnAdFailedToShow += HandleOnAdFailedToShow;
        videoAd.OnAdOpening += HandleOnAdOpening;
        videoAd.OnAdClosed += HandleOnAdClosed;
        videoAd.OnUserEarnedReward += HandleOnUserEarnedReward;
    }

    private void Load()
    {
        AdRequest request = new AdRequest.Builder().Build();
        videoAd.LoadAd(request);
    }

    public RewardedAd ReloadAd()
    {
        RewardedAd videoAd = new RewardedAd(videoID);
        Handle(videoAd);
        AdRequest request = new AdRequest.Builder().Build();
        videoAd.LoadAd(request);
        return videoAd;
    }

    //오브젝트 참조해서 불러줄 함수
    public void Show()
    {
        SoundManager.instance.PlayOneShotEffectSound(1);

        if (GameManager.instance.isValidTimeOver())
        {
            notice.SetActive(false);
            return;
        }

        if (GameManager.instance.GetPlayerData().adCount > 0)
        {
            StartCoroutine("ShowRewardAd");
        }

        notice.SetActive(false);
    }

    private IEnumerator ShowRewardAd()
    {
        while (!videoAd.IsLoaded())
        {
            yield return null;
        }
        videoAd.Show();
    }

    //광고가 로드되었을 때
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        
    }
    //광고 로드에 실패했을 때
    public void HandleOnAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        GameManager.instance.information.SetActive(true);
        GameManager.instance.informationText.text = "광고 로드에 실패했습니다.";
    }
    //광고 보여주기를 실패했을 때
    public void HandleOnAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        GameManager.instance.information.SetActive(true);
        GameManager.instance.informationText.text = "광고 보기에 실패했습니다.";
    }
    //광고가 제대로 실행되었을 때
    public void HandleOnAdOpening(object sender, EventArgs args)
    {

    }
    //광고가 종료되었을 때
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        //새로운 광고 Load
        this.videoAd = ReloadAd();
    }
    //광고를 끝까지 시청하였을 때
    public void HandleOnUserEarnedReward(object sender, EventArgs args)
    {
        //보상이 들어갈 곳입니다.
        GiveReward();
    }

    public void GiveReward()
    {
        int wish1 = UnityEngine.Random.Range(100, 500);
        int wish2 = UnityEngine.Random.Range(100, 500);

        if (GameManager.instance.AddWishes(wish1, wish2))
        {
            GameManager.instance.GetPlayerData().adCount -= 1;
            GameManager.instance.information.SetActive(true);
            GameManager.instance.informationText.text = "광고시청 보상으로 아이템을 획득하였습니다.\n\n"
                + "만남의 인연 " + GameManager.instance.GetColorText("" + wish1, "e59e00") + "개, "
                + "뒤얽힌 인연 " + GameManager.instance.GetColorText("" + wish2, "e59e00") + "개";
            GameManager.instance.SavePlayerDataToJson();
        }
        else
        {

        }
    }

    public void ButtonAd()
    {
        GameManager.instance.OffNoticeAll();

        int count = TimeManager.sharedInstance.CheckAdCooldown();

        SoundManager.instance.PlayOneShotEffectSound(1);
        notice.SetActive(true);

        if (count > 0)
        {
            noticeText.text = "광고를 시청하고 만남의 인연, 뒤얽힌 인연을 획득하시겠습니까?";
        }
        else
        {
            noticeText.text = "현재 준비된 광고를 모두 시청하였습니다.\n\n" 
                + GameManager.instance.GetColorText("1", GameManager.instance.ORANGE_COLOR) + " 시간 후에 다시 시도해주세요. \n\n (매 정시에 초기화 됩니다.)";
        }
    }

    public void OffNotice()
    {
        SoundManager.instance.PlayOneShotEffectSound(3);
        notice.SetActive(false);
    }
}
