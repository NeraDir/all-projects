using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuidPagesManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> pages;

    private int cuurrentPageIndex;
    private GameObject currentPage;

    [SerializeField]
    private GameObject homePage;


    private void OnEnable()
    {
        cuurrentPageIndex = 0;
        ShowPageByIndex();
    }

    private void ShowPageByIndex()
    {
        for (int i = 0; i < pages.Count; i++)
        {
            if (i == cuurrentPageIndex)
            {
                pages[i].SetActive(true);
            }
            else
            {
                pages[i].SetActive(false);
            }
        }
    }

    public void ShowNextPage()
    {
        cuurrentPageIndex++;

        if (cuurrentPageIndex == pages.Count)
        {
            homePage.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            
            ShowPageByIndex();
        }

    }


}
