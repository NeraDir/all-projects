using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _egyptianGameInfoPage;

    [SerializeField]
    private TMP_Text _egyptianMaxLevelShow;

    [SerializeField]
    private TMP_Text _egyptianCrystallsRocksCountShow;

    [SerializeField]
    private TMP_Text[] _egyptianSkinsPriceShow;

    [SerializeField]
    private int[] _egyptianPrices;

    [SerializeField]
    private GameObject[] _egyptianSkinsSelectedImages;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("EgyptianGameInfoShowedSaveKey"))
        {
            _egyptianGameInfoPage.SetActive(true);
            OnBuyButtonPressed(0);
            PlayerPrefs.SetInt("EgyptianGameInfoShowedSaveKey", 1);
        }
        CheckStateOfSkin();
    }

    public void OnBuyButtonPressed(int index) 
    {
        if (!PlayerPrefs.HasKey("EgyptianBuyedSkins" + index + "SaveKey"))
        {
            if (GameManager.crystallRocksCount > _egyptianPrices[index])
            {
                GameManager.crystallRocksCount -= _egyptianPrices[index];
                PlayerPrefs.SetInt("EgyptianBuyedSkins" + index + "SaveKey", 1);
                OnEquip(index);
            }
        }
        else
        {
            OnEquip(index);
        }
    }

    private void CheckStateOfSkin() 
    {
        for (int i = 0; i < _egyptianPrices.Length; i++)
        {
            if (!PlayerPrefs.HasKey("EgyptianBuyedSkins" + i + "SaveKey"))
            {
                _egyptianSkinsPriceShow[i].text = "x" + _egyptianPrices[i].ToString();
                _egyptianSkinsSelectedImages[i].SetActive(false);
            }
            else
            {
                if (GameManager.egyptianSelectedSkinValue == i)
                {
                    _egyptianSkinsPriceShow[i].text = "";
                    _egyptianSkinsSelectedImages[i].SetActive(true);
                }
                else
                {
                    _egyptianSkinsPriceShow[i].text = "";
                    _egyptianSkinsSelectedImages[i].SetActive(false);
                }
            }
        }
    }

    public void OnEquip(int index) 
    {
        GameManager.egyptianSelectedSkinValue = index;
        if (GameManager.egyptianSelectedSkinValue == index)
        {
            _egyptianSkinsSelectedImages[index].SetActive(true);
        }
        else
        {
            _egyptianSkinsSelectedImages[index].SetActive(false);
        }
        CheckStateOfSkin();
    }

    public void OnEgyptianPlayButtonPressed()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnEgyptianExitButtonPressed()
    {
        Application.Quit();
    }

    private void LateUpdate()
    {
        _egyptianCrystallsRocksCountShow.text = "x" + GameManager.crystallRocksCount.ToString();
        _egyptianMaxLevelShow.text = GameManager.egyptianMaxLevelValue.ToString();
    }
}
