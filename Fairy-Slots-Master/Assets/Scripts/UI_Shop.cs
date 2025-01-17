using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Shop : MonoBehaviour
{

    [SerializeField]
    private GameObject MenuPanel;


    public void TapCloseButton()
    {
        MenuPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
