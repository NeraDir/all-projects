using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MachineUIButtonController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Animator closePage;

    [SerializeField]
    private GameObject openPage;

    [SerializeField]
    private string sceneName;

    public static bool isClicked;

    public int choosers;

    public bool isAdders;

    

    private void OnMouseDown()
    {
        if (!isAdders)
        {
            if (SceneManager.GetActiveScene().name == "BoxingScene" && !MachineGameController.isGameStarted)
                return;
            if (isClicked)
                return;
            isClicked = true;
            StartCoroutine(DoMotion());
        }
        else
        {
            if (choosers == 5)
            {
                MachineGameController.changeViewMode?.Invoke();
            }
            else
            {
                if (SceneManager.GetActiveScene().name == "BoxingScene" && !MachineGameController.isGameStarted)
                    return;
                MachineGameController.betChanged?.Invoke(choosers);
            }
        }
    }

    private IEnumerator DoMotion() 
    {
        closePage.SetBool("PAGESSTATES", true);
        yield return new WaitForSeconds(0.5f);
        closePage.gameObject.SetActive(false);
        if (choosers == 0)
        {
            openPage.SetActive(true);
        }
        else if (choosers == 1)
        {
            SceneManager.LoadScene(sceneName);
        }
        else if (choosers == 2)
        {
            Application.Quit();
        }
        isClicked = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isAdders)
        {
            if (SceneManager.GetActiveScene().name == "BoxingScene" && !MachineGameController.isGameStarted)
                return;
            if (isClicked)
                return;
            isClicked = true;
            StartCoroutine(DoMotion());
        }
        else
        {
            if (choosers == 5)
            {
                MachineGameController.changeViewMode?.Invoke();
            }
            else
            {
                if (SceneManager.GetActiveScene().name == "BoxingScene" && !MachineGameController.isGameStarted)
                    return;
                MachineGameController.betChanged?.Invoke(choosers);
            }
        }
    }
}
