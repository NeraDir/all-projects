using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class shopComponent : MonoBehaviour
{
    private Button _button;

    private Image _image;

    private TMP_Text _nameLabel;

    private TMP_Text _priceLabel;

    private int _price;

    private ShopData _shopData;

    private int _isBuyed;

    public void Init(ShopData data)
    {
        _image =GetComponentsInChildren<Image>()[1];
        _button = GetComponent<Button>();
        TMP_Text[] textes = GetComponentsInChildren<TMP_Text>();
        _nameLabel = textes[0];
        _priceLabel = textes[1];
        _shopData = data;
        _price = _shopData.price;
        _isBuyed = PlayerPrefs.GetInt($"MysteriousCircuitsShopComponentIsBuyedState{data.name}");
        _button.onClick.AddListener(OnClickButton);
        VisualUpdate();
    }

    private void OnClickButton()
    {
        if (_isBuyed != 0)
        {
            Equip();
        }
        else
        {
            Buy();
        }
    }

    private void Buy()
    {
        if (menuScreenController.userDiamondsCount < _price)
            return;
        menuScreenController.userDiamondsCount -= _price;
        menuScreenController.updateDiamondCount?.Invoke();
        PlayerPrefs.SetInt($"MysteriousCircuitsShopComponentIsBuyedState{_shopData.name}", 1);
        _isBuyed = PlayerPrefs.GetInt($"MysteriousCircuitsShopComponentIsBuyedState{_shopData.name}");
        Equip();
    }

    private void Equip()
    {
        ballComponent.BallSpriteIndex = _shopData.index;
        shopScreenController.updateShopScreen?.Invoke();
    }

    public void VisualUpdate()
    {
        if (_isBuyed != 0)
        {
            _priceLabel.text = _shopData.index == ballComponent.BallSpriteIndex ? "EQUIPPED" : "EQUIP";
        }
        else
        {
            _priceLabel.text = "x" + _price.ToString();
        }
        _nameLabel.text = _shopData.name;
        _image.sprite = _shopData.sprite;
    }
}
