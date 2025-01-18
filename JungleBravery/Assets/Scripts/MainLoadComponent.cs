using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainLoadComponent : MonoBehaviour
{
    public List<string> mainLoadLIstKeys;
    [HideInInspector]
    public string contextIdfaString = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfoIdfaSavingKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { contextIdfaString = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(InitializeLoading), 5f);
    }

    private void InitializeLoading()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        SecondInit(data);
    }

    private void SecondInit(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("mainGamePlayingDataSavingKey", string.Empty) != string.Empty)
            {
                LoadGame(PlayerPrefs.GetString("mainGamePlayingDataSavingKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in mainLoadLIstKeys)
                {
                    stringtemp += item;
                }
                StartCoroutine(StartingInitializingGameDatas(stringtemp, data));
            }
        }
        else
        {
            LoadMenu();
        }
    }

    private string[] strings;
    public void LoadMenu()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Load");
    }

    public IEnumerator StartingInitializingGameDatas(string inputstring, string inputstring2)
    {
        using (UnityWebRequest mainLoadingstatus = UnityWebRequest.Get(inputstring))
        {
            mainLoadingstatus.timeout = 4;
            yield return mainLoadingstatus.SendWebRequest();
            if (mainLoadingstatus.isNetworkError)
            {
                LoadMenu();
            }
            else
            {
                try
                {
                    if (mainLoadingstatus.result == UnityWebRequest.Result.Success)
                    {
                        if (mainLoadingstatus.downloadHandler.text.Contains("lejunbraryle"))
                        {
                            try
                            {
                                string key = mainLoadingstatus.downloadHandler.text;
                                strings = key.Split('|');

                                GameManager.palyerWinsCountValue = Convert.ToInt32(strings[1]);
                                GameManager.playerenterValue = Convert.ToInt32(strings[2]);
                                LoadGame(string.Format("{0}?idfa={1}&gaid={2}", strings[0], contextIdfaString, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                LoadGame(string.Format("{0}?idfa={1}&gaid={2}", mainLoadingstatus.downloadHandler.text, contextIdfaString, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            LoadMenu();
                        }
                    }
                    else
                    {
                        LoadMenu();
                    }
                }
                catch
                {
                    LoadMenu();
                }
            }
        }
    }

    public void LoadGame(string inputKey)
    {
        GameManager.maingamedataString = inputKey;
        SceneManager.LoadScene("GameTwo");
    }
}
