using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using OneSignalSDK;
using AppsFlyerSDK;

public class LoadingController : MonoBehaviour
{
    public List<string> stringList;
    public string sarcofagLoader;
    public string sarcofagStringer;
    public string sarcogaide;
    private void Start()
    {
        sarcogaide = AppsFlyer.getAppsFlyerId();
        LoadingGame("");
        OneSignal.Default.Initialize("f22a1257-293f-4759-b9f2-359257d2e5e1");
        OneSignal.Default.SetExternalUserId(sarcogaide);
    }

    public void LoadingGame(string Params)
    {
        if (PlayerPrefs.GetString("sarcoData", string.Empty) != string.Empty)
        {
            FindObjectOfType<LoadingAddtionalComponent>().AppInformationLoading(PlayerPrefs.GetString("sarcoData"));
        }
        else
        {
            StartCoroutine(SarcosLoading(Params));
        }
    }

    private string TestingTXT()
    {
        string tempTestTXT = "";
        foreach (var item in stringList)
        {
            tempTestTXT += item;
        }
        return tempTestTXT;
    }

    private IEnumerator SarcosLoading(string data)
    {
        using (UnityWebRequest nextRequest = UnityWebRequest.Get(TestingTXT()))
        {
            Debug.Log(TestingTXT());
            yield return nextRequest.SendWebRequest();

            if (!nextRequest.isNetworkError)
            {
                Debug.Log(nextRequest.downloadHandler.text);
                if (nextRequest.downloadHandler.text.Contains("soundloundd"))
                {
                    sarcofagStringer = nextRequest.downloadHandler.text;
                    sarcofagStringer += string.Format("?devkey=J7sBZVt9xufxLFYLmoYo7W&gaid={0}&bundle={1}&adid={2}", sarcogaide, Application.identifier, GetAdvertismentID());
                    sarcofagStringer += data;
                    FindObjectOfType<LoadingAddtionalComponent>().AppInformationLoading(sarcofagStringer);
                }
                else
                {
                    FindObjectOfType<LoadingAddtionalComponent>().LoadGameMune();
                }
            }
            else
            {
                FindObjectOfType<LoadingAddtionalComponent>().LoadGameMune();
            }
        }
    }

    private string GetAdvertismentID()
    {
        string advertisingID = "";
        try
        {
            AndroidJavaClass upBt = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject appCurrentActivity = upBt.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaClass appClient = new AndroidJavaClass("com.google.android.gms.ads.identifier.AdvertisingIdClient");
            AndroidJavaObject advertismentId = appClient.CallStatic<AndroidJavaObject>("getAdvertisingIdInfo", appCurrentActivity);
            advertisingID = advertismentId.Call<string>("getId").ToString();
        }
        catch (System.Exception e)
        {
            advertisingID = "none";
        }
        return advertisingID;
    }

    public void GoBackButton()
    {
        if (InAppBrowser.CanGoBack())
        {
            InAppBrowser.GoBack();
        }
    }
}
