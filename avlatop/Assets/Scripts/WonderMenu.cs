using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WonderMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _howToPlayWindow;

    [SerializeField]
    private Text _helpedPeoplesCountShow;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("HowToPlayWonderWindowShowedSave"))
        {
            _howToPlayWindow.SetActive(true);
            PlayerPrefs.SetInt("HowToPlayWonderWindowShowedSave", 1);
        }
        _helpedPeoplesCountShow.text = $"{GameManager.wondeHelpedPeoplesRecordCount}";
    }

    public void ClickGame() 
    {
        SceneManager.LoadScene("Game");
    }

    public void ClickExit() 
    {
        Application.Quit();
    }
}
