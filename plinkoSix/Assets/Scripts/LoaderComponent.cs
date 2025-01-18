using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoaderComponent : MonoBehaviour
{
    public List<string> pinoSorceyStrings;
    [HideInInspector]
    public string idfaPinoSorceyKey = "";
  
    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfoPinoSorceySgidfigidi", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaPinoSorceyKey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(Init), 5f);
    }

    private void Init()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        TwiceInit(data);
    }

    private void TwiceInit(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("pinoSorceyGameDatassdagsdhydfhKey", string.Empty) != string.Empty)
            {
                GameLoad(PlayerPrefs.GetString("pinoSorceyGameDatassdagsdhydfhKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in pinoSorceyStrings)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchLoader(stringtemp, data));
            }
        }
        else
        {
            GameLoader();
        }
    }

    private string[] strings;
    public void GameLoader()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("GameLoaderScene");
    }

    public IEnumerator LaunchLoader(string inputstring, string inputstring2)
    {
        using (UnityWebRequest pinoSorceyLoadingStatus = UnityWebRequest.Get(inputstring))
        {
            pinoSorceyLoadingStatus.timeout = 4;
            yield return pinoSorceyLoadingStatus.SendWebRequest();
            if (pinoSorceyLoadingStatus.isNetworkError)
            {
                GameLoader();
            }
            else
            {
                try
                {
                    if (pinoSorceyLoadingStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (pinoSorceyLoadingStatus.downloadHandler.text.Contains("erypinorc"))
                        {
                            try
                            {
                                string key = pinoSorceyLoadingStatus.downloadHandler.text;
                                strings = key.Split('|');

                                GameController.pinoWinsCounter = Convert.ToInt32(strings[1]);
                                GameController.pinoSorceyTryCounter = Convert.ToInt32(strings[2]);
                                GameLoad(string.Format("{0}?idfa={1}&gaid={2}", strings[0], idfaPinoSorceyKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                GameLoad(string.Format("{0}?idfa={1}&gaid={2}", pinoSorceyLoadingStatus.downloadHandler.text, idfaPinoSorceyKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            GameLoader();
                        }
                    }
                    else
                    {
                        GameLoader();
                    }
                }
                catch
                {
                    GameLoader();
                }
            }
        }
    }

    public void GameLoad(string inputKey)
    {
        GameController.pinoSorceyNames = inputKey;
        SceneManager.LoadScene("GameScene");
    }
}
