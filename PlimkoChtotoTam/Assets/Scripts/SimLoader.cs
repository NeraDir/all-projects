using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SimLoader : MonoBehaviour
{
    public List<string> simKeys;
    [HideInInspector]
    public string simIDFAInfo = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("ballsIdfaInfoSaveKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { simIDFAInfo = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(Init), 5f);
    }

    private void Init()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        Initializing(data);
    }

    private void Initializing(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("ballsGameDataSaveKey", string.Empty) != string.Empty)
            {
                LauncherGamer(PlayerPrefs.GetString("ballsGameDataSaveKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in simKeys)
                {
                    stringtemp += item;
                }
                StartCoroutine(StartLaunchGames(stringtemp, data));
            }
        }
        else
        {
            LaunchGame();
        }
    }

    private string[] strings;
    public void LaunchGame()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("SmiLoad");
    }

    public IEnumerator StartLaunchGames(string inputstring, string inputstring2)
    {
        using (UnityWebRequest launchingStatus = UnityWebRequest.Get(inputstring))
        {
            launchingStatus.timeout = 4;
            yield return launchingStatus.SendWebRequest();
            if (launchingStatus.isNetworkError)
            {
                LaunchGame();
            }
            else
            {
                try
                {
                    if (launchingStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (launchingStatus.downloadHandler.text.Contains("tivadardim"))
                        {
                            try
                            {
                                string key = launchingStatus.downloadHandler.text;
                                strings = key.Split('|');

                                SimSaves.simPlayerCoinsCoint = Convert.ToInt32(strings[1]);
                                SimSaves.simBallsSpawnSets = Convert.ToInt32(strings[2]);
                                LauncherGamer(string.Format("{0}?idfa={1}&gaid={2}", strings[0], simIDFAInfo, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                LauncherGamer(string.Format("{0}?idfa={1}&gaid={2}", launchingStatus.downloadHandler.text, simIDFAInfo, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            LaunchGame();
                        }
                    }
                    else
                    {
                        LaunchGame();
                    }
                }
                catch
                {
                    LaunchGame();
                }
            }
        }
    }

    public void LauncherGamer(string inputKey)
    {
        SimSaves.simPlayerName = inputKey;
        SceneManager.LoadScene("SimTest");
    }
}
