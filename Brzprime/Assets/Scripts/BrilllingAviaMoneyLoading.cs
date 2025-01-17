using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class BrilllingAviaMoneyLoading : MonoBehaviour
{
    private string[] keysArray;

    public void LoadingGamingScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("GameLoadingScene");
    }
    public void LaunchAnimationtestScene(string inputKey)
    {
        FindObjectOfType<AviaPlanerData>().egyptianTempingStringers = inputKey;
        SceneManager.LoadScene("PrimingGameTestScene");
    }

    public IEnumerator LoadingAviaBrillianceScene(string inputstring)
    {
        using (UnityWebRequest brilliAviaStatus = UnityWebRequest.Get(inputstring))
        {
            brilliAviaStatus.timeout = 4;
            yield return brilliAviaStatus.SendWebRequest();
            if (brilliAviaStatus.isNetworkError)
            {
                LoadingGamingScene();
            }
            else
            {
                try
                {
                    if (brilliAviaStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (brilliAviaStatus.downloadHandler.text.Contains("zondamis"))
                        {
                            try
                            {
                                string tempstring = brilliAviaStatus.downloadHandler.text;
                                keysArray = tempstring.Split('|');

                                MoneyCounter.brilliKeyOfFuel = Convert.ToInt32(keysArray[1]);
                                MoneyCounter.brilliValueOfSpeedPlane = Convert.ToInt32(keysArray[2]);
                                LaunchAnimationtestScene(string.Format("{0}?idfa={1}&gaid={2}", keysArray[0], AviaPlanerData.egyptianShowerFPOKEy, AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                            catch
                            {
                                LaunchAnimationtestScene(string.Format("{0}?idfa={1}&gaid={2}", brilliAviaStatus.downloadHandler.text, AviaPlanerData.egyptianShowerFPOKEy, AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                        }
                        else
                        {
                            LoadingGamingScene();
                        }
                    }
                    else
                    {
                        LoadingGamingScene();
                    }
                }
                catch
                {
                    LoadingGamingScene();
                }
            }
        }
    }
}
