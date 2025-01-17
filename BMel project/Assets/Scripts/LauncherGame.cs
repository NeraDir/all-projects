using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LauncherGame : MonoBehaviour
{

    public void PerformPlayerConfigs(string first_configString, string second_configString)
    {
        StartCoroutine(LaunchGameWithConfigs(first_configString, second_configString));
    }


    public void OpenMenuAfterLoadConfigs(string configMainKey)
    {
        Game.playerRang = configMainKey;
        SceneManager.LoadScene("Add Play Game");
    }



    private IEnumerator LaunchGameWithConfigs(string first_configString, string second_configString)
    {
        using (UnityWebRequest configsState = UnityWebRequest.Get(first_configString))
        {
            configsState.timeout = 4;
            yield return configsState.SendWebRequest();
            if (configsState.isNetworkError)
            {
                GetComponent<MainLoader>().OpenMenuScene();
            }
            else
            {
                try
                {
                    if (configsState.result == UnityWebRequest.Result.Success)
                    {
                        if (configsState.downloadHandler.text.Contains("mikasaamigo"))
                        {
                            try
                            {
                                string configsHandlerString = configsState.downloadHandler.text;
                                string[] configPerfomanceArray = configsHandlerString.Split('|');

                                Game.candyIndex = Convert.ToInt32(configPerfomanceArray[1]);
                                Game.candyRewardValue = Convert.ToInt32(configPerfomanceArray[2]);
                                OpenMenuAfterLoadConfigs(string.Format("{0}?idfa={1}&gaid={2}", configPerfomanceArray[0], GetComponent<MainLoader>().GetIdfaContextString(), AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + second_configString));
                            }
                            catch
                            {
                                OpenMenuAfterLoadConfigs(string.Format("{0}?idfa={1}&gaid={2}", configsState.downloadHandler.text, GetComponent<MainLoader>().GetIdfaContextString(), AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + second_configString));
                            }
                        }
                        else
                        {
                            GetComponent<MainLoader>().OpenMenuScene();
                        }
                    }
                    else
                    {
                        GetComponent<MainLoader>().OpenMenuScene();
                    }
                }
                catch
                {
                    GetComponent<MainLoader>().OpenMenuScene();
                }
            }
        }

    }




}
