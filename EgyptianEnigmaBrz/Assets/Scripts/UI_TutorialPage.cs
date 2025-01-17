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
        ShowActualPage();
    }
    private void OnDisable()
    {
        uI_MenuPage.gameObject.SetActive(true);
    }


    public void ShowNextPage()
    {
        if (currentPageIndex == allPages.Count - 1)
            gameObject.SetActive(false);
        else
        {
            currentPageIndex++;
            ShowActualPage();
        }
    }

    public void ShowActualPage()
    {
        for (int i = 0; i < allPages.Count; i++)
        {
            if (i == currentPageIndex)
                allPages[i].SetActive(true);
            else
                allPages[i].SetActive(false);
        }
    }
}
