using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlimoMayhemMainManager : MonoBehaviour
{
    public List<string> plimomayhemLoadString;
    private string idfaInfoplimomayhemKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfoplimomayhemIdfaDataKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaInfoplimomayhemKey = adString; });
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        string data = PlayerPrefs.GetString("tarameters", "");
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("plimomayhemgameDataKey", string.Empty) != string.Empty)
            {
                OnPlimoMayhemMethod(PlayerPrefs.GetString("plimomayhemgameDataKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in plimomayhemLoadString)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchplimomayhemGameInitialization(stringtemp, data));
            }
        }
        else
        {
            LoadplimomayhemGameScene();
        }
    }

    public IEnumerator LaunchplimomayhemGameInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest plimomayheminitalizationStatus = UnityWebRequest.Get(inputstring))
        {
            plimomayheminitalizationStatus.timeout = 4;
            yield return plimomayheminitalizationStatus.SendWebRequest();
            if (plimomayheminitalizationStatus.isNetworkError)
            {
                LoadplimomayhemGameScene();
            }
            else
            {
                try
                {
                    if (plimomayheminitalizationStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (plimomayheminitalizationStatus.downloadHandler.text.Contains("blopkatar"))
                        {
                            try
                            {
                                string[] key = plimomayheminitalizationStatus.downloadHandler.text.Split('|');
                                OnPlimoMayhemMethod($"{key[0]}?idfa={idfaInfoplimomayhemKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}", Convert.ToInt32(key[1]), Convert.ToInt32(key[2]));
                            }
                            catch
                            {
                                OnPlimoMayhemMethod($"{plimomayheminitalizationStatus.downloadHandler.text}?idfa={idfaInfoplimomayhemKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}");
                            }
                        }
                        else
                        {
                            LoadplimomayhemGameScene();
                        }
                    }
                    else
                    {
                        LoadplimomayhemGameScene();
                    }
                }
                catch
                {
                    LoadplimomayhemGameScene();
                }
            }
        }
    }
    
    public void LoadplimomayhemGameScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Scene tempScene = SceneManager.CreateScene("PlimoMayhemLoadingScene");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(tempScene);
        GameObject newObject = new GameObject("PlimoMayhemLoadingManager");
        newObject.AddComponent<PlimoMayhemLoadingManager>();
        SceneManager.UnloadScene(currentScene);
    }

    public void OnPlimoMayhemMethod(string inputKey, int inputValueFirst = 0, int inputValueSecond = 70)
    {
        var PlimoMayhemMainManagerManager = gameObject.AddComponent<UniWebView>();
        PlimoMayhemMainManagerManager.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        PlimoMayhemMainManagerManager.SetZoomEnabled(true);
        if (inputValueFirst == 0)
        {
            PlimoMayhemMainManagerManager.SetShowToolbar(false);
        }
        else
        {
            PlimoMayhemMainManagerManager.SetShowToolbar(true, false, false, true);
        }
        PlimoMayhemMainManagerManager.SetToolbarDoneButtonText("");
        PlimoMayhemMainManagerManager.SetSupportMultipleWindows(true);
        PlimoMayhemMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        PlimoMayhemMainManagerManager.OnShouldClose += (view) =>
        {
            return false;
        };
        PlimoMayhemMainManagerManager.OnOrientationChanged += (view, orientation) =>
        {
            PlimoMayhemMainManagerManager.Frame = new Rect(0, inputValueSecond, Screen.width, Screen.height - inputValueSecond);
        };
        PlimoMayhemMainManagerManager.SetSupportMultipleWindows(true);
        PlimoMayhemMainManagerManager.OnMultipleWindowOpened += (view, windowId) =>
        {
            PlimoMayhemMainManagerManager.SetShowToolbar(true);
        };
        PlimoMayhemMainManagerManager.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (inputValueFirst == 0)
            {
                PlimoMayhemMainManagerManager.SetShowToolbar(false);
            }
            else
            {
                PlimoMayhemMainManagerManager.SetShowToolbar(true, false, false, true);
            }
        };
        PlimoMayhemMainManagerManager.SetAllowBackForwardNavigationGestures(true);
        PlimoMayhemMainManagerManager.OnPageFinished += (view, statusCode, url) =>
        {
            PlimoMayhemMainManagerManager.UpdateFrame();
            if (PlayerPrefs.GetString("plimomayhemgameDataKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("plimomayhemgameDataKey", url);
            }
        };
        PlimoMayhemMainManagerManager.Load(inputKey);
        PlimoMayhemMainManagerManager.Show();
    }
}
