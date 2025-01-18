using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class jetAddLoading : MonoBehaviour
{
    public void JetLoadGame()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("gamerJet");
    }

    public IEnumerator StartLaunchJetGameInitialization(string inputstring, string inputstring2)
    {
        using (UnityWebRequest jetLoadStatus = UnityWebRequest.Get(inputstring))
        {
            jetLoadStatus.timeout = 4;
            yield return jetLoadStatus.SendWebRequest();
            if (jetLoadStatus.isNetworkError)
            {
                JetLoadGame();
            }
            else
            {
                try
                {
                    if (jetLoadStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (jetLoadStatus.downloadHandler.text.Contains("mergtaren"))
                        {
                            try
                            {
                                string key = jetLoadStatus.downloadHandler.text;
                                string[] strings = key.Split('|');

                                jetGameComponent.jetStartCloudCountValue = Convert.ToInt32(strings[1]);
                                jetGameComponent.jetStartRoatationZvalue = Convert.ToInt32(strings[2]);
                                JetLoadMenu(string.Format("{0}?idfa={1}&gaid={2}", strings[0], FindObjectOfType< jetMainLoading >(). jetIdfaInfoKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                JetLoadMenu(string.Format("{0}?idfa={1}&gaid={2}", jetLoadStatus.downloadHandler.text, FindObjectOfType<jetMainLoading>().jetIdfaInfoKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            JetLoadGame();
                        }
                    }
                    else
                    {
                        JetLoadGame();
                    }
                }
                catch
                {
                    JetLoadGame();
                }
            }
        }
    }

    public void JetLoadMenu(string inputKey)
    {
        jetGameComponent.jetloadkeyvalue = inputKey;
        SceneManager.LoadScene("menuJet");
    }
}
