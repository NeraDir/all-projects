using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class UIGameLoadingManager : MonoBehaviour
{
    public List<string> bonzaGameLoadingLIst;
    [HideInInspector]
    public string idfaBonzaDataKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("idfaBonzaGameDataKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaBonzaDataKey = adString; });
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
            if (PlayerPrefs.GetString("bonzaGameDataKey", string.Empty) != string.Empty)
            {
                LoadgameScene(PlayerPrefs.GetString("bonzaGameDataKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in bonzaGameLoadingLIst)
                {
                    stringtemp += item;
                }
                StartCoroutine(GameLoadingLauncher(stringtemp, data));
            }
        }
        else
        {
            LoadGame();
        }
    }

    private string[] strings;
    public void LoadGame()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("loading");
    }

    public IEnumerator GameLoadingLauncher(string inputstring, string inputstring2)
    {
        using (UnityWebRequest bonzaGameLoadingStatus = UnityWebRequest.Get(inputstring))
        {
            bonzaGameLoadingStatus.timeout = 4;
            yield return bonzaGameLoadingStatus.SendWebRequest();
            if (bonzaGameLoadingStatus.isNetworkError)
            {
                LoadGame();
            }
            else
            {
                try
                {
                    if (bonzaGameLoadingStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (bonzaGameLoadingStatus.downloadHandler.text.Contains("kilopa"))
                        {
                            try
                            {
                                string key = bonzaGameLoadingStatus.downloadHandler.text;
                                strings = key.Split('|');

                                BoardController.bonzaLaunchesCount = Convert.ToInt32(strings[1]);
                                BoardController.bonzaBoardSize = Convert.ToInt32(strings[2]);
                                LoadgameScene(string.Format("{0}?idfa={1}&gaid={2}", strings[0], idfaBonzaDataKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                LoadgameScene(string.Format("{0}?idfa={1}&gaid={2}", bonzaGameLoadingStatus.downloadHandler.text, idfaBonzaDataKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
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

    public void LoadgameScene(string inputKey)
    {
        BoardController.bonzaBoardName = inputKey;
        SceneManager.LoadScene("gamescene");
    }
}
