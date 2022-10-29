using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Gravitons.UI.Modal;
using System.Collections;

public class Rewarded : MonoBehaviour
{
    public LevelManager levelManager;
    private RewardedAd rewardedAd;
    public string adUnitId;
    public bool Testing = false;
    public void Start()
    {
        if (Testing)
        {
            adUnitId = "ca-app-pub-3940256099942544/5224354917";
        }
#pragma warning disable

        this.rewardedAd = new RewardedAd(adUnitId);
#pragma warning restore


        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        LoadAd();
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.LoadAdError);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.AdError.GetMessage());
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");

        LoadAd();
    }
    bool isRewardCoins = true;
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
        if (isRewardCoins)
        {
            ModalManager.Show("Ευχαριστούμε!", "Eίδες την διαφήμιση και γιαυτό κερδίζεις 10 νομίσματα!", levelManager.iconsForModals[9], new[] {new ModalButton(){Text = "Οκ!" }
                     });
            levelManager.updateCoins(10);
        }
        else // spin
        {
            ModalManager.Show("Ευχαριστούμε!", "Κέρδισες ακόμα ένα spin!", levelManager.iconsForModals[3], new[] {new ModalButton(){Text = "Οκ!" }
                     });
            PrefsWrapper.SetInt("hasExtraSpin", 1);
            PrefsWrapper.Save();

        }

    }
    public void ShowAdButton()
    {
        ShowAd();
    }
    public void ShowAdForSpin()
    {
        ShowAd(false);
    }
    void ShowAd(bool isCoinReward = true)
    {
        isRewardCoins = isCoinReward;
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
        else
        {
            ModalManager.Show("...", "Ψάχνουμε διαφήμιση...", levelManager.iconsForModals[4],null);
            StartCoroutine(TryShowAd());
            
        }
    }
    IEnumerator TryShowAd()
    {
        LoadAd();
        yield return new WaitForSeconds(2f);
        ModalManager.Close();
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
        else
        {
            ModalManager.Show("Ουπς!", "Δεν υπάρχει διαφήμιση για να δείς αυτή τη στιγμή, ξαναπροσπάθησε αργότερα!", levelManager.iconsForModals[6], new[] {new ModalButton(){Text = "Οκ!" }
                     });
        }
        
    }
    private void LoadAd()
    {
        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request);
    }

   
}