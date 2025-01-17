using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PrefsControl
{
    public static void LoadGame()
    {
        if (PlayerPrefs.HasKey("gold"))
            return;

        PlayerPrefs.SetInt("load_lvl", 0);
        PlayerPrefs.SetInt("open_lvls", 1);
        PlayerPrefs.SetInt("num_skeen_used", 0);
        PlayerPrefs.SetInt("gold", 0);

        for (int i = 0; i < 6; i++)
        {
            PlayerPrefs.SetInt("have_skeen_" + i.ToString(), 0);
        }

    }
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
        if (PlayerPrefs.GetInt("open_lvls") == num + 1)
        {
            PlayerPrefs.SetInt("open_lvls", num + 2);
        }
    }

    public static bool HaveSkeen(int num)
    {
        if (PlayerPrefs.GetInt("have_skeen_" + num.ToString()) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public static bool TrySetSkeen(int num)
    {
        if (PlayerPrefs.GetInt("have_skeen_" + num.ToString()) == 0)
        {
            return false;
        }
        else
        {
            PlayerPrefs.SetInt("num_skeen_used", num);
            return true;
        }
    }
    public static void BuySceen(int num)
    {
        PlayerPrefs.SetInt("have_skeen_" + num.ToString(), 1);
        PlayerPrefs.SetInt("num_skeen_used", num);
    }
    public static int GetSceenNum()
    {
        return PlayerPrefs.GetInt("num_skeen_used");
    }


    public static void ChageGoald(int num)
    {
        PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold") + num);
    }
    public static int GetGoald()
    {
        return PlayerPrefs.GetInt("gold");
    }
}
