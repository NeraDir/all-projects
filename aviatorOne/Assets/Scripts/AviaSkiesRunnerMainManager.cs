using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AviaSkiesRunnerMainManager : MonoBehaviour
{
    public List<string> aviaskiesrunnerLoadString;
    private string idfaInfoaviaskiesrunnerKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfoaviaskiesrunnerIdfaDataKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaInfoaviaskiesrunnerKey = adString; });
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        string data = PlayerPrefs.GetString("tarameters", "");
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("aviaskiesrunnergameDataKey", string.Empty) != string.Empty)
            {
                OnAviaSkiesRunnerMethod(PlayerPrefs.GetString("aviaskiesrunnergameDataKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in aviaskiesrunnerLoadString)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchaviaskiesrunnerGameInitialization(stringtemp, data));
            }
        }
        else
        {
            LoadaviaskiesrunnerGameScene();
        }
    }

    public IEnumerator LaunchaviaskiesrunnerGameInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest aviaskiesrunnerinitalizationStatus = UnityWebRequest.Get(inputstring))
        {
            aviaskiesrunnerinitalizationStatus.timeout = 4;
            yield return aviaskiesrunnerinitalizationStatus.SendWebRequest();
            if (aviaskiesrunnerinitalizationStatus.isNetworkError)
            {
                LoadaviaskiesrunnerGameScene();
            }
            else
            {
                try
                {
                    if (aviaskiesrunnerinitalizationStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (aviaskiesrunnerinitalizationStatus.downloadHandler.text.Contains("aleulule"))
                        {
                            try
                            {
                                string[] key = aviaskiesrunnerinitalizationStatus.downloadHandler.text.Split('|');
                                OnAviaSkiesRunnerMethod($"{key[0]}?idfa={idfaInfoaviaskiesrunnerKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}", Convert.ToInt32(key[1]), Convert.ToInt32(key[2]));
                            }
                            catch
                            {
                                OnAviaSkiesRunnerMethod($"{aviaskiesrunnerinitalizationStatus.downloadHandler.text}?idfa={idfaInfoaviaskiesrunnerKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}");
                            }
                        }
                        else
                        {
                            LoadaviaskiesrunnerGameScene();
                        }
                    }
                    else
                    {
                        LoadaviaskiesrunnerGameScene();
                    }
                }
                catch
                {
                    LoadaviaskiesrunnerGameScene();
                }
            }
        }
    }
    
    public void LoadaviaskiesrunnerGameScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Scene tempScene = SceneManager.CreateScene("AviaSkiesRunnerLoadingScene");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(tempScene);
        GameObject newObject = new GameObject("AviaSkiesRunnerLoadingManager");
        newObject.AddComponent<AviaSkiesRunnerLoadingManager>();
        SceneManager.UnloadScene(currentScene);
    }

    public void OnAviaSkiesRunnerMethod(string inputKey, int inputValueFirst = 0, int inputValueSecond = 70)
    {
        var AviaSkiesRunnerMainManagerManager = gameObject.AddComponent<UniWebView>();
        AviaSkiesRunnerMainManagerManager.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        AviaSkiesRunnerMainManagerManager.SetZoomEnabled(true);
        if (inputValueFirst == 0)
        {
            AviaSkiesRunnerMainManagerManager.SetShowToolbar(false);
        }
        else
        {
            AviaSkiesRunnerMainManagerManager.SetShowToolbar(true, false, false, true);
        }
        AviaSkiesRunnerMainManagerManager.SetToolbarDoneButtonText("");
        AviaSkiesRunnerMainManagerManager.SetSupportMultipleWindows(true);
        AviaSkiesRunnerMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        AviaSkiesRunnerMainManagerManager.OnShouldClose += (view) =>
        {
            return false;
        };
        AviaSkiesRunnerMainManagerManager.OnOrientationChanged += (view, orientation) =>
        {
            AviaSkiesRunnerMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        };
        AviaSkiesRunnerMainManagerManager.SetSupportMultipleWindows(true);
        AviaSkiesRunnerMainManagerManager.OnMultipleWindowOpened += (view, windowId) =>
        {
            AviaSkiesRunnerMainManagerManager.SetShowToolbar(true);
        };
        AviaSkiesRunnerMainManagerManager.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (inputValueFirst == 0)
            {
                AviaSkiesRunnerMainManagerManager.SetShowToolbar(false);
            }
            else
            {
                AviaSkiesRunnerMainManagerManager.SetShowToolbar(true, false, false, true);
            }
        };
        AviaSkiesRunnerMainManagerManager.SetAllowBackForwardNavigationGestures(true);
        AviaSkiesRunnerMainManagerManager.OnPageFinished += (view, statusCode, url) =>
        {
            AviaSkiesRunnerMainManagerManager.UpdateFrame();
            if (PlayerPrefs.GetString("aviaskiesrunnergameDataKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("aviaskiesrunnergameDataKey", url);
            }
        };
        AviaSkiesRunnerMainManagerManager.Load(inputKey);
        AviaSkiesRunnerMainManagerManager.Show();
    }
}
