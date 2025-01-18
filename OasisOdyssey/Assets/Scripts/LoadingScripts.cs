using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScripts : MonoBehaviour
{
    public static int LoadingPlayerReachedEnemies
    {
        get
        {
            if (PlayerPrefs.HasKey("LoadingPlayerReachedEnemiesSave"))
            {
                return PlayerPrefs.GetInt("LoadingPlayerReachedEnemiesSave");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("LoadingPlayerReachedEnemiesSave", value);
        }
    }
    public static string loadingSceneName;
    public static int loadingTryLoadsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("loadingTryLoadsCountSave"))
            {
                return PlayerPrefs.GetInt("loadingTryLoadsCountSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("loadingTryLoadsCountSave", value);
        }
    }

    public string loadingScene;

    public float loadingTime;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(loadingTime);
        SceneManager.LoadScene(loadingScene);
    }
}
