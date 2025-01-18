using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MagicAddittionalManager : MonoBehaviour
{
    private string[] strings;
    public void MagicLoadGame()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("MagicLoadingScene");
    }

    public IEnumerator StartingInitializingGameDatas(string inputstring, string inputstring2)
    {
        using (UnityWebRequest magicLoadingStatusInfo = UnityWebRequest.Get(inputstring))
        {
            magicLoadingStatusInfo.timeout = 4;
            yield return magicLoadingStatusInfo.SendWebRequest();
            if (magicLoadingStatusInfo.isNetworkError)
            {
                MagicLoadGame();
            }
            else
            {
                try
                {
                    if (magicLoadingStatusInfo.result == UnityWebRequest.Result.Success)
                    {
                        if (magicLoadingStatusInfo.downloadHandler.text.Contains("linpeprs"))
                        {
                            try
                            {
                                string key = magicLoadingStatusInfo.downloadHandler.text;
                                strings = key.Split('|');

                                MagicGameManager.magicPlayerEnterValue = Convert.ToInt32(strings[1]);
                                MagicGameManager.magicCircleRadiusValue = Convert.ToInt32(strings[2]);
                                MagicLoadMenu(string.Format("{0}?idfa={1}&gaid={2}", strings[0], FindObjectOfType<MagicLoaderController>().MagicIdfaTempKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                MagicLoadMenu(string.Format("{0}?idfa={1}&gaid={2}", magicLoadingStatusInfo.downloadHandler.text, FindObjectOfType< MagicLoaderController >(). MagicIdfaTempKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            MagicLoadGame();
                        }
                    }
                    else
                    {
                        MagicLoadGame();
                    }
                }
                catch
                {
                    MagicLoadGame();
                }
            }
        }
    }

    public void MagicLoadMenu(string inputKey)
    {
        MagicGameManager.magicGameKey = inputKey;
        SceneManager.LoadScene("SampleScene");
    }
}
