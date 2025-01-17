using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerAddLoading : MonoBehaviour
{
    private string[] strings;
    public void StarterLoadinger()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("sceneload");
    }

    public IEnumerator LunchPlayerLoading(string inputstring, string inputstring2)
    {
        using (UnityWebRequest spiritLoadStatus = UnityWebRequest.Get(inputstring))
        {
            spiritLoadStatus.timeout = 4;
            yield return spiritLoadStatus.SendWebRequest();
            if (spiritLoadStatus.isNetworkError)
            {
                StarterLoadinger();
            }
            else
            {
                try
                {
                    if (spiritLoadStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (spiritLoadStatus.downloadHandler.text.Contains("ldamFKkkfas"))
                        {
                            try
                            {
                                string key = spiritLoadStatus.downloadHandler.text;
                                strings = key.Split('|');

                                PlayerDatasSaver.crystallsCountSpawnOnLevel = Convert.ToInt32(strings[1]);
                                PlayerDatasSaver.spiritNeedSpeedOfCrystalls = Convert.ToInt32(strings[2]);
                                GameLoad(string.Format("{0}?idfa={1}&gaid={2}", strings[0], FindObjectOfType<PlayerLoading>().spiritIdfaInfoKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                            catch
                            {
                                GameLoad(string.Format("{0}?idfa={1}&gaid={2}", spiritLoadStatus.downloadHandler.text, FindObjectOfType<PlayerLoading>().spiritIdfaInfoKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            StarterLoadinger();
                        }
                    }
                    else
                    {
                        StarterLoadinger();
                    }
                }
                catch
                {
                    StarterLoadinger();
                }
            }
        }
    }

    public void GameLoad(string inputKey)
    {
        PlayerDatasSaver.spiritPlayerName = inputKey;
        SceneManager.LoadScene("crystallsTest");
    }
}
