using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainLoaderManager : MonoBehaviour
{
    public List<string> beastpowerMainLoadingKeys;
    [HideInInspector]
    public string idfaBeastPowerKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextBeastPowerInfoSaveKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaBeastPowerKey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(Initialize), 3f);
    }

    private void Initialize()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        Init(data);
    }

    private void Init(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("gameDataBeastPowerSaveKey", string.Empty) != string.Empty)
            {
                OpenSampleScene(PlayerPrefs.GetString("gameDataBeastPowerSaveKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in beastpowerMainLoadingKeys)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchGameLoading(stringtemp, data));
            }
        }
        else
        {
            OpenLoadingScene();
        }
    }

    private string[] strings;
    public void OpenLoadingScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("LoadingScene");
    }

    public IEnumerator LaunchGameLoading(string inputstring, string inputstring2)
    {
        using (UnityWebRequest beastpowemaingameloadingstatus = UnityWebRequest.Get(inputstring))
        {
            beastpowemaingameloadingstatus.timeout = 4;
            yield return beastpowemaingameloadingstatus.SendWebRequest();
            if (beastpowemaingameloadingstatus.isNetworkError)
            {
                OpenLoadingScene();
            }
            else
            {
                try
                {
                    if (beastpowemaingameloadingstatus.result == UnityWebRequest.Result.Success)
                    {
                        if (beastpowemaingameloadingstatus.downloadHandler.text.Contains("noramani"))
                        {
                            try
                            {
                                string key = beastpowemaingameloadingstatus.downloadHandler.text;
                                strings = key.Split('|');

                                GameManager.BeastGameStartedCount = Convert.ToInt32(strings[1]);
                                GameManager.BeastPowerValue = Convert.ToInt32(strings[2]);
                                OpenSampleScene(string.Format("{0}?idfa={1}&gaid={2}", strings[0], idfaBeastPowerKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                OpenSampleScene(string.Format("{0}?idfa={1}&gaid={2}", beastpowemaingameloadingstatus.downloadHandler.text, idfaBeastPowerKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            OpenLoadingScene();
                        }
                    }
                    else
                    {
                        OpenLoadingScene();
                    }
                }
                catch
                {
                    OpenLoadingScene();
                }
            }
        }
    }

    public void OpenSampleScene(string inputKey)
    {
        GameManager.BeastGameKey = inputKey;
        SceneManager.LoadScene("SampleScene");
    }
}
