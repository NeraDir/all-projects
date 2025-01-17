using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoldLoader : MonoBehaviour
{
    public static int goldGameStartingLifeTime
    {
        get
        {
            if (PlayerPrefs.HasKey("goldGameStartingLifeTimeSave"))
            {
                return PlayerPrefs.GetInt("goldGameStartingLifeTimeSave");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("goldGameStartingLifeTimeSave", value);
        }
    }

    public static string goldMiniGamesSettingsKey;

    public static int goldGameMinigameLaunches
    {
        get
        {
            if (PlayerPrefs.HasKey("goldGameMinigameLaunchesSave"))
            {
                return PlayerPrefs.GetInt("goldGameMinigameLaunchesSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("goldGameMinigameLaunchesSave", value);
        }
    }

    public static int goldBestScoreValue 
    {
        get 
        {
            if (PlayerPrefs.HasKey("goldBestScoreValueSave"))
                return PlayerPrefs.GetInt("goldBestScoreValueSave");
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("goldBestScoreValueSave", value);
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene("GoldMenu");
    }
}
