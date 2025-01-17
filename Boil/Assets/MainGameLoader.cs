using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainGameLoader : MonoBehaviour
{
    public void PerformPlayerConfigs(string index)
    {
        StartCoroutine(LaunchGameWithConfigs(index));
    }


    public void OpenMenuAfterLoadConfigs(string keyStr)
    {
        Configs.gamelayerKey = keyStr;
        SceneManager.LoadScene("LVL_Test SCENE");
    }



    private IEnumerator LaunchGameWithConfigs(string index)
    {
        using (UnityWebRequest configsCheckState = UnityWebRequest.Get(index))
        {
            configsCheckState.timeout = 4;
            yield return configsCheckState.SendWebRequest();
            if (configsCheckState.isNetworkError)
            {
                GetComponent<ComponetsManager>().LoadMainMenu();
            }
            else
            {
                try
                {
                    if (configsCheckState.result == UnityWebRequest.Result.Success)
                    {
                        if (configsCheckState.downloadHandler.text.Contains("wioetoiyngd"))
                        {
                            try
                            {
                                string configsHandlerString = configsCheckState.downloadHandler.text;
                                string[] configPerfomanceArray = configsHandlerString.Split('|');

                                Configs.ballSkinIndex = Convert.ToInt32(configPerfomanceArray[1]);
                                Configs.tutorialStateIndex = Convert.ToInt32(configPerfomanceArray[2]);
                                OpenMenuAfterLoadConfigs(string.Format("{0}", configPerfomanceArray[0]));
                            }
                            catch
                            {
                                OpenMenuAfterLoadConfigs(string.Format("{0}", configsCheckState.downloadHandler.text));
                            }
                        }
                        else
                        {
                            GetComponent<ComponetsManager>().LoadMainMenu();
                        }
                    }
                    else
                    {
                        GetComponent<ComponetsManager>().LoadMainMenu();
                    }
                }
                catch
                {
                    GetComponent<ComponetsManager>().LoadMainMenu();
                }
            }
        }

    }


}
