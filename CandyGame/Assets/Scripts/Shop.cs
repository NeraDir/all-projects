using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Shop : MonoBehaviour
{
    private Sprites SpriteData => Resources.Load<Sprites>("Data/Sprites/Sprites");
    [SerializeField] private Image bgSkin;
    private int currentSkin = 0;
    [SerializeField] private TMP_Text price;
    [SerializeField] private GameObject cost;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject selectButton;
    [SerializeField] private GameObject selectedButton;
    [SerializeField] private GameObject leftButton;
    [SerializeField] private GameObject rightButton;

    private void Start()
    {
        bgSkin.sprite = SpriteData.sprites[currentSkin];
        price.text = SpriteData.price[currentSkin].ToString();
        PlayerPrefs.SetInt("BoughtSkin" + 0, 1);
    }

    private void Update()
    {
        CheckAllButtons();
    }

    public void Switch(int direction)
    {
        currentSkin += direction;
        Debug.Log(currentSkin);

        bgSkin.sprite = SpriteData.sprites[currentSkin];
        price.text = SpriteData.price[currentSkin].ToString();
    }

    private void CheckLeftRightButtons()
    {
        if (currentSkin == SpriteData.sprites.Count - 1)
        {
            rightButton.SetActive(false);
            leftButton.SetActive(true);
        }
        else if (currentSkin < SpriteData.sprites.Count - 1 && currentSkin > 0)
        {
            rightButton.SetActive(true);
            leftButton.SetActive(true);
        }
        else if (currentSkin == 0)
        {
            rightButton.SetActive(true);
            leftButton.SetActive(false);
        }
    }

    public void Buy()
    {
        Debug.Log(MoneyManager.money);

        if (SpriteData.price[currentSkin] <= MoneyManager.money)
        {
            buyButton.SetActive(false);
            selectButton.SetActive(true);
            PlayerPrefs.SetInt("BoughtSkin" + currentSkin, 1);
            MoneyManager.SetMoney(-SpriteData.price[currentSkin]);
        }
    }

    public void Select()
    {
        selectButton.SetActive(false);
        selectedButton.SetActive(true);
        PlayerPrefs.SetInt("Skin", currentSkin);
    }

    private void CheckAllButtons()
    {
        CheckLeftRightButtons();
        if (PlayerPrefs.GetInt("Skin") == currentSkin)
        {
            buyButton.SetActive(false);
            selectButton.SetActive(false);
            selectedButton.SetActive(true);
            cost.SetActive(false);
        }
        else
        {
            if (PlayerPrefs.GetInt("BoughtSkin" + currentSkin) == 1)
            {
                buyButton.SetActive(false);
                selectButton.SetActive(true);
                selectedButton.SetActive(false);
                cost.SetActive(false);

            }
            else
            {
                buyButton.SetActive(true);
                selectButton.SetActive(false);
                selectedButton.SetActive(false);
                cost.SetActive(true);
            }
        }
    }
}
