using Game.Shop;
using UnityEngine;

namespace Game.Shop
{
    public class Shop : MonoBehaviour
    {
        private ShopSkin[] _allShopSkins;
        public static Shop Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            _allShopSkins = GameObject.FindObjectsOfType<ShopSkin>();
        }

        public void UnSelectAll()
        {
            foreach(var shopSkin in _allShopSkins) shopSkin.UnSelect();
        }

        public void SaveAll()
        {
            foreach(var shopSkin in _allShopSkins) shopSkin.skin.Save();
        }
    }
}