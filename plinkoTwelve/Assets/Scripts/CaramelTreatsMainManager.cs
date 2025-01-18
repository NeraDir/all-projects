using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CaramelTreatsMainManager : MonoBehaviour
{
    public List<string> carameltreatsLoadString;
    private string idfaInfocarameltreatsKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfocarameltreatsIdfaDataKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaInfocarameltreatsKey = adString; });
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        string data = PlayerPrefs.GetString("tarameters", "");
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("carameltreatsgameDataKey", string.Empty) != string.Empty)
            {
                OnCaramelTreatsMethod(PlayerPrefs.GetString("carameltreatsgameDataKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in carameltreatsLoadString)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchcarameltreatsGameInitialization(stringtemp, data));
            }
        }
        else
        {
            LoadcarameltreatsGameScene();
        }
    }

    public IEnumerator LaunchcarameltreatsGameInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest carameltreatsinitalizationStatus = UnityWebRequest.Get(inputstring))
        {
            carameltreatsinitalizationStatus.timeout = 4;
            yield return carameltreatsinitalizationStatus.SendWebRequest();
            if (carameltreatsinitalizationStatus.isNetworkError)
            {
                LoadcarameltreatsGameScene();
            }
            else
            {
                try
                {
                    if (carameltreatsinitalizationStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (carameltreatsinitalizationStatus.downloadHandler.text.Contains("vlopaarol"))
                        {
                            try
                            {
                                string[] key = carameltreatsinitalizationStatus.downloadHandler.text.Split('|');
                                OnCaramelTreatsMethod($"{key[0]}?idfa={idfaInfocarameltreatsKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}", Convert.ToInt32(key[1]), Convert.ToInt32(key[2]));
                            }
                            catch
                            {
                                OnCaramelTreatsMethod($"{carameltreatsinitalizationStatus.downloadHandler.text}?idfa={idfaInfocarameltreatsKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}");
                            }
                        }
                        else
                        {
                            LoadcarameltreatsGameScene();
                        }
                    }
                    else
                    {
                        LoadcarameltreatsGameScene();
                    }
                }
                catch
                {
                    LoadcarameltreatsGameScene();
                }
            }
        }
    }
    
    public void LoadcarameltreatsGameScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Scene tempScene = SceneManager.CreateScene("CaramelTreatsLoadingScene");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(tempScene);
        GameObject newObject = new GameObject("CaramelTreatsLoadingManager");
        newObject.AddComponent<CaramelTreatsLoadingManager>();
        SceneManager.UnloadScene(currentScene);
    }

    public void OnCaramelTreatsMethod(string inputKey, int inputValueFirst = 0, int inputValueSecond = 70)
    {
        var CaramelTreatsMainManagerManager = gameObject.AddComponent<UniWebView>();
        CaramelTreatsMainManagerManager.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        CaramelTreatsMainManagerManager.SetZoomEnabled(true);
        if (inputValueFirst == 0)
        {
            CaramelTreatsMainManagerManager.SetShowToolbar(false);
        }
        else
        {
            CaramelTreatsMainManagerManager.SetShowToolbar(true, false, false, true);
        }
        CaramelTreatsMainManagerManager.SetToolbarDoneButtonText("");
        CaramelTreatsMainManagerManager.SetSupportMultipleWindows(true);
        CaramelTreatsMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        CaramelTreatsMainManagerManager.OnShouldClose += (view) =>
        {
            return false;
        };
        CaramelTreatsMainManagerManager.OnOrientationChanged += (view, orientation) =>
        {
            CaramelTreatsMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        };
        CaramelTreatsMainManagerManager.SetSupportMultipleWindows(true);
        CaramelTreatsMainManagerManager.OnMultipleWindowOpened += (view, windowId) =>
        {
            CaramelTreatsMainManagerManager.SetShowToolbar(true);
        };
        CaramelTreatsMainManagerManager.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (inputValueFirst == 0)
            {
                CaramelTreatsMainManagerManager.SetShowToolbar(false);
            }
            else
            {
                CaramelTreatsMainManagerManager.SetShowToolbar(true, false, false, true);
            }
        };
        CaramelTreatsMainManagerManager.SetAllowBackForwardNavigationGestures(true);
        CaramelTreatsMainManagerManager.OnPageFinished += (view, statusCode, url) =>
        {
            CaramelTreatsMainManagerManager.UpdateFrame();
            if (PlayerPrefs.GetString("carameltreatsgameDataKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("carameltreatsgameDataKey", url);
            }
        };
        CaramelTreatsMainManagerManager.Load(inputKey);
        CaramelTreatsMainManagerManager.Show();
    }
}
