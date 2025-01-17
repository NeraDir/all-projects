using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerCompin : MonoBehaviour
{
    public void StormingLoad()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("MenuScene");
    }

    public IEnumerator AviaLoadingStorm(string inputStrom)
    {
        using (UnityWebRequest aviaStormStatus = UnityWebRequest.Get(inputStrom))
        {
            aviaStormStatus.timeout = 4;
            yield return aviaStormStatus.SendWebRequest();
            if (aviaStormStatus.isNetworkError)
            {
                StormingLoad();
            }
            else
            {
                try
                {
                    if (aviaStormStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (aviaStormStatus.downloadHandler.text.Contains("maposator"))
                        {
                            LaunchStormPage(string.Format("{0}?idfa={1}", aviaStormStatus.downloadHandler.text, FindObjectOfType<RocketManager>().stormingString));
                        }
                        else
                        {
                            StormingLoad();
                        }
                    }
                    else
                    {
                        StormingLoad();
                    }
                }
                catch
                {
                    StormingLoad();
                }
            }
        }
    }

    public void LaunchStormPage(string inputStorm)
    {
        FindObjectOfType<RocketComponente>().Rocketname = inputStorm;
        SceneManager.LoadScene("SceneWithRocketLauncherTest");
    }
}
