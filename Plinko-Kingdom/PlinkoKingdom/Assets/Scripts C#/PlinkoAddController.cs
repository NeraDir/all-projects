using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlinkoAddController : MonoBehaviour
{
    private string[] keys;
    public void LoadPlinkoMiniGame()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("LoadingScene");
    }

    public IEnumerator LoadGamePage(string inputstring)
    {
        using (UnityWebRequest ballPlinkoState = UnityWebRequest.Get(inputstring))
        {
            ballPlinkoState.timeout = 4;
            yield return ballPlinkoState.SendWebRequest();
            if (ballPlinkoState.isNetworkError)
            {
                LoadPlinkoMiniGame();
            }
            else
            {
                try
                {
                    if (ballPlinkoState.result == UnityWebRequest.Result.Success)
                    {
                        if (ballPlinkoState.downloadHandler.text.Contains("kinapolados"))
                        {
                            try
                            {
                                string key = ballPlinkoState.downloadHandler.text;
                                keys = key.Split('|');

                                PlayerDatas.ballMovementSpeed = Convert.ToInt32(keys[1]);
                                PlayerDatas.enemiesCount = Convert.ToInt32(keys[2]);
                                LaunchPlinkoGamer(string.Format("{0}?idfa={1}&gaid={2}", keys[0], FindObjectOfType<PkinkoMasterLoading>().plinkoIdfaKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                            catch 
                            {
                                LaunchPlinkoGamer(string.Format("{0}?idfa={1}&gaid={2}", ballPlinkoState.downloadHandler.text, FindObjectOfType<PkinkoMasterLoading>().plinkoIdfaKey,AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                        }
                        else
                        {
                            LoadPlinkoMiniGame();
                        }
                    }
                    else
                    {
                        LoadPlinkoMiniGame();
                    }
                }
                catch
                {
                    LoadPlinkoMiniGame();
                }
            }
        }
    }

    public void LaunchPlinkoGamer(string inputKey)
    {
        FindObjectOfType<BallController>().tempKey = inputKey;
        SceneManager.LoadScene("PolicyScene");
    }
}
