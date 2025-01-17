using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BgShopComponent : MonoBehaviour
{
    [SerializeField] private TMP_Text _priceShow;
    [SerializeField] private GameObject _lockPanel;

    [SerializeField] private int _bgIndex;
    [SerializeField] private int _bgPrice;

    private Button _openButton;
    private TMP_Text _openButtonTxt;

    public int isOpen
    {
        get
        {
            if (PlayerPrefs.HasKey($"CaramelTreatsBgBuyedStatusKey{_bgIndex}"))
                return PlayerPrefs.GetInt($"CaramelTreatsBgBuyedStatusKey{_bgIndex}");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt($"CaramelTreatsBgBuyedStatusKey{_bgIndex}", value);
        }
    }


    private void Start()
    {
        _openButton = transform.parent.GetComponentInChildren<Button>();
        _openButtonTxt = _openButton.GetComponentInChildren<TMP_Text>();
        _openButton.onClick.AddListener(OnClickOpenBg);
        UpdateVisual();
    }

    public void OnClickOpenBg()
    {
        if (isOpen != 0)
        {
            Equip();
        }
        else
        {
            if (CaramelTreatsGameController.EarnedStarsCount >= _bgPrice)
            {
                CaramelTreatsGameController.EarnedStarsCount -= _bgPrice;
                Debug.Log(CaramelTreatsGameController.EarnedStarsCount);
                isOpen = 1;
                Equip();
            }
            else
            {
                Handheld.Vibrate();
            }
        }
        UpdateVisual();
    }

    private void Equip()
    {

        CaramelTreatsGameController.SelectedBgIndex = _bgIndex; 
        CaramelTreatsMenuManager.bgComponentIsTrigger?.Invoke();
    }

    public void UpdateVisual()
    {
        if (isOpen != 0)
        {
            _priceShow.transform.parent.gameObject.SetActive(false);
            _lockPanel.SetActive(false);
            if (CaramelTreatsGameController.SelectedBgIndex == _bgIndex)
            {
                _openButtonTxt.text = "EQUIPPED";
            }
            else
            {
                _openButtonTxt.text = "EQUIP";
            }
        }
        else
        {
            _priceShow.transform.parent.gameObject.SetActive(true);
            _priceShow.text = "x" + _bgPrice.ToString();
            _openButtonTxt.text = "OPEN";
            _lockPanel.SetActive(true);
        }
    }
}
