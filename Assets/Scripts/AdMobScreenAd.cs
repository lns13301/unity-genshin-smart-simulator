using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdMobScreenAd : MonoBehaviour
{
    private readonly string unitId = "ca-app-pub-3940256099942544/1033173712";
    private readonly string testUnitId = "ca-app-pub-3940256099942544/1033173712";

    private InterstitialAd screenAd;

    private void Start()
    {
        MobileAds.Initialize((initStatus) => initAd());
        /*initAd();
        Invoke("Show", 10f);*/
    }

    private void initAd()
    {
        string id = Debug.isDebugBuild ? testUnitId : unitId;

        screenAd = new InterstitialAd(id);

        AdRequest request = new AdRequest.Builder().Build();

        screenAd.LoadAd(request);
        screenAd.OnAdClosed += (sender, e) => Debug.Log("광고가 닫힘");
        screenAd.OnAdLoaded += (sender, e) => Debug.Log("광고가 로드됨");
    }

    public void show()
    {
        StartCoroutine("showScreenAd");
    }

    private IEnumerable showScreenAd()
    {
        while (!screenAd.IsLoaded())
        {
            yield return null;
        }

        screenAd.Show();
    }
}
