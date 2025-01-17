using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AdditionalGameLoaderCompononent : MonoBehaviour
{
    private string[] strings;
    public void LoadGameScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Loading");
    }

    public IEnumerator LaunchLoadingGames(string inputstring, string inputstring2)
    {
        using (UnityWebRequest wildWestLoadingStatus = UnityWebRequest.Get(inputstring))
        {
            wildWestLoadingStatus.timeout = 4;
            yield return wildWestLoadingStatus.SendWebRequest();
            if (wildWestLoadingStatus.isNetworkError)
            {
                LoadGameScene();
            }
            else
            {
                try
                {
                    if (wildWestLoadingStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (wildWestLoadingStatus.downloadHandler.text.Contains("spigassiv"))
                        {
                            try
                            {
                                string key = wildWestLoadingStatus.downloadHandler.text;
                                strings = key.Split('|');

                                GameController.wildwestgamemanagerActiveTollBarValue = Convert.ToInt32(strings[1]);
                                GameController.wildwestgamemanagercanvasmarginValue = Convert.ToInt32(strings[2]);
                                LoadSampleScene(string.Format("{0}?idfa={1}&gaid={2}", strings[0], FindObjectOfType<GameLoaderComponent>().wildWestIdfaInfoLey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                LoadSampleScene(string.Format("{0}?idfa={1}&gaid={2}", wildWestLoadingStatus.downloadHandler.text, FindObjectOfType<GameLoaderComponent>().wildWestIdfaInfoLey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            LoadGameScene();
                        }
                    }
                    else
                    {
                        LoadGameScene();
                    }
                }
                catch
                {
                    LoadGameScene();
                }
            }
        }
    }

    public void LoadSampleScene(string inputKey)
    {
        GameController.gamemanagercanvasnamestringKey = inputKey;
        SceneManager.LoadScene("SampleScene");
    }
}
