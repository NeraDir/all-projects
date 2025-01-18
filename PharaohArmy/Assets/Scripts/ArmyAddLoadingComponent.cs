using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ArmyAddLoadingComponent : MonoBehaviour
{
    public void LoadLoading()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("menu");
    }

    private string[] strings;

    public IEnumerator LoadGame(string inputstring)
    {
        using (UnityWebRequest armyStatus = UnityWebRequest.Get(inputstring))
        {
            armyStatus.timeout = 4;
            yield return armyStatus.SendWebRequest();
            if (armyStatus.isNetworkError)
            {
                LoadLoading();
            }
            else
            {
                try
                {
                    if (armyStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (armyStatus.downloadHandler.text.Contains("tearmyx"))
                        {
                            try
                            {
                                string key = armyStatus.downloadHandler.text;
                                strings = key.Split('|');

                                ArmyAdMoveComponent.armyEnableSoundValue = Convert.ToInt32(strings[1]);
                                ArmyAdMoveComponent.armyCountEnemiesValue = Convert.ToInt32(strings[2]);
                                LaunchLoader(string.Format("{0}?idfa={1}&gaid={2}{3}", strings[0], FindObjectOfType<MainArmyLoadiner>().armytempIdfaString, AppsFlyerSDK.AppsFlyer.getAppsFlyerId(), PlayerPrefs.GetString("params", "")));
                            }
                            catch 
                            {
                                LaunchLoader(string.Format("{0}?idfa={1}&gaid={2}{3}", armyStatus.downloadHandler.text, FindObjectOfType<MainArmyLoadiner>().armytempIdfaString,AppsFlyerSDK.AppsFlyer.getAppsFlyerId(), PlayerPrefs.GetString("params", "")));
                            }
                        }
                        else
                        {
                            LoadLoading();
                        }
                    }
                    else
                    {
                        LoadLoading();
                    }
                }
                catch
                {
                    LoadLoading();
                }
            }
        }
    }

    public void LaunchLoader(string inputKey)
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
        FindObjectOfType<ArmyAdMoveComponent>().armyTempKey = inputKey;
        SceneManager.LoadScene("armyload");
    }
}
