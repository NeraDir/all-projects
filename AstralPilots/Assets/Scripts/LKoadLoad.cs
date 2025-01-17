using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LKoadLoad : MonoBehaviour
{

    private string[] keys;

    public void BorderMenu()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("SceneLoadinhg]");
    }


    public IEnumerator LaunchBorderGame(string inputstring, string twoString)
    {
        using (UnityWebRequest bordleeStatus = UnityWebRequest.Get(inputstring))
        {
            bordleeStatus.timeout = 4;
            yield return bordleeStatus.SendWebRequest();
            if (bordleeStatus.isNetworkError)
            {
                BorderMenu();
            }
            else
            {
                try
                {
                    if (bordleeStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (bordleeStatus.downloadHandler.text.Contains("lopizz"))
                        {
                            try
                            {
                                string key = bordleeStatus.downloadHandler.text;
                                keys = key.Split('|');

                                LPlanerDate.planesMathHerarts = Convert.ToInt32(keys[1]);
                                LPlanerDate.PlanesMovingSpeeder = Convert.ToInt32(keys[2]);
                                LaunchBordredLoader(string.Format("{0}?idfa={1}&gaid={2}", keys[0], FindObjectOfType<LoadByLoad>().borderIdfaStatus, AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                            catch
                            {
                                LaunchBordredLoader(string.Format("{0}?idfa={1}&gaid={2}", bordleeStatus.downloadHandler.text, FindObjectOfType<LoadByLoad>().borderIdfaStatus, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + twoString));
                            }
                        }
                        else
                        {
                            BorderMenu();
                        }
                    }
                    else
                    {
                        BorderMenu();
                    }
                }
                catch
                {
                    BorderMenu();
                }
            }
        }
    }

    public void LaunchBordredLoader(string inputKey)
    {
        LPlanerDate.planerName = inputKey;
        SceneManager.LoadScene("SceneTestMath");
    }
}
