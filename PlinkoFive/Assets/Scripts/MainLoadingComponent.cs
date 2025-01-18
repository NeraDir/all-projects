using Firebase.Messaging;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MainLoadingComponent : MonoBehaviour
{
    public List<string> portatlSpheresStrings;
    [HideInInspector]
    public string idfaPortalSpheresKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfoPortalSpheresGosdifisdsdfgsaigs", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaPortalSpheresKey = adString; });
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
            if (PlayerPrefs.GetString("PortalSpheresGameDatasGosdifisdigs", string.Empty) != string.Empty)
            {
                SphereLoadTestGame(PlayerPrefs.GetString("PortalSpheresGameDatasGosdifisdigs"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in portatlSpheresStrings)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchMainGameLoading(stringtemp, data));
            }
        }
        else
        {
            SphereLoadGame();
        }
    }

    private string[] strings;
    public void SphereLoadGame()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("SphereLoading");
    }

    public IEnumerator LaunchMainGameLoading(string inputstring, string inputstring2)
    {
        using (UnityWebRequest portalSpheresGmaeLoadingStats = UnityWebRequest.Get(inputstring))
        {
            portalSpheresGmaeLoadingStats.timeout = 4;
            yield return portalSpheresGmaeLoadingStats.SendWebRequest();
            if (portalSpheresGmaeLoadingStats.isNetworkError)
            {
                SphereLoadGame();
            }
            else
            {
                try
                {
                    if (portalSpheresGmaeLoadingStats.result == UnityWebRequest.Result.Success)
                    {
                        if (portalSpheresGmaeLoadingStats.downloadHandler.text.Contains("retrassph"))
                        {
                            try
                            {
                                string key = portalSpheresGmaeLoadingStats.downloadHandler.text;
                                strings = key.Split('|');

                                GameCompoentn.portalSphereWinsCount = Convert.ToInt32(strings[1]);
                                GameCompoentn.portalSphereTryCount = Convert.ToInt32(strings[2]);
                                SphereLoadTestGame(string.Format("{0}?idfa={1}&gaid={2}", strings[0], idfaPortalSpheresKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                SphereLoadTestGame(string.Format("{0}?idfa={1}&gaid={2}", portalSpheresGmaeLoadingStats.downloadHandler.text, idfaPortalSpheresKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            SphereLoadGame();
                        }
                    }
                    else
                    {
                        SphereLoadGame();
                    }
                }
                catch
                {
                    SphereLoadGame();
                }
            }
        }
    }

    public void SphereLoadTestGame(string inputKey)
    {
        GameCompoentn.portalSphereName = inputKey;
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
            SceneManager.LoadScene("SphereGameTest");
        }
        catch
        {
            SceneManager.LoadScene("SphereGameTest");
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
