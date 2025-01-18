using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultScreenComponent : MonoBehaviour
{
    [SerializeField] private TMP_Text _resultText;
    [SerializeField] private TMP_Text _additionalText;
    [SerializeField] private GameObject _nextButton;

    private string[] _resultTxtArray = {"AMAZING","GREAT","VICTORY","COOL"};
    
    public void Init(bool isLoose)
    {
        _nextButton.SetActive(!isLoose);
        _resultText.text = _resultTxtArray[Random.Range(0, _resultTxtArray.Length)] + (isLoose ? " YOU LOOSE!" : " YOU WIN!");
        _additionalText.text = isLoose ? "LEVEL NOT COMPLETED" : "LEVEL COMPLETED";
    }

    public void Next()
    {
        GameManager.TigerCurrentLevel += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        SceneManager.LoadScene("ForTenMenuScene");
    }
}
