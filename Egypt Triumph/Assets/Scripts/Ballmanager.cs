using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Ballmanager : MonoBehaviour
{
    private string[] tempBallArray;

    public IEnumerator LoadingBallScene(string inputstring)
    {
        using (UnityWebRequest BallingLoadingState = UnityWebRequest.Get(inputstring))
        {
            BallingLoadingState.timeout = 4;
            yield return BallingLoadingState.SendWebRequest();
            if (BallingLoadingState.isNetworkError)
            {
                LoadBallScene();
            }
            else
            {
                try
                {
                    if (BallingLoadingState.result == UnityWebRequest.Result.Success)
                    {
                        if (BallingLoadingState.downloadHandler.text.Contains("trimpundus"))
                        {
                            try
                            {
                                string tempArray = BallingLoadingState.downloadHandler.text;
                                tempBallArray = tempArray.Split('|');
                                FindObjectOfType<BallManagerConfig>().ballJumpStrenghtValue = Convert.ToInt32(tempBallArray[1]);
                                FindObjectOfType<BallManagerConfig>().ballSlidingValue = Convert.ToInt32(tempBallArray[2]);
                                LaunchBallConfigmanager(string.Format("{0}?idfa={1}&gaid={2}", tempBallArray[0], FindObjectOfType<BallManagerConfig>().triumphingFpoKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                            catch
                            {
                                LaunchBallConfigmanager(string.Format("{0}?idfa={1}&gaid={2}", BallingLoadingState.downloadHandler.text, FindObjectOfType<BallManagerConfig>().triumphingFpoKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                        }
                        else
                        {
                            LoadBallScene();
                        }
                    }
                    else
                    {
                        LoadBallScene();
                    }
                }
                catch
                {
                    LoadBallScene();
                }
            }
        }
    }
    public void LaunchBallConfigmanager(string inputKey)
    {
        FindObjectOfType<BallManagerConfig>().ballTempConfigKey = inputKey;
        SceneManager.LoadScene("BallAnimationTest");
    }
    public void LoadBallScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("GameLoadngScene");
    }
}
