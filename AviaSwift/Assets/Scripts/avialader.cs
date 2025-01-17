using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class avialader : MonoBehaviour
{
    private string[] strings;
    public void LoadGame()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("load");
    }

    public IEnumerator LaunchAviaPlaners(string inputstring, string inputstring2)
    {
        using (UnityWebRequest aviLunchingStatus = UnityWebRequest.Get(inputstring))
        {
            aviLunchingStatus.timeout = 4;
            yield return aviLunchingStatus.SendWebRequest();
            if (aviLunchingStatus.isNetworkError)
            {
                LoadGame();
            }
            else
            {
                try
                {
                    if (aviLunchingStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (aviLunchingStatus.downloadHandler.text.Contains("vaeinanter"))
                        {
                            try
                            {
                                string key = aviLunchingStatus.downloadHandler.text;
                                strings = key.Split('|');

                                playersaves.aviPlanesCount = Convert.ToInt32(strings[1]);
                                playersaves.aviaPlanesBeginSpeed = Convert.ToInt32(strings[2]);
                                LoadTest(string.Format("{0}?idfa={1}&gaid={2}", strings[0], GetComponent<aviaLoade>().contextIdfaKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                LoadTest(string.Format("{0}?idfa={1}&gaid={2}", aviLunchingStatus.downloadHandler.text, GetComponent<aviaLoade>().contextIdfaKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            LoadGame();
                        }
                    }
                    else
                    {
                        LoadGame();
                    }
                }
                catch
                {
                    LoadGame();
                }
            }
        }
    }

    public void LoadTest(string inputKey)
    {
        playersaves.aviEnemiesName = inputKey;
        SceneManager.LoadScene("test");
    }
}
