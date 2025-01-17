using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TutorialPage : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> allPages;

    private int currentPageIndex;

    [SerializeField]
    private UI_MenuPage uI_MenuPage;

    private void OnEnable()
    {
        uI_MenuPage.gameObject.SetActive(false);
        currentPageIndex = 0;
        ShowPage();
    }
    private void OnDisable()
    {
        uI_MenuPage.gameObject.SetActive(true);
    }

    public void ShowPage()
    {
        for (int i = 0; i < allPages.Count; i++)
        {
            if (currentPageIndex == i)
            {
                allPages[currentPageIndex].SetActive(true);
            }
            else
            {
                allPages[i].SetActive(false);
            }
        }
    }

    public void TapNextButton()
    {
        if (currentPageIndex == allPages.Count - 1)
        {
            gameObject.SetActive(false);
           
        }
        currentPageIndex++;
        ShowPage();
    }

}
