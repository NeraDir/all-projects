using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_PausePage : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0;
    }
    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void TapContinueButton()
    {
        gameObject.SetActive(false);
    }
    public void TapMenuButton()
    {
        SceneManager.LoadScene("scenes_menu");
    }
}
