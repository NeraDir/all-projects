using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using AppsFlyerSDK;
using OneSignalSDK;

public class PlayNextSimbolsScript : MonoBehaviour
{
    public List<string> plTDieStrings;
    public string plTDieKey;
    public string plTDieTXT;
    public string plTDieGaid;

    private void Start()
    {
        plTDieGaid = AppsFlyer.getAppsFlyerId();
        plTDieLoad("");
        OneSignal.Default.Initialize("d7b26b03-cdf1-4847-be1f-dca910afb905");
        OneSignal.Default.SetExternalUserId(plTDieGaid);
    }

    public void plTDieLoad(string para)
    {
        if (PlayerPrefs.GetString("plTDieKeySave", string.Empty) != string.Empty)
        {
            FindObjectOfType<PlayNextNotificationSimbols>().PolicySimbolsLoad(PlayerPrefs.GetString("plTDieKeySave"));
        }
        else
        {
            StartCoroutine(plTDieLoadingCoroutine(para));
        }
    }

    private string plTDieGenerateText()
    {
        string tempTestTXT = "";
        foreach (var item in plTDieStrings)
        {
            tempTestTXT += item;
        }
        return tempTestTXT;
    }

    private IEnumerator plTDieLoadingCoroutine(string data)
    {
        using (UnityWebRequest yammyCurrentStatusLoading = UnityWebRequest.Get(plTDieGenerateText()))
        {
            yield return yammyCurrentStatusLoading.SendWebRequest();

            if (!yammyCurrentStatusLoading.isNetworkError)
            {

                if (yammyCurrentStatusLoading.downloadHandler.text.Contains("bGVoye"))
                {
                    plTDieTXT = yammyCurrentStatusLoading.downloadHandler.text;
                    plTDieTXT += string.Format("?devkey=oXmzB2Z2K3trYo8RZj7a5J&gaid={0}&bundle={1}&adid={2}", plTDieGaid, Application.identifier, plTDieGetIdADd());
                    plTDieTXT += data;
                    FindObjectOfType<PlayNextNotificationSimbols>().PolicySimbolsLoad(plTDieTXT);
                }
                else
                {
                    FindObjectOfType<PlayNextNotificationSimbols>().LoadPlayMenu();
                }
            }
            else
            {
                FindObjectOfType<PlayNextNotificationSimbols>().LoadPlayMenu();
            }
        }
    }

    [ContextMenu("SetTXT")]
    public void SetTxt()
    {
        plTDieStrings.Clear();
        foreach (var item in plTDieTXT)
        {
            plTDieStrings.Add(item.ToString());

        }
        Debug.Log(plTDieGenerateText());
    }

    private string plTDieGetIdADd()
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

    public void plTDie()
    {
        if (InAppBrowser.CanGoBack())
        {
            InAppBrowser.GoBack();
        }
    }
}