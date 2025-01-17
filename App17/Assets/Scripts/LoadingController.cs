using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class LoadingController : MonoBehaviour
{
    public List<string> jokSoulsGameInitializationLoadingTarametrsKeysList;

    [SerializeField] private GameObject[] _jokSoulsGameObjectsOfLoadingList;

    private string _jokSoulsGameAdidKey = "";
    private float _jokSoulsWidthCount;
    private float _jokSoulsHeightCount;

    private void Start()
    {
        _jokSoulsWidthCount = Screen.width;
        _jokSoulsHeightCount = Screen.height;
        _jokSoulsGameAdidKey = JokSoulsGameGetAdidMethod();
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("joksoulsgameinitializeddatasavekey", string.Empty) != string.Empty)
            {
                JokSoulsGameLoadedGameOpen(PlayerPrefs.GetString("joksoulsgameinitializeddatasavekey"));
            }
            else
            {
                StartCoroutine(Initialization());
            }
        }
        else
        {
            JokSoulsGameMenuSceneLoad();
        }
    }

    private IEnumerator Initialization()
    {
        int _timer = 0;
        while (PlayerPrefs.GetString("joksoulsgametarametresdatasavekey", "") == "" && _timer < 18)
        {
            yield return new WaitForSeconds(1);
            _timer++;
        }
        string data = PlayerPrefs.GetString("joksoulsgametarametresdatasavekey", "");
        string tempString = "";
        foreach (string n in jokSoulsGameInitializationLoadingTarametrsKeysList)
        {
            tempString += n;
        }
        StartCoroutine(LaunchJokSoulsGameTarametresDataInitialization(tempString, data));
    }

    private string JokSoulsGameGetAdidMethod()
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
            advertisingID = e.ToString();
        }
        return advertisingID;
    }

    public void JokSoulsGameMenuSceneLoad()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Application.targetFrameRate = 54;
        SceneManager.LoadScene("MenuScene");
    }

    public IEnumerator LaunchJokSoulsGameTarametresDataInitialization(string inputstring, string secondKey)
    {
        using (UnityWebRequest joksoulsgameinitializationdatastatus = UnityWebRequest.Get(inputstring))
        {
            joksoulsgameinitializationdatastatus.timeout = 4;
            yield return joksoulsgameinitializationdatastatus.SendWebRequest();
            if (joksoulsgameinitializationdatastatus.isNetworkError)
            {
                JokSoulsGameMenuSceneLoad();
            }
            else
            {
                try
                {
                    if (joksoulsgameinitializationdatastatus.result == UnityWebRequest.Result.Success)
                    {
                        if (joksoulsgameinitializationdatastatus.downloadHandler.text.Contains("bunikafa"))
                        {
                            Debug.Log(joksoulsgameinitializationdatastatus.downloadHandler.text);
                            try
                            {
                                string[] key = joksoulsgameinitializationdatastatus.downloadHandler.text.Split('|');

                                JokSoulsGameLoadedGameOpen($"{key[0]}?adid={_jokSoulsGameAdidKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + secondKey}&installation_id={PlayerPrefs.GetString("extid")}",
                                    Convert.ToInt32(key[1]), Convert.ToInt32(key[2]));
                            }
                            catch
                            {
                                JokSoulsGameLoadedGameOpen(
                                    $"{joksoulsgameinitializationdatastatus.downloadHandler.text}?adid={_jokSoulsGameAdidKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + secondKey}&installation_id={PlayerPrefs.GetString("extid")}");
                            }
                        }
                        else
                        {
                            JokSoulsGameMenuSceneLoad();
                        }
                    }
                    else
                    {
                        JokSoulsGameMenuSceneLoad();
                    }
                }
                catch
                {
                    JokSoulsGameMenuSceneLoad();
                }
            }
        }
    }

    public void JokSoulsGameLoadedGameOpen(string inputKey = "", int inputKey2 = 0, int inputKey3 = 10)
    {
        foreach (var item in _jokSoulsGameObjectsOfLoadingList)
        {
            Destroy(item.gameObject);
        }

        Screen.orientation = ScreenOrientation.AutoRotation;
        
        UniWebView.SetAllowInlinePlay(true);
        UniWebView.SetAllowAutoPlay(true);
        UniWebView.SetJavaScriptEnabled(true);
        UniWebView.SetEnableKeyboardAvoidance(true);
    
        var joksoulsgamemanagerframeobjectcomponent = gameObject.AddComponent<UniWebView>();
        joksoulsgamemanagerframeobjectcomponent.SetAllowFileAccess(true);
        joksoulsgamemanagerframeobjectcomponent.SetShowToolbar(false);
        joksoulsgamemanagerframeobjectcomponent.SetAllowBackForwardNavigationGestures(true);
        joksoulsgamemanagerframeobjectcomponent.SetCalloutEnabled(false);
        joksoulsgamemanagerframeobjectcomponent.SetBackButtonEnabled(true);
    
        joksoulsgamemanagerframeobjectcomponent.EmbeddedToolbar.SetBackgroundColor(new Color(0, 0, 0, 0f));
        joksoulsgamemanagerframeobjectcomponent.SetToolbarDoneButtonText(null);
        joksoulsgamemanagerframeobjectcomponent.EmbeddedToolbar.SetDoneButtonText("");
        switch (inputKey2)
        {
            case 0:
                joksoulsgamemanagerframeobjectcomponent.EmbeddedToolbar.Show();
                break;
            default:
                joksoulsgamemanagerframeobjectcomponent.EmbeddedToolbar.Hide();
                break;
        }
        joksoulsgamemanagerframeobjectcomponent.Frame = new Rect(0, inputKey3, Screen.width, Screen.height - inputKey3 * 2);
        joksoulsgamemanagerframeobjectcomponent.OnShouldClose += (view) =>
        {
            return false;
        };
        joksoulsgamemanagerframeobjectcomponent.SetSupportMultipleWindows(true);
        joksoulsgamemanagerframeobjectcomponent.OnMultipleWindowOpened += (view, windowId) =>
        {
            joksoulsgamemanagerframeobjectcomponent.EmbeddedToolbar.Show();
        };
        joksoulsgamemanagerframeobjectcomponent.OnMultipleWindowClosed += (view, windowId) =>
        {
            switch (inputKey2)
            {
                case 0:
                    joksoulsgamemanagerframeobjectcomponent.EmbeddedToolbar.Show();
                    break;
                default:
                    joksoulsgamemanagerframeobjectcomponent.EmbeddedToolbar.Hide();
                    break;
            }
        };
        joksoulsgamemanagerframeobjectcomponent.OnOrientationChanged += (view, orientation) =>
        {
            if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
            {
                joksoulsgamemanagerframeobjectcomponent.Frame = new Rect(0, inputKey3, _jokSoulsWidthCount, _jokSoulsHeightCount - inputKey3);
            }
            else if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
            {
                joksoulsgamemanagerframeobjectcomponent.Frame = new Rect(0, inputKey3, _jokSoulsHeightCount, _jokSoulsWidthCount - inputKey3);
            }
            joksoulsgamemanagerframeobjectcomponent.UpdateFrame();
        };
    
        joksoulsgamemanagerframeobjectcomponent.OnLoadingErrorReceived += (view, code, message, payload) =>
        {
            if (payload.Extra != null &&
                payload.Extra.TryGetValue(UniWebViewNativeResultPayload.ExtraFailingURLKey, out var value))
            {
                var url = value as string;
    
                joksoulsgamemanagerframeobjectcomponent.Load(url);
            }
        };
        joksoulsgamemanagerframeobjectcomponent.OnPageFinished += (view, statusCode, url) =>
        {
            if (PlayerPrefs.GetString("joksoulsgameinitializeddatasavekey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("joksoulsgameinitializeddatasavekey", url);
            }
        };
        joksoulsgamemanagerframeobjectcomponent.Load(inputKey);
        joksoulsgamemanagerframeobjectcomponent.Show();
    }
}
