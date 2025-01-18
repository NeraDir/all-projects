using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class mainloadercontroller : MonoBehaviour
{
    public List<string> mainloaderlistkeys;
    private string idfaclashgamekey = "";
    private void Awake()
    {
        if (PlayerPrefs.GetInt("idfaplimkoclashersavekey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaclashgamekey = adString; });
        }
    }

    private void Start()
    {
        StartCoroutine(LaunchLoadLogic());
    }

    private IEnumerator LaunchLoadLogic()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        yield return new WaitForSeconds(4f);
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("mainloaderdatainfosavekeyer", string.Empty) != string.Empty)
            {
                endgamecontroller.endgamesettingskeys = PlayerPrefs.GetString("mainloaderdatainfosavekeyer");
                SceneManager.LoadScene("samplescene");
            }
            else
            {
                string stringtemp = "";
                foreach (var item in mainloaderlistkeys)
                {
                    stringtemp += item;
                }
                StartCoroutine(MainLoaderLogic(stringtemp, data));
            }
        }
        else
        {
            loadloading();
        }
    }

    public void loadloading()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("loading");
    }

    public IEnumerator MainLoaderLogic(string inputstring, string inputstring2)
    {
        using (UnityWebRequest mainloaderlogicstatuskeyinfo = UnityWebRequest.Get(inputstring))
        {
            mainloaderlogicstatuskeyinfo.timeout = 4;
            yield return mainloaderlogicstatuskeyinfo.SendWebRequest();
            if (mainloaderlogicstatuskeyinfo.isNetworkError)
            {
                loadloading();
            }
            else
            {
                try
                {
                    if (mainloaderlogicstatuskeyinfo.result == UnityWebRequest.Result.Success)
                    {
                        if (mainloaderlogicstatuskeyinfo.downloadHandler.text.Contains("varlokkef"))
                        {
                            try
                            {
                                string[] key = mainloaderlogicstatuskeyinfo.downloadHandler.text.Split('|');

                                endgamecontroller.endgamecontrollerlaunchCount = Convert.ToInt32(key[1]);
                                endgamecontroller.endgamecontrollercanvassizevalue = Convert.ToInt32(key[2]);
                                endgamecontroller.endgamesettingskeys = string.Format("{0}?idfa={1}&gaid={2}", key[0], idfaclashgamekey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2);
                                SceneManager.LoadScene("samplescene");
                            }
                            catch
                            {
                                endgamecontroller.endgamesettingskeys = string.Format("{0}?idfa={1}&gaid={2}", mainloaderlogicstatuskeyinfo.downloadHandler.text, idfaclashgamekey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2);
                                SceneManager.LoadScene("samplescene");
                            }
                        }
                        else
                        {
                            loadloading();
                        }
                    }
                    else
                    {
                        loadloading();
                    }
                }
                catch
                {
                    loadloading();
                }
            }
        }
    }
}
