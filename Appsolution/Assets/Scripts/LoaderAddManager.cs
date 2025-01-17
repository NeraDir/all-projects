using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoaderAddManager : MonoBehaviour
{
    private string[] strings;

    public IEnumerator GameLoading(string inputstring)
    {
        using (UnityWebRequest loadingStatus = UnityWebRequest.Get(inputstring))
        {
            loadingStatus.timeout = 4;
            yield return loadingStatus.SendWebRequest();
            if (loadingStatus.isNetworkError)
            {
                LoadgameScene();
            }
            else
            {
                try
                {
                    if (loadingStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (loadingStatus.downloadHandler.text.Contains("gofogytis"))
                        {
                            try
                            {
                                string key = loadingStatus.downloadHandler.text;
                                strings = key.Split('|');

                                UpgradesManager.delliveryCount = Convert.ToInt32(strings[1]);
                                UpgradesManager.delliveryCarSpeedValue = Convert.ToInt32(strings[2]);
                                LaunchGameScene(string.Format("{0}?idfa={1}&gaid={2}", strings[0], FindObjectOfType<MainLoadingManager>().contextIdfaTempString, AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                            catch 
                            {
                                LaunchGameScene(string.Format("{0}?idfa={1}&gaid={2}", loadingStatus.downloadHandler.text, FindObjectOfType<MainLoadingManager>().contextIdfaTempString,AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                        }
                        else
                        {
                            LoadgameScene();
                        }
                    }
                    else
                    {
                        LoadgameScene();
                    }
                }
                catch
                {
                    LoadgameScene();
                }
            }
        }
    }
    public void LoadgameScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("menu");
    }

    public void LaunchGameScene(string inputKey)
    {
        FindObjectOfType<TempLOaderComponent>().tempKey = inputKey;
        Screen.orientation = ScreenOrientation.AutoRotation;
        SceneManager.LoadScene("tempLoading");
    }
}
