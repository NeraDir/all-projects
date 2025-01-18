using UnityEngine;
using System;

public class Recepts : MonoBehaviour
{
    public GoodFood GoodFoods;
    public TrashFoods TrashFood;
    public MaybeFood MaybeFoods;
    public OneOfDayFood OneOfDayFoods;
    public WeeklyFood WeeklyFoods;
    public AuthorFood AuthorFoods;

    [Serializable]
    public class GoodFood
    {
        public string Name;
        public MainFood[] mainFoods;
        public SecondFood[] secondFoods;
        public DrankFood[] drankFoods;

        [Serializable]
        public class MainFood
        {
            public Sprite FoodImage;
            public string FoodLabel;
            public string FoodHowToCake;
            public string FoodIngredients;
        }
        [Serializable]
        public class SecondFood
        {
            public Sprite FoodImage;
            public string FoodLabel;
            public string FoodHowToCake;
            public string FoodIngredients;
        }

        [Serializable]
        public class DrankFood
        {
            public Sprite FoodImage;
            public string FoodLabel;
            public string FoodHowToCake;
            public string FoodIngredients;
        }
    }
    [Serializable]
    public class TrashFoods
    {
        public string Name;
        public MainFood[] mainFoods;
        public SecondFood[] secondFoods;
        public DrankFood[] drankFoods;

        [Serializable]
        public class MainFood
        {
            public Sprite FoodImage;
            public string FoodLabel;
            public string FoodHowToCake;
            public string FoodIngredients;
        }
        [Serializable]
        public class SecondFood
        {
            public Sprite FoodImage;
            public string FoodLabel;
            public string FoodHowToCake;
            public string FoodIngredients;
        }
        [Serializable]
        public class DrankFood
        {
            public Sprite FoodImage;
            public string FoodLabel;
            public string FoodHowToCake;
            public string FoodIngredients;
        }
    }
    [Serializable]
    public class MaybeFood
    {
        public string Name;
        public MainFood[] mainFoods;
        public SecondFood[] secondFoods;
        public DrankFood[] drankFoods;

        [Serializable]
        public class MainFood
        {
            public Sprite FoodImage;
            public string FoodLabel;
            public string FoodHowToCake;
            public string FoodIngredients;
        }
        [Serializable]
        public class SecondFood
        {
            public Sprite FoodImage;
            public string FoodLabel;
            public string FoodHowToCake;
            public string FoodIngredients;
        }
        [Serializable]
        public class DrankFood
        {
            public Sprite FoodImage;
            public string FoodLabel;
            public string FoodHowToCake;
            public string FoodIngredients;
        }
    }
    [Serializable]
    public class OneOfDayFood
    {
        public string Name;
        public MainFood[] mainFoods;
        public SecondFood[] secondFoods;
        public DrankFood[] drankFoods;

        [Serializable]
        public class MainFood
        {
            public Sprite FoodImage;
            public string FoodLabel;
            public string FoodHowToCake;
            public string FoodIngredients;
        }
        [Serializable]
        public class SecondFood
        {
            public Sprite FoodImage;
            public string FoodLabel;
            public string FoodHowToCake;
            public string FoodIngredients;
        }
        [Serializable]
        public class DrankFood
        {
            public Sprite FoodImage;
            public string FoodLabel;
            public string FoodHowToCake;
            public string FoodIngredients;
        }
    }
    [Serializable]
    public class WeeklyFood
    {
        public string Name;
        public MainFood[] mainFoods;
        public SecondFood[] secondFoods;
        public DrankFood[] drankFoods;

        [Serializable]
        public class MainFood
        {
            public Sprite FoodImage;
            public string FoodLabel;
            public string FoodHowToCake;
            public string FoodIngredients;
        }
        [Serializable]
        public class SecondFood
        {
            public Sprite FoodImage;
            public string FoodLabel;
            public string FoodHowToCake;
            public string FoodIngredients;
        }
        [Serializable]
        public class DrankFood
        {
            public Sprite FoodImage;
            public string FoodLabel;
            public string FoodHowToCake;
            public string FoodIngredients;
        }
    }
    [Serializable]
    public class AuthorFood
    {
        public string Name;
        public MainFood[] mainFoods;
        public SecondFood[] secondFoods;
        public DrankFood[] drankFoods;

        [Serializable]
        public class MainFood
        {
            public Sprite FoodImage;
            public string FoodLabel;
            public string FoodHowToCake;
            public string FoodIngredients;
        }
        [Serializable]
        public class SecondFood
        {
            public Sprite FoodImage;
            public string FoodLabel;
            public string FoodHowToCake;
            public string FoodIngredients;
        }
        [Serializable]
        public class DrankFood
        {
            public Sprite FoodImage;
            public string FoodLabel;
            public string FoodHowToCake;
            public string FoodIngredients;
        }
    }
}
