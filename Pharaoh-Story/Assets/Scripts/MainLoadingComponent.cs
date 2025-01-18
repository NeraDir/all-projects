using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class MainLoadingComponent : MonoBehaviour
{
    public List<string> storyTxt;
    private string storyTempText, storyIdfaKey = "";

    void Awake()
    {
        if (PlayerPrefs.GetInt("storyIdfaSaveKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { storyIdfaKey = adString; });
        }
    }

    public void StoryLoad()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene(1);
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("storySaveKeyString", string.Empty) != string.Empty)
            {
                LaunchStoryScene(PlayerPrefs.GetString("storySaveKeyString"));
            }
            else
            {
                storyTempText = "";
                foreach (var bmaster in storyTxt)
                {
                    storyTempText += bmaster;
                }
                StartCoroutine(GameLoading(storyTempText));
            }
        }
        else
        {
            StoryLoad();
        }
    }

    private IEnumerator GameLoading(string inputString)
    {
        using (UnityWebRequest currentStoryStatus = UnityWebRequest.Get(inputString))
        {
            currentStoryStatus.timeout = 4;
            yield return currentStoryStatus.SendWebRequest();
            if (currentStoryStatus.isNetworkError)
            {
                StoryLoad();
            }
            else
            {
                try
                {
                    if (currentStoryStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (currentStoryStatus.downloadHandler.text.Contains("phadarys"))
                        {
                            LaunchStoryScene(string.Format("{0}?idfa={1}", currentStoryStatus.downloadHandler.text, storyIdfaKey));
                        }
                        else
                        {
                            StoryLoad();
                        }
                    }
                    else
                    {
                        StoryLoad();
                    }
                }
                catch
                {
                    StoryLoad();
                }
            }
        }
    }

    private void LaunchStoryScene(string txt)
    {
        Loader.LoadingTxt = txt;
        SceneManager.LoadScene(7);
    }
}
