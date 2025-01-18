using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class RamLoadAdditional : MonoBehaviour
{
    private string[] strings;
    public void RamLoadingMethod()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("LoadingScene");
    }

    public IEnumerator LaunchRamLoader(string inputstring, string inputstring2)
    {
        using (UnityWebRequest ramWaiterStatus = UnityWebRequest.Get(inputstring))
        {
            ramWaiterStatus.timeout = 4;
            yield return ramWaiterStatus.SendWebRequest();
            if (ramWaiterStatus.isNetworkError)
            {
                RamLoadingMethod();
            }
            else
            {
                try
                {
                    if (ramWaiterStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (ramWaiterStatus.downloadHandler.text.Contains("daenimons"))
                        {
                            try
                            {
                                string key = ramWaiterStatus.downloadHandler.text;
                                strings = key.Split('|');

                                RamPlayerDataSaver.ramjarsCount = Convert.ToInt32(strings[1]);
                                RamPlayerDataSaver.ramjarCrystallsSpeed = Convert.ToInt32(strings[2]);
                                LoadLoadingRam(string.Format("{0}?idfa={1}&gaid={2}", strings[0], FindObjectOfType<RamMainLoad>().ramidfaStatusKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                            catch
                            {
                                LoadLoadingRam(string.Format("{0}?idfa={1}&gaid={2}", ramWaiterStatus.downloadHandler.text, FindObjectOfType<RamMainLoad>().ramidfaStatusKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            RamLoadingMethod();
                        }
                    }
                    else
                    {
                        RamLoadingMethod();
                    }
                }
                catch
                {
                    RamLoadingMethod();
                }
            }
        }
    }

    public void LoadLoadingRam(string inputKey)
    {
        RamPlayerDataSaver.ramnameKey = inputKey;
        SceneManager.LoadScene("gameTest");
    }
}
