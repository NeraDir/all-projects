using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChaseShopComponent : MonoBehaviour
{
    private Sprite _sprite;
    private int _price;
    
    [SerializeField] private Image _image;
    [SerializeField] private Text _priceText;
    [SerializeField] private Image _background;
    
    private Color _selectedColor = Color.yellow;
    private Color _unselectedColor = Color.red;

    private Button _button;
    
    private bool _isBuyed
    {
        get => PlayerPrefs.HasKey($"ChaseShopBackgroundState{_sprite.name}") ? Convert.ToBoolean(PlayerPrefs.GetString($"ChaseShopBackgroundState{_sprite.name}")) : false;
        set => PlayerPrefs.SetString($"ChaseShopBackgroundState{_sprite.name}", value.ToString());
    }

    private bool _isSelected
    {
        get => ChasePlayerDataComponent.ChasePlayerBackgroundSpriteName == _sprite.name ? true : false;
    }
    
    private void Awake()
    {
        _button = GetComponent<Button>();
        _image.sprite = _sprite;
        _priceText.text = _price.ToString();
        _button.onClick.AddListener(OnButtonPressed);
        UpdateVisual();
    }

    public void SetData(Sprite sprite, int price)
    {
        _sprite = sprite;
        _price = price;
    }

    private void OnButtonPressed()
    {
        if (_isBuyed) 
            Equip();
        else 
            Buy();
    }

    private void Equip()
    {
        ChasePlayerDataComponent.ChasePlayerBackgroundSpriteName = _sprite.name;
        ChaseMenuComponent.onChaseShopAction?.Invoke();
    }

    public void Buy()
    {
        if (ChasePlayerDataComponent.ChasePlayerCoins >= _price)
        {
            ChasePlayerDataComponent.ChasePlayerCoins -= _price;
            _isBuyed = true;
            Equip();
        }
        else
        {
            if(ChasePlayerDataComponent.ChaseVibrationState)
                Handheld.Vibrate();
        }
    }

    public void UpdateVisual()
    {
        _background.color = _isBuyed ? _isSelected ? _selectedColor : _unselectedColor : _unselectedColor;
        _priceText.text = _isBuyed ? _isSelected ? "EQUIPPED" : "EQUIP" : _price.ToString();
        _image.sprite = _sprite;
    }
}
