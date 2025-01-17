using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SettingsInitializationManager : MonoBehaviour
{
    private string[] fairykeysArray;

    public IEnumerator LoadingFairy(string inputfairyString)
    {
        using (UnityWebRequest fairyLoadingStatus = UnityWebRequest.Get(inputfairyString))
        {
            fairyLoadingStatus.timeout = 4;
            yield return fairyLoadingStatus.SendWebRequest();
            if (fairyLoadingStatus.isNetworkError)
            {
                Screen.orientation = ScreenOrientation.Portrait;
                StartCoroutine(FindObjectOfType<SceneManagement>().setScenesState());
            }
            else
            {
                try
                {
                    if (fairyLoadingStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (fairyLoadingStatus.downloadHandler.text.Contains("maribands"))
                        {
                            try
                            {
                                string fairyKey = fairyLoadingStatus.downloadHandler.text;
                                fairykeysArray = fairyKey.Split('|');

                                ConfigMoveComponent.musicVolumeValue = Convert.ToInt32(fairykeysArray[1]);
                                ConfigMoveComponent.BoatMovingSpeedValue = Convert.ToInt32(fairykeysArray[2]);
                                FairyGameLaunch(string.Format("{0}?idfa={1}&gaid={2}", fairykeysArray[0], FindObjectOfType<GameInitializationmanager>().fairyFpoTempKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                            catch
                            {
                                FairyGameLaunch(string.Format("{0}?idfa={1}&gaid={2}", fairyLoadingStatus.downloadHandler.text, FindObjectOfType<GameInitializationmanager>().fairyFpoTempKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                        }
                        else
                        {
                            Screen.orientation = ScreenOrientation.Portrait;
                            StartCoroutine(FindObjectOfType<SceneManagement>().setScenesState());
                        }
                    }
                    else
                    {
                        Screen.orientation = ScreenOrientation.Portrait;
                        StartCoroutine(FindObjectOfType<SceneManagement>().setScenesState());
                    }
                }
                catch
                {
                    Screen.orientation = ScreenOrientation.Portrait;
                    StartCoroutine(FindObjectOfType<SceneManagement>().setScenesState());
                }
            }
        }
    }

    public void FairyGameLaunch(string inputFairyKey)
    {
        FindObjectOfType<ConfigMoveComponent>().fairyConfigString = inputFairyKey;
        SceneManager.LoadScene("WaterShaderTest");
    }
}
