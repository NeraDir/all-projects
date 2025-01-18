using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDatas : MonoBehaviour
{
    public static int Points 
    {
        get
        {
            if (PlayerPrefs.HasKey("PlayerPoints"))
            {
                return PlayerPrefs.GetInt("PlayerPoints");
            }
            return 100;
        }
        set
        {
            PlayerPrefs.SetInt("PlayerPoints", value);
        }
    }

    public static int m_SelectedWeapon
    {
        get
        {
            if (PlayerPrefs.HasKey("PlayerSelectedWeaponIndexSaveKey"))
            {
                return PlayerPrefs.GetInt("PlayerSelectedWeaponIndexSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("PlayerSelectedWeaponIndexSaveKey", value);
        }
    }

    public static int m_WeaponBulletsCount 
    {
        get
        {
            if (PlayerPrefs.HasKey("PlayerSelectedWeaponBUlletsCountIndexSaveKey"))
            {
                return PlayerPrefs.GetInt("PlayerSelectedWeaponBUlletsCountIndexSaveKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("PlayerSelectedWeaponBUlletsCountIndexSaveKey", value);
        }
    }

    public static int m_PlayerSelectedBulletIndex
    {
        get
        {
            if (PlayerPrefs.HasKey("PlayerSelectedBulletIndexSaveKey"))
            {
                return PlayerPrefs.GetInt("PlayerSelectedBulletIndexSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("PlayerSelectedBulletIndexSaveKey", value);
        }
    }

    public static int ballMovementSpeed
    {
        get
        {
            if (PlayerPrefs.HasKey("ballMovementSpeedSaveKEy"))
            {
                return PlayerPrefs.GetInt("ballMovementSpeedSaveKEy");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("ballMovementSpeedSaveKEy", value);
        }
    }

    public static int enemiesCount
    {
        get
        {
            if (PlayerPrefs.HasKey("enemiesCountSaveKey"))
            {
                return PlayerPrefs.GetInt("enemiesCountSaveKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("enemiesCountSaveKey", value);
        }
    }

    [SerializeField]
    private TMP_Text m_ShowPointsCount;

    [SerializeField]
    private WeaponShopContainer m_WeaponShopContainer;

    [SerializeField]
    private GameObject m_HwpPage;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("firstWeaponEquipted"))
        {
            if (m_WeaponShopContainer != null)
                m_WeaponShopContainer.Buy();
            m_HwpPage.SetActive(true);
            PlayerPrefs.SetInt("firstWeaponEquipted", 1);
        }
    }

    private void LateUpdate()
    {
        if (m_ShowPointsCount != null)
            m_ShowPointsCount.text = "x" + Points.ToString("0");
    }
}
