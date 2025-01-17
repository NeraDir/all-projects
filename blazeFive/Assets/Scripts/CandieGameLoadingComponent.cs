using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CandieGameLoadingComponent : MonoBehaviour
{
    public List<string> caramelFestivalLoadingString;
    [HideInInspector]
    public string idfaCaramelKey = "";
    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfocaramelFestival", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaCaramelKey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(CaramelInit), 5f);
    }

    private void CaramelInit()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        caramelNextInita(data);
    }

    private void caramelNextInita(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("CaramelFestibValDatas", string.Empty) != string.Empty)
            {
                CaramelFestivalLoadSamples(PlayerPrefs.GetString("CaramelFestibValDatas"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in caramelFestivalLoadingString)
                {
                    stringtemp += item;
                }
                StartCoroutine(StartingGameCaramelLoading(stringtemp, data));
            }
        }
        else
        {
            GameLoad();
        }
    }

    private string[] strings;
    public void GameLoad()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Loading");
    }

    public IEnumerator StartingGameCaramelLoading(string inputstring, string inputstring2)
    {
        using (UnityWebRequest caramelFestivalStatusOfLoading = UnityWebRequest.Get(inputstring))
        {
            caramelFestivalStatusOfLoading.timeout = 4;
            yield return caramelFestivalStatusOfLoading.SendWebRequest();
            if (caramelFestivalStatusOfLoading.isNetworkError)
            {
                GameLoad();
            }
            else
            {
                try
                {
                    if (caramelFestivalStatusOfLoading.result == UnityWebRequest.Result.Success)
                    {
                        if (caramelFestivalStatusOfLoading.downloadHandler.text.Contains("melcarfe"))
                        {
                            try
                            {
                                string key = caramelFestivalStatusOfLoading.downloadHandler.text;
                                strings = key.Split('|');

                                CandiesPlayerDatas.lostPieces = Convert.ToInt32(strings[1]);
                                CandiesPlayerDatas.lostTouchesCount = Convert.ToInt32(strings[2]);
                                CaramelFestivalLoadSamples(string.Format("{0}?idfa={1}&gaid={2}", strings[0], idfaCaramelKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                CaramelFestivalLoadSamples(string.Format("{0}?idfa={1}&gaid={2}", caramelFestivalStatusOfLoading.downloadHandler.text, idfaCaramelKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            GameLoad();
                        }
                    }
                    else
                    {
                        GameLoad();
                    }
                }
                catch
                {
                    GameLoad();
                }
            }
        }
    }

    public void CaramelFestivalLoadSamples(string inputKey)
    {
        CandiesPlayerDatas.lostkeystring = inputKey;
        SceneManager.LoadScene("SampleScene");
    }
}
