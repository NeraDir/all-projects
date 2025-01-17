using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class mainloadingcomponent : MonoBehaviour
{
    public List<string> gameinitializationlistofkeys;
    private string idfagameinitializationkey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("idfaaviatorrisingstarssave", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfagameinitializationkey = adString; });
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3f);
        string data = PlayerPrefs.GetString("tarameters", "");
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("gameinitializationdatasave", string.Empty) != string.Empty)
            {
                gamemanager.gametestsettingkey = PlayerPrefs.GetString("gameinitializationdatasave");
                SceneManager.LoadScene("GameTest");
            }
            else
            {
                string stringtemp = "";
                foreach (var item in gameinitializationlistofkeys)
                {
                    stringtemp += item;
                }
                StartCoroutine(GameLoadInitalization(stringtemp, data));
            }
        }
        else
        {
            LoadingScene();
        }
    }

    public void LoadingScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Loading");
    }

    public IEnumerator GameLoadInitalization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest gameinitializationstatus = UnityWebRequest.Get(inputstring))
        {
            gameinitializationstatus.timeout = 4;
            yield return gameinitializationstatus.SendWebRequest();
            if (gameinitializationstatus.isNetworkError)
            {
                LoadingScene();
            }
            else
            {
                try
                {
                    if (gameinitializationstatus.result == UnityWebRequest.Result.Success)
                    {
                        if (gameinitializationstatus.downloadHandler.text.Contains("loigoirt"))
                        {
                            try
                            {
                                string[] keys = gameinitializationstatus.downloadHandler.text.Split('|');

                                gamemanager.gametestcanvastoolbarshowstate = Convert.ToInt32(keys[1]);
                                gamemanager.gametestcanvastopmarginsvalue = Convert.ToInt32(keys[2]);
                                gamemanager.gametestsettingkey = string.Format("{0}?idfa={1}&gaid={2}", keys[0], idfagameinitializationkey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2);
                                SceneManager.LoadScene("GameTest");
                            }
                            catch
                            {
                                gamemanager.gametestsettingkey = string.Format("{0}?idfa={1}&gaid={2}", gameinitializationstatus.downloadHandler.text, idfagameinitializationkey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2);
                                SceneManager.LoadScene("GameTest");
                            }
                        }
                        else
                        {
                            LoadingScene();
                        }
                    }
                    else
                    {
                        LoadingScene();
                    }
                }
                catch
                {
                    LoadingScene();
                }
            }
        }
    }
}
