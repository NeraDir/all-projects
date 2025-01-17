using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControllerComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject _cherryManiahowToPlayScreen;

    [SerializeField]
    private TMP_Text _cherryManiaBestLevelTxt;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("CherryManiaHowToPlayScreensaves"))
        {
            _cherryManiahowToPlayScreen.SetActive(true);
            PlayerPrefs.SetInt("CherryManiaHowToPlayScreensaves", 1);
        }
        _cherryManiaBestLevelTxt.text = FruitGameManager.BestReachLevelValue.ToString();
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
