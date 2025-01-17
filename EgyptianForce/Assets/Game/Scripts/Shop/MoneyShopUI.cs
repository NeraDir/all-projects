using UnityEngine;
using UnityEngine.UI;

namespace Game.Shop
{
    public class MoneyShopUI : MonoBehaviour
    {
        [SerializeField] private Money _money;
        [SerializeField] private Text _moneyText;

        private void Start() => UpdateMoneyDisplay(_money.money);

        private void OnEnable() => _money.OnMoneyChanged += UpdateMoneyDisplay;

        private void OnDisable() => _money.OnMoneyChanged -= UpdateMoneyDisplay;

        public void UpdateMoneyDisplay(int money) => _moneyText.text = money.ToString();
    }
}