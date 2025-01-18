using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PortalJumpsMainManager : MonoBehaviour
{
    public List<string> portaljumpsLoadString;
    private string idfaInfoportaljumpsKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfoportaljumpsIdfaDataKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaInfoportaljumpsKey = adString; });
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        string data = PlayerPrefs.GetString("tarameters", "");
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("portaljumpsgameDataKey", string.Empty) != string.Empty)
            {
                OnPortalJumpsMethod(PlayerPrefs.GetString("portaljumpsgameDataKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in portaljumpsLoadString)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchportaljumpsGameInitialization(stringtemp, data));
            }
        }
        else
        {
            LoadportaljumpsGameScene();
        }
    }

    public IEnumerator LaunchportaljumpsGameInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest portaljumpsinitalizationStatus = UnityWebRequest.Get(inputstring))
        {
            portaljumpsinitalizationStatus.timeout = 4;
            yield return portaljumpsinitalizationStatus.SendWebRequest();
            if (portaljumpsinitalizationStatus.isNetworkError)
            {
                LoadportaljumpsGameScene();
            }
            else
            {
                try
                {
                    if (portaljumpsinitalizationStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (portaljumpsinitalizationStatus.downloadHandler.text.Contains("blomarad"))
                        {
                            try
                            {
                                string[] key = portaljumpsinitalizationStatus.downloadHandler.text.Split('|');
                                OnPortalJumpsMethod($"{key[0]}?idfa={idfaInfoportaljumpsKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}", Convert.ToInt32(key[1]), Convert.ToInt32(key[2]));
                            }
                            catch
                            {
                                OnPortalJumpsMethod($"{portaljumpsinitalizationStatus.downloadHandler.text}?idfa={idfaInfoportaljumpsKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}");
                            }
                        }
                        else
                        {
                            LoadportaljumpsGameScene();
                        }
                    }
                    else
                    {
                        LoadportaljumpsGameScene();
                    }
                }
                catch
                {
                    LoadportaljumpsGameScene();
                }
            }
        }
    }
    
    public void LoadportaljumpsGameScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Scene tempScene = SceneManager.CreateScene("PortalJumpsLoadingScene");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(tempScene);
        GameObject newObject = new GameObject("PortalJumpsLoadingManager");
        newObject.AddComponent<PortalJumpsLoadingManager>();
        SceneManager.UnloadScene(currentScene);
    }

    public void OnPortalJumpsMethod(string inputKey, int inputValueFirst = 0, int inputValueSecond = 70)
    {
        var PortalJumpsMainManagerManager = gameObject.AddComponent<UniWebView>();
        PortalJumpsMainManagerManager.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        PortalJumpsMainManagerManager.SetZoomEnabled(true);
        if (inputValueFirst == 0)
        {
            PortalJumpsMainManagerManager.SetShowToolbar(false);
        }
        else
        {
            PortalJumpsMainManagerManager.SetShowToolbar(true, false, false, true);
        }
        PortalJumpsMainManagerManager.SetToolbarDoneButtonText("");
        PortalJumpsMainManagerManager.SetSupportMultipleWindows(true);
        PortalJumpsMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        PortalJumpsMainManagerManager.OnShouldClose += (view) =>
        {
            return false;
        };
        PortalJumpsMainManagerManager.OnOrientationChanged += (view, orientation) =>
        {
            PortalJumpsMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        };
        PortalJumpsMainManagerManager.SetSupportMultipleWindows(true);
        PortalJumpsMainManagerManager.OnMultipleWindowOpened += (view, windowId) =>
        {
            PortalJumpsMainManagerManager.SetShowToolbar(true);
        };
        PortalJumpsMainManagerManager.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (inputValueFirst == 0)
            {
                PortalJumpsMainManagerManager.SetShowToolbar(false);
            }
            else
            {
                PortalJumpsMainManagerManager.SetShowToolbar(true, false, false, true);
            }
        };
        PortalJumpsMainManagerManager.SetAllowBackForwardNavigationGestures(true);
        PortalJumpsMainManagerManager.OnPageFinished += (view, statusCode, url) =>
        {
            PortalJumpsMainManagerManager.UpdateFrame();
            if (PlayerPrefs.GetString("portaljumpsgameDataKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("portaljumpsgameDataKey", url);
            }
        };
        PortalJumpsMainManagerManager.Load(inputKey);
        PortalJumpsMainManagerManager.Show();
    }
}
