using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AviaShopConteiner : MonoBehaviour
{
    [SerializeField]
    private Button _buyBtn;

    [SerializeField]
    private TMP_Text _priceTxt;

    [SerializeField]
    private TMP_Text _sellTxt;

    public int price;

    public int index;

    [SerializeField]
    private AviaShopConteiner[] containers;

    public int BuyedIndex 
    {
        get 
        {
            if (PlayerPrefs.HasKey("PlaneSaveBuyed" + index))
            {
               return PlayerPrefs.GetInt("PlaneSaveBuyed" + index);
            }
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("PlaneSaveBuyed" + index, value);
        }
    }

    private void Start()
    {
        UpdateStateOfContainer();
    }

    private void EquipBttn() 
    {
        GamePlayerInformation.PlaneSelected = index;
        UpdateStateOfContainer();
        foreach (var item in containers) 
        {
            if (item != null && item.BuyedIndex != 0) 
            {
                item.UpdateStateOfContainer();
            }
        }
    }

    private void BuyBttn()
    {
        if (GamePlayerInformation.GameCoins >= price)
        {
            GamePlayerInformation.GameCoins -= price;
            BuyedIndex = 1;
            EquipBttn();
        }
    }

    public void UpdateStateOfContainer() 
    {
        if (BuyedIndex != 0)
        {
            _priceTxt.text = "";
            _priceTxt.gameObject.SetActive(false);
            if (GamePlayerInformation.PlaneSelected == index)
            {
                _sellTxt.text = "EQUIPTED";
                _buyBtn.onClick.RemoveAllListeners();
            }
            else
            {
                _sellTxt.text = "EQUIP";
                _buyBtn.onClick.RemoveAllListeners();
                _buyBtn.onClick.AddListener(EquipBttn);
            }
        }
        else 
        {
            _sellTxt.text = "BUY";
            _priceTxt.text = price.ToString();
            _buyBtn.onClick.RemoveAllListeners();
            _buyBtn.onClick.AddListener(BuyBttn);
        }
    }
}
