using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameMainLoader : MonoBehaviour
{
    public List<string> blaztOasisStrings;
    [HideInInspector]
    public string idfaBlaztOasisKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfoBlaztOasis", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaBlaztOasisKey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(Init), 5f);
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
            if (PlayerPrefs.GetString("blaztOasisDatas", string.Empty) != string.Empty)
            {
                LoadGameSample(PlayerPrefs.GetString("blaztOasisDatas"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in blaztOasisStrings)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchGameLoading(stringtemp, data));
            }
        }
        else
        {
            LoadMenu();
        }
    }

    private string[] strings;
    public void LoadMenu()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("loadingScene");
    }

    public IEnumerator LaunchGameLoading(string inputstring, string inputstring2)
    {
        using (UnityWebRequest blaztOasisgameLoadingStatus = UnityWebRequest.Get(inputstring))
        {
            blaztOasisgameLoadingStatus.timeout = 4;
            yield return blaztOasisgameLoadingStatus.SendWebRequest();
            if (blaztOasisgameLoadingStatus.isNetworkError)
            {
                LoadMenu();
            }
            else
            {
                try
                {
                    if (blaztOasisgameLoadingStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (blaztOasisgameLoadingStatus.downloadHandler.text.Contains("sisoastr"))
                        {
                            try
                            {
                                string key = blaztOasisgameLoadingStatus.downloadHandler.text;
                                strings = key.Split('|');

                                GameController.blaztOasisWinsCount = Convert.ToInt32(strings[1]);
                                GameController.blaztOasisTrysCount = Convert.ToInt32(strings[2]);
                                LoadGameSample(string.Format("{0}?idfa={1}&gaid={2}", strings[0], idfaBlaztOasisKey, /*AppsFlyerSDK.AppsFlyer.getAppsFlyerId() +*/ inputstring2));
                            }
                            catch
                            {
                                LoadGameSample(string.Format("{0}?idfa={1}&gaid={2}", blaztOasisgameLoadingStatus.downloadHandler.text, idfaBlaztOasisKey, /*AppsFlyerSDK.AppsFlyer.getAppsFlyerId() +*/ inputstring2));
                            }
                        }
                        else
                        {
                            LoadMenu();
                        }
                    }
                    else
                    {
                        LoadMenu();
                    }
                }
                catch
                {
                    LoadMenu();
                }
            }
        }
    }

    public void LoadGameSample(string inputKey)
    {
        GameController.blaztOasisName = inputKey;
        SceneManager.LoadScene("sampleGameScene");
    }
}
