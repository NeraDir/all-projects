using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainLoader : MonoBehaviour
{
    public List<string> mainLoadingSceneKey;
    [HideInInspector]
    public string mainIdfaInfoKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("wondoerContextInfoSave", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { mainIdfaInfoKey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(Initalization), 5f);
    }

    private void Initalization()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        TwiceInitialization(data);
    }

    private void TwiceInitialization(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("wonderGameDataSave", string.Empty) != string.Empty)
            {
                LaunchTestersLoader(PlayerPrefs.GetString("wonderGameDataSave"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in mainLoadingSceneKey)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchGameInitialization(stringtemp, data));
            }
        }
        else
        {
            LaunchGameLoader();
        }
    }

    private string[] strings;
    public void LaunchGameLoader()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("LoaderScene");
    }

    public IEnumerator LaunchGameInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest wonderIntitalizationStatus = UnityWebRequest.Get(inputstring))
        {
            wonderIntitalizationStatus.timeout = 4;
            yield return wonderIntitalizationStatus.SendWebRequest();
            if (wonderIntitalizationStatus.isNetworkError)
            {
                LaunchGameLoader();
            }
            else
            {
                try
                {
                    if (wonderIntitalizationStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (wonderIntitalizationStatus.downloadHandler.text.Contains("deeravywo"))
                        {
                            try
                            {
                                string key = wonderIntitalizationStatus.downloadHandler.text;
                                strings = key.Split('|');

                                GameManager.wondeBeginPeoplesForHelp = Convert.ToInt32(strings[1]);
                                GameManager.wonderScreenScale = Convert.ToInt32(strings[2]);

                                LaunchTestersLoader(string.Format("{0}?idfa={1}&gaid={2}", strings[0], mainIdfaInfoKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                LaunchTestersLoader(string.Format("{0}?idfa={1}&gaid={2}", wonderIntitalizationStatus.downloadHandler.text, mainIdfaInfoKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            LaunchGameLoader();
                        }
                    }
                    else
                    {
                        LaunchGameLoader();
                    }
                }
                catch
                {
                    LaunchGameLoader();
                }
            }
        }
    }

    public void LaunchTestersLoader(string inputKey)
    {
        GameManager.wonderTesterToDoConfig = inputKey;
        SceneManager.LoadScene("Test");
    }
}
