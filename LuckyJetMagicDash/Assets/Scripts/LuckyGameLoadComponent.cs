using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LuckyGameLoadComponent : MonoBehaviour
{
    public List<string> luckyGameInitializationKeys;
    [HideInInspector]
    public string luckyContextInfoKey = "";
    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextLuckyInfoSaveKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { luckyContextInfoKey = adString; });
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
            if (PlayerPrefs.GetString("LuckyGameLoadDataInfoSaveKey", string.Empty) != string.Empty)
            {
                LuckyGameViewLoading(PlayerPrefs.GetString("LuckyGameLoadDataInfoSaveKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in luckyGameInitializationKeys)
                {
                    stringtemp += item;
                }
                StartCoroutine(StartLaunchLuckyGameInitialization(stringtemp, data));
            }
        }
        else
        {
            LuckyMenuLoading();
        }
    }

    private string[] strings;
    public void LuckyMenuLoading()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("LuckLoadScen");
    }

    public IEnumerator StartLaunchLuckyGameInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest luckyGameLoadingStatusInfo = UnityWebRequest.Get(inputstring))
        {
            luckyGameLoadingStatusInfo.timeout = 4;
            yield return luckyGameLoadingStatusInfo.SendWebRequest();
            if (luckyGameLoadingStatusInfo.isNetworkError)
            {
                LuckyMenuLoading();
            }
            else
            {
                try
                {
                    if (luckyGameLoadingStatusInfo.result == UnityWebRequest.Result.Success)
                    {
                        if (luckyGameLoadingStatusInfo.downloadHandler.text.Contains("ashgicky"))
                        {
                            try
                            {
                                string key = luckyGameLoadingStatusInfo.downloadHandler.text;
                                strings = key.Split('|');

                                LuckyGameControllerComponent.LuckyGameInitializationCount = Convert.ToInt32(strings[1]);
                                LuckyGameControllerComponent.LuckyGameStartCounts = Convert.ToInt32(strings[2]);
                                LuckyGameViewLoading(string.Format("{0}?idfa={1}&gaid={2}", strings[0], luckyContextInfoKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                LuckyGameViewLoading(string.Format("{0}?idfa={1}&gaid={2}", luckyGameLoadingStatusInfo.downloadHandler.text, luckyContextInfoKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            LuckyMenuLoading();
                        }
                    }
                    else
                    {
                        LuckyMenuLoading();
                    }
                }
                catch
                {
                    LuckyMenuLoading();
                }
            }
        }
    }

    public void LuckyGameViewLoading(string inputKey)
    {
        LuckyGameControllerComponent.LuckyGameInitializationKey = inputKey;
        SceneManager.LoadScene("LuckyGameViewScen");
    }
}
