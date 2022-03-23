using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    private BannerView bannerAd;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;

    public static AdManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    void Start()
    {
        MobileAds.Initialize(InitializationStatus => { });
    }

    public void DestroyAds()
    {
        this.bannerAd.Destroy();
        this.interstitialAd.Destroy();
        this.rewardedAd.Destroy();
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();

    }

    public void RequestBanner()
    {
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
        this.bannerAd = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        this.bannerAd.LoadAd(this.CreateAdRequest());
    }

    public void RequestBanner2()
    {
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";

        AdSize adsize = new AdSize(350,50);

        this.bannerAd = new BannerView(adUnitId, adsize, AdPosition.Bottom);

        this.bannerAd.LoadAd(this.CreateAdRequest());
    }

    public void RequestBanner3()
    {
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
        this.bannerAd = new BannerView(adUnitId, AdSize.Leaderboard, AdPosition.Bottom);

        this.bannerAd.LoadAd(this.CreateAdRequest());
    }

    public void RequestBanner4()
    {
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
        this.bannerAd = new BannerView(adUnitId, AdSize.MediumRectangle, AdPosition.Bottom);

        this.bannerAd.LoadAd(this.CreateAdRequest());
    }

    public void RequestBanner5()
    {
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
        this.bannerAd = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

        this.bannerAd.LoadAd(this.CreateAdRequest());
    }

    public void RequestBanner6()
    {
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
        this.bannerAd = new BannerView(adUnitId, AdSize.IABBanner, AdPosition.Bottom);

        this.bannerAd.LoadAd(this.CreateAdRequest());
    }

    public void RequestIntertial()
    {
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";

        if (this.interstitialAd != null)
            this.interstitialAd.Destroy();

        this.interstitialAd = new InterstitialAd(adUnitId);

        this.interstitialAd.LoadAd(this.CreateAdRequest());
    }

    public void ShowIntertial()
    {
        if (this.interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
        }

        else
        {
            print("interstitialAd not loaded yet.");
        }
    }



    public void RequestRewarded()
    {
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
        if (this.rewardedAd != null)
            this.rewardedAd.Destroy();

        this.rewardedAd = new RewardedAd(adUnitId);

        this.rewardedAd.LoadAd(this.CreateAdRequest());
    }

    public void ShowRewarded()
    {
        if (this.rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }

        else
        {
            print("rewardedAd not loaded yet.");
        }
    }
}
