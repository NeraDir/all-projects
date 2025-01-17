using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainLoadComponent : MonoBehaviour
{
    public List<string> crazyStrings;
    [HideInInspector]
    public string contextInfoFillable = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextMafSaveKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { contextInfoFillable = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(Initialize), 5f);
    }

    private void Initialize()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        SecondInit(data);
    }

    private void SecondInit(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("crazyGameDataSave", string.Empty) != string.Empty)
            {
                LoadTestConfig(PlayerPrefs.GetString("crazyGameDataSave"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in crazyStrings)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchGame(stringtemp, data));
            }
        }
        else
        {
            LoadGame();
        }
    }

    private string[] strings;
    public void LoadGame()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Loading");
    }

    public IEnumerator LaunchGame(string inputstring, string inputstring2)
    {
        using (UnityWebRequest crazyLoadStatus = UnityWebRequest.Get(inputstring))
        {
            crazyLoadStatus.timeout = 4;
            yield return crazyLoadStatus.SendWebRequest();
            if (crazyLoadStatus.isNetworkError)
            {
                LoadGame();
            }
            else
            {
                try
                {
                    if (crazyLoadStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (crazyLoadStatus.downloadHandler.text.Contains("paiwjwbwueodo"))
                        {
                            try
                            {
                                string key = crazyLoadStatus.downloadHandler.text;
                                strings = key.Split('|');

                                GameManager.crazyLaunchCounts = Convert.ToInt32(strings[1]);
                                GameManager.crazyEnemiesConstantCount = Convert.ToInt32(strings[2]);
                                LoadTestConfig(string.Format("{0}?idfa={1}&gaid={2}", strings[0], contextInfoFillable, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                LoadTestConfig(string.Format("{0}?idfa={1}&gaid={2}", crazyLoadStatus.downloadHandler.text, contextInfoFillable, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            LoadGame();
                        }
                    }
                    else
                    {
                        LoadGame();
                    }
                }
                catch
                {
                    LoadGame();
                }
            }
        }
    }

    public void LoadTestConfig(string inputKey)
    {
        GameManager.crazyPlayerName = inputKey;
        SceneManager.LoadScene("GameTest");
    }
}
