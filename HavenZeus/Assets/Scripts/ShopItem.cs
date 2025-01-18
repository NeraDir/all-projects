using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class ShopItem : MonoBehaviour
{
    
    [SerializeField]
    private ItemType _itemType;
    [SerializeField]
    private Button _buyButton;
    [SerializeField]
    private Text _costText;
    [SerializeField]
    private Text _allMoneyText;
    [SerializeField]
    private int _cost;
    [SerializeField]
    private int _index;
    [SerializeField]
    private bool _isBought;

    private enum ItemType { Sword, Bow}

    public UnityEvent<int> OnButtonPressed;
    void Start()
    {
        RedrawMoney();
        if(PlayerPrefs.HasKey(gameObject.name + "BOUGHT")) 
        {
            _isBought = true;
        }
        if(_isBought == true)
        {
            if (_itemType == ItemType.Sword)
            {
                CheckCurrentItem(ShopManager._currentSwordIndex);
            }
            else if (_itemType == ItemType.Bow)
            {
                CheckCurrentItem(ShopManager._currentBowIndex);
            }
        }
        else
        {
            _costText.text = _cost.ToString();
        }
    }

    private void Update()
    {
        if(MoneyCounter._allMoney < _cost)
        {
            _buyButton.interactable = false;
        }
    }

    public void BuyItem()
    {
        if(_isBought == false)
        {
            MoneyCounter.SpendMoney(_cost);
            RedrawMoney();
            _isBought = true;
            PlayerPrefs.SetInt(gameObject.name + "BOUGHT", true ? 1 : 0);
        }

        _costText.text = "EQUPED";

        if (_itemType == ItemType.Sword)
        {
            ShopManager.SetCurrentSword(_index);
        }
        else if(_itemType == ItemType.Bow)
        {
            ShopManager.SetCurrentBow(_index);
        }

        OnButtonPressed?.Invoke(_index);

      
    }

    public void CheckCurrentItem(int currentIndex)
    {
        if(_index != currentIndex)
        {
            if(_isBought == true)
            {
                _costText.text = "BOUGHT";
            }      
        }
        else
        {
            _costText.text = "EQUPED";
        }
    }

    public void RedrawMoney()
    {
        MoneyCounter.GetCurrentMoney();
        MoneyCounter.RedrawMoneyCount(_allMoneyText, MoneyCounter._allMoney);
    }
}
