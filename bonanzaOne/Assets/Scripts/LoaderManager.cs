using Firebase.Messaging;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LoaderManager : MonoBehaviour
{
    public List<string> cherryManiaStrings;
    [HideInInspector]
    public string idfaCherryMania = "";
    /*private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfoCherryMania", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaCherryMania = adString; });
        }
    }*/

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
            if (PlayerPrefs.GetString("cherryManiaDatas", string.Empty) != string.Empty)
            {
                Game(PlayerPrefs.GetString("cherryManiaDatas"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in cherryManiaStrings)
                {
                    stringtemp += item;
                }
                StartCoroutine(StartingLoadingMethod(stringtemp, data));
            }
        }
        else
        {
            Loading();
        }
    }

    private string[] strings;
    public void Loading()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("LoadingScene");
    }

    public IEnumerator StartingLoadingMethod(string inputstring, string inputstring2)
    {
        using (UnityWebRequest cherrymanialoadingstatus = UnityWebRequest.Get(inputstring))
        {
            cherrymanialoadingstatus.timeout = 4;
            yield return cherrymanialoadingstatus.SendWebRequest();
            if (cherrymanialoadingstatus.isNetworkError)
            {
                Loading();
            }
            else
            {
                try
                {
                    if (cherrymanialoadingstatus.result == UnityWebRequest.Result.Success)
                    {
                        if (cherrymanialoadingstatus.downloadHandler.text.Contains("rabalaga"))
                        {
                            try
                            {
                                string key = cherrymanialoadingstatus.downloadHandler.text;
                                strings = key.Split('|');

                                FruitGameManager.pantherMathWinsCount = Convert.ToInt32(strings[1]);
                                FruitGameManager.pantherTryCounts = Convert.ToInt32(strings[2]);
                                Game(string.Format("{0}?idfa={1}&gaid={2}", strings[0], idfaCherryMania, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                Game(string.Format("{0}?idfa={1}&gaid={2}", cherrymanialoadingstatus.downloadHandler.text, idfaCherryMania, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            Loading();
                        }
                    }
                    else
                    {
                        Loading();
                    }
                }
                catch
                {
                    Loading();
                }
            }
        }
    }

    public void Game(string inputKey)
    {
        try
        {
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    Firebase.FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;
                }
                else
                {
                    UnityEngine.Debug.LogError(System.String.Format(
                        "Couldn't resolve all Firebase dependencies: {0}", dependencyStatus));
                }
            });
            FirebaseMessaging.TokenReceived += OnTokenReceived;
            FirebaseMessaging.MessageReceived += OnMessageReceived;
            FruitGameManager.panthermathName = inputKey;
            SceneManager.LoadScene("GameScene");
        }
        catch
        {
            FruitGameManager.panthermathName = inputKey;
            SceneManager.LoadScene("GameScene");
        }
    }

    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        Debug.Log("Received Registration Token: " + token.Token);
    }

    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        Debug.Log("Received a new message from: " + e.Message.From);
    }
}
