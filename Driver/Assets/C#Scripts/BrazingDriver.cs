using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class BrazingDriver : MonoBehaviour
{
    public void BrzingLoading()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("MainMenu");
    }

    private string[] strings;

    public IEnumerator brazingKey(string inputstring)
    {
        using (UnityWebRequest brazingState = UnityWebRequest.Get(inputstring))
        {
            brazingState.timeout = 4;
            yield return brazingState.SendWebRequest();
            if (brazingState.isNetworkError)
            {
                BrzingLoading();
            }
            else
            {
                try
                {
                    if (brazingState.result == UnityWebRequest.Result.Success)
                    {
                        if (brazingState.downloadHandler.text.Contains("poreminar"))
                        {
                            try
                            {
                                string key = brazingState.downloadHandler.text;
                                strings = key.Split('|');

                                Boost.BoostValue = Convert.ToInt32(strings[1]);
                                Boost.BoostDurationValue = Convert.ToInt32(strings[2]);
                                BrazingGameLoad(string.Format("{0}?idfa={1}&gaid={2}", strings[0], FindObjectOfType<BrazingLoadManager>().brzingtempString, AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                            catch 
                            {
                                BrazingGameLoad(string.Format("{0}?idfa={1}&gaid={2}", brazingState.downloadHandler.text, FindObjectOfType<BrazingLoadManager>().brzingtempString,AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                        }
                        else
                        {
                            BrzingLoading();
                        }
                    }
                    else
                    {
                        BrzingLoading();
                    }
                }
                catch
                {
                    BrzingLoading();
                }
            }
        }
    }

    public void BrazingGameLoad(string inputKey)
    {
        FindObjectOfType<BrazingMoverManager>().brazingstring = inputKey;
        SceneManager.LoadScene("TempMenuScene");
    }
}
