using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinData : MonoBehaviour
{
    [SerializeField]
    private Image _skinImage;
    [SerializeField]
    private Sprite _skinSprite;
    [SerializeField]
    private Text _skinCostText;
    [SerializeField]
    private Text _allMoneyText;
    [SerializeField]
    private int _skinCost;
    [SerializeField]
    private int _skinIndex;
    [SerializeField]
    private bool _isSkinOpen;


    private Button _thisButton;

    private void Awake()
    {
        _thisButton = GetComponent<Button>();
        if (PlayerPrefs.HasKey(gameObject.name + "Open") || _isSkinOpen == true)
        {
            _isSkinOpen = true;

            _skinCostText.text = "Bought";
        }
        else
        {
            _skinCostText.text = _skinCost.ToString();
        }

        _skinImage.sprite = _skinSprite;
        MoneyCounter.GetCurrentGold();
        _allMoneyText.text = MoneyCounter._currentMoney.ToString();
    }

    private void Update()
    {
        if(MoneyCounter._currentMoney < _skinCost && PlayerPrefs.GetInt(gameObject.name + "Open") != 1)
        {
            _thisButton.interactable = false;
        }
    }
    public void SelectSkin()
    {
        if(_isSkinOpen == false && MoneyCounter._currentMoney >= _skinCost)
        {
            MoneyCounter.SpendMoney(_skinCost);
            _isSkinOpen = true;
            PlayerPrefs.SetInt(gameObject.name + "Open", (true ? 1 : 0));
            _skinCostText.text = "Bought";
            _allMoneyText.text = MoneyCounter._currentMoney.ToString();
        }
        else if(_isSkinOpen == true)
        {
            PlayerPrefs.SetInt("CurrentSkin", _skinIndex);
        }
    }
}
