using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class prodigMenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject howToPlay;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("prodigMenuSaveLey"))
        {
            howToPlay.SetActive(true);
            PlayerPrefs.SetInt("prodigMenuSaveLey", 1);
        }
    }

    public void Play() 
    {
        SceneManager.LoadScene("Gemae");
    }

    public void Exit() 
    {
        Application.Quit();
    }
}
