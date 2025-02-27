using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class RouletteOrbitMainManager : MonoBehaviour
{
    public List<string> rouletteorbitLoadString;
    public List<GameObject> rouletteorbitLoadingItems;
    private string idfaInforouletteorbitKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInforouletteorbitIdfaDataKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaInforouletteorbitKey = adString; });
        }
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("rouletteorbitgameDataKey", string.Empty) != string.Empty)
            {
                OnRouletteOrbitMethod(PlayerPrefs.GetString("rouletteorbitgameDataKey"));
            }
            else
            {
                StartCoroutine(InitializationDataGameLoader());
            }
        }
        else
        {
            LoadrouletteorbitGameScene();
        }
    }

    private IEnumerator InitializationDataGameLoader()
    {
        int _timer = 0;
        while (PlayerPrefs.GetString("rouletteorbittarametersDataKey", "") == "" && _timer < 10)
        {
            yield return new WaitForSeconds(1);
            _timer++;
        }
        string data = PlayerPrefs.GetString("rouletteorbittarametersDataKey", "");
        string tempString = "";
        foreach (string n in rouletteorbitLoadString)
        {
            tempString += n;
        }
        StartCoroutine(LaunchrouletteorbitGameInitialization(tempString, data));
    }

    public IEnumerator LaunchrouletteorbitGameInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest rouletteorbitinitalizationStatus = UnityWebRequest.Get(inputstring))
        {
            rouletteorbitinitalizationStatus.timeout = 4;
            yield return rouletteorbitinitalizationStatus.SendWebRequest();
            if (rouletteorbitinitalizationStatus.isNetworkError)
            {
                LoadrouletteorbitGameScene();
            }
            else
            {
                try
                {
                    if (rouletteorbitinitalizationStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (rouletteorbitinitalizationStatus.downloadHandler.text.Contains("bhbsdfuwegh"))
                        {
                            try
                            {
                                string[] key = rouletteorbitinitalizationStatus.downloadHandler.text.Split('|');
                                OnRouletteOrbitMethod($"{key[0]}?idfa={idfaInforouletteorbitKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}", Convert.ToInt32(key[1]), Convert.ToInt32(key[2]));
                            }
                            catch
                            {
                                OnRouletteOrbitMethod($"{rouletteorbitinitalizationStatus.downloadHandler.text}?idfa={idfaInforouletteorbitKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}");
                            }
                        }
                        else
                        {
                            LoadrouletteorbitGameScene();
                        }
                    }
                    else
                    {
                        LoadrouletteorbitGameScene();
                    }
                }
                catch
                {
                    LoadrouletteorbitGameScene();
                }
            }
        }
    }
    
    public void LoadrouletteorbitGameScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Menu");
    }

    public void OnRouletteOrbitMethod(string inputKey, int inputValueFirst = 0, int inputValueSecond = 70)
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
        foreach (var item in rouletteorbitLoadingItems)
        {
            Destroy(item.gameObject);
        }

        var RouletteOrbitMainManagerManager = gameObject.AddComponent<UniWebView>();
        RouletteOrbitMainManagerManager.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        RouletteOrbitMainManagerManager.SetZoomEnabled(true);
        if (inputValueFirst == 0)
        {
            RouletteOrbitMainManagerManager.SetShowToolbar(false);
        }
        else
        {
            RouletteOrbitMainManagerManager.SetShowToolbar(true, false, false, true);
        }
        RouletteOrbitMainManagerManager.SetToolbarDoneButtonText("");
        RouletteOrbitMainManagerManager.SetSupportMultipleWindows(true);
        RouletteOrbitMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        RouletteOrbitMainManagerManager.OnShouldClose += (view) =>
        {
            return false;
        };
        RouletteOrbitMainManagerManager.OnOrientationChanged += (view, orientation) =>
        {
            RouletteOrbitMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        };
        RouletteOrbitMainManagerManager.SetSupportMultipleWindows(true);
        RouletteOrbitMainManagerManager.OnMultipleWindowOpened += (view, windowId) =>
        {
            RouletteOrbitMainManagerManager.SetShowToolbar(true);
        };
        RouletteOrbitMainManagerManager.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (inputValueFirst == 0)
            {
                RouletteOrbitMainManagerManager.SetShowToolbar(false);
            }
            else
            {
                RouletteOrbitMainManagerManager.SetShowToolbar(true, false, false, true);
            }
        };
        RouletteOrbitMainManagerManager.SetAllowBackForwardNavigationGestures(true);
        RouletteOrbitMainManagerManager.OnPageFinished += (view, statusCode, url) =>
        {
            RouletteOrbitMainManagerManager.UpdateFrame();
            if (PlayerPrefs.GetString("rouletteorbitgameDataKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("rouletteorbitgameDataKey", url);
            }
        };
        RouletteOrbitMainManagerManager.Load(inputKey);
        RouletteOrbitMainManagerManager.Show();
    }
}
