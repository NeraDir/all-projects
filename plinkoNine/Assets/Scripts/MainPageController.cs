using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPageController : MonoBehaviour
{
    [SerializeField]
    private GameObject _welcomeBonusPage;

    [SerializeField]
    private GameObject _mainPage;

    public static bool _isRegistrationButtonClicked;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("DIAMONDSPHEREGIFUUGDUIIFUGDSAVE"))
        {
            _welcomeBonusPage.SetActive(true);
            _mainPage.SetActive(false);
            PlayerPrefs.SetInt("DIAMONDSPHEREGIFUUGDUIIFUGDSAVE", 1);
        }
    }

    public void OnClickRegistration()
    {
        if (_isRegistrationButtonClicked)
            return;
        _isRegistrationButtonClicked = true;
        Invoke(nameof(Back), 1.5f);
    }

    private void Back()
    {
        _isRegistrationButtonClicked = false;
    }
}
