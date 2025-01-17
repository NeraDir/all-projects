using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Home : MonoBehaviour
{

    public TMP_Text poinstDisplay;

    private Animator animator;

    [SerializeField]
    private GameObject HelloPage;

    private void OnEnable()
    {
        if (!PlayerPrefs.HasKey("enter"))
        {
            PlayerPrefs.SetInt("enter", 111);
            HelloPage.SetActive(true);
            gameObject.SetActive(false);
        }

        animator = GetComponent<Animator>();
        poinstDisplay.text = "x" + (Stars.points != 0 ? Stars.points : "0");
    }



    public void StartButtonFunction()
    {
        CloseWindowAnimation();
    }
    public void ExitGameButtonFFunction()
    {
        Application.Quit();
    }

    public void CloseWindowAnimation()
    {
        animator.SetInteger("animationIndex", 1);
    }
    public void LoadDestroyPlanesScene()
    {
        SceneManager.LoadScene("DestroyPlaneScene");
    }
}
