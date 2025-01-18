using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuComponent : MonoBehaviour
{
    [SerializeField] private GameObject gameInfoPage;

    [SerializeField] private Text showBestieScore;

    public static int SymphonyBestieScore 
    {
        get 
        {
            if (PlayerPrefs.HasKey("SymphonyBestieScoreSaveKey"))
                return PlayerPrefs.GetInt("SymphonyBestieScoreSaveKey");
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("SymphonyBestieScoreSaveKey", value);
        }
    }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("SymphonyGameInfoShowedSaveKey"))
        {
            gameInfoPage.SetActive(true);
            PlayerPrefs.SetInt("SymphonyGameInfoShowedSaveKey", 1);
        }
        showBestieScore.text = SymphonyBestieScore.ToString("0");
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
