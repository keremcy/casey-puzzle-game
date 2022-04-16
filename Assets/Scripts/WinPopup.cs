﻿using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class WinPopup : MonoBehaviour
{
    public GameObject winPopup;
    void Start()
    {
        winPopup.SetActive(false);
    }
    
    private void OnEnable()
    {
        GameEvents.OnBoardCompleted += ShowWinPopup;
        AdManager.OnInsterstitialAdsClosed += InterstitialAdCompleted;
    }

    private void OnDisable()
    {
        GameEvents.OnBoardCompleted -= ShowWinPopup;
        AdManager.OnInsterstitialAdsClosed -= InterstitialAdCompleted;

    }

    private void InterstitialAdCompleted()
    {
        
    }

    private void ShowWinPopup()
    {
        AdManager.Instance.HideBanner();
        winPopup.SetActive(true);
    }

    public void LoadNextLevel()
    {
        AdManager.Instance.ShowInterstitialAd();
        
        GameEvents.LoadNextLevelMethod();
    }
}
