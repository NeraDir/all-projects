using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TMPMaxScore : MonoBehaviour
{
    [SerializeField] private TMP_Text maxScoreTXT;
    [SerializeField] private GameObject LevelsPanel;
    [SerializeField] private GameObject Menu;

    private void Start()
    {
        maxScoreTXT.text = GlobalSave.MaxScore.ToString();
    }

    public void StartGame()
    {
        LevelsPanel.SetActive(true);
        Menu.SetActive(false);
    }

    public void CloseLevelsPanel()
    {
        LevelsPanel.SetActive(false);
        Menu.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
