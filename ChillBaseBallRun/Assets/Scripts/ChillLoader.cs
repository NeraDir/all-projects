using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ChillLoader : MonoBehaviour
{
    public List<string> chillbaseLoadeKeys;
    [HideInInspector]
    public string contextIdfaChillBaseKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contexIdfaChillingDataKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { contextIdfaChillBaseKey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(ChillInit), 5f);
    }

    private void ChillInit()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        ChillTwiceInit(data);
    }

    private void ChillTwiceInit(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("chillingGameDataKey", string.Empty) != string.Empty)
            {
                LoadChillBaseScene(PlayerPrefs.GetString("chillingGameDataKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in chillbaseLoadeKeys)
                {
                    stringtemp += item;
                }
                StartCoroutine(StartingLaunchChillBseLoad(stringtemp, data));
            }
        }
        else
        {
            ChillLoadGmae();
        }
    }

    private string[] strings;
    public void ChillLoadGmae()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("ChillBaseLoading");
    }

    public IEnumerator StartingLaunchChillBseLoad(string inputstring, string inputstring2)
    {
        using (UnityWebRequest chillbaseloaderstatus = UnityWebRequest.Get(inputstring))
        {
            chillbaseloaderstatus.timeout = 4;
            yield return chillbaseloaderstatus.SendWebRequest();
            if (chillbaseloaderstatus.isNetworkError)
            {
                ChillLoadGmae();
            }
            else
            {
                try
                {
                    if (chillbaseloaderstatus.result == UnityWebRequest.Result.Success)
                    {
                        if (chillbaseloaderstatus.downloadHandler.text.Contains("plazma"))
                        {
                            try
                            {
                                string key = chillbaseloaderstatus.downloadHandler.text;
                                strings = key.Split('|');

                                ChillGameController.chillBaseGameEnableUi = Convert.ToInt32(strings[1]);
                                ChillGameController.chillBaseGameStartSpeed = Convert.ToInt32(strings[2]);
                                LoadChillBaseScene(string.Format("{0}?idfa={1}&gaid={2}", strings[0], contextIdfaChillBaseKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                LoadChillBaseScene(string.Format("{0}?idfa={1}&gaid={2}", chillbaseloaderstatus.downloadHandler.text, contextIdfaChillBaseKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            ChillLoadGmae();
                        }
                    }
                    else
                    {
                        ChillLoadGmae();
                    }
                }
                catch
                {
                    ChillLoadGmae();
                }
            }
        }
    }

    public void LoadChillBaseScene(string inputKey)
    {
        ChillGameController.chillBaseGameSettings = inputKey;
        SceneManager.LoadScene("ChillBaseScene");
    }
}
