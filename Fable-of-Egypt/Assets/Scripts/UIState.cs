using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIState : MonoBehaviour
{
    //[SerializeField] private GameObject canvasMainMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelsMenu;
    [SerializeField] private GameObject shopMenu;

    [SerializeField] private GameObject buttonShoot;

    public void StatePanel(GameObject obj) => obj.SetActive(!obj.activeSelf);
    public void OpenMainMenu()
    {
       // canvasMainMenu.SetActive(true);
        mainMenu.SetActive(true);
        buttonShoot.SetActive(false);
    }

	public void OnClickCloseGame()
{
Application.Quit();
}

    public void CloseAll()
    {
        mainMenu.SetActive(false);
        shopMenu.SetActive(false);
        levelsMenu.SetActive(false);
        //canvasMainMenu.SetActive(false);
        buttonShoot.SetActive(true);
    }
}
