using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    public void ShowRewardedAd()
    {
        /*if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions {
                resultCallback = HandleShowResult
            };
            Advertisement.Show("rewardedVideo", );
        }*/
    }

    void HandleShowResult(ShowResult result)
    {

    }
}
