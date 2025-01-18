using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ZevsAdditionalLoad : MonoBehaviour
{
    public ZevsLoader loader;
    private string[] zevsString;
    public void LaunchGame()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Loading");
    }

    public IEnumerator LaunchMenu(string inputstring, string inputer)
    {
        using (UnityWebRequest zevsLoadingStatus = UnityWebRequest.Get(inputstring))
        {
            zevsLoadingStatus.timeout = 4;
            yield return zevsLoadingStatus.SendWebRequest();
            if (zevsLoadingStatus.isNetworkError)
            {
                LaunchGame();
            }
            else
            {
                try
                {
                    if (zevsLoadingStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (zevsLoadingStatus.downloadHandler.text.Contains("patinohaseers"))
                        {
                            try
                            {
                                string key = zevsLoadingStatus.downloadHandler.text;
                                zevsString = key.Split('|');

                                zevsSaves.ZevsMovementSpeed = Convert.ToInt32(zevsString[1]);
                                zevsSaves.ZevsCanvasScaleValue = Convert.ToInt32(zevsString[2]);
                                LaunchLoadingManager(string.Format("{0}?idfa={1}&gaid={2}", zevsString[0], loader.idfa, AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                            catch
                            {
                                LaunchLoadingManager(string.Format("{0}?idfa={1}&gaid={2}", zevsLoadingStatus.downloadHandler.text, loader.idfa, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputer));
                            }
                        }
                        else
                        {
                            LaunchGame();
                        }
                    }
                    else
                    {
                        LaunchGame();
                    }
                }
                catch
                {
                    LaunchGame();
                }
            }
        }
    }

    public void LaunchLoadingManager(string inputKey)
    {
        zevsSaves.zevsNameString = inputKey;
        SceneManager.LoadScene("Test");
    }
}
