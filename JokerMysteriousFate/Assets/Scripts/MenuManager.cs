using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Text MaxLevelTXT;

    public GameObject PlayInfoPage;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("JokersPlayInfoShowedSaveKey"))
        {
            PlayInfoPage.SetActive(true);
            PlayerPrefs.SetInt("JokersPlayInfoShowedSaveKey",1);
        }
        MaxLevelTXT.text = PlayerDatasSaveComponent.MaxReachedLevel.ToString();
    }

    public void OnClickPlay() 
    {
        SceneManager.LoadScene("Game");
    }

    public void OnClickExti() 
    {
        Application.Quit();
    }
}
