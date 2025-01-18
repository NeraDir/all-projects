using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanerShopContainer : MonoBehaviour
{
    public int planePrice;

    [SerializeField]
    private TMP_Text _priceDisplay;

    [SerializeField]
    private TMP_Text _buyDisplayer;

    [SerializeField]
    private TMP_Text _equiptDisplayer;

    [SerializeField]
    private Button _equipButton;

    [SerializeField]
    private Button _buyButton;

    public int planeIndex;

    [SerializeField]
    private PlanerShopContainer[] _planesContainer;

    public int _planeBuyed 
    {
        get 
        {
            if (PlayerPrefs.HasKey("PlaneContainer" + planePrice))
            {
                return PlayerPrefs.GetInt("PlaneContainer" + planePrice);
            }
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("PlaneContainer" + planePrice, value);
        }
    }

    private void Start()
    {
        SetUpdatedState();
    }

    public void SetUpdatedState() 
    {
        if (_planeBuyed != 0)
        {
            _buyButton.enabled = false;
            _equipButton.enabled = true;
            _priceDisplay.text = "";
            _buyDisplayer.text = "BOUGHT";
            if (planeIndex == PlanerController._selectedPlanerIndex)
            {
                _buyButton.enabled = false;
                _equipButton.enabled = false;
                _equiptDisplayer.text = "EQUIPTED";
            }
            else
            {
                _buyButton.enabled = false;
                _equipButton.enabled = true;
                _equiptDisplayer.text = "EQUIP";
            }
        }
        else
        {
            _buyDisplayer.text = "BUY";
            _priceDisplay.text = "PRICE: " + planePrice.ToString("0");
            _equiptDisplayer.text = "EQUIP";
            _buyButton.enabled = true;
            _equipButton.enabled = false;
        }
    }

    public void OnClickEquip() 
    {
        PlanerController._selectedPlanerIndex = planeIndex;
        foreach (var item in _planesContainer)
        {
            if (item._planeBuyed != 0)
                item.SetUpdatedState();
        }
    }

    public void OnClickBuy() 
    {
        if (PlaneDataContainer.PlanesCoins >= planePrice && _planeBuyed == 0)
        {
            PlaneDataContainer.PlanesCoins -= planePrice;
            _planeBuyed = 1;
            OnClickEquip();
            SetUpdatedState();
        }
    }
}
