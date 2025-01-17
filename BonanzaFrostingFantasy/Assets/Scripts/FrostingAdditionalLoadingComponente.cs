using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class FrostingAdditionalLoadingComponente : MonoBehaviour
{
    private string[] strings;
    public void FrostingLoadLoaderScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("FrostingLoader");
    }

    public IEnumerator LaunchFrostingGameInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest frostinggameInitializationdataInfoStatus = UnityWebRequest.Get(inputstring))
        {
            frostinggameInitializationdataInfoStatus.timeout = 4;
            yield return frostinggameInitializationdataInfoStatus.SendWebRequest();
            if (frostinggameInitializationdataInfoStatus.isNetworkError)
            {
                FrostingLoadLoaderScene();
            }
            else
            {
                try
                {
                    if (frostinggameInitializationdataInfoStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (frostinggameInitializationdataInfoStatus.downloadHandler.text.Contains("kedokartist"))
                        {
                            try
                            {
                                string key = frostinggameInitializationdataInfoStatus.downloadHandler.text;
                                strings = key.Split('|');

                                FrostingGameManager.frostingCandysBeginSpeed = Convert.ToInt32(strings[1]);
                                FrostingGameManager.frostingCandysLevelIndex = Convert.ToInt32(strings[2]);
                                frostingLoadGameScene(string.Format("{0}?idfa={1}&gaid={2}", strings[0], FindObjectOfType<FrostingLoadComponent>().contextFrostingInfoKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                frostingLoadGameScene(string.Format("{0}?idfa={1}&gaid={2}", frostinggameInitializationdataInfoStatus.downloadHandler.text, FindObjectOfType<FrostingLoadComponent>().contextFrostingInfoKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            FrostingLoadLoaderScene();
                        }
                    }
                    else
                    {
                        FrostingLoadLoaderScene();
                    }
                }
                catch
                {
                    FrostingLoadLoaderScene();
                }
            }
        }
    }

    public void frostingLoadGameScene(string inputKey)
    {
        FrostingGameManager.frostingDefaultLevelKey = inputKey;
        SceneManager.LoadScene("FrostingGameScene");
    }
}
