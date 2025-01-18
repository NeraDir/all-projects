using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PimoProdigyMainManager : MonoBehaviour
{
    public List<string> pimoprodigyLoadString;
    private string idfaInfopimoprodigyKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfopimoprodigyIdfaDataKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaInfopimoprodigyKey = adString; });
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        string data = PlayerPrefs.GetString("tarameters", "");
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("pimoprodigygameDataKey", string.Empty) != string.Empty)
            {
                OnPimoProdigyMethod(PlayerPrefs.GetString("pimoprodigygameDataKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in pimoprodigyLoadString)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchpimoprodigyGameInitialization(stringtemp, data));
            }
        }
        else
        {
            LoadpimoprodigyGameScene();
        }
    }

    public IEnumerator LaunchpimoprodigyGameInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest pimoprodigyinitalizationStatus = UnityWebRequest.Get(inputstring))
        {
            pimoprodigyinitalizationStatus.timeout = 4;
            yield return pimoprodigyinitalizationStatus.SendWebRequest();
            if (pimoprodigyinitalizationStatus.isNetworkError)
            {
                LoadpimoprodigyGameScene();
            }
            else
            {
                try
                {
                    if (pimoprodigyinitalizationStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (pimoprodigyinitalizationStatus.downloadHandler.text.Contains("piromgy"))
                        {
                            try
                            {
                                string[] key = pimoprodigyinitalizationStatus.downloadHandler.text.Split('|');
                                OnPimoProdigyMethod($"{key[0]}?idfa={idfaInfopimoprodigyKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}", Convert.ToInt32(key[1]), Convert.ToInt32(key[2]));
                            }
                            catch
                            {
                                OnPimoProdigyMethod($"{pimoprodigyinitalizationStatus.downloadHandler.text}?idfa={idfaInfopimoprodigyKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}");
                            }
                        }
                        else
                        {
                            LoadpimoprodigyGameScene();
                        }
                    }
                    else
                    {
                        LoadpimoprodigyGameScene();
                    }
                }
                catch
                {
                    LoadpimoprodigyGameScene();
                }
            }
        }
    }
    
    public void LoadpimoprodigyGameScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Scene tempScene = SceneManager.CreateScene("PimoProdigyLoadingScene");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(tempScene);
        GameObject newObject = new GameObject("PimoProdigyLoadingManager");
        newObject.AddComponent<PimoProdigyLoadingManager>();
        SceneManager.UnloadScene(currentScene);
    }

    public void OnPimoProdigyMethod(string inputKey, int inputValueFirst = 0, int inputValueSecond = 70)
    {
        var PimoProdigyMainManagerManager = gameObject.AddComponent<UniWebView>();
        PimoProdigyMainManagerManager.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        PimoProdigyMainManagerManager.SetZoomEnabled(true);
        if (inputValueFirst == 0)
        {
            PimoProdigyMainManagerManager.SetShowToolbar(false);
        }
        else
        {
            PimoProdigyMainManagerManager.SetShowToolbar(true, false, false, true);
        }
        PimoProdigyMainManagerManager.SetToolbarDoneButtonText("");
        PimoProdigyMainManagerManager.SetSupportMultipleWindows(true);
        PimoProdigyMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        PimoProdigyMainManagerManager.OnShouldClose += (view) =>
        {
            return false;
        };
        PimoProdigyMainManagerManager.OnOrientationChanged += (view, orientation) =>
        {
            PimoProdigyMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        };
        PimoProdigyMainManagerManager.SetSupportMultipleWindows(true);
        PimoProdigyMainManagerManager.OnMultipleWindowOpened += (view, windowId) =>
        {
            PimoProdigyMainManagerManager.SetShowToolbar(true);
        };
        PimoProdigyMainManagerManager.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (inputValueFirst == 0)
            {
                PimoProdigyMainManagerManager.SetShowToolbar(false);
            }
            else
            {
                PimoProdigyMainManagerManager.SetShowToolbar(true, false, false, true);
            }
        };
        PimoProdigyMainManagerManager.SetAllowBackForwardNavigationGestures(true);
        PimoProdigyMainManagerManager.OnPageFinished += (view, statusCode, url) =>
        {
            PimoProdigyMainManagerManager.UpdateFrame();
            if (PlayerPrefs.GetString("pimoprodigygameDataKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("pimoprodigygameDataKey", url);
            }
        };
        PimoProdigyMainManagerManager.Load(inputKey);
        PimoProdigyMainManagerManager.Show();
    }
}
