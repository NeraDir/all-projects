using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialPage : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private List<GameObject> allPages;

    [SerializeField]
    private GameObject menuPage;
    private int currentPageIndex;

    private void OnEnable()
    {
        menuPage.SetActive(false);
        allPages[0].SetActive(true);
        allPages[1].SetActive(false);
        allPages[2].SetActive(false);
        allPages[3].SetActive(false);

        currentPageIndex = 0;

    }
    private void OnDisable()
    {
        menuPage.SetActive(true);
    }

    public void ShowPage()
    {

        if (currentPageIndex != allPages.Count - 1)
        {


            allPages[currentPageIndex].SetActive(false);

            allPages[currentPageIndex + 1].SetActive(true);

            currentPageIndex++;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public void Test()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ShowPage();
    }
}
