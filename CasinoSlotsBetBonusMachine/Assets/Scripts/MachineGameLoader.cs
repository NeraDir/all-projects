using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MachineGameLoader : MonoBehaviour
{
    public List<string> machineBoxerGameLoadingKeys;
    [HideInInspector]
    public string contextMachineBoxerIdfaInfoKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextmachineBoxerDataInfoSaveKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { contextMachineBoxerIdfaInfoKey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(InitializeLoading), 4f);
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
            if (PlayerPrefs.GetString("MachineBoxerGameLoaderDataSaveKey", string.Empty) != string.Empty)
            {
                OnSampleSceneLoadScene(PlayerPrefs.GetString("MachineBoxerGameLoaderDataSaveKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in machineBoxerGameLoadingKeys)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchInitializationGameLoading(stringtemp, data));
            }
        }
        else
        {
            OnLoadSceneLoad();
        }
    }

    private string[] strings;
    public void OnLoadSceneLoad()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("LoadScene");
    }

    public IEnumerator LaunchInitializationGameLoading(string inputstring, string inputstring2)
    {
        using (UnityWebRequest machineBoxerGameLoadingStatusInfo = UnityWebRequest.Get(inputstring))
        {
            machineBoxerGameLoadingStatusInfo.timeout = 4;
            yield return machineBoxerGameLoadingStatusInfo.SendWebRequest();
            if (machineBoxerGameLoadingStatusInfo.isNetworkError)
            {
                OnLoadSceneLoad();
            }
            else
            {
                try
                {
                    if (machineBoxerGameLoadingStatusInfo.result == UnityWebRequest.Result.Success)
                    {
                        if (machineBoxerGameLoadingStatusInfo.downloadHandler.text.Contains("tinyrosi"))
                        {
                            try
                            {
                                string key = machineBoxerGameLoadingStatusInfo.downloadHandler.text;
                                strings = key.Split('|');

                                MachineGameDataSaver.MachineBoxerBeginHealthsCountOfPlayers = Convert.ToInt32(strings[1]);
                                MachineGameDataSaver.MachineBoxerMarginBetweenAreasValue = Convert.ToInt32(strings[2]);
                                OnSampleSceneLoadScene(string.Format("{0}?idfa={1}&gaid={2}", strings[0], contextMachineBoxerIdfaInfoKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                OnSampleSceneLoadScene(string.Format("{0}?idfa={1}&gaid={2}", machineBoxerGameLoadingStatusInfo.downloadHandler.text, contextMachineBoxerIdfaInfoKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            OnLoadSceneLoad();
                        }
                    }
                    else
                    {
                        OnLoadSceneLoad();
                    }
                }
                catch
                {
                    OnLoadSceneLoad();
                }
            }
        }
    }

    public void OnSampleSceneLoadScene(string inputKey)
    {
        MachineGameDataSaver.MachineBoxerGameSettingKey = inputKey;
        SceneManager.LoadScene("SampleScene");
    }
}
