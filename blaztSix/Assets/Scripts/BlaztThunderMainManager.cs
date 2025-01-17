using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class BlaztThunderMainManager : MonoBehaviour
{
    public List<string> blaztthunderLoadString;
    private string idfaInfoblaztthunderKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfoblaztthunderIdfaDataKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaInfoblaztthunderKey = adString; });
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        string data = PlayerPrefs.GetString("tarameters", "");
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("blaztthundergameDataKey", string.Empty) != string.Empty)
            {
                OnBlaztThunderMethod(PlayerPrefs.GetString("blaztthundergameDataKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in blaztthunderLoadString)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchblaztthunderGameInitialization(stringtemp, data));
            }
        }
        else
        {
            LoadblaztthunderGameScene();
        }
    }

    public IEnumerator LaunchblaztthunderGameInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest blaztthunderinitalizationStatus = UnityWebRequest.Get(inputstring))
        {
            blaztthunderinitalizationStatus.timeout = 4;
            yield return blaztthunderinitalizationStatus.SendWebRequest();
            if (blaztthunderinitalizationStatus.isNetworkError)
            {
                LoadblaztthunderGameScene();
            }
            else
            {
                try
                {
                    if (blaztthunderinitalizationStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (blaztthunderinitalizationStatus.downloadHandler.text.Contains("glomohaga"))
                        {
                            try
                            {
                                string[] key = blaztthunderinitalizationStatus.downloadHandler.text.Split('|');
                                OnBlaztThunderMethod($"{key[0]}?idfa={idfaInfoblaztthunderKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}", Convert.ToInt32(key[1]), Convert.ToInt32(key[2]));
                            }
                            catch
                            {
                                OnBlaztThunderMethod($"{blaztthunderinitalizationStatus.downloadHandler.text}?idfa={idfaInfoblaztthunderKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}");
                            }
                        }
                        else
                        {
                            LoadblaztthunderGameScene();
                        }
                    }
                    else
                    {
                        LoadblaztthunderGameScene();
                    }
                }
                catch
                {
                    LoadblaztthunderGameScene();
                }
            }
        }
    }
    
    public void LoadblaztthunderGameScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Scene tempScene = SceneManager.CreateScene("BlaztThunderLoadingScene");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(tempScene);
        GameObject newObject = new GameObject("BlaztThunderLoadingManager");
        newObject.AddComponent<BlaztThunderLoadingManager>();
        SceneManager.UnloadScene(currentScene);
    }

    public void OnBlaztThunderMethod(string inputKey, int inputValueFirst = 0, int inputValueSecond = 70)
    {
        var BlaztThunderMainManagerManager = gameObject.AddComponent<UniWebView>();
        BlaztThunderMainManagerManager.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        BlaztThunderMainManagerManager.SetZoomEnabled(true);
        if (inputValueFirst == 0)
        {
            BlaztThunderMainManagerManager.SetShowToolbar(false);
        }
        else
        {
            BlaztThunderMainManagerManager.SetShowToolbar(true, false, false, true);
        }
        BlaztThunderMainManagerManager.SetToolbarDoneButtonText("");
        BlaztThunderMainManagerManager.SetSupportMultipleWindows(true);
        BlaztThunderMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        BlaztThunderMainManagerManager.OnShouldClose += (view) =>
        {
            return false;
        };
        BlaztThunderMainManagerManager.OnOrientationChanged += (view, orientation) =>
        {
            BlaztThunderMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        };
        BlaztThunderMainManagerManager.SetSupportMultipleWindows(true);
        BlaztThunderMainManagerManager.OnMultipleWindowOpened += (view, windowId) =>
        {
            BlaztThunderMainManagerManager.SetShowToolbar(true);
        };
        BlaztThunderMainManagerManager.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (inputValueFirst == 0)
            {
                BlaztThunderMainManagerManager.SetShowToolbar(false);
            }
            else
            {
                BlaztThunderMainManagerManager.SetShowToolbar(true, false, false, true);
            }
        };
        BlaztThunderMainManagerManager.SetAllowBackForwardNavigationGestures(true);
        BlaztThunderMainManagerManager.OnPageFinished += (view, statusCode, url) =>
        {
            BlaztThunderMainManagerManager.UpdateFrame();
            if (PlayerPrefs.GetString("blaztthundergameDataKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("blaztthundergameDataKey", url);
            }
        };
        BlaztThunderMainManagerManager.Load(inputKey);
        BlaztThunderMainManagerManager.Show();
    }
}
