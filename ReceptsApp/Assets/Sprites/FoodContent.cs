using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FoodContent : MonoBehaviour
{
    public FoodInformation Food;

    [SerializeField] private TMP_Text _foodLabel;
    [SerializeField] private TMP_Text _foodDescription;
    [SerializeField] private Image _foodImage;
    [SerializeField] private TMP_Text _foodHowToCook;
    [SerializeField] private TMP_Text _foodIngridients;

    private void Start()
    {
        _foodImage.sprite = Food.FoodImage;
        _foodLabel.text = $"{Food.Foodlabel}";
        _foodHowToCook.text = $"{Food.FoodHowToCoock}<br> _______________________________________________________";
        _foodIngridients.text = $"{Food.FoodIngridients}";
    }
}
