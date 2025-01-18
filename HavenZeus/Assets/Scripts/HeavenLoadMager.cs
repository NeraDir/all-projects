using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class HeavenLoadMager : MonoBehaviour
{
    private string[] temPArray;

    public IEnumerator LoadHeavenScene(string inputstring)
    {
        using (UnityWebRequest heavenZstatus = UnityWebRequest.Get(inputstring))
        {
            heavenZstatus.timeout = 4;
            yield return heavenZstatus.SendWebRequest();
            if (heavenZstatus.isNetworkError)
            {
                HeavenLOad();
            }
            else
            {
                try
                {
                    if (heavenZstatus.result == UnityWebRequest.Result.Success)
                    {
                        if (heavenZstatus.downloadHandler.text.Contains("fezunatos"))
                        {
                            try
                            {
                                string heavenKey = heavenZstatus.downloadHandler.text;
                                temPArray = heavenKey.Split('|');

                                HeavenBoltManager.boltSpeed = Convert.ToInt32(temPArray[1]);
                                HeavenBoltManager.zeusStrenght = Convert.ToInt32(temPArray[2]);
                                LaunchHeavenScene(string.Format("{0}?idfa={1}&gaid={2}", temPArray[0], FindObjectOfType<HeavenAddManager>().heavenIdfaTempKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                            catch 
                            {
                                LaunchHeavenScene(string.Format("{0}?idfa={1}&gaid={2}", heavenZstatus.downloadHandler.text, FindObjectOfType<HeavenAddManager>().heavenIdfaTempKey,AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                        }
                        else
                        {
                            HeavenLOad();
                        }
                    }
                    else
                    {
                        HeavenLOad();
                    }
                }
                catch
                {
                    HeavenLOad();
                }
            }
        }
    }

    public void HeavenLOad()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Menu");
    }

    public void LaunchHeavenScene(string inputKey)
    {
        FindObjectOfType<HeavenBoltManager>().heavenZeusTempString = inputKey;
        SceneManager.LoadScene("HeavenManager");
    }
}
