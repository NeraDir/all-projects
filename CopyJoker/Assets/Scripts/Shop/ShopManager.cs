using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public void BuyBoom()
    {
        if(SaverManager.Coins >= 250)
        {
            SaverManager.Coins -= 250;
            UIManager.Instance.RefreshCoinsTXT();
            SaverManager.BoomCount++;
        }
    }

    public void BuyFirework()
    {
        if(SaverManager.Coins >= 500)
        {
            SaverManager.Coins -= 500;
            UIManager.Instance.RefreshCoinsTXT();
            SaverManager.FireworkCount++;
        }
    }

    public void BuyColor()
    {
        if(SaverManager.Coins >= 1000)
        {
            SaverManager.Coins -= 1000;
            UIManager.Instance.RefreshCoinsTXT();
            SaverManager.ColorCount++;
        }
    }
}
