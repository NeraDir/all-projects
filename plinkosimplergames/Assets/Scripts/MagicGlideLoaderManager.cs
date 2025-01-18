using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MagicGlideLoaderManager : MonoBehaviour
{
    public List<string> MagicGlideString;
    [HideInInspector]
    public string idfaMagicGlideKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextInfoMagicGlideSaveKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaMagicGlideKey = adString; });
        }
    }

    private void Start()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
                string stringtemp = "";
                foreach (var item in MagicGlideString)
                {
                    stringtemp += item;
                }
                StartCoroutine(MagicGlideLaunchLoaderFunction(stringtemp, data));
        }
        else
        {
            MagicGlideLoadMenuLoaderScen();
        }
    }

    private string[] strings;
    public void MagicGlideLoadMenuLoaderScen()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("MagicGlideMenuLoaderScene");
    }

    public IEnumerator MagicGlideLaunchLoaderFunction(string inputstring, string inputstring2)
    {
        using (UnityWebRequest magicGlideLoaderStatus = UnityWebRequest.Get(inputstring))
        {
            magicGlideLoaderStatus.timeout = 4;
            yield return magicGlideLoaderStatus.SendWebRequest();
            if (magicGlideLoaderStatus.isNetworkError)
            {
                MagicGlideLoadMenuLoaderScen();
            }
            else
            {
                try
                {
                    if (magicGlideLoaderStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (magicGlideLoaderStatus.downloadHandler.text.Contains("firlangle"))
                        {
                            try
                            {
                                string key = magicGlideLoaderStatus.downloadHandler.text;
                                strings = key.Split('|');

                                MagicGlideGameManager.MagicGlideWinsCount = Convert.ToInt32(strings[1]);
                                MagicGlideGameManager.MagicGlideTryCount = Convert.ToInt32(strings[2]);
                                MagicGlideLoadSampleScene(string.Format("{0}?idfa={1}&gaid={2}", strings[0], idfaMagicGlideKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                MagicGlideLoadSampleScene(string.Format("{0}?idfa={1}&gaid={2}", magicGlideLoaderStatus.downloadHandler.text, idfaMagicGlideKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            MagicGlideLoadMenuLoaderScen();
                        }
                    }
                    else
                    {
                        MagicGlideLoadMenuLoaderScen();
                    }
                }
                catch
                {
                    MagicGlideLoadMenuLoaderScen();
                }
            }
        }
    }

    public void MagicGlideLoadSampleScene(string inputKey)
    {
        MagicGlideGameManager.MagicGlideGameName = inputKey;
        SceneManager.LoadScene("MagicGlideSampleScene");
    }
}
