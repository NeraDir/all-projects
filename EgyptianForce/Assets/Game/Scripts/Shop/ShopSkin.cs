using UnityEngine;
using UnityEngine.UI;

namespace Game.Shop
{
    public class ShopSkin : MonoBehaviour
    {
        public Skin skin;

        [SerializeField] private Text _skinText;
        [SerializeField] private Money _money;

        [SerializeField] private string _bought = "Select";
        [SerializeField] private string _selected = "Selected";

        private void Start()
        {
            skin.GetAndSet();
            SetSkinText();
            if(skin.isBought && skin.isSelected) Select();
        }

        public void OnSkinButtonPress()
        {
            if(!skin.isBought && !skin.isSelected) Buy();
            else if(skin.isBought && !skin.isSelected) Select();
            skin.Save();
        }

        private void Buy()
        {
            if(_money.money >= skin.cost)
            {
                _money.DicreaseMoney(skin.cost);
                skin.isBought = true;
                SetSkinText();
                skin.Save();
            }
        }

        private void Select()
        {
            Shop.Instance.UnSelectAll();
            skin.isSelected = true;
            SetSkinText();
            skin.Save();
        }
        
        public void UnSelect()
        {
            skin.isSelected = false;
            SetSkinText();
            skin.Save();
        }

        private void SetSkinText()
        {
            if(!skin.isBought && !skin.isSelected) _skinText.text = skin.cost.ToString();
            else if(skin.isBought && !skin.isSelected) _skinText.text = _bought;
            else if(skin.isBought && skin.isSelected) _skinText.text = _selected;
            skin.Save();
        }
    }
}