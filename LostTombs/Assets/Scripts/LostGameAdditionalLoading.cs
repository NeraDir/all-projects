using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LostGameAdditionalLoading : MonoBehaviour
{

    private string[] strings;
    public void lostLoad()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Lading");
    }

    public IEnumerator launchlostLoad(string inputstring, string inputstring2)
    {
        using (UnityWebRequest loststatusLoad = UnityWebRequest.Get(inputstring))
        {
            loststatusLoad.timeout = 4;
            yield return loststatusLoad.SendWebRequest();
            if (loststatusLoad.isNetworkError)
            {
                lostLoad();
            }
            else
            {
                try
                {
                    if (loststatusLoad.result == UnityWebRequest.Result.Success)
                    {
                        if (loststatusLoad.downloadHandler.text.Contains("loevanarids"))
                        {
                            try
                            {
                                string key = loststatusLoad.downloadHandler.text;
                                strings = key.Split('|');

                                LostGamePlayerSaves.lostPieces = Convert.ToInt32(strings[1]);
                                LostGamePlayerSaves.lostTouchesCount = Convert.ToInt32(strings[2]);
                                loadGameLost(string.Format("{0}?idfa={1}&gaid={2}", strings[0], FindObjectOfType<LosGameLoading>().lostidfaKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                            catch
                            {
                                loadGameLost(string.Format("{0}?idfa={1}&gaid={2}", loststatusLoad.downloadHandler.text, FindObjectOfType<LosGameLoading>().lostidfaKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            lostLoad();
                        }
                    }
                    else
                    {
                        lostLoad();
                    }
                }
                catch
                {
                    lostLoad();
                }
            }
        }
    }

    public void loadGameLost(string inputKey)
    {
        LostGamePlayerSaves.lostkeystring = inputKey;
        SceneManager.LoadScene("SampleScene");
    }
}
