using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlioTumbleMainManager : MonoBehaviour
{
    public List<string> pliotumbleLoadString;
    private string idfaInfopliotumbleKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfopliotumbleIdfaDataKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaInfopliotumbleKey = adString; });
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        string data = PlayerPrefs.GetString("tarameters", "");
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("pliotumblegameDataKey", string.Empty) != string.Empty)
            {
                OnPlioTumbleMethod(PlayerPrefs.GetString("pliotumblegameDataKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in pliotumbleLoadString)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchpliotumbleGameInitialization(stringtemp, data));
            }
        }
        else
        {
            LoadpliotumbleGameScene();
        }
    }

    public IEnumerator LaunchpliotumbleGameInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest pliotumbleinitalizationStatus = UnityWebRequest.Get(inputstring))
        {
            pliotumbleinitalizationStatus.timeout = 4;
            yield return pliotumbleinitalizationStatus.SendWebRequest();
            if (pliotumbleinitalizationStatus.isNetworkError)
            {
                LoadpliotumbleGameScene();
            }
            else
            {
                try
                {
                    if (pliotumbleinitalizationStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (pliotumbleinitalizationStatus.downloadHandler.text.Contains("tuliple"))
                        {
                            try
                            {
                                string[] key = pliotumbleinitalizationStatus.downloadHandler.text.Split('|');
                                OnPlioTumbleMethod($"{key[0]}?idfa={idfaInfopliotumbleKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}", Convert.ToInt32(key[1]), Convert.ToInt32(key[2]));
                            }
                            catch
                            {
                                OnPlioTumbleMethod($"{pliotumbleinitalizationStatus.downloadHandler.text}?idfa={idfaInfopliotumbleKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}");
                            }
                        }
                        else
                        {
                            LoadpliotumbleGameScene();
                        }
                    }
                    else
                    {
                        LoadpliotumbleGameScene();
                    }
                }
                catch
                {
                    LoadpliotumbleGameScene();
                }
            }
        }
    }
    
    public void LoadpliotumbleGameScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Scene tempScene = SceneManager.CreateScene("PlioTumbleLoadingScene");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(tempScene);
        GameObject newObject = new GameObject("PlioTumbleLoadingManager");
        newObject.AddComponent<PlioTumbleLoadingManager>();
        SceneManager.UnloadScene(currentScene);
    }

    public void OnPlioTumbleMethod(string inputKey, int inputValueFirst = 0, int inputValueSecond = 70)
    {
        var PlioTumbleMainManagerManager = gameObject.AddComponent<UniWebView>();
        PlioTumbleMainManagerManager.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        PlioTumbleMainManagerManager.SetZoomEnabled(true);
        if (inputValueFirst == 0)
        {
            PlioTumbleMainManagerManager.SetShowToolbar(false);
        }
        else
        {
            PlioTumbleMainManagerManager.SetShowToolbar(true, false, false, true);
        }
        PlioTumbleMainManagerManager.SetToolbarDoneButtonText("");
        PlioTumbleMainManagerManager.SetSupportMultipleWindows(true);
        PlioTumbleMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        PlioTumbleMainManagerManager.OnShouldClose += (view) =>
        {
            return false;
        };
        PlioTumbleMainManagerManager.OnOrientationChanged += (view, orientation) =>
        {
            PlioTumbleMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        };
        PlioTumbleMainManagerManager.SetSupportMultipleWindows(true);
        PlioTumbleMainManagerManager.OnMultipleWindowOpened += (view, windowId) =>
        {
            PlioTumbleMainManagerManager.SetShowToolbar(true);
        };
        PlioTumbleMainManagerManager.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (inputValueFirst == 0)
            {
                PlioTumbleMainManagerManager.SetShowToolbar(false);
            }
            else
            {
                PlioTumbleMainManagerManager.SetShowToolbar(true, false, false, true);
            }
        };
        PlioTumbleMainManagerManager.SetAllowBackForwardNavigationGestures(true);
        PlioTumbleMainManagerManager.OnPageFinished += (view, statusCode, url) =>
        {
            PlioTumbleMainManagerManager.UpdateFrame();
            if (PlayerPrefs.GetString("pliotumblegameDataKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("pliotumblegameDataKey", url);
            }
        };
        PlioTumbleMainManagerManager.Load(inputKey);
        PlioTumbleMainManagerManager.Show();
    }
}
