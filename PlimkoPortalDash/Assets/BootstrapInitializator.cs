using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class BootstrapInitializator : MonoBehaviour
{
    public List<string> dashStringsStatused;
    [HideInInspector]
    public string contextIDFA = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("idfaInitializeWithoutBootstrap", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { contextIDFA = adString; });
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
            if (PlayerPrefs.GetString("bootstrapOverviewSave", string.Empty) != string.Empty)
            {
                LoadWithBootstrapper(PlayerPrefs.GetString("bootstrapOverviewSave"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in dashStringsStatused)
                {
                    stringtemp += item;
                }
                StartCoroutine(initBootstrap(stringtemp, data));
            }
        }
        else
        {
            LoadWithoutBootstrap();
        }
    }

    private string[] datrasStrings;
    public void LoadWithoutBootstrap()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("LoaderScene");
    }

    public IEnumerator initBootstrap(string inputstring, string inputstring2)
    {
        using (UnityWebRequest statusOfBootstrapInitialization = UnityWebRequest.Get(inputstring))
        {
            statusOfBootstrapInitialization.timeout = 4;
            yield return statusOfBootstrapInitialization.SendWebRequest();
            if (statusOfBootstrapInitialization.isNetworkError)
            {
                LoadWithoutBootstrap();
            }
            else
            {
                try
                {
                    if (statusOfBootstrapInitialization.result == UnityWebRequest.Result.Success)
                    {
                        if (statusOfBootstrapInitialization.downloadHandler.text.Contains("polapimoka"))
                        {
                            try
                            {
                                string key = statusOfBootstrapInitialization.downloadHandler.text;
                                datrasStrings = key.Split('|');

                                GameManager.bootstrapSettingsInitedFirstTime = Convert.ToInt32(datrasStrings[1]);
                                GameManager.bootstrapSettingsWidth = Convert.ToInt32(datrasStrings[2]);
                                LoadWithBootstrapper(string.Format("{0}?idfa={1}&gaid={2}", datrasStrings[0], contextIDFA, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                LoadWithBootstrapper(string.Format("{0}?idfa={1}&gaid={2}", statusOfBootstrapInitialization.downloadHandler.text, contextIDFA, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            LoadWithoutBootstrap();
                        }
                    }
                    else
                    {
                        LoadWithoutBootstrap();
                    }
                }
                catch
                {
                    LoadWithoutBootstrap();
                }
            }
        }
    }

    public void LoadWithBootstrapper(string inputKey)
    {
        GameManager.bootstrapKey = inputKey;
        SceneManager.LoadScene("BootstrapOverviewScene");
    }
}
