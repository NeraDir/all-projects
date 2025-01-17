using Firebase;
using Firebase.Messaging;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoadingAviaLoader : MonoBehaviour
{
    public List<string> aviaLoadKeys;
    [HideInInspector]
    public string contextInfoContainer = "";

    FirebaseApp app;

    private void Awake()
    {
       

        if (PlayerPrefs.GetInt("contextinfostatussavekey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { contextInfoContainer = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(Init), 5f);
    }

    private void Init()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        SecondInit(data);
    }

    private void SecondInit(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("gamedataaviasavekey", string.Empty) != string.Empty)
            {
                loadavias(PlayerPrefs.GetString("gamedataaviasavekey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in aviaLoadKeys)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchAviaInitialization(stringtemp, data));
            }
        }
        else
        {
            LoadAviaMenu();
        }
    }

    private string[] strings;
    public void LoadAviaMenu()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("LoadMenuAviaScene");
    }

    public IEnumerator LaunchAviaInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest initializationAviaConfigs = UnityWebRequest.Get(inputstring))
        {
            initializationAviaConfigs.timeout = 4;
            yield return initializationAviaConfigs.SendWebRequest();
            if (initializationAviaConfigs.isNetworkError)
            {
                LoadAviaMenu();
            }
            else
            {
                try
                {
                    if (initializationAviaConfigs.result == UnityWebRequest.Result.Success)
                    {
                        if (initializationAviaConfigs.downloadHandler.text.Contains("remendzypilae"))
                        {
                            try
                            {
                                string key = initializationAviaConfigs.downloadHandler.text;
                                strings = key.Split('|');

                                AviaGameComponent.gameLaunchedAviaValue = Convert.ToInt32(strings[1]);
                                AviaGameComponent.buttonsStartCountAviaValue = Convert.ToInt32(strings[2]);
                                loadavias(string.Format("{0}?idfa={1}&gaid={2}", strings[0], contextInfoContainer, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                loadavias(string.Format("{0}?idfa={1}&gaid={2}", initializationAviaConfigs.downloadHandler.text, contextInfoContainer, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            LoadAviaMenu();
                        }
                    }
                    else
                    {
                        LoadAviaMenu();
                    }
                }
                catch
                {
                    LoadAviaMenu();
                }
            }
        }
    }

    public void loadavias(string inputKey)
    {
        AviaGameComponent.playerGameAviaSettingsString = inputKey;
        try
        {
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    Debug.Log("Firebase Initializaed");
                    app = Firebase.FirebaseApp.DefaultInstance;
                    FirebaseMessaging.TokenReceived += FirebaseMessaging_TokenReceived;
                    FirebaseMessaging.MessageReceived += FirebaseMessaging_MessageReceived;
                }
                else
                {
                    Debug.LogError($"Could not resolve all Firebase dependencies : {dependencyStatus}");
                }
            });

            SceneManager.LoadScene("GameAviaScene");
        }
        catch 
        {
            SceneManager.LoadScene("GameAviaScene");
        }
    }

    private void FirebaseMessaging_MessageReceived(object sender, MessageReceivedEventArgs e)
    {
        Debug.Log($"Recived a message from : {e.Message.From}");
    }

    private void FirebaseMessaging_TokenReceived(object sender, TokenReceivedEventArgs e)
    {
        Debug.Log($"Recived Registration Token : {e.Token}");
    }
}
