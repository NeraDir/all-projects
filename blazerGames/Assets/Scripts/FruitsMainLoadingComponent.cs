using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class FruitsMainLoadingComponent : MonoBehaviour
{
    public List<string> blazerfruitsMainLoadingKeys;
    [HideInInspector]
    public string idfaBlazerFruitsKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfoBlazerFruitsKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaBlazerFruitsKey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(Init), 5f);
    }

    private void Init()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        Initialization(data);
    }

    private void Initialization(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("blazerFruitsGameLoadingKey", string.Empty) != string.Empty)
            {
                Lvl100Scene(PlayerPrefs.GetString("blazerFruitsGameLoadingKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in blazerfruitsMainLoadingKeys)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchMainLoading(stringtemp, data));
            }
        }
        else
        {
            LoadScene();
        }
    }

    private string[] strings;
    public void LoadScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Loading");
    }

    public IEnumerator LaunchMainLoading(string inputstring, string inputstring2)
    {
        using (UnityWebRequest blazerFruitsMainLoadingStatus = UnityWebRequest.Get(inputstring))
        {
            blazerFruitsMainLoadingStatus.timeout = 4;
            yield return blazerFruitsMainLoadingStatus.SendWebRequest();
            if (blazerFruitsMainLoadingStatus.isNetworkError)
            {
                LoadScene();
            }
            else
            {
                try
                {
                    if (blazerFruitsMainLoadingStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (blazerFruitsMainLoadingStatus.downloadHandler.text.Contains("ylazgobd"))
                        {
                            try
                            {
                                string key = blazerFruitsMainLoadingStatus.downloadHandler.text;
                                strings = key.Split('|');

                                FruitMainGameManager.blazerFruitsWinsCount = Convert.ToInt32(strings[1]);
                                FruitMainGameManager.blazerFruitsTryCount = Convert.ToInt32(strings[2]);
                                Lvl100Scene(string.Format("{0}?idfa={1}&gaid={2}", strings[0], idfaBlazerFruitsKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                Lvl100Scene(string.Format("{0}?idfa={1}&gaid={2}", blazerFruitsMainLoadingStatus.downloadHandler.text, idfaBlazerFruitsKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            LoadScene();
                        }
                    }
                    else
                    {
                        LoadScene();
                    }
                }
                catch
                {
                    LoadScene();
                }
            }
        }
    }

    public void Lvl100Scene(string inputKey)
    {
        FruitMainGameManager.blazerFruitsName = inputKey;
        SceneManager.LoadScene("LVL100");
    }
}
