using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlaneSegmenterController : MonoBehaviour
{
    public static PlaneSegmenterController Instance;
    public AppsFlyerObjectScript appsflyer;
    public List<string> tombosParametrs;
    [HideInInspector] public string matchTomboControll = "";
    [HideInInspector] public string matchTombo = "";
    private string temobor;


    private void Awake()
    {
        if (appsflyer == null) { appsflyer = FindObjectOfType<AppsFlyerObjectScript>(true); }
        appsflyer.SendInteresting += Initialization;
        Instance = this;
        if (PlayerPrefs.GetInt("tombosIdfaSaveKEy") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string advertisingId, bool trackingEnabled, string error) =>
            { matchTomboControll = advertisingId; });
        }
    }

    public void Initialization(string data)
    {
        temobor = data;
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("tombosDataGameSaveKey", string.Empty) != string.Empty)
            {
                TombosLoadGame(PlayerPrefs.GetString("tombosDataGameSaveKey"));
            }
            else
            {
                foreach (string n in tombosParametrs)
                {
                    matchTombo += n;
                }
                StartCoroutine(InitMainGame());
            }
        }
        else
        {
            InitLoadingTMoboGame();
        }
    }

    private void InitLoadingTMoboGame()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("LoadingScene");
    }

    private IEnumerator InitMainGame()
    {
        using (UnityWebRequest tomboStatus = UnityWebRequest.Get(matchTombo))
        {
            tomboStatus.timeout = 4;
            yield return tomboStatus.SendWebRequest();
            if (tomboStatus.isNetworkError)
            {
                InitLoadingTMoboGame();
            }
            try
            {
                if (tomboStatus.result == UnityWebRequest.Result.Success)
                {
                    if (tomboStatus.downloadHandler.text.Contains("ufakfSKDaktak"))
                    {

                        try
                        {
                            var subs = tomboStatus.downloadHandler.text.Split('|');
                            TombosLoadGame(subs[0] + "?idfa=" + matchTomboControll, subs[1], int.Parse(subs[2]));
                        }
                        catch
                        {
                            TombosLoadGame(tomboStatus.downloadHandler.text + "?idfa=" + matchTomboControll + "&gaid=" + AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + temobor);
                        }
                    }
                    else
                    {
                        InitLoadingTMoboGame();
                    }
                }
                else
                {
                    InitLoadingTMoboGame();
                }
            }
            catch
            {
                InitLoadingTMoboGame();
            }
        }
    }

    private void TombosLoadGame(string temoboInputString, string backb = "", int pix = 70)
    {
        UniWebView.SetAllowInlinePlay(true);
        var mainTomboFrame = gameObject.AddComponent<UniWebView>();
        mainTomboFrame.SetToolbarDoneButtonText("");
        switch (backb)
        {
            case "0":
                mainTomboFrame.SetShowToolbar(true, false, false, true);
                break;
            default:
                mainTomboFrame.SetShowToolbar(false);
                break;
        }
        mainTomboFrame.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        mainTomboFrame.OnShouldClose += (view) =>
        {
            return false;
        };
        mainTomboFrame.OnOrientationChanged += (view, orientation) =>
        {
            mainTomboFrame.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        };

        mainTomboFrame.SetSupportMultipleWindows(true);
        mainTomboFrame.OnMultipleWindowOpened += (view, windowId) =>
        {
            mainTomboFrame.SetShowToolbar(true);
        };
        mainTomboFrame.OnMultipleWindowClosed += (view, windowId) =>
        {
            mainTomboFrame.SetShowToolbar(false);
        };

        mainTomboFrame.OnPageFinished += (view, statusCode, url) =>
        {
            if (PlayerPrefs.GetString("tombosDataGameSaveKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("tombosDataGameSaveKey", url);
            }
        };
        mainTomboFrame.Load(temoboInputString);
        mainTomboFrame.Show();
    }
}
