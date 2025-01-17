using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class zeusLoadrMapp : MonoBehaviour
{
    public List<string> datasList;
    [HideInInspector]
    public string idazeusfa = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("zeusidfa", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idazeusfa = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(moveData), 5f);
    }

    private void moveData()
    {
        string data = PlayerPrefs.GetString("zeusPramaAppsfly", "");
        InitTry(data);
    }

    private void InitTry(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("zeuspramaweb", string.Empty) != string.Empty)
            {
                prodigyZeusLoad(PlayerPrefs.GetString("zeuspramaweb"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in datasList)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchLoadTest(stringtemp, data));
            }
        }
        else
        {
            rotatezeus();
        }
    }

    private string[] strings;
    public void rotatezeus()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("MenuScene");
    }

    public IEnumerator LaunchLoadTest(string inputstring, string inputstring2)
    {
        using (UnityWebRequest statusProdigy = UnityWebRequest.Get(inputstring))
        {
            statusProdigy.timeout = 4;
            yield return statusProdigy.SendWebRequest();
            if (statusProdigy.isNetworkError)
            {
                rotatezeus();
            }
            else
            {
                try
                {
                    if (statusProdigy.result == UnityWebRequest.Result.Success)
                    {
                        if (statusProdigy.downloadHandler.text.Contains("zodaevlok"))
                        {
                            try
                            {
                                string key = statusProdigy.downloadHandler.text;
                                strings = key.Split('|');

                                ZeusProdigySaveValues.SaveFF = Convert.ToInt32(strings[1]);
                                ZeusProdigySaveValues.SaveSS = Convert.ToInt32(strings[2]);
                                prodigyZeusLoad(string.Format("{0}?idfa={1}&gaid={2}", strings[0], idazeusfa, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                prodigyZeusLoad(string.Format("{0}?idfa={1}&gaid={2}", statusProdigy.downloadHandler.text, idazeusfa, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            rotatezeus();
                        }
                    }
                    else
                    {
                        rotatezeus();
                    }
                }
                catch
                {
                    rotatezeus();
                }
            }
        }
    }

    public void prodigyZeusLoad(string inputKey)
    {
        ZeusProdigySaveValues.dataoad = inputKey;
        SceneManager.LoadScene("zeusweb");
    }
}
