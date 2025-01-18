using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject _instructionScreen;

    [SerializeField]
    private TMP_Text _bestScoreDisplay;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("MenuTigerEatInstructionOPened"))
        {
            _instructionScreen.SetActive(true);
            PlayerPrefs.SetString("MenuTigerEatInstructionOPened", "true");
        }

    }

    private void LateUpdate()
    {
        _bestScoreDisplay.text = GameManager.tigerBestScore.ToString("0");
    }

    public void OnPlayButtonPressed() 
    {
        SceneManager.LoadScene("Game");
    }

    public void OnExitButtonPressed()
    {
        Application.Quit();
    }
}
