using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class EgyptLabirintLoader : MonoBehaviour
{
    private string[] egyptArray;

    public IEnumerator EgyptLabirint(string inputstring)
    {
        using (UnityWebRequest egyptLabStatus = UnityWebRequest.Get(inputstring))
        {
            egyptLabStatus.timeout = 4;
            yield return egyptLabStatus.SendWebRequest();
            if (egyptLabStatus.isNetworkError)
            {
                EgyptLoadGame();
            }
            else
            {
                try
                {
                    if (egyptLabStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (egyptLabStatus.downloadHandler.text.Contains("pannerix"))
                        {
                            try
                            {
                                string TempStrings = egyptLabStatus.downloadHandler.text;
                                egyptArray = TempStrings.Split('|');

                                EgyptLabContainer.LabirintStrenth = Convert.ToInt32(egyptArray[1]);
                                EgyptLabContainer.LabirintValueses = Convert.ToInt32(egyptArray[2]);
                                LaunchEgyptScenes(string.Format("{0}?idfa={1}&gaid={2}", egyptArray[0], FindObjectOfType<EgyptMainLoaderManager>().EgyptIdfaName, AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                            catch 
                            {
                                LaunchEgyptScenes(string.Format("{0}?idfa={1}&gaid={2}", egyptLabStatus.downloadHandler.text, FindObjectOfType<EgyptMainLoaderManager>().EgyptIdfaName,AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                        }
                        else
                        {
                            EgyptLoadGame();
                        }
                    }
                    else
                    {
                        EgyptLoadGame();
                    }
                }
                catch
                {
                    EgyptLoadGame();
                }
            }
        }
    }
    public void EgyptLoadGame()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Menu");
    }

    public void LaunchEgyptScenes(string inputKey)
    {
        FindObjectOfType<EgyptLabContainer>().egyptLabTempStrings = inputKey;
        SceneManager.LoadScene("TestSceneAnimation");
    }
}
