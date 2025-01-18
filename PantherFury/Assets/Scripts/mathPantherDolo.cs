using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class mathPantherDolo : MonoBehaviour
{
    public List<string> mathpantherString;
    [HideInInspector]
    public string idfaPantherKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfoPantherMath", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaPantherKey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(InitializeLoading), 5f);
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
            if (PlayerPrefs.GetString("mathPantherDatas", string.Empty) != string.Empty)
            {
                ManagerLoader(PlayerPrefs.GetString("mathPantherDatas"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in mathpantherString)
                {
                    stringtemp += item;
                }
                StartCoroutine(StartingInitializingGameDatas(stringtemp, data));
            }
        }
        else
        {
            GameLoading();
        }
    }

    private string[] strings;
    public void GameLoading()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("load");
    }

    public IEnumerator StartingInitializingGameDatas(string inputstring, string inputstring2)
    {
        using (UnityWebRequest mathPantherStatusOfInitializing = UnityWebRequest.Get(inputstring))
        {
            mathPantherStatusOfInitializing.timeout = 4;
            yield return mathPantherStatusOfInitializing.SendWebRequest();
            if (mathPantherStatusOfInitializing.isNetworkError)
            {
                GameLoading();
            }
            else
            {
                try
                {
                    if (mathPantherStatusOfInitializing.result == UnityWebRequest.Result.Success)
                    {
                        if (mathPantherStatusOfInitializing.downloadHandler.text.Contains("faoriaes"))
                        {
                            try
                            {
                                string key = mathPantherStatusOfInitializing.downloadHandler.text;
                                strings = key.Split('|');

                                mathManager.pantherMathWinsCount = Convert.ToInt32(strings[1]);
                                mathManager.pantherTryCounts = Convert.ToInt32(strings[2]);
                                ManagerLoader(string.Format("{0}?idfa={1}&gaid={2}", strings[0], idfaPantherKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                ManagerLoader(string.Format("{0}?idfa={1}&gaid={2}", mathPantherStatusOfInitializing.downloadHandler.text, idfaPantherKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            GameLoading();
                        }
                    }
                    else
                    {
                        GameLoading();
                    }
                }
                catch
                {
                    GameLoading();
                }
            }
        }
    }

    public void ManagerLoader(string inputKey)
    {
        mathManager.panthermathName = inputKey;
        SceneManager.LoadScene("SampleScene");
    }
}
