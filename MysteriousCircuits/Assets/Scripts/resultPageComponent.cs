using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class resultPageComponent : MonoBehaviour
{
    [SerializeField]
    private Button[] _buttons;

    [SerializeField]
    private TMP_Text _resultTxt;

    [SerializeField]
    private TMP_Text _subResultTxt;

    private void Awake()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            Debug.Log(i);
            int buttonIndex = i;
            _buttons[buttonIndex].onClick.AddListener(() => OnClickButton(buttonIndex));
        }
    }

    public void Init(bool isWin)
    {
        _buttons[0].gameObject.SetActive(isWin);
        _resultTxt.text = isWin ? "WIN!" : "LOOSE!";
        _subResultTxt.text = isWin ? "LEVEL COMPLETED!" : "LEVEL NOT COMPLETED!";
    }

    private void OnClickButton(int index)
    {
        Debug.Log(index);
        switch (index)
        {
            case 0:
                Next();
                break;
            case 1:
                Menu();
                break;
            case 2:
                Restart();
                break;
        }
    }

    private void Next()
    {
        gameController.LevelIndex += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Menu()
    {
        gameController.LevelIndex = 0;
        Scene nextScene = SceneManager.CreateScene("MysteriousCircuitsMenuScene");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(nextScene);
        GameObject menuCanvas = Resources.Load("Prefabs/MysteriousCircuitsMenu") as GameObject;
        Instantiate(menuCanvas);
        SceneManager.UnloadScene(currentScene);
    }

    private void Restart()
    {
        gameController.LevelIndex = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
