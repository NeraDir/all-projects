using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject lastPage;


    private void OnEnable()
    {
        if (lastPage != null)
            lastPage.SetActive(false);

        Time.timeScale = 0;
    }
    private void OnDisable()
    {
        if (lastPage != null)
            lastPage.SetActive(true);

        Time.timeScale = 1;
    }


    public void TapContinueButton()
    {
        gameObject.SetActive(false);
    }

    public void TapAgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void TapMenuButton()
    {
        SceneManager.LoadScene("GameMenu");
    }
}
