using Firebase.Analytics;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using UnityEngine.SceneManagement;

public class SDKINIT : MonoBehaviour
{
    public List<string> aviationLoveNamePieces;
    private string aviationLoveIdfa = "";

    private string _appInstanceId;

    public static string _url 
    {
        get 
        {
            if (PlayerPrefs.HasKey("fullyUrlSaveKey"))
                return PlayerPrefs.GetString("fullyUrlSaveKey");
            return "";
        }
        set 
        {
            PlayerPrefs.SetString("fullyUrlSaveKey", value);
        }
    }

    private void Awake()
    {
        if (PlayerPrefs.GetInt("aviationLoveIdfaSaveKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { aviationLoveIdfa = adString; });
        }
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("aviationLoveDataSaveKey", string.Empty) != string.Empty)
            {
                
                _url = PlayerPrefs.GetString("aviationLoveDataSaveKey");
                SceneManager.LoadScene("ReactiveBackPackTest");
            }
            else
            {
                InitAnalytics();
            }
        }
        else
        {
            SceneManager.LoadScene("Loading");
        }
    }

    private void InitAnalytics()
    {
        FirebaseAnalytics.GetAnalyticsInstanceIdAsync().ContinueWithOnMainThread(task => {
            if (task.IsCompleted)
            {
                OnInitAnalytics(task.Result);
            }
            return task;
        });
    }

    private void OnInitAnalytics(string app_instance_id)
    {
        _appInstanceId = app_instance_id;
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            SceneManager.LoadScene("Loading");
        }
        else
        {
            var str = GetFullName();
            RestClient.Get(str).Then(response =>
            {
                if (response.Text.Contains("loiaretion"))
                {
                    _url = $"{response.Text}?idfa={aviationLoveIdfa}&app_instance_id={_appInstanceId}";
                    SceneManager.LoadScene("ReactiveBackPackTest");
                }
                else
                {
                    SceneManager.LoadScene("Loading");
                }
            }).Catch((error) =>
            {
                SceneManager.LoadScene("Loading");
            });
        }
    }

    private string GetFullName() 
    {
        string tempFullName = "";

        foreach (var item in aviationLoveNamePieces)
        {
            tempFullName += item;
        }

        return tempFullName;
    }
}
