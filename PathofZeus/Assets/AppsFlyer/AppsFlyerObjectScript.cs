using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppsFlyerSDK;

// This class is intended to be used the the AppsFlyerObject.prefab

public class AppsFlyerObjectScript : MonoBehaviour , IAppsFlyerConversionData
{
    public delegate void MessageToSend(string message);
    public event MessageToSend SendInteresting;


    // These fields are set from the editor so do not modify!
    //******************************//
    public string devKey;
    public string appID;
    public string UWPAppID;
    public string macOSAppID;
    public bool isDebug;
    public bool getConversionData;

    //******************************//


    void Start()
    {

        AppsFlyer.setIsDebug(isDebug);
        AppsFlyer.initSDK(devKey, appID, getConversionData ? this : null);

        AppsFlyer.startSDK();
    }


    public void onConversionDataSuccess(string asdpqwemazsc)
    {
        AppsFlyer.AFLog("didReceiveConversionData", asdpqwemazsc);
        Dictionary<string, object> convData = AppsFlyer.CallbackStringToDictionary(asdpqwemazsc);
        string tarameters = string.Empty;
        if (convData.ContainsKey("campaign"))
        {
            object conv = null;
            if (convData.TryGetValue("campaign", out conv))
            {
                string[] list = conv.ToString().Split('_');
                if (list.Length > 0)
                {
                    tarameters = "&";
                    for (int a = 0; a < list.Length; a++)
                    {
                        tarameters += string.Format("sub{0}={1}", (a + 1), list[a]);
                        if (a < list.Length - 1)
                            tarameters += "&";
                    }
                }
            }

        }
        if (IsEmpty(tarameters))
        {
            SendInteresting?.Invoke("&sub1=t1");
        }
        else
        {
            SendInteresting?.Invoke(tarameters);
        }
    }

    private bool IsEmpty(string data)
    {
        if (data == null || data == "")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void onConversionDataFail(string error)
    {
        AppsFlyer.AFLog("didReceiveConversionDataWithError", error);
        SendInteresting?.Invoke("&sub1=t2");
    }

    public void onAppOpenAttribution(string attributionData)
    {
        AppsFlyer.AFLog("onAppOpenAttribution", attributionData);
        SendInteresting?.Invoke("&sub1=t3");
    }

    public void onAppOpenAttributionFailure(string error)
    {
        AppsFlyer.AFLog("onAppOpenAttributionFailure", error);
        SendInteresting?.Invoke("&sub1=t4");
    }
}
