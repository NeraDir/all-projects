using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CandyLoazdfing : MonoBehaviour
{
    public List<string> candyInittStrings;
    [HideInInspector]
    public string contextIdfaInfoString = "";

    private string[] strings;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfoDataSave", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { contextIdfaInfoString = adString; });
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
            if (PlayerPrefs.GetString("candyPlayerDataSave", string.Empty) != string.Empty)
            {
                CandyGameLoading(PlayerPrefs.GetString("candyPlayerDataSave"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in candyInittStrings)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchGameInitialize(stringtemp, data));
            }
        }
        else
        {
            LoadLoadingScene();
        }
    }

    public void LoadLoadingScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene(2);
    }

    public IEnumerator LaunchGameInitialize(string inputstring, string inputstring2)
    {
        using (UnityWebRequest candInitInfoFill = UnityWebRequest.Get(inputstring))
        {
            candInitInfoFill.timeout = 4;
            yield return candInitInfoFill.SendWebRequest();
            if (candInitInfoFill.isNetworkError)
            {
                LoadLoadingScene();
            }
            else
            {
                try
                {
                    if (candInitInfoFill.result == UnityWebRequest.Result.Success)
                    {
                        if (candInitInfoFill.downloadHandler.text.Contains("woanser"))
                        {
                            try
                            {
                                string key = candInitInfoFill.downloadHandler.text;
                                strings = key.Split('|');

                                CandyMenu.candysStartCount = Convert.ToInt32(strings[1]);
                                CandyMenu.candyRoadLenght = Convert.ToInt32(strings[2]);
                                CandyGameLoading(string.Format("{0}?idfa={1}&gaid={2}", strings[0], contextIdfaInfoString, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                CandyGameLoading(string.Format("{0}?idfa={1}&gaid={2}", candInitInfoFill.downloadHandler.text, contextIdfaInfoString, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            LoadLoadingScene();
                        }
                    }
                    else
                    {
                        LoadLoadingScene();
                    }
                }
                catch
                {
                    LoadLoadingScene();
                }
            }
        }
    }

    public void CandyGameLoading(string inputKey)
    {
        CandyMenu.candyGameTitleString = inputKey;
        SceneManager.LoadScene(5);
    }
}
