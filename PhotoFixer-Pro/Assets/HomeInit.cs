using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeInit : MonoBehaviour
{
    [SerializeField]
    private GameObject homePage;

    [SerializeField]
    private GameObject guidPage;



    private void OnEnable()
    {
        if (!PlayerPrefs.HasKey("FirstEnter"))
        {
            homePage.SetActive(false);
            guidPage.SetActive(true);
            PlayerPrefs.SetInt("FirstEnter", 1);
        }
        else
        {
            homePage.SetActive(true);
            guidPage.SetActive(false);
        }
    }


}
