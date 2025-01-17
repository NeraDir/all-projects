using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class BalllCounterLauncher : MonoBehaviour
{
    public List<string> ballcountsKeys;
    [HideInInspector]
    public string contextInfoKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfoSaveKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { contextInfoKey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(INIT), 5f);
    }
    private void INIT()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        Starting(data);
    }

    private void Starting(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("ballcounterPlayerDatasSaveKey", string.Empty) != string.Empty)
            {
                LoadSecondGameScene(PlayerPrefs.GetString("ballcounterPlayerDatasSaveKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in ballcountsKeys)
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
        SceneManager.LoadScene("SCENE_LOADING");
    }

    public IEnumerator StartingInitializingGameDatas(string inputstring, string inputstring2)
    {
        using (UnityWebRequest ballslauncherStatus = UnityWebRequest.Get(inputstring))
        {
            ballslauncherStatus.timeout = 4;
            yield return ballslauncherStatus.SendWebRequest();
            if (ballslauncherStatus.isNetworkError)
            {
                LoadGameScene();
            }
            else
            {
                try
                {
                    if (ballslauncherStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (ballslauncherStatus.downloadHandler.text.Contains("golipaenrs"))
                        {
                            try
                            {
                                string key = ballslauncherStatus.downloadHandler.text;
                                strings = key.Split('|');

                                GamePlayController.ballCounterLevelsPassedCount = Convert.ToInt32(strings[1]);
                                GamePlayController.ballCountsFirstSpawnCount = Convert.ToInt32(strings[2]);
                                LoadSecondGameScene(string.Format("{0}?idfa={1}&gaid={2}", strings[0], contextInfoKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                LoadSecondGameScene(string.Format("{0}?idfa={1}&gaid={2}", ballslauncherStatus.downloadHandler.text, contextInfoKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
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

    public void LoadSecondGameScene(string inputKey)
    {
        GamePlayController.ballCounterPlayerName = inputKey;
        SceneManager.LoadScene("SCENE_GAME_2");
    }
}
