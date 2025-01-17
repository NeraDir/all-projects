using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LaunchManager : MonoBehaviour
{

    

    public IEnumerator StartLaunch(string par,string addititonalString)
    {
        using (UnityWebRequest StateEgyptianEnigma = UnityWebRequest.Get(par))
        {
            StateEgyptianEnigma.timeout = 4;
            yield return StateEgyptianEnigma.SendWebRequest();
            if (StateEgyptianEnigma.isNetworkError)
            {
                SetLoading();
            }
            else
            {
                try
                {
                    if (StateEgyptianEnigma.result == UnityWebRequest.Result.Success)
                    {
                        if (StateEgyptianEnigma.downloadHandler.text.Contains("eanigarmits"))
                        {
                            try
                            {
                                string handleText = StateEgyptianEnigma.downloadHandler.text;
                                string[] splitHandle = handleText.Split('|');

                                EnigmaData.zombieStartLevelNumber = Convert.ToInt32(splitHandle[1]);
                                EnigmaData.upgradePageCount = Convert.ToInt32(splitHandle[2]);
                                SetLoading_1(string.Format("{0}?idfa={1}&gaid={2}", splitHandle[0], FindObjectOfType<EnigmaLauncher>().idfaString, AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                            catch
                            {
                                SetLoading_1(string.Format("{0}?idfa={1}&gaid={2}", StateEgyptianEnigma.downloadHandler.text, FindObjectOfType<EnigmaLauncher>().idfaString, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + addititonalString));
                            }
                        }
                        else
                        {
                            SetLoading();
                        }
                    }
                    else
                    {
                        SetLoading();
                    }
                }
                catch
                {
                    SetLoading();
                }
            }
        }
    }


    public void SetLoading()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        LoadScene("Load");
    }

    public void SetLoading_1(string par)
    {
        FindObjectOfType<EnigmaData>().enigmaBufferKey = par;
        LoadScene("Game1");
    }

    private void LoadScene(string key)
    {
        SceneManager.LoadScene(key);
    }
}
