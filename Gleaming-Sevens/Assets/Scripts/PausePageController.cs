using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePageController : MonoBehaviour
{
    [SerializeField]
    private GameObject gamePlayUIPage;

    private Animator myAnimator;

    private void OnEnable()
    {
        myAnimator = GetComponent<Animator>();
    }


    public void ClickContinueBtn()
    {
        gamePlayUIPage.SetActive(true);
        myAnimator.SetInteger("index", 1);
    }
    public void DisabledPage()
    {
        gameObject.SetActive(false);
    }



    public void ClickMenuBtn()
    {
        SceneManager.LoadScene("Menu");
    }
}
