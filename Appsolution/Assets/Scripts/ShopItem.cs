using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField]
    private ShopManager _shopManager;
    [SerializeField]
    private GameObject _skinCostObject;
    [SerializeField]
    private Image _skinImage;
    [SerializeField]
    private Sprite _skinSprite;
    [SerializeField]
    private Button _skinBuyButton;
    [SerializeField]
    private int _skinCost;
    [SerializeField]
    private int _skinIndex;
    [SerializeField]
    private bool _isSkinBuy;

    private Text _skinCostText;

    public int skinIndex => _skinIndex;
    public Text _buttonText { get; private set; }


    private void Awake()
    {
        if(PlayerPrefs.HasKey(gameObject.name + "Bought"))
        {
            _isSkinBuy = true;
        }
        else
        {
            _skinCostText = _skinCostObject.transform.GetChild(0).GetComponent<Text>();
            _skinCostText.text = _skinCost.ToString();
        }

        if (_isSkinBuy == true)
        {
            _skinCostObject.SetActive(false);
        }

        _buttonText = _skinBuyButton.transform.GetChild(0).GetComponent<Text>();
            
        _skinImage.sprite = _skinSprite;
    }

    public void Update()
    {
        if(MoneyCounter._allMoney < _skinCost && _isSkinBuy == false)
        {
            _skinBuyButton.interactable = false;
        }
    }

    public void BuySkin()
    {
        MoneyCounter.SpendMoney(_skinCost);
        _isSkinBuy = true;
        PlayerPrefs.SetInt(gameObject.name + "Bought", _isSkinBuy ? 1 : 0);

        _shopManager.SelectCurrentSkin(_skinIndex);

    }

    public void RedrawBuyButton(int currentSkinIndex)
    {
        if(_isSkinBuy == true)
        {
            if(_skinIndex == currentSkinIndex)
            {
                _buttonText.text = "Equped";
            }
            else
            {
                _buttonText.text = "Select";
            }
        }
        else
        {
            _buttonText.text = "Buy";
        }
    }
}
