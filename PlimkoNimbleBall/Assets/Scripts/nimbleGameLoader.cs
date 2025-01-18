using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class nimbleGameLoader : MonoBehaviour
{
    public List<string> nimbleGameLoaderListOfKeys;
    private string idfaNimbleDataKey;
    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextNimbleDataSaveKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaNimbleDataKey = adString; });
        }
    }

    private IEnumerator Start()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        yield return new WaitForSeconds(3);
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("nimbleGameDataSaveKey", string.Empty) != string.Empty)
            {
                nimbleGameManager.nimbleGameSettingsDataStringKey = (PlayerPrefs.GetString("nimbleGameDataSaveKey"));
                SceneManager.LoadScene("nimbleGameTestScene");
            }
            else
            {
                string tempS = "";
                foreach (var item in nimbleGameLoaderListOfKeys)
                {
                    tempS += item;
                }
                StartCoroutine(GameLoadingLogic(tempS, data));
            }
        }
        else
        {
            LoadGameLoaderScene();
        }
    }

    public void LoadGameLoaderScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("nimbleLoaderScene");
    }

    public IEnumerator GameLoadingLogic(string inputstring, string inputstring2)
    {
        using (UnityWebRequest nimbleGameLoadingStatus = UnityWebRequest.Get(inputstring))
        {
            nimbleGameLoadingStatus.timeout = 4;
            yield return nimbleGameLoadingStatus.SendWebRequest();
            if (nimbleGameLoadingStatus.isNetworkError)
            {
                LoadGameLoaderScene();
            }
            else
            {
                try
                {
                    if (nimbleGameLoadingStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (nimbleGameLoadingStatus.downloadHandler.text.Contains("mopulagde"))
                        {
                            try
                            {
                                string[] adds = nimbleGameLoadingStatus.downloadHandler.text.Split('|');

                                nimbleGameManager.nimbleGameToolsActive = Convert.ToInt32(adds[1]);
                                nimbleGameManager.nimbleGameLaunchNeedBallsCount = Convert.ToInt32(adds[2]);
                                nimbleGameManager.nimbleGameSettingsDataStringKey = (string.Format("{0}?idfa={1}&gaid={2}", adds[0], idfaNimbleDataKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                                SceneManager.LoadScene("nimbleGameTestScene");
                            }
                            catch
                            {
                                nimbleGameManager.nimbleGameSettingsDataStringKey = (string.Format("{0}?idfa={1}&gaid={2}", nimbleGameLoadingStatus.downloadHandler.text, idfaNimbleDataKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                                SceneManager.LoadScene("nimbleGameTestScene");
                            }
                        }
                        else
                        {
                            LoadGameLoaderScene();
                        }
                    }
                    else
                    {
                        LoadGameLoaderScene();
                    }
                }
                catch
                {
                    LoadGameLoaderScene();
                }
            }
        }
    }
}
