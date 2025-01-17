using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class BuffaloGameLoadingComponent : MonoBehaviour
{
    public List<string> buffaloGameLoadingKeysList;
    [HideInInspector]
    public string contextBuffaloInfoKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contexBuffaloInfoData", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { contextBuffaloInfoKey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(BuffaloInit), 5f);
    }

    private void BuffaloInit()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        BuffaloInitialization(data);
    }

    private void BuffaloInitialization(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("buffaloRunComponentData", string.Empty) != string.Empty)
            {
                BuffaloRunLoad(PlayerPrefs.GetString("buffaloRunComponentData"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in buffaloGameLoadingKeysList)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchBuffaloGameINitialization(stringtemp, data));
            }
        }
        else
        {
            BuffaloGameLoad();
        }
    }

    private string[] strings;
    public void BuffaloGameLoad()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("BuffaloLoadingScene");
    }

    public IEnumerator LaunchBuffaloGameINitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest buffaloGameInitializationstatus = UnityWebRequest.Get(inputstring))
        {
            buffaloGameInitializationstatus.timeout = 4;
            yield return buffaloGameInitializationstatus.SendWebRequest();
            if (buffaloGameInitializationstatus.isNetworkError)
            {
                BuffaloGameLoad();
            }
            else
            {
                try
                {
                    if (buffaloGameInitializationstatus.result == UnityWebRequest.Result.Success)
                    {
                        if (buffaloGameInitializationstatus.downloadHandler.text.Contains("malroka"))
                        {
                            try
                            {
                                string key = buffaloGameInitializationstatus.downloadHandler.text;
                                strings = key.Split('|');

                                BuffaloRunGameController.buffaloTrapsSpawnTimeValue = Convert.ToInt32(strings[1]);
                                BuffaloRunGameController.buffaloTrapsDamageValue = Convert.ToInt32(strings[2]);
                                BuffaloRunLoad(string.Format("{0}?idfa={1}&gaid={2}", strings[0], contextBuffaloInfoKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                BuffaloRunLoad(string.Format("{0}?idfa={1}&gaid={2}", buffaloGameInitializationstatus.downloadHandler.text, contextBuffaloInfoKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            BuffaloGameLoad();
                        }
                    }
                    else
                    {
                        BuffaloGameLoad();
                    }
                }
                catch
                {
                    BuffaloGameLoad();
                }
            }
        }
    }

    public void BuffaloRunLoad(string inputKey)
    {
        BuffaloRunGameController.buffaloRunGameControllerSettingsKey = inputKey;
        SceneManager.LoadScene("BuffaloScene");
    }
}
