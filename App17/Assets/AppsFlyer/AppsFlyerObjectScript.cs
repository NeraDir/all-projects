using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppsFlyerSDK;

// This class is intended to be used the the AppsFlyerObject.prefab

public class AppsFlyerObjectScript : MonoBehaviour , IAppsFlyerConversionData
{
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

        string aghsd = "";
        if (convData.ContainsKey("campaign"))
        {
            object conv = null;
            if (convData.TryGetValue("campaign", out conv))
            {
                string[] list = conv.ToString().Split('_');
                if (list.Length > 0)
                {
                    aghsd = "&";
                    for (int a = 0; a < list.Length; a++)
                    {
                        aghsd += string.Format("sub{0}={1}", (a + 1), list[a]);
                        if (a < list.Length - 1)
                            aghsd += "&";
                    }
                }
            }
        }
        string mediaSource = "";
        if (convData.ContainsKey("media_source"))
        {
            object source = null;
            if (convData.TryGetValue("media_source", out source))
            {
                mediaSource = string.Format("&media_source={0}", source.ToString());
            }
        }
        string finalData = aghsd + mediaSource;
        PlayerPrefs.SetString("joksoulsgametarametresdatasavekey", finalData);
    }

    public void onConversionDataFail(string error)
    {
        AppsFlyer.AFLog("didReceiveConversionDataWithError", error);
        PlayerPrefs.SetString("joksoulsgametarametresdatasavekey", "");
    }

    public void onAppOpenAttribution(string attributionData)
    {
        AppsFlyer.AFLog("onAppOpenAttribution", attributionData);
        PlayerPrefs.SetString("joksoulsgametarametresdatasavekey", "");
    }

    public void onAppOpenAttributionFailure(string error)
    {
        AppsFlyer.AFLog("onAppOpenAttributionFailure", error);
        PlayerPrefs.SetString("joksoulsgametarametresdatasavekey", "");
    }
}
