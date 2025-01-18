using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    public GameObject Mainui;
    public GameObject FQUUI;

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OpenFQU()
    {
        Mainui.SetActive(false);
        FQUUI.SetActive(true);
    }

    public void CloseFQU()
    {
        FQUUI.SetActive(false);
        Mainui.SetActive(true);
    }
}
