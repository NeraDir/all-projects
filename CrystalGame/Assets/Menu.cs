using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject trainingPanel;

    private void Start()
    {
        if (PlayerDatasSaver.isFirstEnter == 0)
        {
            trainingPanel.SetActive(true);
            PlayerDatasSaver.isFirstEnter = 1;
        }
    }

    public void OnCLickPaly() 
    {
        SceneManager.LoadScene("FirstMap");
    }

    public void OnCLickExit() 
    {
        Application.Quit();
    }
}
