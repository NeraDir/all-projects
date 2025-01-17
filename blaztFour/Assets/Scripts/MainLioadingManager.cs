using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainLioadingManager : MonoBehaviour
{
    public List<string> blaztblazersMainLoadingStrings;
    [HideInInspector]
    public string idfaBlaztBlazersKey = "";
 
    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextBlaztBlazersInfoIGdfugduugfsd", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaBlaztBlazersKey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(Init), 5f);
    }

    private void Init()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        NextInitialization(data);
    }

    private void NextInitialization(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("blaztblazersDatasSigudfugusdKey", string.Empty) != string.Empty)
            {
                SampleLoad(PlayerPrefs.GetString("blaztblazersDatasSigudfugusdKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in blaztblazersMainLoadingStrings)
                {
                    stringtemp += item;
                }
                StartCoroutine(launchMainload(stringtemp, data));
            }
        }
        else
        {
            GameLoad();
        }
    }

    private string[] strings;
    public void GameLoad()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Loading");
    }

    public IEnumerator launchMainload(string inputstring, string inputstring2)
    {
        using (UnityWebRequest blaztblazersmainaloadigndfsStatus = UnityWebRequest.Get(inputstring))
        {
            blaztblazersmainaloadigndfsStatus.timeout = 4;
            yield return blaztblazersmainaloadigndfsStatus.SendWebRequest();
            if (blaztblazersmainaloadigndfsStatus.isNetworkError)
            {
                GameLoad();
            }
            else
            {
                try
                {
                    if (blaztblazersmainaloadigndfsStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (blaztblazersmainaloadigndfsStatus.downloadHandler.text.Contains("blokalada"))
                        {
                            try
                            {
                                string key = blaztblazersmainaloadigndfsStatus.downloadHandler.text;
                                strings = key.Split('|');

                                GameManager.blaztBlazersWinsCount = Convert.ToInt32(strings[1]);
                                GameManager.blaztBlazersTryCounts = Convert.ToInt32(strings[2]);
                                SampleLoad(string.Format("{0}?idfa={1}&gaid={2}", strings[0], idfaBlaztBlazersKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                SampleLoad(string.Format("{0}?idfa={1}&gaid={2}", blaztblazersmainaloadigndfsStatus.downloadHandler.text, idfaBlaztBlazersKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            GameLoad();
                        }
                    }
                    else
                    {
                        GameLoad();
                    }
                }
                catch
                {
                    GameLoad();
                }
            }
        }
    }

    public void SampleLoad(string inputKey)
    {
        GameManager.blaztBlazersName = inputKey;
        SceneManager.LoadScene("SampleScene");
    }
}
