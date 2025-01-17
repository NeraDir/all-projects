using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Skin", menuName = "ScriptableObjects/Skin")]
    public class Skin : ScriptableObject
    {
        public int num;
        public int cost;
        public bool isBought;
        public bool isSelected;
        public bool isMain;

        public void GetAndSet()
        {
            if(isMain)
            {
                isBought = true;
                return;
            }

            if(PlayerPrefs.GetInt($"IsBought{num}") == 1) isBought = true;
            else isBought = false;

            if(PlayerPrefs.GetInt($"IsSelected{num}") == 1) isSelected = true;
            else isSelected = false;
        }

        public void Save()
        {
            if(isBought) PlayerPrefs.SetInt($"IsBought{num}", 1);
            else PlayerPrefs.SetInt($"IsBought{num}", 0);

            if(isSelected) PlayerPrefs.SetInt($"IsSelected{num}", 1);
            else PlayerPrefs.SetInt($"IsSelected{num}", 0);
        }
    }
}