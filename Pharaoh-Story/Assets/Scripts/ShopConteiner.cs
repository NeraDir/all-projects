using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopConteiner : MonoBehaviour
{
    public ShopItem currentContainerItem;

    [SerializeField]
    private Image _currentItemSpriteDisplay;

    [SerializeField]
    private TMP_Text _showPrice;

    [SerializeField]
    private TMP_Text _buttonInfoDispalyer;

    [SerializeField]
    private Button _containerButton;

    AllSellebleItems _allSellebleItems;

    public void INIT()
    {
        _allSellebleItems = FindObjectOfType<AllSellebleItems>();
        _containerButton.onClick.RemoveAllListeners();
        _currentItemSpriteDisplay.sprite = currentContainerItem.itemIcon;
        if (currentContainerItem.itemBuyedIdent != 0)
        {
            _showPrice.text = "BUYED";
            if (currentContainerItem.itemEquipedIdent != 0)
            {
                _buttonInfoDispalyer.text = "EQUIPED";
                _containerButton.onClick.RemoveAllListeners();
            }
            else
            {
                _buttonInfoDispalyer.text = "EQUIP";
                _containerButton.onClick.AddListener(EquipItem);
            }
        }
        else
        {
            _buttonInfoDispalyer.text = "BUY";
            _showPrice.text = currentContainerItem.itemPrice.ToString();
            _containerButton.onClick.AddListener(BuyItem);
        }
    }

    private void EquipItem()
    {
        foreach (var item in _allSellebleItems.shopebleItems)
        {
            if (item.itemBuyedIdent == 1)
            {
                item.itemEquipedIdent = 0;
            }
        }
        currentContainerItem.itemEquipedIdent = 1;
        INIT();
    }

    private void BuyItem()
    {
        if (UserData.userMoney >= currentContainerItem.itemPrice)
        {
            UserData.userMoney -= currentContainerItem.itemPrice;
            currentContainerItem.itemBuyedIdent = 1;
            INIT();
            EquipItem();
        }
        else
        {
            return;
        }
    }
}
