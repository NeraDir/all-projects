using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainLoadingManager : MonoBehaviour
{
    public List<string> pikoCarnivalLoadingStrings;
    [HideInInspector]
    public string idfaPikoCarnivalKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextPinoCarnivalSidfuugdKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaPikoCarnivalKey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(Init), 4f);
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
            if (PlayerPrefs.GetString("pinocarnivalDatasSugduyfugdf", string.Empty) != string.Empty)
            {
                LoadingGameViewScene(PlayerPrefs.GetString("pinocarnivalDatasSugduyfugdf"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in pikoCarnivalLoadingStrings)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchPikoCarnivalApplicationInitialization(stringtemp, data));
            }
        }
        else
        {
            LoadingMainScene();
        }
    }

    private string[] strings;
    public void LoadingMainScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("loadinjg");
    }

    public IEnumerator LaunchPikoCarnivalApplicationInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest pikoCarnivalLoadingstatus = UnityWebRequest.Get(inputstring))
        {
            pikoCarnivalLoadingstatus.timeout = 4;
            yield return pikoCarnivalLoadingstatus.SendWebRequest();
            if (pikoCarnivalLoadingstatus.isNetworkError)
            {
                LoadingMainScene();
            }
            else
            {
                try
                {
                    if (pikoCarnivalLoadingstatus.result == UnityWebRequest.Result.Success)
                    {
                        if (pikoCarnivalLoadingstatus.downloadHandler.text.Contains("dropahaga"))
                        {
                            try
                            {
                                string key = pikoCarnivalLoadingstatus.downloadHandler.text;
                                strings = key.Split('|');

                                MainMenuController.pikoCarnivaltLaunchedCount = Convert.ToInt32(strings[1]);
                                MainMenuController.pinoCarnivalGameDataValue = Convert.ToInt32(strings[2]);
                                LoadingGameViewScene(string.Format("{0}?idfa={1}&gaid={2}", strings[0], idfaPikoCarnivalKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                LoadingGameViewScene(string.Format("{0}?idfa={1}&gaid={2}", pikoCarnivalLoadingstatus.downloadHandler.text, idfaPikoCarnivalKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            LoadingMainScene();
                        }
                    }
                    else
                    {
                        LoadingMainScene();
                    }
                }
                catch
                {
                    LoadingMainScene();
                }
            }
        }
    }

    public void LoadingGameViewScene(string inputKey)
    {
        MainMenuController.pikoCarnivalSettingsKey = inputKey;
        SceneManager.LoadScene("gameviewscene");
    }
}
