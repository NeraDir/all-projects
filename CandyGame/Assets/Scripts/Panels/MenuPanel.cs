using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuPanel : MonoBehaviour
{
    public void OpenShop()
    {
        ShopPanel shopPanel = ServiceLocator.Get<ShopPanel>();
        shopPanel.Open();
    }

    public void OpenSettings()
    {
        SettingPanel settingPanel = ServiceLocator.Get<SettingPanel>();
        settingPanel.Open();
    }

    public void OpenPlayGround()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}
