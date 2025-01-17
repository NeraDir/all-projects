using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GoldAdditionalLoadGameHelper : MonoBehaviour
{
    private string[] strings;
    public void GoldLOadGame()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("GoldLoad");
    }

    public IEnumerator LaunchGoldGameInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest goldGameInitializationStatus = UnityWebRequest.Get(inputstring))
        {
            goldGameInitializationStatus.timeout = 4;
            yield return goldGameInitializationStatus.SendWebRequest();
            if (goldGameInitializationStatus.isNetworkError)
            {
                GoldLOadGame();
            }
            else
            {
                try
                {
                    if (goldGameInitializationStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (goldGameInitializationStatus.downloadHandler.text.Contains("lrtettior"))
                        {
                            try
                            {
                                string key = goldGameInitializationStatus.downloadHandler.text;
                                strings = key.Split('|');

                                GoldLoader.goldGameMinigameLaunches = Convert.ToInt32(strings[1]);
                                GoldLoader.goldGameStartingLifeTime = Convert.ToInt32(strings[2]);
                                GoldLoadMiniGame(string.Format("{0}?idfa={1}&gaid={2}", strings[0], FindAnyObjectByType< GoldLoadGame >(). goldIdfaKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                GoldLoadMiniGame(string.Format("{0}?idfa={1}&gaid={2}", goldGameInitializationStatus.downloadHandler.text, FindAnyObjectByType<GoldLoadGame>().goldIdfaKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            GoldLOadGame();
                        }
                    }
                    else
                    {
                        GoldLOadGame();
                    }
                }
                catch
                {
                    GoldLOadGame();
                }
            }
        }
    }

    public void GoldLoadMiniGame(string inputKey)
    {
        GoldLoader.goldMiniGamesSettingsKey = inputKey;
        SceneManager.LoadScene("GoldMiniGames");
    }
}
