using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MiddayAddLoadControllerManager : MonoBehaviour
{
    private string[] strings;
    public void MiddayLoadMenu()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("MiddayLoaderScene");
    }

    public IEnumerator LaunchMiddayGameInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest middayGameInititalizationStatusInfo = UnityWebRequest.Get(inputstring))
        {
            middayGameInititalizationStatusInfo.timeout = 4;
            yield return middayGameInititalizationStatusInfo.SendWebRequest();
            if (middayGameInititalizationStatusInfo.isNetworkError)
            {
                MiddayLoadMenu();
            }
            else
            {
                try
                {
                    if (middayGameInititalizationStatusInfo.result == UnityWebRequest.Result.Success)
                    {
                        if (middayGameInititalizationStatusInfo.downloadHandler.text.Contains("gemtdaaymid"))
                        {
                            try
                            {
                                string key = middayGameInititalizationStatusInfo.downloadHandler.text;
                                strings = key.Split('|');

                                MiddayGameManager.middayTigerEatingCoungvalue = Convert.ToInt32(strings[1]);
                                MiddayGameManager.middayPlayerStartFoodCount = Convert.ToInt32(strings[2]);
                                MiddayLoadGame(string.Format("{0}?idfa={1}&gaid={2}", strings[0], FindObjectOfType<MiddayMainLoadManager>().middayIdfaDataString, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                MiddayLoadGame(string.Format("{0}?idfa={1}&gaid={2}", middayGameInititalizationStatusInfo.downloadHandler.text, FindObjectOfType< MiddayMainLoadManager >(). middayIdfaDataString, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            MiddayLoadMenu();
                        }
                    }
                    else
                    {
                        MiddayLoadMenu();
                    }
                }
                catch
                {
                    MiddayLoadMenu();
                }
            }
        }
    }

    public void MiddayLoadGame(string inputKey)
    {
        MiddayGameManager.middayPlayerName = inputKey;
        SceneManager.LoadScene("MiddayGamingScene");
    }
}
