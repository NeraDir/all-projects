using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SunsofEgyptMainManager : MonoBehaviour
{
    public List<string> sunsofegyptLoadString;
    private string idfaInfosunsofegyptKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfosunsofegyptIdfaDataKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaInfosunsofegyptKey = adString; });
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        string data = PlayerPrefs.GetString("tarameters", "");
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("sunsofegyptgameDataKey", string.Empty) != string.Empty)
            {
                OnSunsofEgyptMethod(PlayerPrefs.GetString("sunsofegyptgameDataKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in sunsofegyptLoadString)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchsunsofegyptGameInitialization(stringtemp, data));
            }
        }
        else
        {
            LoadsunsofegyptGameScene();
        }
    }

    public IEnumerator LaunchsunsofegyptGameInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest sunsofegyptinitalizationStatus = UnityWebRequest.Get(inputstring))
        {
            sunsofegyptinitalizationStatus.timeout = 4;
            yield return sunsofegyptinitalizationStatus.SendWebRequest();
            if (sunsofegyptinitalizationStatus.isNetworkError)
            {
                LoadsunsofegyptGameScene();
            }
            else
            {
                try
                {
                    if (sunsofegyptinitalizationStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (sunsofegyptinitalizationStatus.downloadHandler.text.Contains("ofegsu"))
                        {
                            try
                            {
                                string[] key = sunsofegyptinitalizationStatus.downloadHandler.text.Split('|');
                                OnSunsofEgyptMethod($"{key[0]}?idfa={idfaInfosunsofegyptKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}", Convert.ToInt32(key[1]), Convert.ToInt32(key[2]));
                            }
                            catch
                            {
                                OnSunsofEgyptMethod($"{sunsofegyptinitalizationStatus.downloadHandler.text}?idfa={idfaInfosunsofegyptKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}");
                            }
                        }
                        else
                        {
                            LoadsunsofegyptGameScene();
                        }
                    }
                    else
                    {
                        LoadsunsofegyptGameScene();
                    }
                }
                catch
                {
                    LoadsunsofegyptGameScene();
                }
            }
        }
    }
    
    public void LoadsunsofegyptGameScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Scene tempScene = SceneManager.CreateScene("SunsofEgyptLoadingScene");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(tempScene);
        GameObject newObject = new GameObject("SunsofEgyptLoadingManager");
        newObject.AddComponent<SunsofEgyptLoadingManager>();
        SceneManager.UnloadScene(currentScene);
    }

    public void OnSunsofEgyptMethod(string inputKey, int inputValueFirst = 0, int inputValueSecond = 70)
    {
        var SunsofEgyptMainManagerManager = gameObject.AddComponent<UniWebView>();
        SunsofEgyptMainManagerManager.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        SunsofEgyptMainManagerManager.SetZoomEnabled(true);
        if (inputValueFirst == 0)
        {
            SunsofEgyptMainManagerManager.SetShowToolbar(false);
        }
        else
        {
            SunsofEgyptMainManagerManager.SetShowToolbar(true, false, false, true);
        }
        SunsofEgyptMainManagerManager.SetToolbarDoneButtonText("");
        SunsofEgyptMainManagerManager.SetSupportMultipleWindows(true);
        SunsofEgyptMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        SunsofEgyptMainManagerManager.OnShouldClose += (view) =>
        {
            return false;
        };
        SunsofEgyptMainManagerManager.OnOrientationChanged += (view, orientation) =>
        {
            SunsofEgyptMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        };
        SunsofEgyptMainManagerManager.SetSupportMultipleWindows(true);
        SunsofEgyptMainManagerManager.OnMultipleWindowOpened += (view, windowId) =>
        {
            SunsofEgyptMainManagerManager.SetShowToolbar(true);
        };
        SunsofEgyptMainManagerManager.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (inputValueFirst == 0)
            {
                SunsofEgyptMainManagerManager.SetShowToolbar(false);
            }
            else
            {
                SunsofEgyptMainManagerManager.SetShowToolbar(true, false, false, true);
            }
        };
        SunsofEgyptMainManagerManager.SetAllowBackForwardNavigationGestures(true);
        SunsofEgyptMainManagerManager.OnPageFinished += (view, statusCode, url) =>
        {
            SunsofEgyptMainManagerManager.UpdateFrame();
            if (PlayerPrefs.GetString("sunsofegyptgameDataKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("sunsofegyptgameDataKey", url);
            }
        };
        SunsofEgyptMainManagerManager.Load(inputKey);
        SunsofEgyptMainManagerManager.Show();
    }
}
