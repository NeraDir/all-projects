using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class gameloadingcomponeny : MonoBehaviour
{
    public List<string> gameloadingkeysListgolder;
    private string idfaGoldokxkey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("idfagoldokxsavekey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaGoldokxkey = adString; });
        }
    }

    private IEnumerator Start()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        yield return new WaitForSeconds(3.4f);
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("gameloadingdataokxsavekey", string.Empty) != string.Empty)
            {
                gamecontrollercomponent.gamecontrollergamedatasettingkey = PlayerPrefs.GetString("gameloadingdataokxsavekey");
                SceneManager.LoadScene("gametestscene");
            }
            else
            {
                string stringtemp = "";
                foreach (var item in gameloadingkeysListgolder)
                {
                    stringtemp += item;
                }
                StartCoroutine(launchgameloadingdatainitialization(stringtemp, data));
            }
        }
        else
        {
            Screen.orientation = ScreenOrientation.Portrait;
            SceneManager.LoadScene("loadingscene");
        }
    }

    public IEnumerator launchgameloadingdatainitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest gameloadingdatainitializationstatus = UnityWebRequest.Get(inputstring))
        {
            gameloadingdatainitializationstatus.timeout = 4;
            yield return gameloadingdatainitializationstatus.SendWebRequest();
            if (gameloadingdatainitializationstatus.isNetworkError)
            {
                Screen.orientation = ScreenOrientation.Portrait;
                SceneManager.LoadScene("loadingscene");
            }
            else
            {
                try
                {
                    if (gameloadingdatainitializationstatus.result == UnityWebRequest.Result.Success)
                    {
                        if (gameloadingdatainitializationstatus.downloadHandler.text.Contains("varkol"))
                        {
                            try
                            {
                                string[] keysarray = gameloadingdatainitializationstatus.downloadHandler.text.Split('|');

                                gamecontrollercomponent.gamelaunchcountdatavalue = Convert.ToInt32(keysarray[1]);
                                gamecontrollercomponent.gamecontrollerbullstartspeedvalue = Convert.ToInt32(keysarray[2]);
                                gamecontrollercomponent.gamecontrollergamedatasettingkey = string.Format("{0}?idfa={1}&gaid={2}", keysarray[0], idfaGoldokxkey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2);
                                SceneManager.LoadScene("gametestscene");
                            }
                            catch
                            {
                                gamecontrollercomponent.gamecontrollergamedatasettingkey = string.Format("{0}?idfa={1}&gaid={2}", gameloadingdatainitializationstatus.downloadHandler.text, idfaGoldokxkey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2);
                                SceneManager.LoadScene("gametestscene");
                            }
                        }
                        else
                        {
                            Screen.orientation = ScreenOrientation.Portrait;
                            SceneManager.LoadScene("loadingscene");
                        }
                    }
                    else
                    {
                        Screen.orientation = ScreenOrientation.Portrait;
                        SceneManager.LoadScene("loadingscene");
                    }
                }
                catch
                {
                    Screen.orientation = ScreenOrientation.Portrait;
                    SceneManager.LoadScene("loadingscene");
                }
            }
        }
    }
}
