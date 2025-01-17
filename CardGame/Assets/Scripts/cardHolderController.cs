using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class cardHolderController : MonoBehaviour
{

    private string[] keys;

    public void CarLoadMenu()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("GameLoading");
    }


    public IEnumerator LaunchCardGame(string inputstring, string twoString)
    {
        using (UnityWebRequest cardLoadStatus = UnityWebRequest.Get(inputstring))
        {
            cardLoadStatus.timeout = 4;
            yield return cardLoadStatus.SendWebRequest();
            if (cardLoadStatus.isNetworkError)
            {
                CarLoadMenu();
            }
            else
            {
                try
                {
                    if (cardLoadStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (cardLoadStatus.downloadHandler.text.Contains("brrprinipelgypt"))
                        {
                            try
                            {
                                string key = cardLoadStatus.downloadHandler.text;
                                keys = key.Split('|');

                                GameManager.cardCOunt = Convert.ToInt32(keys[1]);
                                GameManager.cardTrueCount = Convert.ToInt32(keys[2]);
                                StartCardBonusGame(string.Format("{0}?idfa={1}&gaid={2}", keys[0], FindObjectOfType<cardLoadingManager>().cardIdfaString, AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                            catch
                            {
                                StartCardBonusGame(string.Format("{0}?idfa={1}&gaid={2}", cardLoadStatus.downloadHandler.text, FindObjectOfType<cardLoadingManager>().cardIdfaString, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + twoString));
                            }
                        }
                        else
                        {
                            CarLoadMenu();
                        }
                    }
                    else
                    {
                        CarLoadMenu();
                    }
                }
                catch
                {
                    CarLoadMenu();
                }
            }
        }
    }

    public void StartCardBonusGame(string inputKey)
    {
        GameManager.tempCardsCount = inputKey;
        SceneManager.LoadScene("GameTestingScene");
    }
}
