using UnityEngine;
using UnityEngine.UI;

public class ShopContainer : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private Image _image;

    private int _index;
    private int _price;

    private bool _isBuyed
    {
        get => bool.Parse(PlayerPrefs.GetString($"TlineShopContainerIsBuyedIndex{_index}","false"));
        set => PlayerPrefs.SetString($"TlineShopContainerIsBuyedIndex{_index}", value.ToString());
    }

    public void SetupData(int index, int price, Sprite sprite)
    {
        _index = index;
        _price = price;
        _image.sprite = sprite;
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        _text.text = _isBuyed ? TlineGameDataSaves.TlineCurrentBackgroundIndex == _index ? "EQUIPPED" : "EQUIP" : "x" + _price.ToString();
    }

    public void Buy()
    {
        if (TlineGameDataSaves.TlineCoins >= _price)
        {
            TlineGameDataSaves.TlineCoins -= _price;
            _isBuyed = true;
            Equip();
        }
    }

    private void Equip()
    {
        if (_isBuyed)
        {
            TlineGameDataSaves.TlineCurrentBackgroundIndex = _index;
            TlineMenuController.sendUpdateShop?.Invoke();
        }
    }

    public void OnClick()
    {
        if (_isBuyed)
        {
            Equip();
        }
        else
        {
            Buy();
        }
    }
}
