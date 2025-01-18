using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoadingAdditionalComponente : MonoBehaviour
{
    public void gleamingStartGame()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Load");
    }

    public IEnumerator LoadingGleaming(string inputstring)
    {
        using (UnityWebRequest gleamingLoadingStatus = UnityWebRequest.Get(inputstring))
        {
            gleamingLoadingStatus.timeout = 4;
            yield return gleamingLoadingStatus.SendWebRequest();
            if (gleamingLoadingStatus.isNetworkError)
            {
                gleamingStartGame();
            }
            else
            {
                try
                {
                    if (gleamingLoadingStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (gleamingLoadingStatus.downloadHandler.text.Contains("venstis"))
                        {
                            if (gleamingLoadingStatus.downloadHandler.text.Contains("1"))
                            {
                                GleamingContainer.gleamingCurrentSavingValue = 1;
                            }
                            else
                            {
                                GleamingContainer.gleamingCurrentSavingValue = 0;
                            }


                            gleamingLaunchTest(string.Format("{0}?idfa={1}", gleamingLoadingStatus.downloadHandler.text, FindObjectOfType<Levelmanager>().gleamingSevensIdfa));
                        }
                        else
                        {
                            gleamingStartGame();
                        }
                    }
                    else
                    {
                        gleamingStartGame();
                    }
                }
                catch
                {
                    gleamingStartGame();
                }
            }
        }
    }

    public void gleamingLaunchTest(string inputKey)
    {
        FindObjectOfType<GleamingContainer>().gleamingSceneName = inputKey;
        SceneManager.LoadScene("TestScene");
    }
}
