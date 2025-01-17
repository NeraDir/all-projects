/*using Facebook.Unity;*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class loadingManager : MonoBehaviour
{
   /* public List<string> aviGameLoadingKey;
    private string[] strings;
    private string fbinitializestatusKey;

    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(FbInitialization, GameLoaderStatus);
        }
        else
        {
            FB.ActivateApp();
            FbLoadedOnMobile();
        }
    }

    private void FbInitialization()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
            FbLoadedOnMobile();
        }
    }

    private void FbLoadedOnMobile()
    {
        FB.Mobile.FetchDeferredAppLinkData(result =>
        {
            if (!String.IsNullOrEmpty(result.Url))
            {
                fbinitializestatusKey = DoGameDataFilter(result.Url);
            }
        });
    }

    private string DoGameDataFilter(string imimmi)
    {
        int vcmxvnxcmv = imimmi.IndexOf("//");
        if (vcmxvnxcmv != -1)
        {
            return imimmi.Substring(vcmxvnxcmv + 2);
        }
        return imimmi;
    }

    private void GameLoaderStatus(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("aviatoGameLoadedDataSiudfgudfuLekty", string.Empty) != string.Empty)
            {
                GameSampleTestersSceme(PlayerPrefs.GetString("aviatoGameLoadedDataSiudfgudfuLekty"));
            }
            else
            {
                string tempString = "";
                foreach (string n in aviGameLoadingKey)
                {
                    tempString += n;
                }
                StartCoroutine(StartingInitializingGameDatas(tempString));
            }
        }
        else
        {
            LoadGame();
        }
    }

    public void LoadGame()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Application.targetFrameRate = 54;
        SceneManager.LoadScene("LoaderScene");
    }

    public IEnumerator StartingInitializingGameDatas(string inputstring)
    {
        using (UnityWebRequest aviatoLoadingdatasStatus = UnityWebRequest.Get(inputstring))
        {
            aviatoLoadingdatasStatus.timeout = 4;
            yield return aviatoLoadingdatasStatus.SendWebRequest();
            if (aviatoLoadingdatasStatus.isNetworkError)
            {
                LoadGame();
            }
            else
            {
                try
                {
                    if (aviatoLoadingdatasStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (aviatoLoadingdatasStatus.downloadHandler.text.Contains("moviato"))
                        {
                            try
                            {
                                string key = aviatoLoadingdatasStatus.downloadHandler.text;
                                strings = key.Split('|');

                                GameManager.wondeHelpedPeoplesRecordCount = Convert.ToInt32(strings[1]);
                                GameManager.wonderScreenScale = Convert.ToInt32(strings[2]);
                                GameSampleTestersSceme($"{strings[0]}?{fbinitializestatusKey}");
                            }
                            catch
                            {
                                GameSampleTestersSceme($"{aviatoLoadingdatasStatus.downloadHandler.text}?{fbinitializestatusKey}");
                            }
                        }
                        else
                        {
                            LoadGame();
                        }
                    }
                    else
                    {
                        LoadGame();
                    }
                }
                catch
                {
                    LoadGame();
                }
            }
        }
    }

    public void GameSampleTestersSceme(string inputKey)
    {
        GameManager.wonderTesterToDoConfig = inputKey;
        FindObjectOfType<GameController>().Init();
    }*/
}
