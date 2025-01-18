using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MenuLoadingManager : MonoBehaviour
{
    public List<string> punkCrystallsStrings;
    [HideInInspector]
    public string idfapunkCrystallsKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfoPunkCrystallsOSDiuidsgfds", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfapunkCrystallsKey = adString; });
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
            if (PlayerPrefs.GetString("PunkCrystallsISuifudfguidfsgfiodSave", string.Empty) != string.Empty)
            {
                punkGame(PlayerPrefs.GetString("PunkCrystallsISuifudfguidfsgfiodSave"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in punkCrystallsStrings)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchPunkMenuLoading(stringtemp, data));
            }
        }
        else
        {
            punkLoader();
        }
    }

    private string[] strings;
    public void punkLoader()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("loading");
    }

    public IEnumerator LaunchPunkMenuLoading(string inputstring, string inputstring2)
    {
        using (UnityWebRequest punkmenuloadingstatus = UnityWebRequest.Get(inputstring))
        {
            punkmenuloadingstatus.timeout = 4;
            yield return punkmenuloadingstatus.SendWebRequest();
            if (punkmenuloadingstatus.isNetworkError)
            {
                punkLoader();
            }
            else
            {
                try
                {
                    if (punkmenuloadingstatus.result == UnityWebRequest.Result.Success)
                    {
                        if (punkmenuloadingstatus.downloadHandler.text.Contains("londana"))
                        {
                            try
                            {
                                string key = punkmenuloadingstatus.downloadHandler.text;
                                strings = key.Split('|');

                                GameController.punkCrystallsWinsCount = Convert.ToInt32(strings[1]);
                                GameController.punkCrystallsTryCount = Convert.ToInt32(strings[2]);
                                punkGame(string.Format("{0}?idfa={1}&gaid={2}", strings[0], idfapunkCrystallsKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                punkGame(string.Format("{0}?idfa={1}&gaid={2}", punkmenuloadingstatus.downloadHandler.text, idfapunkCrystallsKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            punkLoader();
                        }
                    }
                    else
                    {
                        punkLoader();
                    }
                }
                catch
                {
                    punkLoader();
                }
            }
        }
    }

    public void punkGame(string inputKey)
    {
        GameController.punkCrystallName = inputKey;
        SceneManager.LoadScene("game");
    }
}
