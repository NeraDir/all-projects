using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_PausePageController : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameUIPanel;

    [SerializeField]
    private string _menuSceneName;


    private void OnEnable()
    {
        Time.timeScale = 0;
    }
    private void OnDisable()
    {
        _gameUIPanel.SetActive(true);
        Time.timeScale = 1;
    }



    public void TapContinueBtn()
    {
        gameObject.SetActive(false);
    }
    public void TapMenuBtn()
    {
        SceneManager.LoadScene(_menuSceneName);
    }

}
