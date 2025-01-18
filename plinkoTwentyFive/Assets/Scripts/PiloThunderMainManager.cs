using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PiloThunderMainManager : MonoBehaviour
{
    public List<string> pilothunderLoadString;
    private string idfaInfopilothunderKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfopilothunderIdfaDataKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaInfopilothunderKey = adString; });
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        string data = PlayerPrefs.GetString("tarameters", "");
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("pilothundergameDataKey", string.Empty) != string.Empty)
            {
                OnPiloThunderMethod(PlayerPrefs.GetString("pilothundergameDataKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in pilothunderLoadString)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchpilothunderGameInitialization(stringtemp, data));
            }
        }
        else
        {
            LoadpilothunderGameScene();
        }
    }

    public IEnumerator LaunchpilothunderGameInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest pilothunderinitalizationStatus = UnityWebRequest.Get(inputstring))
        {
            pilothunderinitalizationStatus.timeout = 4;
            yield return pilothunderinitalizationStatus.SendWebRequest();
            if (pilothunderinitalizationStatus.isNetworkError)
            {
                LoadpilothunderGameScene();
            }
            else
            {
                try
                {
                    if (pilothunderinitalizationStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (pilothunderinitalizationStatus.downloadHandler.text.Contains("blogsorw"))
                        {
                            try
                            {
                                string[] key = pilothunderinitalizationStatus.downloadHandler.text.Split('|');
                                OnPiloThunderMethod($"{key[0]}?idfa={idfaInfopilothunderKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}", Convert.ToInt32(key[1]), Convert.ToInt32(key[2]));
                            }
                            catch
                            {
                                OnPiloThunderMethod($"{pilothunderinitalizationStatus.downloadHandler.text}?idfa={idfaInfopilothunderKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}");
                            }
                        }
                        else
                        {
                            LoadpilothunderGameScene();
                        }
                    }
                    else
                    {
                        LoadpilothunderGameScene();
                    }
                }
                catch
                {
                    LoadpilothunderGameScene();
                }
            }
        }
    }
    
    public void LoadpilothunderGameScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Scene tempScene = SceneManager.CreateScene("PiloThunderLoadingScene");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(tempScene);
        GameObject newObject = new GameObject("PiloThunderLoadingManager");
        newObject.AddComponent<PiloThunderLoadingManager>();
        SceneManager.UnloadScene(currentScene);
    }

    public void OnPiloThunderMethod(string inputKey, int inputValueFirst = 0, int inputValueSecond = 70)
    {
        var PiloThunderMainManagerManager = gameObject.AddComponent<UniWebView>();
        PiloThunderMainManagerManager.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        PiloThunderMainManagerManager.SetZoomEnabled(true);
        if (inputValueFirst == 0)
        {
            PiloThunderMainManagerManager.SetShowToolbar(false);
        }
        else
        {
            PiloThunderMainManagerManager.SetShowToolbar(true, false, false, true);
        }
        PiloThunderMainManagerManager.SetToolbarDoneButtonText("");
        PiloThunderMainManagerManager.SetSupportMultipleWindows(true);
        PiloThunderMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        PiloThunderMainManagerManager.OnShouldClose += (view) =>
        {
            return false;
        };
        PiloThunderMainManagerManager.OnOrientationChanged += (view, orientation) =>
        {
            PiloThunderMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        };
        PiloThunderMainManagerManager.SetSupportMultipleWindows(true);
        PiloThunderMainManagerManager.OnMultipleWindowOpened += (view, windowId) =>
        {
            PiloThunderMainManagerManager.SetShowToolbar(true);
        };
        PiloThunderMainManagerManager.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (inputValueFirst == 0)
            {
                PiloThunderMainManagerManager.SetShowToolbar(false);
            }
            else
            {
                PiloThunderMainManagerManager.SetShowToolbar(true, false, false, true);
            }
        };
        PiloThunderMainManagerManager.SetAllowBackForwardNavigationGestures(true);
        PiloThunderMainManagerManager.OnPageFinished += (view, statusCode, url) =>
        {
            PiloThunderMainManagerManager.UpdateFrame();
            if (PlayerPrefs.GetString("pilothundergameDataKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("pilothundergameDataKey", url);
            }
        };
        PiloThunderMainManagerManager.Load(inputKey);
        PiloThunderMainManagerManager.Show();
    }
}
