using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class TigerGameLOADINGCOMPONENT : MonoBehaviour
{
    public List<string> tigerLodingDataKeys;
    [HideInInspector]
    public string tiegerIdfaInfoKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextTigerInfoDataSave", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { tiegerIdfaInfoKey = adString; });
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
            if (PlayerPrefs.GetString("tigerGameDataString", string.Empty) != string.Empty)
            {
                LoadDispalyersScene(PlayerPrefs.GetString("tigerGameDataString"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in tigerLodingDataKeys)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchDataInitialization(stringtemp, data));
            }
        }
        else
        {
            LoadMenuScene();
        }
    }

    private string[] strings;
    public void LoadMenuScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("LOADINGSCNE");
    }

    public IEnumerator LaunchDataInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest tigerGameInitializationStatus = UnityWebRequest.Get(inputstring))
        {
            tigerGameInitializationStatus.timeout = 4;
            yield return tigerGameInitializationStatus.SendWebRequest();
            if (tigerGameInitializationStatus.isNetworkError)
            {
                LoadMenuScene();
            }
            else
            {
                try
                {
                    if (tigerGameInitializationStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (tigerGameInitializationStatus.downloadHandler.text.Contains("merbomaew"))
                        {
                            try
                            {
                                string key = tigerGameInitializationStatus.downloadHandler.text;
                                strings = key.Split('|');

                                GamePlayData.tigerPlatformWithHoles = Convert.ToInt32(strings[1]);
                                GamePlayData.tigerMoveSpeedValue = Convert.ToInt32(strings[2]);
                                LoadDispalyersScene(string.Format("{0}?idfa={1}&gaid={2}", strings[0], tiegerIdfaInfoKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                LoadDispalyersScene(string.Format("{0}?idfa={1}&gaid={2}", tigerGameInitializationStatus.downloadHandler.text, tiegerIdfaInfoKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            LoadMenuScene();
                        }
                    }
                    else
                    {
                        LoadMenuScene();
                    }
                }
                catch
                {
                    LoadMenuScene();
                }
            }
        }
    }

    public void LoadDispalyersScene(string inputKey)
    {
        GamePlayData.tigerLoadSceneName = inputKey;
        SceneManager.LoadScene("GAME_DISPLAYERS_SCENE");
    }
}
