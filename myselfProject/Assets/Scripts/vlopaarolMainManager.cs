using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class vlopaarolMainManager : MonoBehaviour
{
    public List<string> vlopaarolLoadString;
    private string idfaInfovlopaarolKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfovlopaarolIdfaDataKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaInfovlopaarolKey = adString; });
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        string data = PlayerPrefs.GetString("tarameters", "");
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("vlopaarolgameDataKey", string.Empty) != string.Empty)
            {
                OnvlopaarolMethod(PlayerPrefs.GetString("vlopaarolgameDataKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in vlopaarolLoadString)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchvlopaarolGameInitialization(stringtemp, data));
            }
        }
        else
        {
            LoadvlopaarolGameScene();
        }
    }

    public IEnumerator LaunchvlopaarolGameInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest vlopaarolinitalizationStatus = UnityWebRequest.Get(inputstring))
        {
            vlopaarolinitalizationStatus.timeout = 4;
            yield return vlopaarolinitalizationStatus.SendWebRequest();
            if (vlopaarolinitalizationStatus.isNetworkError)
            {
                LoadvlopaarolGameScene();
            }
            else
            {
                try
                {
                    if (vlopaarolinitalizationStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (vlopaarolinitalizationStatus.downloadHandler.text.Contains("vlopaarol"))
                        {
                            try
                            {
                                string[] key = vlopaarolinitalizationStatus.downloadHandler.text.Split('|');
                                OnvlopaarolMethod($"{key[0]}?idfa={idfaInfovlopaarolKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}", Convert.ToInt32(key[1]), Convert.ToInt32(key[2]));
                            }
                            catch
                            {
                                OnvlopaarolMethod($"{vlopaarolinitalizationStatus.downloadHandler.text}?idfa={idfaInfovlopaarolKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}");
                            }
                        }
                        else
                        {
                            LoadvlopaarolGameScene();
                        }
                    }
                    else
                    {
                        LoadvlopaarolGameScene();
                    }
                }
                catch
                {
                    LoadvlopaarolGameScene();
                }
            }
        }
    }
    
    public void LoadvlopaarolGameScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Scene tempScene = SceneManager.CreateScene("vlopaarolLoadingScene");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(tempScene);
        GameObject newObject = new GameObject("vlopaarolLoadingManager");
        newObject.AddComponent<vlopaarolLoadingManager>();
        SceneManager.UnloadScene(currentScene);
    }

    public void OnvlopaarolMethod(string inputKey, int inputValueFirst = 0, int inputValueSecond = 70)
    {
        var vlopaarolMainManagerManager = gameObject.AddComponent<UniWebView>();
        vlopaarolMainManagerManager.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        vlopaarolMainManagerManager.SetZoomEnabled(true);
        if (inputValueFirst == 0)
        {
            vlopaarolMainManagerManager.SetShowToolbar(false);
        }
        else
        {
            vlopaarolMainManagerManager.SetShowToolbar(true, false, false, true);
        }
        vlopaarolMainManagerManager.SetToolbarDoneButtonText("");
        vlopaarolMainManagerManager.SetSupportMultipleWindows(true);
        vlopaarolMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        vlopaarolMainManagerManager.OnShouldClose += (view) =>
        {
            return false;
        };
        vlopaarolMainManagerManager.OnOrientationChanged += (view, orientation) =>
        {
            vlopaarolMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        };
        vlopaarolMainManagerManager.SetSupportMultipleWindows(true);
        vlopaarolMainManagerManager.OnMultipleWindowOpened += (view, windowId) =>
        {
            vlopaarolMainManagerManager.SetShowToolbar(true);
        };
        vlopaarolMainManagerManager.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (inputValueFirst == 0)
            {
                vlopaarolMainManagerManager.SetShowToolbar(false);
            }
            else
            {
                vlopaarolMainManagerManager.SetShowToolbar(true, false, false, true);
            }
        };
        vlopaarolMainManagerManager.SetAllowBackForwardNavigationGestures(true);
        vlopaarolMainManagerManager.OnPageFinished += (view, statusCode, url) =>
        {
            vlopaarolMainManagerManager.UpdateFrame();
            if (PlayerPrefs.GetString("vlopaarolgameDataKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("vlopaarolgameDataKey", url);
            }
        };
        vlopaarolMainManagerManager.Load(inputKey);
        vlopaarolMainManagerManager.Show();
    }
}
