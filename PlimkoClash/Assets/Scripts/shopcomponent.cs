using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class shopcomponent : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]
    private int _index;

    [SerializeField]
    private int _price;

    private int _isBuyed 
    {
        get
        {
            if (PlayerPrefs.HasKey($"shopcomponentballindexsavekey{_index}"))
            {
                return PlayerPrefs.GetInt($"shopcomponentballindexsavekey{_index}");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt($"shopcomponentballindexsavekey{_index}", value);
        }
    }

    private Text _text;

    private void Start()
    {
        _text = GetComponentInChildren<Text>();
        OnCheck();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnBuy();
    }

    public void OnBuy() 
    {
        if (_isBuyed != 0)
        {
            OnEquip();
        }
        else
        {
            if (gamecontoller.ballStars >= _price)
            {
                gamecontoller.ballStars -= _price;
                _isBuyed = 1;
                OnEquip();
            }
        }
    }

    private void OnEquip() 
    {
        ballmovement.ballSpriteIndex = _index;
        OnCheck();
    }

    public void OnCheck() 
    {
        if (_isBuyed != 0)
        {
            if (_index == ballmovement.ballSpriteIndex)
            {
                _text.text = "equiped";
            }
            else
            {
                _text.text = "equip";
            }
        }
        else
        {
            _text.text = _price.ToString("0") + "c";
        }
    }
}
