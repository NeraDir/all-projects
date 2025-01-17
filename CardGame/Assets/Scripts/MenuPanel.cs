using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuPanel : MonoBehaviour
{

    [SerializeField]
    private GameObject htpPage;



    private void OnEnable()
    {
        htpPage.SetActive(false);

        if (!PlayerPrefs.HasKey("FirstEnterToGame"))
        {
            PlayerPrefs.SetString("FirstEnterToGame", "hasEnter");
            htpPage.SetActive(true);
        }
    }


    public void TapStartButton()
    {
        SceneManager.LoadScene("GamePlayScene");
    }
    public void TapExitButton()
    {
        Application.Quit();
    }
    public void TapHTPButton()
    {
        htpPage.SetActive(true);
    }
}
