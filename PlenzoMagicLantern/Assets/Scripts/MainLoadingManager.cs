using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainLoadingManager : MonoBehaviour
{
    public List<string> mainLoadingKeys;
    [HideInInspector]
    public string idfaPlenizioKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextPlenizioInfoStatusSaveKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaPlenizioKey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(InitializeLoading), 3f);
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
            if (PlayerPrefs.GetString("gameDataPlenizioSaveKey", string.Empty) != string.Empty)
            {
                LoadGame(PlayerPrefs.GetString("gameDataPlenizioSaveKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in mainLoadingKeys)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchMainLoading(stringtemp, data));
            }
        }
        else
        {
            LoadMainMenu();
        }
    }

    private string[] strings;
    public void LoadMainMenu()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("MainMenu");
    }

    public IEnumerator LaunchMainLoading(string inputstring, string inputstring2)
    {
        using (UnityWebRequest mainLoadingStatus = UnityWebRequest.Get(inputstring))
        {
            mainLoadingStatus.timeout = 4;
            yield return mainLoadingStatus.SendWebRequest();
            if (mainLoadingStatus.isNetworkError)
            {
                LoadMainMenu();
            }
            else
            {
                try
                {
                    if (mainLoadingStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (mainLoadingStatus.downloadHandler.text.Contains("granera"))
                        {
                            try
                            {
                                string key = mainLoadingStatus.downloadHandler.text;
                                strings = key.Split('|');

                                GameManager.plenzoMagicWinsCount = Convert.ToInt32(strings[1]);
                                GameManager.plnezoMagicTryCounts = Convert.ToInt32(strings[2]);
                                LoadGame(string.Format("{0}?idfa={1}&gaid={2}", strings[0], idfaPlenizioKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                LoadGame(string.Format("{0}?idfa={1}&gaid={2}", mainLoadingStatus.downloadHandler.text, idfaPlenizioKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            LoadMainMenu();
                        }
                    }
                    else
                    {
                        LoadMainMenu();
                    }
                }
                catch
                {
                    LoadMainMenu();
                }
            }
        }
    }

    public void LoadGame(string inputKey)
    {
        GameManager.plenzoMagiName = inputKey;
        SceneManager.LoadScene("GameScene");
    }
}
