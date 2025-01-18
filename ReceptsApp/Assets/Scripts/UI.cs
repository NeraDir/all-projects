using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public int ActivePage;

    [SerializeField] private GameObject _backButton;

    [SerializeField] private GameObject[] _pages;

    [SerializeField] private Recepts recepts;

    [Header("Labels Of Pages")]
    [SerializeField] private TextMeshProUGUI _choosedFoode;
    [SerializeField] private TextMeshProUGUI _choosedVarOfThreeFoods;

    [Header("Text Showers")]
    [SerializeField] private TextMeshProUGUI[] _foodLabel;
    [SerializeField] private TextMeshProUGUI[] _foodIngridients;
    [SerializeField] private TextMeshProUGUI[] _foodHowToCook;
    [SerializeField] private Image[] _foodImages;

    [Header("Pages")]
    [SerializeField] private GameObject _pageOfFood;
    [SerializeField] private GameObject _selectOfVariantsOfFoods;
    [SerializeField] private GameObject _foodsSelect;

    private int _selectedFoodOpinion;

    private void Start()
    {
        /*for (int i = 0; i < recepts.TrashFood.mainFoods.Length; i++)
        {
            _foodLabel[i].text = recepts.TrashFood.drankFoods[i].FoodLabel;
            _foodIngridients[i].text = recepts.TrashFood.drankFoods[i].FoodIngredients;
            _foodHowToCook[i].text = recepts.TrashFood.drankFoods[i].FoodHowToCake;
            _foodImages[i].sprite = recepts.TrashFood.drankFoods[i].FoodImage;
        }*/
    }


    private void Update()
    {
        if (ActivePage > 0)
        {
            _backButton.SetActive(true);
        }
        else
        {
            _backButton.SetActive(false);
        }
    }

    public void OnClickGOBack() 
    {
        ActivePage--;
        for (int i = 0; i < _pages.Length; i++)
        {
            if (ActivePage == i)
            {
                _pages[i].SetActive(true);
            }
            else
            {
                _pages[i].SetActive(false);
            }
        }
    }

    public void OnSelectGoodFoods(int selectPage) 
    {
        _choosedFoode.text = recepts.GoodFoods.Name;
        PageOpen(selectPage);
    }

    public void OnSelectTrashFoods(int selectPage)
    {
        _choosedFoode.text = recepts.TrashFood.Name;
        PageOpen(selectPage);
    }

    public void OnSelectOneOfDayFood(int selectPage)
    {
        _choosedFoode.text = recepts.OneOfDayFoods.Name;
        ActivePage++;
        PageOpen(selectPage);
    }

    public void OnClickMaybeFoods(int selectPage) 
    {
        _choosedFoode.text = recepts.MaybeFoods.Name;
        ActivePage++;
        PageOpen(selectPage);
    }

    public void OnSelectWeeklyFoods(int selectPage) 
    {
        _choosedFoode.text = recepts.WeeklyFoods.Name;
        ActivePage++;
        PageOpen(selectPage);
    }

    public void OnClickAuthorFood(int selectPage) 
    {
        _choosedFoode.text = recepts.AuthorFoods.Name;
        ActivePage++;
        PageOpen(selectPage);
    }


    public void OnClickMainFood() 
    {
        _choosedVarOfThreeFoods.text = "ÎÑÍÎÂÍÎÅ ÁËÞÄÎ";
        ActivePage++;
        _pageOfFood.SetActive(true);
        _selectOfVariantsOfFoods.SetActive(false);
    }

    public void OnClickSecondFood()
    {
        _choosedVarOfThreeFoods.text = "ÇÀÊÓÑÊÈ";
        ActivePage++;
        _pageOfFood.SetActive(true);
        _selectOfVariantsOfFoods.SetActive(false);
    }

    public void OnClickDrankFood()
    {
        _choosedVarOfThreeFoods.text = "ÍÀÏÈÒÊÈ";
        ActivePage++;
        _pageOfFood.SetActive(true);
        _selectOfVariantsOfFoods.SetActive(false);
    }

    private void PageOpen(int selectedFood) 
    {
        _selectedFoodOpinion = selectedFood;
        ActivePage++;
        _selectOfVariantsOfFoods.SetActive(true);
        _foodsSelect.SetActive(false);

    }

   /* private void OnClickedMain() 
    {
        for (int i = 1; i < 2; i++)
        {
            if (_selectedFoodOpinion == 1)
            {
                for (int j = 0; j < recepts.GoodFoods.mainFoods.Length; j++)
                {
                    _foodLabel[j].text = recepts.GoodFoods.mainFoods[j].FoodLabel;
                    _foodIngridients[j].text = recepts.GoodFoods.mainFoods[j].FoodIngredients;
                    _foodHowToCook[j].text = recepts.GoodFoods.mainFoods[j].FoodHowToCake;
                    _foodImages[j].sprite = recepts.GoodFoods.mainFoods[j].FoodImage;
                }
            }
            else if (_selectedFoodOpinion == 2)
            {
                for (int j = 0; j < recepts.TrashFood.mainFoods.Length; j++)
                {
                    _foodLabel[j].text = recepts.TrashFood.mainFoods[j].FoodLabel;
                    _foodIngridients[j].text = recepts.TrashFood.mainFoods[j].FoodIngredients;
                    _foodHowToCook[j].text = recepts.TrashFood.mainFoods[j].FoodHowToCake;
                    _foodImages[j].sprite = recepts.TrashFood.mainFoods[j].FoodImage;
                }
            }
        }
    }
*/
}
