using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PrefsControl
{
    //-----
    public static void LoadGame()
    {
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey("gold"))
            return;

        PlayerPrefs.SetInt("load_lvl", 0);
        PlayerPrefs.SetInt("open_lvls", 1);
        PlayerPrefs.SetInt("gold", 0);

        for (int i = 0; i < 3; i++)
        {
            PlayerPrefs.SetInt("have_upgrades_" + i.ToString(), 0);
        }
    }
    //-----
    public static bool TryLoadLvl(int num)
    {
        if (num >= PlayerPrefs.GetInt("open_lvls"))
            return false;

        PlayerPrefs.SetInt("load_lvl", num);
        return true;
    }
    public static int GetMaksOpenedLvlNum()
    {
        return PlayerPrefs.GetInt("open_lvls");
    }
    public static int GetLvlNum()
    {
        return PlayerPrefs.GetInt("load_lvl");
    }
    public static void FinisLvl(int num)
    {
        if (num >= 6)
            return;
        if (PlayerPrefs.GetInt("open_lvls") == num + 1)
        {
            PlayerPrefs.SetInt("open_lvls", num + 2);
        }
    }
    //-----

    public static void BuyUpgade(int num)
    {
        PlayerPrefs.SetInt("have_upgrades_" + num.ToString(), PlayerPrefs.GetInt("have_upgrades_" + num.ToString()) + 1);
    }
    public static int GetUpgrade(int num)
    {
        return PlayerPrefs.GetInt("have_upgrades_" + num.ToString());
    }
    //-----
    public static void ChageGoald(int num)
    {
        PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold") + num);
    }
    public static int GetGoald()
    {
        return PlayerPrefs.GetInt("gold");
    }
}
