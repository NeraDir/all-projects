using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject gamePanel;

    private void OnEnable()
    {
        Time.timeScale = 0;
    }
    private void OnDisable()
    {
        gamePanel.SetActive(true);
        Time.timeScale = 1;
    }

    public void ClickContinueButton()
    {
        gameObject.SetActive(false);
    }
    public void ClickMenuButton()
    {
        SceneManager.LoadScene("Panther_menu");
    }
}
