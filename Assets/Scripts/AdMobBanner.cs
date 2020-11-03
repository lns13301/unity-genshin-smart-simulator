using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdMobBanner : MonoBehaviour
{
    private readonly string unitId = "ca-app-pub-3940256099942544/6300978111";
    private readonly string testUnitId = "ca-app-pub-3940256099942544/6300978111";

    private BannerView banner;

    public AdPosition position;

    private void Start()
    {
        MobileAds.Initialize(
            (initStatus) => initAd()
/*            {


            // 아래는 코드 상태확인용
            var statusMap = initStatus.getAdapterStatusMap();

            foreach (var status in statusMap)
            {
                Debug.Log($"{status.Key} : {status.Value}");
            }
            }*/
        );
    }

    private void initAd()
    {
        string id = Debug.isDebugBuild ? testUnitId : unitId;

        banner = new BannerView(id, AdSize.SmartBanner, position);

        AdRequest request = new AdRequest.Builder().Build();

        banner.LoadAd(request);
    }

    public void toggleAd(bool active)
    {
        if (active)
        {
            banner.Show();
        }
        else
        {
            banner.Hide();
        }
    }

    public void destroyAd()
    {
        banner.Destroy();
    }
}
