using System;
using UnityEngine;

namespace Game.Shop
{
    public class Money : MonoBehaviour
    {
        public int money;
        public event Action<int> OnMoneyChanged;
        public static readonly string MoneySave = "money";

        private void Start() => money = PlayerPrefs.GetInt(MoneySave);

        public void AddMoney(int amount)
        {
            money += amount;
            OnMoneyChanged?.Invoke(money);
            UpdateMoneySave();
        }

        public void DicreaseMoney(int amount)
        {
            money -= amount;
            if(money < 0) money = 0;
            OnMoneyChanged?.Invoke(money);
            UpdateMoneySave();
        }

        private void UpdateMoneySave()
        {
            PlayerPrefs.SetInt(MoneySave, money);
            PlayerPrefs.Save();
        }
    }
}