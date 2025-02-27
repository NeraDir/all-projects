using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultComponent : MonoBehaviour
{
    public string resultData;

    [SerializeField] private Text resultTxt;

    [SerializeField] private GameObject _nextButton;

    private void OnEnable()
    {
        if (resultData.Contains("NOT"))
        {
            _nextButton.SetActive(false);
        }
        else
        {
            _nextButton.SetActive(true);
        }
        resultTxt.text = resultData;
    }

    public void OnNext()
    {
        GameComponent.Level += 1;
        if (GameComponent.Level > GameComponent.MaxLevel)
        {
            GameComponent.MaxLevel = GameComponent.Level;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnRestart()
    {
        if (GameComponent.Level > GameComponent.MaxLevel)
        {
            GameComponent.MaxLevel = GameComponent.Level;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMenu()
    {
        if (GameComponent.Level > GameComponent.MaxLevel)
        {
            GameComponent.MaxLevel = GameComponent.Level;
        }
        GameComponent.Level = 1;
        SceneManager.LoadScene("Menu");
    }
}
