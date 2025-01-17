using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_PausePage : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> gameUIPages;

    private void OnEnable()
    {
        for (int i = 0; i < gameUIPages.Count; i++)
            gameUIPages[i].SetActive(false);

        Time.timeScale = 0;

    }
    private void OnDisable()
    {
        for (int i = 0; i < gameUIPages.Count; i++)
            gameUIPages[i].SetActive(true);

        Time.timeScale = 1;
    }

    public void TapContinueButton()
    {
        GetComponent<Animator>().SetInteger("stateID", 1);
    }
    public void TapMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }
    public void DisablePage()
    {
        gameObject.SetActive(false);
    }
}
