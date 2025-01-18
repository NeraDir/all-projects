using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rules : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> pages;

    private int openPageCount;



    private void OnEnable()
    {
        openPageCount = 0;

        pages[0].SetActive(true);
        pages[1].SetActive(false);
    }

    public void ShowNextPage()
    {
        openPageCount++;

        if (openPageCount == pages.Count)
        {
            gameObject.SetActive(false);
            return;
        }

        pages[openPageCount].SetActive(true);

    }
}
