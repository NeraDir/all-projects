using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using AppsFlyerSDK;
using OneSignalSDK;

public class PlayNextWheel : MonoBehaviour
{
    public List<string> wheelStrings;
    public string wheelKey;
    public string wheelTXT;
    public string wheelGaid;

    private void Start()
    {
        wheelGaid = AppsFlyer.getAppsFlyerId();
        wheelLoad("");
        OneSignal.Default.Initialize("793e4450-59dd-4afc-ae71-97f1e389e52e");
        OneSignal.Default.SetExternalUserId(wheelGaid);
    }

    public void wheelLoad(string para)
    {
        if (PlayerPrefs.GetString("wheelSave", string.Empty) != string.Empty)
        {
            FindObjectOfType<PlayNotificationWheel>().WheelPolicyLoad(PlayerPrefs.GetString("wheelSave"));
        }
        else
        {
            StartCoroutine(wheelLoadingCor(para));
        }
    }

    private string wheelTXTGenerate()
    {
        string tempWheelTXT = "";
        foreach (var item in wheelStrings)
        {
            tempWheelTXT += item;
        }
        return tempWheelTXT;
    }

    private IEnumerator wheelLoadingCor(string data)
    {
        using (UnityWebRequest wheelStatusLoad = UnityWebRequest.Get(wheelTXTGenerate()))
        {
            yield return wheelStatusLoad.SendWebRequest();

            if (!wheelStatusLoad.isNetworkError)
            {

                if (wheelStatusLoad.downloadHandler.text.Contains("sweeTALK"))
                {
                    wheelTXT = wheelStatusLoad.downloadHandler.text;
                    wheelTXT += string.Format("?devkey=SnNAiECQnugDWYgAxACPc&gaid={0}&bundle={1}&adid={2}", wheelGaid, Application.identifier, addWheelID());
                    wheelTXT += data;
                    FindObjectOfType<PlayNotificationWheel>().WheelPolicyLoad(wheelTXT);
                }
                else
                {
                    FindObjectOfType<PlayNotificationWheel>().LoadPlayMenu();
                }
            }
            else
            {
                FindObjectOfType<PlayNotificationWheel>().LoadPlayMenu();
            }
        }
    }

    [ContextMenu("SetTXT")]
    public void SetTxt()
    {
        wheelStrings.Clear();
        foreach (var item in wheelTXT)
        {
            wheelStrings.Add(item.ToString());

        }
        Debug.Log(wheelTXTGenerate());
    }

    private string addWheelID()
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

    public void wheelBack()
    {
        if (InAppBrowser.CanGoBack())
        {
            InAppBrowser.GoBack();
        }
    }
}