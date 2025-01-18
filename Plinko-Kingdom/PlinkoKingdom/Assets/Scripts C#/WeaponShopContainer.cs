using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShopContainer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_ShowPrice;

    [SerializeField]
    private Button m_ShopButton;

    [SerializeField]
    private TMP_Text m_ShowCurrentState;

    [SerializeField]
    private GameObject[] m_ObjectDisActivate;

    public int m_Price;

    public int m_WeaponIndex;

    [SerializeField]
    private WeaponShopContainer[] m_WeaponContainers;

    public int m_WeaponBuyed
    {
        get
        {
            if (PlayerPrefs.HasKey("m_WeaponBuyedIndexSaveKey" + gameObject.name))
            {
                return PlayerPrefs.GetInt("m_WeaponBuyedIndexSaveKey" + gameObject.name);
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("m_WeaponBuyedIndexSaveKey" + gameObject.name, value);
        }
    }

    private void Awake()
    {
        CheckValues();
    }

    public void CheckValues() 
    {
        if (m_WeaponBuyed == 1)
        {
            foreach (var item in m_ObjectDisActivate)
            {
                item.SetActive(false);
            }
            if (m_WeaponIndex == PlayerDatas.m_SelectedWeapon)
            {
                m_ShowCurrentState.text = "EQIPTED";
                m_ShopButton.onClick.RemoveAllListeners();
            }
            else
            {
                m_ShowCurrentState.text = "EQUIP";
                m_ShopButton.onClick.RemoveAllListeners();
                m_ShopButton.onClick.AddListener(Equip);
            }
        }
        else 
        {
            m_ShowPrice.text = "x" + m_Price.ToString("0");
            m_ShopButton.onClick.RemoveAllListeners();
            m_ShopButton.onClick.AddListener(Buy);
        }
    }

    public void Buy() 
    {
        if (m_WeaponBuyed  == 1)
            return;
        if (PlayerDatas.Points < m_Price)
            return;
        PlayerDatas.Points -= m_Price;
        m_WeaponBuyed = 1;
        Equip();
    }

    public void Equip() 
    {
        if (m_WeaponBuyed == 0)
            return;
        PlayerDatas.m_SelectedWeapon= m_WeaponIndex;
        foreach (var item in m_WeaponContainers)
        {
            item.CheckValues();
        }
        CheckValues();
    }
}
