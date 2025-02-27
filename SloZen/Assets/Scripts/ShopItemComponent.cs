using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemComponent : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Text _priceTxt;
    [SerializeField] private Text _stateTxt;
    [SerializeField] private Button _button;

    private ShopItemData _itemData;

    private AudioClip _errorClip;
    private AudioClip _successClip;
    private AudioClip _clickClip;

    private Vector3 _scale;
    private Quaternion _rotation;

    private bool isBought
    {
        get => bool.Parse(PlayerPrefs.GetString($"{_itemData.sprite.name}SloZenShopItemIsBoughtSaveKey", "false"));
        set => PlayerPrefs.SetString($"{_itemData.sprite.name}SloZenShopItemIsBoughtSaveKey", value.ToString());
    }

    public void Init(ShopItemData data)
    {
        _clickClip = Resources.Load("Sounds/click") as AudioClip;
        _errorClip = Resources.Load("Sounds/error") as AudioClip;
        _successClip = Resources.Load("Sounds/success") as AudioClip;
        _itemData = data;
        _scale = _button.transform.localScale;
        _rotation = _button.transform.rotation;
        ShopWindow.onUpdateShop.AddListener(VisualUpdate);
        if (_itemData.sprite.name == "1" && !isBought)
        {
            OnBuy();
        }
    }

    private void Start()
    {
        VisualUpdate();
    }

    private void OnDestroy()
    {
        ShopWindow.onUpdateShop.RemoveListener(VisualUpdate);
    }

    private void VisualUpdate()
    {
        _image.sprite = _itemData.sprite;
        if (isBought)
            _priceTxt.gameObject.SetActive(false);
        else
             _priceTxt.text = _itemData.price.ToString();
        _stateTxt.text = isBought ? BackgroundComponent.CurrentBackgroundName == _itemData.sprite.name ? "EQUIPPED" : "EQUIP" : "BUY";
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() =>
        {
            _button.transform.DORotateQuaternion(Quaternion.Euler(0, 0, 15), 0.1f).OnComplete(() =>
            {
                _button.transform.DORotateQuaternion(Quaternion.Euler(0, 0, -15), 0.1f).OnComplete(() =>
                {
                    _button.transform.DORotateQuaternion(_rotation, 0.1f);
                });
            });
            _button.transform.DOScale(_scale / 1.6f, 0.1f).OnComplete(() =>
            {
                _button.transform.DOScale(_scale * 1.2f, 0.1f).OnComplete(() =>
                {
                    _button.transform.DOScale(_scale, 0.1f).OnComplete(() =>
                    {
                        if (isBought)
                            OnEquip();
                        else
                            OnBuy();
                    });
                });
            });
        });
    }

    private void OnBuy()
    {
        if (ShopWindow.Coins >= _itemData.price)
        {
            ShopWindow.Coins -= _itemData.price;
            SettingsController.onPlayEffect?.Invoke(_successClip);
            isBought = true;
            ShopWindow.onUpdateShop?.Invoke();
        }
        else
        {
            SettingsController.onPlayEffect?.Invoke(_errorClip);
        }
    }

    private void OnEquip()
    {
        BackgroundComponent.CurrentBackgroundName = _itemData.sprite.name;
        ShopWindow.onUpdateShop?.Invoke();
        SettingsController.onPlayEffect?.Invoke(_clickClip);
    }
}
