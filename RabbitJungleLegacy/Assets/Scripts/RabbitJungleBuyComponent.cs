using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RabbitJungleBuyComponent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Text _displayState;

    [SerializeField]
    private int _price;

    [SerializeField]
    private int _index;

    private string _savingKey => "RabbitJungleSkinState" + _index + "SaveKey";

    public void OnPointerClick(PointerEventData eventData)
    {
        if (PlayerPrefs.HasKey(_savingKey))
        {
            Equip();
        }
        else
        {
            Buy();
        }
    }

    private void Buy() 
    {
        if (RabbitJungleGameManager.rabbitJungleBestRecord < _price)
                return;
        RabbitJungleGameManager.rabbitJungleBestRecord -= _price;
        PlayerPrefs.SetInt(_savingKey, 1);
        Equip();
    }

    private void Equip() 
    {
        RabbitJungleGameManager.rabbitJungleSkinSelectedIndex = _index;
        foreach (var item in FindObjectsOfType<RabbitJungleBuyComponent>())
        {
            item.Init();
        }
    }

    public void Init() 
    {
        UpdateBox();
    }

    private void UpdateBox() 
    {
        if (PlayerPrefs.HasKey(_savingKey))
        {
            if (RabbitJungleGameManager.rabbitJungleSkinSelectedIndex == _index)
            {
                _displayState.text = "EQUIPPED";
            }
            else
            {
                _displayState.text = "EQUIP";
            }
        }
        else
        {
            _displayState.text = _price.ToString() + "G";
        }
    }
}
