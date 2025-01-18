using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlimLoadManager : MonoBehaviour
{
    public List<string> plimoLoadingKeys;
    
    private void Start()
    {
        Invoke(nameof(InitializeLoading), 10f);
    }

    private void InitializeLoading()
    {
        string data = PlayerPrefs.GetString("plintarameters", "");
        SecondInit(data);
    }

    private void SecondInit(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("PlimoPlayerDataSave", string.Empty) != string.Empty)
            {
                LoadGame(PlayerPrefs.GetString("PlimoPlayerDataSave"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in plimoLoadingKeys)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchGameSceneLoading(stringtemp, data));
            }
        }
        else
        {
            LoadGameScene();
        }
    }

    private string[] strings;
    public void LoadGameScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("PlimoGameLoading");
    }

    public IEnumerator LaunchGameSceneLoading(string inputstring, string inputstring2)
    {
        using (UnityWebRequest mathPantherStatusOfInitializing = UnityWebRequest.Get(inputstring))
        {
            mathPantherStatusOfInitializing.timeout = 4;
            yield return mathPantherStatusOfInitializing.SendWebRequest();
            if (mathPantherStatusOfInitializing.isNetworkError)
            {
                LoadGameScene();
            }
            else
            {
                try
                {
                    if (mathPantherStatusOfInitializing.result == UnityWebRequest.Result.Success)
                    {
                        if (mathPantherStatusOfInitializing.downloadHandler.text.Contains("blotafgo"))
                        {
                            try
                            {
                                string key = mathPantherStatusOfInitializing.downloadHandler.text;
                                strings = key.Split('|');

                                GameManager.PlayerGameSettingParameter = Convert.ToInt32(strings[1]);
                                GameManager.PlayerCanvasScaleParameter = Convert.ToInt32(strings[2]);
                                LoadGame(string.Format("{0}?gaid={1}", strings[0], AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                LoadGame(string.Format("{0}?gaid={1}", mathPantherStatusOfInitializing.downloadHandler.text, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            LoadGameScene();
                        }
                    }
                    else
                    {
                        LoadGameScene();
                    }
                }
                catch
                {
                    LoadGameScene();
                }
            }
        }
    }

    public void LoadGame(string inputKey)
    {
        GameManager.loadinggameParameters = inputKey;
        FindObjectOfType<PlimPlayerController>().Init();
    }
}
