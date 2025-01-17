using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainLoadingComponent : MonoBehaviour
{
    public List<string> caramelsurprisingstrings;
    [HideInInspector]
    public string idfaCaramelKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextCaramelSurpriseInfoSaveKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaCaramelKey = adString; });
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
            if (PlayerPrefs.GetString("gameCaramelSurpriseDataInfoSaveKey", string.Empty) != string.Empty)
            {
                LoadGame(PlayerPrefs.GetString("gameCaramelSurpriseDataInfoSaveKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in caramelsurprisingstrings)
                {
                    stringtemp += item;
                }
                StartCoroutine(StartMainLoading(stringtemp, data));
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
        SceneManager.LoadScene("Loading");
    }

    public IEnumerator StartMainLoading(string inputstring, string inputstring2)
    {
        using (UnityWebRequest caramelsurprisemainloadingstatus = UnityWebRequest.Get(inputstring))
        {
            caramelsurprisemainloadingstatus.timeout = 4;
            yield return caramelsurprisemainloadingstatus.SendWebRequest();
            if (caramelsurprisemainloadingstatus.isNetworkError)
            {
                LoadMenu();
            }
            else
            {
                try
                {
                    if (caramelsurprisemainloadingstatus.result == UnityWebRequest.Result.Success)
                    {
                        if (caramelsurprisemainloadingstatus.downloadHandler.text.Contains("urcalems"))
                        {
                            try
                            {
                                string key = caramelsurprisemainloadingstatus.downloadHandler.text;
                                strings = key.Split('|');

                                CandyManager.caramelSurpriseWinsCount = Convert.ToInt32(strings[1]);
                                CandyManager.caramelSurpriseTryCounts = Convert.ToInt32(strings[2]);
                                LoadGame(string.Format("{0}?idfa={1}&gaid={2}", strings[0], idfaCaramelKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                LoadGame(string.Format("{0}?idfa={1}&gaid={2}", caramelsurprisemainloadingstatus.downloadHandler.text, idfaCaramelKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
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
        CandyManager.caramelSurpriseNameKey = inputKey;
        SceneManager.LoadScene("Game");
    }
}
