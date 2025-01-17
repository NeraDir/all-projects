using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private Sprite[] _itemImages;
    [SerializeField] private TMP_Text _itemName;
    [SerializeField] private String[] _itemNames;
    [SerializeField] private TMP_Text _itemCostName;
    [SerializeField] private TMP_Text _money;
    private int _itemCost;
    private int id = 0;
    private int[] itemsLevels;
    void Awake() 
    {
        staticInfo.money = PlayerPrefs.GetInt("money", staticInfo.money);
        _money.text = staticInfo.money+"";
    }
    void Start()
    {
        
        itemsLevels = new[]
        {
            PlayerPrefs.GetInt("maxHp",0),
            PlayerPrefs.GetInt("bulletLevel",0),
            PlayerPrefs.GetInt("shootTime",0),
            PlayerPrefs.GetInt("reloadTime",0),
            PlayerPrefs.GetInt("speed",0),
            PlayerPrefs.GetInt("magnetSize",0)
        };
        
        ChangeItem();
    }
    public void NextItemRight() 
    {
        id++;
        if (id>5) id = 0;
        ChangeItem();
    }
    public void NextItemLeft() 
    {
        id--;
        if (id<0) id = 5;
        ChangeItem();
    }

    public void ChangeItem() 
    {
        _itemImage.sprite = _itemImages[id];
        _itemName.text = _itemNames[id];
        if (itemsLevels[id]>=4) 
        {
            _itemCost=9999;
            _itemCostName.text = "MAX";
        }
        else 
        {
            _itemCost =(itemsLevels[id]+1)*5;
            _itemCostName.text = _itemCost+"";
        }
        
    }
    public void TryToBuy() 
    {
        if (_itemCost<=staticInfo.money) 
        {
            itemsLevels[id]++;
            staticInfo.money-=_itemCost;
            PlayerPrefs.SetInt("money", staticInfo.money);
            _money.text = staticInfo.money+"";
            PlayerPrefs.SetInt("maxHp", itemsLevels[0]);
            PlayerPrefs.SetInt("bulletLevel", itemsLevels[1]);
            PlayerPrefs.SetInt("shootTime", itemsLevels[2]);
            PlayerPrefs.SetInt("reloadTime",itemsLevels[3]);
            PlayerPrefs.SetInt("speed", itemsLevels[4]);
            PlayerPrefs.SetInt("magnetSize", itemsLevels[5]);
            ChangeItem();
        }
    }

}
