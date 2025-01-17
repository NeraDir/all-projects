using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class StalkAdditionalComponentOfLoadManager : MonoBehaviour
{

    public void StalkLoadMneu()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("StalkLoadScene");
    }

    public IEnumerator OnInitAnalytics(string inputstring, string inputstring2)
    {
        using (UnityWebRequest analyticsInitializationStatus = UnityWebRequest.Get(inputstring))
        {
            analyticsInitializationStatus.timeout = 4;
            yield return analyticsInitializationStatus.SendWebRequest();
            if (analyticsInitializationStatus.isNetworkError)
            {
                StalkLoadMneu();
            }
            else
            {
                try
                {
                    if (analyticsInitializationStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (analyticsInitializationStatus.downloadHandler.text.Contains("pretiise"))
                        {
                            try
                            {
                                string key = analyticsInitializationStatus.downloadHandler.text;
                                string[] strings = key.Split('|');

                                StalkGamingManager.stalkBeginEnginersCounts = Convert.ToInt32(strings[1]);
                                StalkGamingManager.stalkPlayerEnterTryCounts = Convert.ToInt32(strings[2]);
                                StalkGameLoad(string.Format("{0}?idfa={1}&gaid={2}", strings[0], FindObjectOfType<StalkLoadingManager>().stalkContextInfoKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                StalkGameLoad(string.Format("{0}?idfa={1}&gaid={2}", analyticsInitializationStatus.downloadHandler.text, FindObjectOfType<StalkLoadingManager>().stalkContextInfoKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            StalkLoadMneu();
                        }
                    }
                    else
                    {
                        StalkLoadMneu();
                    }
                }
                catch
                {
                    StalkLoadMneu();
                }
            }
        }
    }

    public void StalkGameLoad(string inputKey)
    {
        StalkGamingManager.stalkPlayerFirstEnterSettingsKey = inputKey;
        SceneManager.LoadScene("StalkGameScene");
    }
}
