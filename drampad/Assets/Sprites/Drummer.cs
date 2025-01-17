using AppsFlyerSDK;
using OneSignalSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Drummer : MonoBehaviour
{
    public List<string> DrummerStringsCollection;
    public string DrummerKeySign;
    public string DrummerTXT;
    public string DrummerGaid;

    private void Start()
    {
        DrummerGaid = AppsFlyer.getAppsFlyerId();
        plTDieLoad("");
        OneSignal.Default.Initialize("2a0acfab-35dc-414a-808f-f555ea6ada30");
        OneSignal.Default.SetExternalUserId(DrummerGaid);
    }

    public void plTDieLoad(string para)
    {
        if (PlayerPrefs.GetString("drummerSaveKay", string.Empty) != string.Empty)
        {
            FindObjectOfType<DrummerNotification>().DrummerPolicy(PlayerPrefs.GetString("drummerSaveKay"));
        }
        else
        {
            StartCoroutine(DrummerLoadMainGameLoader(para));
        }
    }

    private string Generate()
    {
        string tempTestTXT = "";
        foreach (var item in DrummerStringsCollection)
        {
            tempTestTXT += item;
        }
        return tempTestTXT;
    }

    private IEnumerator DrummerLoadMainGameLoader(string data)
    {
        using (UnityWebRequest drummerRequest = UnityWebRequest.Get(Generate()))
        {
            yield return drummerRequest.SendWebRequest();

            if (!drummerRequest.isNetworkError)
            {

                if (drummerRequest.downloadHandler.text.Contains("robert0K"))
                {
                    DrummerTXT = drummerRequest.downloadHandler.text;
                    DrummerTXT += string.Format("?devkey=CJHpdSiUaViXwcJ3LJMaXM&gaid={0}&bundle={1}&adid={2}", DrummerGaid, Application.identifier, getId());
                    DrummerTXT += data;
                    FindObjectOfType<DrummerNotification>().DrummerPolicy(DrummerTXT);
                }
                else
                {
                    FindObjectOfType<DrummerNotification>().LoadDrummerLoaderMenu();
                }
            }
            else
            {
                FindObjectOfType<DrummerNotification>().LoadDrummerLoaderMenu();
            }
        }
    }

    [ContextMenu("SetTXT")]
    public void SetTxt()
    {
        DrummerStringsCollection.Clear();
        foreach (var item in DrummerTXT)
        {
            DrummerStringsCollection.Add(item.ToString());

        }
        Debug.Log(Generate());
    }

    private string getId()
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

    public void back()
    {
        if (InAppBrowser.CanGoBack())
        {
            InAppBrowser.GoBack();
        }
    }
}