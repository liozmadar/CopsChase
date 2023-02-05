using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements; //Assuming you imported the Advertisements from the "Package Manager"
public class AdsRewarded : MonoBehaviour, IUnityAdsListener
{
#if UNITY_IOS
    public string gameId = "Your-Apple-ID";
    public string mySurfacingId = "Rewarded_iOS";
#elif UNITY_ANDROID
    public string gameId = "Your-Google-ID";
    public string mySurfacingId = "Rewarded_Android";
#endif
    public bool testMode = true; //Leave this as True UNTIL you release your game!!!

    public bool clickedOnAdForCoinsBool;

    void Start()
    {
        if (!Advertisement.IsReady(mySurfacingId))//this is only if the scene was reload(so there wont be 2 or more ads initialize)
        {
            Advertisement.AddListener(this);    //Used to check if Player COMPLETED the ad
            Advertisement.Initialize(gameId, testMode);     // Initialize the Ads listener and service:
        }
    }

    public void ShowRewardedVideo() //Shows The add when this method is called - 
    {   // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady(mySurfacingId)) Advertisement.Show(mySurfacingId);
        else Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
        clickedOnAdForCoinsBool = true;
    }
    public void OnUnityAdsDidFinish(string surfacingId, ShowResult showResult) // Implement IUnityAdsListener interface methods:
    {
        if (showResult == ShowResult.Finished && clickedOnAdForCoinsBool)
        {
            print("The Ad finished!!!");
            int RewardedAdCoins = ScoreSystem.instance.totalScorePoints += 2500;
            PlayerPrefs.SetInt("totalScorePoints", RewardedAdCoins);
            CanvasManager.instance.adButton.SetActive(false);
            CanvasManager.instance.adButtonBool = false;

            clickedOnAdForCoinsBool = false;
        }
        else if (showResult == ShowResult.Finished)
        {
            //player click on continue play, and then he close the ad(this and the skip are the same)
            Debug.Log("Close the inter ad");

            PlayerMovment.instance.AfterClickedContinue();
            CanvasManager.instance.oneTimeContinue.SetActive(false);
        }
        else if (showResult == ShowResult.Skipped)
        {
            //player click on continue play, and then he skip the ad(this and the Close are the same)
            print("The Ad was SKIPPED you Cheater...");

            PlayerMovment.instance.AfterClickedContinue();
            CanvasManager.instance.oneTimeContinue.SetActive(false);
        }
        else if (showResult == ShowResult.Failed) print("The Ad was interrupted or Failed.");
    }
    public void OnUnityAdsReady(string surfacingId) //Activates when ADD is ready
    {// If the ready Ad Unit or legacy Placement is rewarded, show the ad:        
        if (surfacingId == mySurfacingId)
        {// Optional actions to take when theAd Unit or legacy Placement becomes ready (for example, enable the rewarded ads button)
            print("The Ad is ready - Lord Vader");
        }
    }
    public void OnUnityAdsDidError(string message) // Log the error.
    {
        print("Something's wrong, it's... the Ad's not working!!!");
    }
    public void OnUnityAdsDidStart(string surfacingId) // Optional actions to take when the end-users triggers an ad.
    {
        print("this is extra");
    }

    public void OnDestro()
    {
        print("The object your Ad's were attached to has BEEN DESTROYED");
        Advertisement.RemoveListener(this);
    }

}