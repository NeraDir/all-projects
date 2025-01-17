using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragonLanSkinsManager : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]
    private Text showPrice;

    [SerializeField]
    private Text showSouls;

    [SerializeField]
    private int skinIndex;

    [SerializeField]
    private int skinPrice;

    [SerializeField]
    private DragonLanSkinsManager[] skinManagers;

    [SerializeField]
    private GameObject lockPanel;

    [SerializeField]
    private int needSoulsCount;

    private int state 
    {
        get
        {
            if (PlayerPrefs.HasKey("DragonLanSkinStateSaveKey" + skinIndex))
            {
                return PlayerPrefs.GetInt("DragonLanSkinStateSaveKey" + skinIndex);
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("DragonLanSkinStateSaveKey" + skinIndex, value);
        }
    }

    private int open
    {
        get
        {
            if (PlayerPrefs.HasKey("DragonLanSkinStateOpenSaveKey" + skinIndex))
            {
                return PlayerPrefs.GetInt("DragonLanSkinStateOpenSaveKey" + skinIndex);
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("DragonLanSkinStateOpenSaveKey" + skinIndex, value);
        }
    }

    private void Start()
    {
        UpdateSkin();
    }

    public void OnClickOpenSkin() 
    {
        if (DragonLanController.DragonLanSoulsCount >= needSoulsCount)
        {
            DragonLanController.DragonLanSoulsCount -= needSoulsCount;
            open = 1;
            UpdateSkin();
        }
        else
        {
            Handheld.Vibrate();
        }
    }

    public void Buy() 
    {
        if (state == 0)
        {
            if (DragonLanGameController.coins >= skinPrice)
            {
                DragonLanGameController.coins -= skinPrice;
                state = 1;
                Equip();
            }
            else
            {
                Handheld.Vibrate();
            }
        }
        else
        {
            Equip();
        }
    }

    private void Equip() 
    {
        DragonLanController.DragonLanSkinIndex = skinIndex;
        foreach (var item in skinManagers)
        {
            item.UpdateSkin();
        }
    }

    public void UpdateSkin() 
    {
        showSouls.text = needSoulsCount.ToString("0");
        if (open == 0)
            lockPanel.SetActive(true);
        else
            lockPanel.SetActive(false);
        if (state == 0)
        {
            showPrice.text = skinPrice.ToString("0");
        }
        else
        {
            if (DragonLanController.DragonLanSkinIndex == skinIndex)
            {
                showPrice.text = "EQUIPED";
            }
            else
            {
                showPrice.text = "EQUIP";
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Buy();
    }
}
