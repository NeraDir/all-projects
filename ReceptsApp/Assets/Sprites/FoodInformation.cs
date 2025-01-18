using UnityEngine;

[CreateAssetMenu (fileName = "Food",menuName ="Create Food")]
public class FoodInformation : ScriptableObject
{
    public string Foodlabel;
    public string FoodDescription;
    public string FoodIngridients;
    public string FoodHowToCoock;
    public Sprite FoodImage;
}
