using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class UIGameLoadingManager : MonoBehaviour
{
    public List<string> bonzaGameLoadingLIst;

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

    private IEnumerator Start() {
        
        yield return new WaitForSeconds(3);
        string data = PlayerPrefs.GetString("tarameters", "");
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
                StartCoroutine(GameLoadingLauncher(stringtemp,data));
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
        SceneManager.LoadScene("loading");
    }

    public IEnumerator GameLoadingLauncher(string inputstring,string inputstring2)
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
                        if (bonzaGameLoadingStatus.downloadHandler.text.Contains("cancthse"))
                        {
                            try
                            {
                                string key = bonzaGameLoadingStatus.downloadHandler.text;
                                string[] strings = key.Split('|');

                                BoardController.bonzaLaunchesCount = Convert.ToInt32(strings[1]);
                                BoardController.bonzaBoardSize = Convert.ToInt32(strings[2]);
                                LoadgameScene($"{strings[0]}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}");
                            }
                            catch
                            {
                                LoadgameScene($"{bonzaGameLoadingStatus.downloadHandler.text}&gaid={AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2}");
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
        FindObjectOfType<BonzaBoardManager>().init();
    }
}
