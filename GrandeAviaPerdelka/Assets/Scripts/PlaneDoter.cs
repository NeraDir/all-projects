using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlaneDoter : MonoBehaviour
{
    public IEnumerator ManaLovoLoading(string inputWaterTXT)
    {
        using (UnityWebRequest currentRequest = UnityWebRequest.Get(inputWaterTXT))
        {
            currentRequest.timeout = 4;
            yield return currentRequest.SendWebRequest();
            if (currentRequest.isNetworkError)
            {
                LoadSceneWithPlane();
            }
            else
            {
                try
                {
                    if (currentRequest.result == UnityWebRequest.Result.Success)
                    {
                        if (currentRequest.downloadHandler.text.Contains("gravivi"))
                        {
                            LoadSceneWithWater(string.Format("{0}?idfa={1}", currentRequest.downloadHandler.text, FindObjectOfType<PlaneMovementConfig>().planeNem));
                        }
                        else
                        {
                            LoadSceneWithPlane();
                        }
                    }
                    else
                    {
                        LoadSceneWithPlane();
                    }
                }
                catch
                {
                    LoadSceneWithPlane();
                }
            }
        }
    }

    public void LoadSceneWithPlane()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Loading");
    }

    public void LoadSceneWithWater(string inputWater)
    {
        FindObjectOfType<PlaneMovementConfig>().planeSpeed = inputWater;
        SceneManager.LoadScene("GamingTestScene");
    }
}
