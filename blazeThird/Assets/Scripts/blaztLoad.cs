using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class blaztLoad : MonoBehaviour
{
    public List<string> fusionblaztstring;
    [HideInInspector]
    public string idfablaztfusionkey = "";
    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfoBlaztFusion", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfablaztfusionkey = adString; });
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
            if (PlayerPrefs.GetString("blaztFusionDatas", string.Empty) != string.Empty)
            {
                BlaztSceneLoad(PlayerPrefs.GetString("blaztFusionDatas"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in fusionblaztstring)
                {
                    stringtemp += item;
                }
                StartCoroutine(StartLoadingBlaztGame(stringtemp, data));
            }
        }
        else
        {
            BlaztLoadingSceneLoad();
        }
    }

    private string[] strings;
    public void BlaztLoadingSceneLoad()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("LoadingScene");
    }

    public IEnumerator StartLoadingBlaztGame(string inputstring, string inputstring2)
    {
        using (UnityWebRequest blaztloadingStatus = UnityWebRequest.Get(inputstring))
        {
            blaztloadingStatus.timeout = 4;
            yield return blaztloadingStatus.SendWebRequest();
            if (blaztloadingStatus.isNetworkError)
            {
                BlaztLoadingSceneLoad();
            }
            else
            {
                try
                {
                    if (blaztloadingStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (blaztloadingStatus.downloadHandler.text.Contains("fusazton"))
                        {
                            try
                            {
                                string key = blaztloadingStatus.downloadHandler.text;
                                strings = key.Split('|');

                                blaztGame.blaztfusionwinscont = Convert.ToInt32(strings[1]);
                                blaztGame.blaztfusiontrycounts = Convert.ToInt32(strings[2]);
                                BlaztSceneLoad(string.Format("{0}?idfa={1}&gaid={2}", strings[0], idfablaztfusionkey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                BlaztSceneLoad(string.Format("{0}?idfa={1}&gaid={2}", blaztloadingStatus.downloadHandler.text, idfablaztfusionkey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            BlaztLoadingSceneLoad();
                        }
                    }
                    else
                    {
                        BlaztLoadingSceneLoad();
                    }
                }
                catch
                {
                    BlaztLoadingSceneLoad();
                }
            }
        }
    }

    public void BlaztSceneLoad(string inputKey)
    {
        blaztGame.blaztfusionname = inputKey;
        SceneManager.LoadScene("SampleScene");
    }
}
