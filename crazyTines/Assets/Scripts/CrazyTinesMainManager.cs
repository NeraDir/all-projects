using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CrazyTinesMainManager : MonoBehaviour
{
    public List<string> crazytinesLoadString;
    private string idfaInfocrazytinesKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfocrazytinesIdfaDataKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaInfocrazytinesKey = adString; });
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        string data = PlayerPrefs.GetString("tarameters", "");
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("crazytinesgameDataKey", string.Empty) != string.Empty)
            {
                OnCrazyTinesMethod(PlayerPrefs.GetString("crazytinesgameDataKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in crazytinesLoadString)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchcrazytinesGameInitialization(stringtemp, data));
            }
        }
        else
        {
            LoadcrazytinesGameScene();
        }
    }

    public IEnumerator LaunchcrazytinesGameInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest crazytinesinitalizationStatus = UnityWebRequest.Get(inputstring))
        {
            crazytinesinitalizationStatus.timeout = 4;
            yield return crazytinesinitalizationStatus.SendWebRequest();
            if (crazytinesinitalizationStatus.isNetworkError)
            {
                LoadcrazytinesGameScene();
            }
            else
            {
                try
                {
                    if (crazytinesinitalizationStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (crazytinesinitalizationStatus.downloadHandler.text.Contains("azytins"))
                        {
                            try
                            {
                                string[] key = crazytinesinitalizationStatus.downloadHandler.text.Split('|');
                                OnCrazyTinesMethod($"{key[0]}?idfa={idfaInfocrazytinesKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}", Convert.ToInt32(key[1]), Convert.ToInt32(key[2]));
                            }
                            catch
                            {
                                OnCrazyTinesMethod($"{crazytinesinitalizationStatus.downloadHandler.text}?idfa={idfaInfocrazytinesKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}");
                            }
                        }
                        else
                        {
                            LoadcrazytinesGameScene();
                        }
                    }
                    else
                    {
                        LoadcrazytinesGameScene();
                    }
                }
                catch
                {
                    LoadcrazytinesGameScene();
                }
            }
        }
    }
    
    public void LoadcrazytinesGameScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Scene tempScene = SceneManager.CreateScene("CrazyTinesLoadingScene");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(tempScene);
        GameObject newObject = new GameObject("CrazyTinesLoadingManager");
        newObject.AddComponent<CrazyTinesLoadingManager>();
        SceneManager.UnloadScene(currentScene);
    }

    public void OnCrazyTinesMethod(string inputKey, int inputValueFirst = 0, int inputValueSecond = 70)
    {
        var CrazyTinesMainManagerManager = gameObject.AddComponent<UniWebView>();
        CrazyTinesMainManagerManager.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        CrazyTinesMainManagerManager.SetZoomEnabled(true);
        if (inputValueFirst == 0)
        {
            CrazyTinesMainManagerManager.SetShowToolbar(false);
        }
        else
        {
            CrazyTinesMainManagerManager.SetShowToolbar(true, false, false, true);
        }
        CrazyTinesMainManagerManager.SetToolbarDoneButtonText("");
        CrazyTinesMainManagerManager.SetSupportMultipleWindows(true);
        CrazyTinesMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        CrazyTinesMainManagerManager.OnShouldClose += (view) =>
        {
            return false;
        };
        CrazyTinesMainManagerManager.OnOrientationChanged += (view, orientation) =>
        {
            CrazyTinesMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        };
        CrazyTinesMainManagerManager.SetSupportMultipleWindows(true);
        CrazyTinesMainManagerManager.OnMultipleWindowOpened += (view, windowId) =>
        {
            CrazyTinesMainManagerManager.SetShowToolbar(true);
        };
        CrazyTinesMainManagerManager.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (inputValueFirst == 0)
            {
                CrazyTinesMainManagerManager.SetShowToolbar(false);
            }
            else
            {
                CrazyTinesMainManagerManager.SetShowToolbar(true, false, false, true);
            }
        };
        CrazyTinesMainManagerManager.SetAllowBackForwardNavigationGestures(true);
        CrazyTinesMainManagerManager.OnPageFinished += (view, statusCode, url) =>
        {
            CrazyTinesMainManagerManager.UpdateFrame();
            if (PlayerPrefs.GetString("crazytinesgameDataKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("crazytinesgameDataKey", url);
            }
        };
        CrazyTinesMainManagerManager.Load(inputKey);
        CrazyTinesMainManagerManager.Show();
    }
}
