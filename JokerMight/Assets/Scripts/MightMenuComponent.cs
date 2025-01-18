using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MightMenuComponent : MonoBehaviour
{
    public GameObject howToPlayPage;

    public TMP_Text showBestScore;

    public static int BestScore
    {
        get 
        {
            if (PlayerPrefs.HasKey("MightBestScoreSaveKey"))
                return PlayerPrefs.GetInt("MightBestScoreSaveKey");
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("MightBestScoreSaveKey", value);
        }
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("MightHowPlayPageShowSaveKey"))
        {
            howToPlayPage.SetActive(true);
            PlayerPrefs.SetInt("MightHowPlayPageShowSaveKey", 1);
        }
        showBestScore.text = BestScore.ToString();
    }

    public void OnClickPlay() 
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnClickExit() 
    {
        Application.Quit();
    }
}
