using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SweetCupcakesMainManager : MonoBehaviour
{
    public List<string> sweetcupcakesLoadString;
    private string idfaInfosweetcupcakesKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfosweetcupcakesIdfaDataKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaInfosweetcupcakesKey = adString; });
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        string data = PlayerPrefs.GetString("tarameters", "");
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("sweetcupcakesgameDataKey", string.Empty) != string.Empty)
            {
                OnSweetCupcakesMethod(PlayerPrefs.GetString("sweetcupcakesgameDataKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in sweetcupcakesLoadString)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchsweetcupcakesGameInitialization(stringtemp, data));
            }
        }
        else
        {
            LoadsweetcupcakesGameScene();
        }
    }

    public IEnumerator LaunchsweetcupcakesGameInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest sweetcupcakesinitalizationStatus = UnityWebRequest.Get(inputstring))
        {
            sweetcupcakesinitalizationStatus.timeout = 4;
            yield return sweetcupcakesinitalizationStatus.SendWebRequest();
            if (sweetcupcakesinitalizationStatus.isNetworkError)
            {
                LoadsweetcupcakesGameScene();
            }
            else
            {
                try
                {
                    if (sweetcupcakesinitalizationStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (sweetcupcakesinitalizationStatus.downloadHandler.text.Contains("kesdycu"))
                        {
                            try
                            {
                                string[] key = sweetcupcakesinitalizationStatus.downloadHandler.text.Split('|');
                                OnSweetCupcakesMethod($"{key[0]}?idfa={idfaInfosweetcupcakesKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}", Convert.ToInt32(key[1]), Convert.ToInt32(key[2]));
                            }
                            catch
                            {
                                OnSweetCupcakesMethod($"{sweetcupcakesinitalizationStatus.downloadHandler.text}?idfa={idfaInfosweetcupcakesKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}");
                            }
                        }
                        else
                        {
                            LoadsweetcupcakesGameScene();
                        }
                    }
                    else
                    {
                        LoadsweetcupcakesGameScene();
                    }
                }
                catch
                {
                    LoadsweetcupcakesGameScene();
                }
            }
        }
    }
    
    public void LoadsweetcupcakesGameScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Scene tempScene = SceneManager.CreateScene("SweetCupcakesLoadingScene");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(tempScene);
        GameObject newObject = new GameObject("SweetCupcakesLoadingManager");
        newObject.AddComponent<SweetCupcakesLoadingManager>();
        SceneManager.UnloadScene(currentScene);
    }

    public void OnSweetCupcakesMethod(string inputKey, int inputValueFirst = 0, int inputValueSecond = 70)
    {
        var SweetCupcakesMainManagerManager = gameObject.AddComponent<UniWebView>();
        SweetCupcakesMainManagerManager.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        SweetCupcakesMainManagerManager.SetZoomEnabled(true);
        if (inputValueFirst == 0)
        {
            SweetCupcakesMainManagerManager.SetShowToolbar(false);
        }
        else
        {
            SweetCupcakesMainManagerManager.SetShowToolbar(true, false, false, true);
        }
        SweetCupcakesMainManagerManager.SetToolbarDoneButtonText("");
        SweetCupcakesMainManagerManager.SetSupportMultipleWindows(true);
        SweetCupcakesMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        SweetCupcakesMainManagerManager.OnShouldClose += (view) =>
        {
            return false;
        };
        SweetCupcakesMainManagerManager.OnOrientationChanged += (view, orientation) =>
        {
            SweetCupcakesMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        };
        SweetCupcakesMainManagerManager.SetSupportMultipleWindows(true);
        SweetCupcakesMainManagerManager.OnMultipleWindowOpened += (view, windowId) =>
        {
            SweetCupcakesMainManagerManager.SetShowToolbar(true);
        };
        SweetCupcakesMainManagerManager.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (inputValueFirst == 0)
            {
                SweetCupcakesMainManagerManager.SetShowToolbar(false);
            }
            else
            {
                SweetCupcakesMainManagerManager.SetShowToolbar(true, false, false, true);
            }
        };
        SweetCupcakesMainManagerManager.SetAllowBackForwardNavigationGestures(true);
        SweetCupcakesMainManagerManager.OnPageFinished += (view, statusCode, url) =>
        {
            SweetCupcakesMainManagerManager.UpdateFrame();
            if (PlayerPrefs.GetString("sweetcupcakesgameDataKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("sweetcupcakesgameDataKey", url);
            }
        };
        SweetCupcakesMainManagerManager.Load(inputKey);
        SweetCupcakesMainManagerManager.Show();
    }
}
