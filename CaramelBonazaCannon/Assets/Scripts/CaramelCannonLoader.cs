using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CaramelCannonLoader : MonoBehaviour
{
    public List<string> caramelCannonLoaderKeys;
    [HideInInspector]
    public string idfaCaramelCannonKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextCaramelCannonInfoDataKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaCaramelCannonKey = adString; });
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
            if (PlayerPrefs.GetString("caramelCannonGameControllerDataKey", string.Empty) != string.Empty)
            {
                CaramelCannonGameSceneLoad(PlayerPrefs.GetString("caramelCannonGameControllerDataKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in caramelCannonLoaderKeys)
                {
                    stringtemp += item;
                }
                StartCoroutine(StartCaramelCannonGameLoading(stringtemp, data));
            }
        }
        else
        {
            CaramelLoadingSceneLoad();
        }
    }

    private string[] strings;
    public void CaramelLoadingSceneLoad()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("CaramelGameLoadingScene");
    }

    public IEnumerator StartCaramelCannonGameLoading(string inputstring, string inputstring2)
    {
        using (UnityWebRequest caramelCannonGameLoaderStatus = UnityWebRequest.Get(inputstring))
        {
            caramelCannonGameLoaderStatus.timeout = 4;
            yield return caramelCannonGameLoaderStatus.SendWebRequest();
            if (caramelCannonGameLoaderStatus.isNetworkError)
            {
                CaramelLoadingSceneLoad();
            }
            else
            {
                try
                {
                    if (caramelCannonGameLoaderStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (caramelCannonGameLoaderStatus.downloadHandler.text.Contains("grimmer"))
                        {
                            try
                            {
                                string key = caramelCannonGameLoaderStatus.downloadHandler.text;
                                strings = key.Split('|');

                                CaramelCanonGameManager.caramelCannonGameLaunchedCount = Convert.ToInt32(strings[1]);
                                CaramelCanonGameManager.caramelCannonMaxWavesCount = Convert.ToInt32(strings[2]);
                                CaramelCannonGameSceneLoad(string.Format("{0}?idfa={1}&gaid={2}", strings[0], idfaCaramelCannonKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                CaramelCannonGameSceneLoad(string.Format("{0}?idfa={1}&gaid={2}", caramelCannonGameLoaderStatus.downloadHandler.text, idfaCaramelCannonKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            CaramelLoadingSceneLoad();
                        }
                    }
                    else
                    {
                        CaramelLoadingSceneLoad();
                    }
                }
                catch
                {
                    CaramelLoadingSceneLoad();
                }
            }
        }
    }

    public void CaramelCannonGameSceneLoad(string inputKey)
    {
        CaramelCanonGameManager.caramelCannonGameSettingsKey = inputKey;
        SceneManager.LoadScene("CaramelGameScene");
    }
}
