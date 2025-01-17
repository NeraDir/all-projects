using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuServiceRegistrator : MonoBehaviour
{
    [SerializeField] private ShopPanel shopPanel;
    [SerializeField] private SettingPanel settingPanel;

    private void Awake()
    {
        ServiceLocator.Register(shopPanel);
        ServiceLocator.Register(settingPanel);

    }
}
