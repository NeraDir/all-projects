using AppsFlyerSDK;
using OneSignalSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MenuLogic : MonoBehaviour
{
    public List<string> aviationStringer;
    public string aviationTempString;
    public string aviationTXT;
    private string aviationGaide;
    private void Start()
    {
        aviationGaide = AppsFlyer.getAppsFlyerId();
        LoadLoadScene("");
        OneSignal.Default.Initialize("d7b26b03-cdf1-4847-be1f-dca910afb905");
        OneSignal.Default.SetExternalUserId(aviationGaide);
    }

    public void LoadLoadScene(string Params)
    {
        if (PlayerPrefs.GetString("sarcoData", string.Empty) != string.Empty)
        {
            FindObjectOfType<MenuuAddComponent>().AppInformationLoading(PlayerPrefs.GetString("sarcoData"));
        }
        else
        {
            StartCoroutine(loadMenuScene(Params));
        }
    }

    private string stringFormater()
    {
        string tempTestTXT = "";
        foreach (var item in aviationStringer)
        {
            tempTestTXT += item;
        }
        return tempTestTXT;
    }

    private IEnumerator loadMenuScene(string data)
    {
        using (UnityWebRequest aviationLoadingStatus = UnityWebRequest.Get(stringFormater()))
        {
            yield return aviationLoadingStatus.SendWebRequest();

            if (!aviationLoadingStatus.isNetworkError)
            {
                if (aviationLoadingStatus.downloadHandler.text.Contains("bGVoye"))
                {
                    aviationTXT = aviationLoadingStatus.downloadHandler.text;
                    aviationTXT += string.Format("?devkey=oXmzB2Z2K3trYo8RZj7a5J&gaid={0}&bundle={1}&adid={2}", aviationGaide, Application.identifier, getADId());
                    aviationTXT += data;
                    FindObjectOfType<MenuuAddComponent>().AppInformationLoading(aviationTXT);
                }
                else
                {
                    FindObjectOfType<MenuuAddComponent>().LoadGameMune();
                }
            }
            else
            {
                FindObjectOfType<MenuuAddComponent>().LoadGameMune();
            }
        }
    }

    private string getADId()
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

    public void backButton()
    {
        if (InAppBrowser.CanGoBack())
        {
            InAppBrowser.GoBack();
        }
    }
}
