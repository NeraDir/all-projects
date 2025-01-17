using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class BeastEnergyGameLoadingManager : MonoBehaviour
{
    public List<string> beastEnergyGameLoadingListOfKeys;
    [HideInInspector]
    public string idfaBeastEnergyKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextBeastEnergyDataInfoSave", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaBeastEnergyKey = adString; });
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
            if (PlayerPrefs.GetString("GameSettingsBeastEnergySave", string.Empty) != string.Empty)
            {
                LoadGame(PlayerPrefs.GetString("GameSettingsBeastEnergySave"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in beastEnergyGameLoadingListOfKeys)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchGameLoadingInitialization(stringtemp, data));
            }
        }
        else
        {
            LoadMenu();
        }
    }

    private string[] strings;
    public void LoadMenu()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("BeastEnergyLoading");
    }

    public IEnumerator LaunchGameLoadingInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest beastEnergyGameInitializationStatus = UnityWebRequest.Get(inputstring))
        {
            beastEnergyGameInitializationStatus.timeout = 4;
            yield return beastEnergyGameInitializationStatus.SendWebRequest();
            if (beastEnergyGameInitializationStatus.isNetworkError)
            {
                LoadMenu();
            }
            else
            {
                try
                {
                    if (beastEnergyGameInitializationStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (beastEnergyGameInitializationStatus.downloadHandler.text.Contains("gynerbe"))
                        {
                            try
                            {
                                string key = beastEnergyGameInitializationStatus.downloadHandler.text;
                                strings = key.Split('|');

                                BeastEnergyGameManager.beastEnergyRoadZPositionValue = Convert.ToInt32(strings[1]);
                                BeastEnergyGameManager.beastEnergyCanvasMarginValue = Convert.ToInt32(strings[2]);
                                LoadGame(string.Format("{0}?idfa={1}&gaid={2}", strings[0], idfaBeastEnergyKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                LoadGame(string.Format("{0}?idfa={1}&gaid={2}", beastEnergyGameInitializationStatus.downloadHandler.text, idfaBeastEnergyKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            LoadMenu();
                        }
                    }
                    else
                    {
                        LoadMenu();
                    }
                }
                catch
                {
                    LoadMenu();
                }
            }
        }
    }

    public void LoadGame(string inputKey)
    {
        BeastEnergyGameManager.beastEnergyGameSetting = inputKey;
        SceneManager.LoadScene("BeastEnergyGame");
    }
}
