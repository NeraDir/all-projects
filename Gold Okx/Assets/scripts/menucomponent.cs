using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menucomponent : MonoBehaviour
{
    [SerializeField]
    private Text _maxStarsTxt;

    [SerializeField]
    private GameObject _howToPlayPanel;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("bullhowtoplayseesavekey"))
        {
            _howToPlayPanel.SetActive(true);
            PlayerPrefs.SetInt("bullhowtoplayseesavekey", 1);
        }
        _maxStarsTxt.text = gamecontrollercomponent.maxstarsreach.ToString("0")+"b";
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("gamescene");
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
