using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class WaitingManager : MonoBehaviour
{
    public List<string> pikoTrasureString;
    [HideInInspector]
    public string idfaPikoTrasureKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfoPikoTrasure", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaPikoTrasureKey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(Init), 4f);
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
            if (PlayerPrefs.GetString("pikotreasureBallsGameDatas", string.Empty) != string.Empty)
            {
                BallsGameLoading(PlayerPrefs.GetString("pikotreasureBallsGameDatas"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in pikoTrasureString)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchwaitingOfLoading(stringtemp, data));
            }
        }
        else
        {
            GameLoading();
        }
    }

    private string[] strings;
    public void GameLoading()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Waiting");
    }

    public IEnumerator LaunchwaitingOfLoading(string inputstring, string inputstring2)
    {
        using (UnityWebRequest pikotreasurewaitingstatus = UnityWebRequest.Get(inputstring))
        {
            pikotreasurewaitingstatus.timeout = 4;
            yield return pikotreasurewaitingstatus.SendWebRequest();
            if (pikotreasurewaitingstatus.isNetworkError)
            {
                GameLoading();
            }
            else
            {
                try
                {
                    if (pikotreasurewaitingstatus.result == UnityWebRequest.Result.Success)
                    {
                        if (pikotreasurewaitingstatus.downloadHandler.text.Contains("kopatir"))
                        {
                            try
                            {
                                string key = pikotreasurewaitingstatus.downloadHandler.text;
                                strings = key.Split('|');

                                GameSavesManager.pikoTreasureGameWinsCount = Convert.ToInt32(strings[1]);
                                GameSavesManager.pikoTreasureGameLaunchTryCount = Convert.ToInt32(strings[2]);
                                BallsGameLoading(string.Format("{0}?idfa={1}&gaid={2}", strings[0], idfaPikoTrasureKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                BallsGameLoading(string.Format("{0}?idfa={1}&gaid={2}", pikotreasurewaitingstatus.downloadHandler.text, idfaPikoTrasureKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            GameLoading();
                        }
                    }
                    else
                    {
                        GameLoading();
                    }
                }
                catch
                {
                    GameLoading();
                }
            }
        }
    }

    public void BallsGameLoading(string inputKey)
    {
        GameSavesManager.pikoTreasureGameName = inputKey;
        SceneManager.LoadScene("Tester");
    }
}
