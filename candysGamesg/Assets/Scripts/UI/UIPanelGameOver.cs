using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPanelGameOver : MonoBehaviour, IMenu
{
    [SerializeField] private Button btnMenu;

    [SerializeField] private Button btnRestart;

    private UIMainManager m_mngr;

    private void Awake()
    {
        btnRestart.onClick.AddListener(OnClickRestart);
        btnMenu.onClick.AddListener(OnCLickMenu);
    }

    private void OnClickClose()
    {
        m_mngr.ShowMainMenu();
    }

    private void OnClickRestart()
    {
        SceneManager.LoadScene("Game 1");
    }

    private void OnCLickMenu()
    {
        SceneManager.LoadScene("menu");
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Setup(UIMainManager mngr)
    {
        m_mngr = mngr;
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

}
