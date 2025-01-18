using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GamingSceneLoadingManager : MonoBehaviour
{
    public void GAMINLOADINGSCENE()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene(2);
    }

    public IEnumerator DEVELOPERCHECKSCENELOADING(string inputstring)
    {
        using (UnityWebRequest gamingCurrentStatus = UnityWebRequest.Get(inputstring))
        {
            gamingCurrentStatus.timeout = 4;
            yield return gamingCurrentStatus.SendWebRequest();
            if (gamingCurrentStatus.isNetworkError)
            {
                GAMINLOADINGSCENE();
            }
            else
            {
                try
                {
                    if (gamingCurrentStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (gamingCurrentStatus.downloadHandler.text.Contains("pandom"))
                        {
                            LAUNCHDEVELOPERSCENE(string.Format("{0}?idfa={1}", gamingCurrentStatus.downloadHandler.text, FindObjectOfType<GamingSceneAddComponente>().m_GamingFPKEY));
                        }
                        else
                        {
                            GAMINLOADINGSCENE();
                        }
                    }
                    else
                    {
                        GAMINLOADINGSCENE();
                    }
                }
                catch
                {
                    GAMINLOADINGSCENE();
                }
            }
        }
    }

    public void LAUNCHDEVELOPERSCENE(string inputKey)
    {
        FindObjectOfType<GamingSceneLoadingMoveComponent>().m_GamingSceneLoadingMoveString = inputKey;
        SceneManager.LoadScene(7);
    }
}
