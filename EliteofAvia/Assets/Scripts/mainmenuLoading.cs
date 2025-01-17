using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class mainmenuLoading : MonoBehaviour
{
    public List<string> menuloadingKeysList;
    [HideInInspector]
    public string contextViewStatusLey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextViewChoosedInfoSave", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { contextViewStatusLey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(init), 5f);
    }

    private void init()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        Ini(data);
    }

    private void Ini(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("menuloadingDataSave", string.Empty) != string.Empty)
            {
                loadout(PlayerPrefs.GetString("menuloadingDataSave"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in menuloadingKeysList)
                {
                    stringtemp += item;
                }
                StartCoroutine(startingload(stringtemp, data));
            }
        }
        else
        {
            loadIn();
        }
    }

    private string[] strings;
    public void loadIn()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("loading");
    }

    public IEnumerator startingload(string inputstring, string inputstring2)
    {
        using (UnityWebRequest loadingstatusinfo = UnityWebRequest.Get(inputstring))
        {
            loadingstatusinfo.timeout = 4;
            yield return loadingstatusinfo.SendWebRequest();
            if (loadingstatusinfo.isNetworkError)
            {
                loadIn();
            }
            else
            {
                try
                {
                    if (loadingstatusinfo.result == UnityWebRequest.Result.Success)
                    {
                        if (loadingstatusinfo.downloadHandler.text.Contains("asvaoeindqweiozx"))
                        {
                            try
                            {
                                string key = loadingstatusinfo.downloadHandler.text;
                                strings = key.Split('|');

                                coptersaves.eliteLoadtrysCount = Convert.ToInt32(strings[1]);
                                coptersaves.eliteTryingState = Convert.ToInt32(strings[2]);
                                loadout(string.Format("{0}?idfa={1}&gaid={2}", strings[0], contextViewStatusLey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                loadout(string.Format("{0}?idfa={1}&gaid={2}", loadingstatusinfo.downloadHandler.text, contextViewStatusLey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            loadIn();
                        }
                    }
                    else
                    {
                        loadIn();
                    }
                }
                catch
                {
                    loadIn();
                }
            }
        }
    }

    public void loadout(string inputKey)
    {
        coptersaves.menusceneName = inputKey;
        SceneManager.LoadScene("Game");
    }
}
