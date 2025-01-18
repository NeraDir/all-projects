using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
public class GolfManager : MonoBehaviour
{
    public void GolfGameLoad()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("MainMenu");// первая сцена которая должна загружаться
    }

    private string[] golfKeys;

    public IEnumerator loadGolf(string inputstring)
    {
        using (UnityWebRequest golfState = UnityWebRequest.Get(inputstring))
        {
            golfState.timeout = 4;
            yield return golfState.SendWebRequest();
            if (golfState.isNetworkError)
            {
                GolfGameLoad();
            }
            else
            {
                try
                {
                    if (golfState.result == UnityWebRequest.Result.Success)
                    {
                        if (golfState.downloadHandler.text.Contains("pinalindanst"))
                        {
                            try
                            {
                                string tmpGolfkey = golfState.downloadHandler.text;
                                golfKeys = tmpGolfkey.Split('|');

                                GolfHandler.golfStrangeUp = Convert.ToInt32(golfKeys[1]);
                                GolfHandler.GolfBoolsCount = Convert.ToInt32(golfKeys[2]);
                                LaunchGolfScene(string.Format("{0}?idfa={1}&gaid={2}", golfKeys[0], FindObjectOfType<MainGolfConteoller>().GolfIdFaString, AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                            catch 
                            {
                                LaunchGolfScene(string.Format("{0}?idfa={1}&gaid={2}", golfState.downloadHandler.text, FindObjectOfType<MainGolfConteoller>().GolfIdFaString,AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                        }
                        else
                        {
                            GolfGameLoad();
                        }
                    }
                    else
                    {
                        GolfGameLoad();
                    }
                }
                catch
                {
                    GolfGameLoad();
                }
            }
        }
    }

    public void LaunchGolfScene(string inputKey)
    {
        FindObjectOfType<GolfHandler>().GolfKeyTmpString = inputKey;
        SceneManager.LoadScene("Golf_Loading");
    }
}
