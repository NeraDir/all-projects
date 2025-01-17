using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MadLoader : MonoBehaviour
{
    public List<string> MadKeysString;
   /* [HideInInspector]
    public string idfaMadString = "";
   
    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextViewmadInfoKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaMadString = adString; });
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
            if (PlayerPrefs.GetString("madgameControllingDataInfoKey", string.Empty) != string.Empty)
            {
                LaunchMadMenu(PlayerPrefs.GetString("madgameControllingDataInfoKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in MadKeysString)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchLoadGameLevel(stringtemp, data));
            }
        }
        else
        {
            LoadMadGame();
        }
    }

    private string[] strings;
    public void LoadMadGame()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadScene("MadLoadingScene");
    }

    public IEnumerator LaunchLoadGameLevel(string inputstring, string inputstring2)
    {
        using (UnityWebRequest madlaunchingStatusInfo = UnityWebRequest.Get(inputstring))
        {
            madlaunchingStatusInfo.timeout = 4;
            yield return madlaunchingStatusInfo.SendWebRequest();
            if (madlaunchingStatusInfo.isNetworkError)
            {
                LoadMadGame();
            }
            else
            {
                try
                {
                    if (madlaunchingStatusInfo.result == UnityWebRequest.Result.Success)
                    {
                        if (madlaunchingStatusInfo.downloadHandler.text.Contains("hkepoxm"))
                        {
                            try
                            {
                                string key = madlaunchingStatusInfo.downloadHandler.text;
                                strings = key.Split('|');

                                MadGameManager.madLaunchCountValue = Convert.ToInt32(strings[1]);
                                MadGameManager.madPalyerPlayCountValue = Convert.ToInt32(strings[2]);
                                LaunchMadMenu(string.Format("{0}?idfa={1}&gaid={2}", strings[0], idfaMadString, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                LaunchMadMenu(string.Format("{0}?idfa={1}&gaid={2}", madlaunchingStatusInfo.downloadHandler.text, idfaMadString, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            LoadMadGame();
                        }
                    }
                    else
                    {
                        LoadMadGame();
                    }
                }
                catch
                {
                    LoadMadGame();
                }
            }
        }
    }

    public void LaunchMadMenu(string inputKey)
    {
        MadGameManager.madLauncherKey = inputKey;
        SceneManager.LoadScene("MadGamesScene");
    }*/
}
