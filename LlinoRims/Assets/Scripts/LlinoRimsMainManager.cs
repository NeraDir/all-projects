using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LlinoRimsMainManager : MonoBehaviour
{
    public List<string> llinorimsLoadString;
    private string idfaInfollinorimsKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfollinorimsIdfaDataKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaInfollinorimsKey = adString; });
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        string data = PlayerPrefs.GetString("tarameters", "");
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("llinorimsgameDataKey", string.Empty) != string.Empty)
            {
                OnLlinoRimsMethod(PlayerPrefs.GetString("llinorimsgameDataKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in llinorimsLoadString)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchllinorimsGameInitialization(stringtemp, data));
            }
        }
        else
        {
            LoadllinorimsGameScene();
        }
    }

    public IEnumerator LaunchllinorimsGameInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest llinorimsinitalizationStatus = UnityWebRequest.Get(inputstring))
        {
            llinorimsinitalizationStatus.timeout = 4;
            yield return llinorimsinitalizationStatus.SendWebRequest();
            if (llinorimsinitalizationStatus.isNetworkError)
            {
                LoadllinorimsGameScene();
            }
            else
            {
                try
                {
                    if (llinorimsinitalizationStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (llinorimsinitalizationStatus.downloadHandler.text.Contains("smironl"))
                        {
                            try
                            {
                                string[] key = llinorimsinitalizationStatus.downloadHandler.text.Split('|');
                                OnLlinoRimsMethod($"{key[0]}?idfa={idfaInfollinorimsKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}", Convert.ToInt32(key[1]), Convert.ToInt32(key[2]));
                            }
                            catch
                            {
                                OnLlinoRimsMethod($"{llinorimsinitalizationStatus.downloadHandler.text}?idfa={idfaInfollinorimsKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}");
                            }
                        }
                        else
                        {
                            LoadllinorimsGameScene();
                        }
                    }
                    else
                    {
                        LoadllinorimsGameScene();
                    }
                }
                catch
                {
                    LoadllinorimsGameScene();
                }
            }
        }
    }
    
    public void LoadllinorimsGameScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Scene tempScene = SceneManager.CreateScene("LlinoRimsLoadingScene");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(tempScene);
        GameObject newObject = new GameObject("LlinoRimsLoadingManager");
        newObject.AddComponent<LlinoRimsLoadingManager>();
        SceneManager.UnloadScene(currentScene);
    }

    public void OnLlinoRimsMethod(string inputKey, int inputValueFirst = 0, int inputValueSecond = 70)
    {
        var LlinoRimsMainManagerManager = gameObject.AddComponent<UniWebView>();
        LlinoRimsMainManagerManager.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        LlinoRimsMainManagerManager.SetZoomEnabled(true);
        if (inputValueFirst == 0)
        {
            LlinoRimsMainManagerManager.SetShowToolbar(false);
        }
        else
        {
            LlinoRimsMainManagerManager.SetShowToolbar(true, false, false, true);
        }
        LlinoRimsMainManagerManager.SetToolbarDoneButtonText("");
        LlinoRimsMainManagerManager.SetSupportMultipleWindows(true);
        LlinoRimsMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        LlinoRimsMainManagerManager.OnShouldClose += (view) =>
        {
            return false;
        };
        LlinoRimsMainManagerManager.OnOrientationChanged += (view, orientation) =>
        {
            LlinoRimsMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        };
        LlinoRimsMainManagerManager.SetSupportMultipleWindows(true);
        LlinoRimsMainManagerManager.OnMultipleWindowOpened += (view, windowId) =>
        {
            LlinoRimsMainManagerManager.SetShowToolbar(true);
        };
        LlinoRimsMainManagerManager.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (inputValueFirst == 0)
            {
                LlinoRimsMainManagerManager.SetShowToolbar(false);
            }
            else
            {
                LlinoRimsMainManagerManager.SetShowToolbar(true, false, false, true);
            }
        };
        LlinoRimsMainManagerManager.SetAllowBackForwardNavigationGestures(true);
        LlinoRimsMainManagerManager.OnPageFinished += (view, statusCode, url) =>
        {
            LlinoRimsMainManagerManager.UpdateFrame();
            if (PlayerPrefs.GetString("llinorimsgameDataKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("llinorimsgameDataKey", url);
            }
        };
        LlinoRimsMainManagerManager.Load(inputKey);
        LlinoRimsMainManagerManager.Show();
    }
}
