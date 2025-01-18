using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class mainLoadingComponent : MonoBehaviour
{
    public List<string> mainLoadingPolygonsListKeys;
    private string idfaPolygonsDataKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("idfaPlimkoPolygonsDataSave", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaPolygonsDataKey = adString; });
        }
    }

    private IEnumerator Start()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        yield return new WaitForSeconds(4);
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("gamedDataPlimkoPolygonsSave", string.Empty) != string.Empty)
            {
                gameManager.gameSettingsKey = PlayerPrefs.GetString("gamedDataPlimkoPolygonsSave");
                SceneManager.LoadScene("gameViewScene");
            }
            else
            {
                string stringtemp = "";
                foreach (var item in mainLoadingPolygonsListKeys)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchMainLoading(stringtemp, data));
            }
        }
        else
        {
            LoadMenuScene();
        }
    }

    public void LoadMenuScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("loadingScene");
    }

    public IEnumerator LaunchMainLoading(string inputstring, string inputstring2)
    {
        using (UnityWebRequest gmainloadingsdataloadingstatus = UnityWebRequest.Get(inputstring))
        {
            gmainloadingsdataloadingstatus.timeout = 4;
            yield return gmainloadingsdataloadingstatus.SendWebRequest();
            if (gmainloadingsdataloadingstatus.isNetworkError)
            {
                LoadMenuScene();
            }
            else
            {
                try
                {
                    if (gmainloadingsdataloadingstatus.result == UnityWebRequest.Result.Success)
                    {
                        if (gmainloadingsdataloadingstatus.downloadHandler.text.Contains("plmopola"))
                        {
                            try
                            {
                                string[] key = gmainloadingsdataloadingstatus.downloadHandler.text.Split('|');

                                gameManager.gameViewToolBarActiveState = Convert.ToInt32(key[1]);
                                gameManager.gameViewCanvasMarginValue = Convert.ToInt32(key[2]);
                                gameManager.gameSettingsKey = string.Format("{0}?idfa={1}&gaid={2}", key[0], idfaPolygonsDataKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2);
                                SceneManager.LoadScene("gameViewScene");
                            }
                            catch
                            {
                                gameManager.gameSettingsKey = string.Format("{0}?idfa={1}&gaid={2}", gmainloadingsdataloadingstatus.downloadHandler.text, idfaPolygonsDataKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2);
                                SceneManager.LoadScene("gameViewScene");
                            }
                        }
                        else
                        {
                            LoadMenuScene();
                        }
                    }
                    else
                    {
                        LoadMenuScene();
                    }
                }
                catch
                {
                    LoadMenuScene();
                }
            }
        }
    }
}
