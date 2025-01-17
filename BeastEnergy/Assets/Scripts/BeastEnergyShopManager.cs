using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BeastEnergyShopManager : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private TMP_Text _currentStateDisplayer;

    [SerializeField] private int _beastSkinIndex;

    [SerializeField] private int _starPrice;

    public void Init() 
    {
        CheckState();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!PlayerPrefs.HasKey("BeastEnergySkinStateSave" + _beastSkinIndex))
        {
            Buy();
        }
        else
        {
            Equip();
        }
    }

    public void Buy() 
    {
        if (BeastEnergyGameManager.beastEnergyCoinsCount >= _starPrice)
        {
            BeastEnergyGameManager.beastEnergyCoinsCount -= _starPrice;
            PlayerPrefs.SetInt("BeastEnergySkinStateSave" + _beastSkinIndex,1);
            Equip();
        }
        else
        {
            Handheld.Vibrate();
        }
    }

    public void Equip() 
    {
        BeastEnergyGameManager.beastCurrentSkinIndex = _beastSkinIndex;
        BeastEnergyShopManager[] shopManagers = FindObjectsOfType<BeastEnergyShopManager>();
        foreach (var item in shopManagers)
        {
            item.CheckState();
        }
    }

    public void CheckState() 
    {
        if (!PlayerPrefs.HasKey("BeastEnergySkinStateSave" + _beastSkinIndex))
        {
            _currentStateDisplayer.text = _starPrice.ToString("0") + "C";
        }
        else
        {
            if (BeastEnergyGameManager.beastCurrentSkinIndex != _beastSkinIndex)
            {
                _currentStateDisplayer.text = "EQUIP";
            }
            else
            {
                _currentStateDisplayer.text = "EQUIPED";
            }
        }
    }

}
