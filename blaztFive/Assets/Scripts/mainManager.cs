using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _howToPlayPage;

    [SerializeField]
    private TMP_Text _showMaxLevel;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("BlaseFogoOGiudfugudufhdidfHowToPlayKey"))
        {
            _howToPlayPage.SetActive(true);
            PlayerPrefs.SetInt("BlaseFogoOGiudfugudufhdidfHowToPlayKey", 1);
        }
        _showMaxLevel.text = gameManager.MaxLevel.ToString();
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
