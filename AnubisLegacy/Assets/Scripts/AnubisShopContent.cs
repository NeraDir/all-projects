using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnubisShopContent : MonoBehaviour
{
    [SerializeField]
    private Text _priceLabel = null;

    [SerializeField]
    private Image _image = null;

    [SerializeField]
    private Button _button = null;

    public static Action UpdateShopContent = null;

    private int _price = 0;
    private Sprite _sprite = null;

    private const string SHOP_CONTENT_BUY_KEY = "anubis_shop_buyed_state";

    private bool _isBuyed
    {
        get => PlayerPrefs.HasKey(SHOP_CONTENT_BUY_KEY + _sprite.name) ? Convert.ToBoolean(PlayerPrefs.GetString(SHOP_CONTENT_BUY_KEY + _sprite.name)) : false;
        set => PlayerPrefs.SetString(SHOP_CONTENT_BUY_KEY + _sprite.name, value.ToString());
    }

    private bool _isEquipped
    {
        get => AnubisUserData.CurrentBackgroundName == _sprite.name;
    }

    public void Init()
    {
        UpdateShopContent += UpdateContent;
        _button.onClick.AddListener(OnButtonPressed);
        UpdateContent();
    }

    private void OnDestroy()
    {
        UpdateShopContent -= UpdateContent;
        _button.onClick.RemoveListener(OnButtonPressed);
    }

    public void SetData(Sprite sprite, int price)
    {
        _price = price;
        _sprite = sprite;
    }

    private void UpdateContent()
    {
        _image.sprite = _sprite;
        _priceLabel.text = _isBuyed ? (_isEquipped ? "EQUIPPED" : "EQUIP") : _price.ToString();
    }

    private void OnButtonPressed()
    {
        if (_isBuyed) 
            Equip();
        else
            if (_price <= AnubisUserData.Coins)
                Buy();
        else
            Handheld.Vibrate();
    }

    private void Buy()
    {
        AnubisUserData.Coins -= _price;
        _isBuyed = true;
        Equip();
    }

    private void Equip()
    {
        AnubisUserData.CurrentBackgroundName = _sprite.name;
        UpdateShopContent?.Invoke();
    }
}
