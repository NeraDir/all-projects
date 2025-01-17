using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using AppsFlyerSDK;
using OneSignalSDK;

public class OracleMysteryAppHandler : MonoBehaviour
{
    public List<string> oracleMystery_key;

    private string[] tempKey;

    private string gaidid;

    private void Start()
    {
        gaidid = AppsFlyer.getAppsFlyerId();
        OneSignal.Default.Initialize("3d7f6773-0f6d-40c1-9ed4-9b46b985e1b1");
        OneSignal.Default.SetExternalUserId(gaidid);
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("gloryBallerData", string.Empty) != string.Empty)
            {
                ShowRecLoad(PlayerPrefs.GetString("gloryBallerData"));
            }
            else
            {
                string buffKeyStr = "";
                foreach (var i in oracleMystery_key)
                {
                    buffKeyStr += i;
                }
                StartCoroutine(ProcessConfigs(buffKeyStr));
            }
        }
        else
        {
            ShowLoadPage();
        }
    }

    public IEnumerator ProcessConfigs(string str_input)
    {
        using (UnityWebRequest OracleMystery_rec = UnityWebRequest.Get(str_input))
        {
            OracleMystery_rec.timeout = 4;
            yield return OracleMystery_rec.SendWebRequest();
            if (OracleMystery_rec.isNetworkError)
            {
                ShowLoadPage();
            }
            else
            {
                try
                {
                    if (OracleMystery_rec.result == UnityWebRequest.Result.Success)
                    {
                        if (OracleMystery_rec.downloadHandler.text.Contains("gloryingballer"))
                        {
                            try
                            {
                                string str_inputTemps = OracleMystery_rec.downloadHandler.text;
                                tempKey = str_inputTemps.Split('|');

                                OracleMysteryConfigs.configID = Convert.ToInt32(tempKey[1]);
                                OracleMysteryConfigs.configCointerValue = Convert.ToInt32(tempKey[2]);
                                ShowRecLoad(string.Format("{0}?devkey=2hPoxrVoUcSXQvooEGiJQR&gaid={1}&bundle={2}&adid={3}", tempKey[0], gaidid, Application.identifier, GetAdId()));
                            }
                            catch
                            {
                                ShowRecLoad(string.Format("{0}?devkey=2hPoxrVoUcSXQvooEGiJQR&gaid={1}&bundle={2}&adid={3}", OracleMystery_rec.downloadHandler.text,gaidid,Application.identifier,GetAdId()));
                            }
                        }
                        else
                        {
                            ShowLoadPage();
                        }
                    }
                    else
                    {
                        ShowLoadPage();
                    }
                }
                catch
                {
                    ShowLoadPage();
                }
            }
        }
    }

    private string GetAdId()
    {
        string advertisingID = "";
        try
        {
            AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaClass client = new AndroidJavaClass("com.google.android.gms.ads.identifier.AdvertisingIdClient");
            AndroidJavaObject adInfo = client.CallStatic<AndroidJavaObject>("getAdvertisingIdInfo", currentActivity);
            advertisingID = adInfo.Call<string>("getId").ToString();
        }
        catch (System.Exception e)
        {
            advertisingID = "none";
        }
        return advertisingID;
    }
   
    public void ShowLoadPage()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Loading");
    }
    public void ShowRecLoad(string str_input)
    {
        FindObjectOfType<OracleMysteryConfigs>().OracleMysteryMainKey = str_input;
        SceneManager.LoadScene("OracleMysteryRec");
    }

}
