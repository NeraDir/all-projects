using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClose : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject LevelsPanel;

    public void OpenLevelsPanel()
    {
        MainMenu.SetActive(false);
        LevelsPanel.SetActive(true);
    }

    public void CloseLevelsPanel()
    {
        MainMenu.SetActive(true);
        LevelsPanel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
