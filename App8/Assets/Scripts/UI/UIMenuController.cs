using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuController : MonoBehaviour
{
    public GameObject HTPPanel;
    public int FirstIn
    {
        get
        {
            if (!PlayerPrefs.HasKey("FirstIn"))
                return 0;

            return PlayerPrefs.GetInt("FirstIn");
        }
        set
        {
            PlayerPrefs.SetInt("FirstIn", value);
        }
    }

    private void Start()
    {
        if(FirstIn == 0)
        {
            HTPPanel.SetActive(true);
            FirstIn = 1;
        }
    }

    public void PlayEasy()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void PlayHard()
    {
        SceneManager.LoadScene("GameScene2");
    }
    public void PlayHell()
    {
        SceneManager.LoadScene("GameScene3");
    }
}
