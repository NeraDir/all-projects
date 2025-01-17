using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopPanel : MonoBehaviour, IService
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject closeButton;

    public void Open()
    {
        panel.SetActive(true);
    }

    public void Close()
    {
        panel.SetActive(false);
    }
}
