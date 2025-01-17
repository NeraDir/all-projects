using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AnubisButtonComponent : MonoBehaviour
{
    [SerializeField] private GameObject _openPage;
    [SerializeField] private GameObject _closePage;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnAnubisButtonPressed);
    }

    private void OnAnubisButtonPressed()
    {
        if(_closePage != null)
            _closePage.SetActive(false);
        if(_openPage != null)
            _openPage.SetActive(true);
    }
}
