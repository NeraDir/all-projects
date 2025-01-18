using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomePageManager : MonoBehaviour
{

    [SerializeField]
    private GameObject FQUPage;
    private Animator m_animator;


    private void OnEnable()
    {
        m_animator = GetComponent<Animator>();
    }

    public void StartEditor()
    {
        m_animator.SetInteger("state", 1);
    }
    public void OpenFQU()
    {
        m_animator.SetInteger("state", 2);
    }
    public void ExitApp()
    {
        Application.Quit();
    }


    public void ShowFQUPage()
    {
        FQUPage.SetActive(true);
        gameObject.SetActive(false);
    }
    public void LoadEditorScene()
    {
        SceneManager.LoadScene("GameRedactor");
    }

}
