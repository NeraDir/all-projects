using System;
using UnityEngine;

public class WeeklyFood : MonoBehaviour
{
    public MainFood[] mainFoods;
    public SecondFood[] secondFoods;
    public DrankFood[] drankFoods;

    [Serializable]
    public class MainFood
    {
        public string FoodLabel;
        public string FoodDirection;
        public string FoodHowToCake;
        public string FoodIngredients;
    }
    [Serializable]
    public class SecondFood
    {
        public string FoodLabel;
        public string FoodDirection;
        public string FoodHowToCake;
        public string FoodIngredients;
    }
    [Serializable]
    public class DrankFood
    {
        public string FoodLabel;
        public string FoodDirection;
        public string FoodHowToCake;
        public string FoodIngredients;
    }
}
