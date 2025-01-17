using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseOpen : MonoBehaviour
{
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject Shop;
    [SerializeField] private GameObject FQU;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Hfdgdsfsd"))
        {
            FQU.SetActive(true);
            PlayerPrefs.SetInt("Hfdgdsfsd", 1);
        }
    }

    public void CloseShop()
    {
        Menu.SetActive(true);
        Shop.SetActive(false);
    }

    public void OpenShop()
    {
        Menu.SetActive(false);
        Shop.SetActive(true);
    }

    public void CloseFQU()
    {
        Menu.SetActive(true);
        FQU.SetActive(false);
    }

    public void OpenFQU()
    {
        Menu.SetActive(false);
        FQU.SetActive(true);
    }

    public void Play()
    {
        SceneManager.LoadScene(2);
    }
}
