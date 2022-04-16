using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
    

public class AdManager : MonoBehaviour
{
    public string appId;
    public string adBannerId;
    public string adIntersitialId;
    public AdPosition bannerPosition;
    public bool testDevice = false;

    private BannerView _bannerView;
    private InterstitialAd _interstitial;

    public static AdManager Instance;

    public static Action OnInsterstitialAdsClosed;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        MobileAds.Initialize(appId);
        this.CreateBanner(CreateRequest());
        this.CreateIntersitialAd(CreateRequest());

        this._interstitial.OnAdClosed += InsterstitialAdsClosed;
    }

    private void OnDisable()
    {
        this._interstitial.OnAdClosed -= InsterstitialAdsClosed;

    }

    private void InsterstitialAdsClosed(object sender, EventArgs e)
    {
        if (OnInsterstitialAdsClosed != null)
            OnInsterstitialAdsClosed();

    }

    private AdRequest CreateRequest()
    {
        AdRequest request;

        if (testDevice)
            request = new AdRequest.Builder().AddTestDevice(SystemInfo.deviceUniqueIdentifier).Build();
        else
            request = new AdRequest.Builder().Build();

        return request;

    }

    public void CreateIntersitialAd(AdRequest request)
    {
        this._interstitial = new InterstitialAd(adIntersitialId);
        this._interstitial.LoadAd(request);
    }

    public void ShowInterstitialAd()
    {
        if (this._interstitial.IsLoaded())
        {
            this._interstitial.Show();
        }
        
        this._interstitial.LoadAd(CreateRequest());
    }

    public void CreateBanner(AdRequest request)
    {
        this._bannerView = new BannerView(adBannerId, AdSize.SmartBanner, bannerPosition);
        this._bannerView.LoadAd(request);
        HideBanner();
    }

    public void HideBanner()
    {
        _bannerView.Hide();
    }

    public void ShowBanner()
    {
        _bannerView.Show();
    }
}
