using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class FoodConteinerSpawner : MonoBehaviour
{
    private int PageID;

    [SerializeField] private GameObject AuthorFoods;
    [SerializeField] private GameObject MainFoods;

    [SerializeField] private GameObject m_SimpleSelectPage;
    [SerializeField] private GameObject m_WeekSelectPage;

    [SerializeField] private TMP_Text mainLabel;
    [SerializeField] private TMP_Text secondLabel;
    [SerializeField] private TMP_Text drunkLabel;

    private UI ui;


    [Header("SpawnLogics")]
    [SerializeField] private Transform SpawnPosition;
    [SerializeField] private FoodContent _foodContent;

    [Header("Good Food")]
    public List<FoodInformation> _MAINgoodFoods = new List<FoodInformation>();
    public List<FoodInformation> _SECONDgoodFoods = new List<FoodInformation>();
    public List<FoodInformation> _DRUNKgoodFoods = new List<FoodInformation>();

    [Header("Bad Food")]
    public List<FoodInformation> _MAINbadFoods = new List<FoodInformation>();
    public List<FoodInformation> _SECONDbadFoods = new List<FoodInformation>();
    public List<FoodInformation> _DRUNKbadFoods = new List<FoodInformation>();

    [Header("Can Only Try Food")]
    public List<FoodInformation> _MAINcanOrTryFoods = new List<FoodInformation>();
    public List<FoodInformation> _SECONDcanOrTryFoods = new List<FoodInformation>();
    public List<FoodInformation> _DRUNKcanOrTryFoods = new List<FoodInformation>();

    [Header("Can Only 1 Food")]
    public List<FoodInformation> _MAINonley1DayFoods = new List<FoodInformation>();
    public List<FoodInformation> _SECONDonley1DayFoods = new List<FoodInformation>();
    public List<FoodInformation> _DRUNKonley1DayFoods = new List<FoodInformation>();

    [Header("Week Food")]
    public List<FoodInformation> _MondayFoods = new List<FoodInformation>();
    public List<FoodInformation> _ThusdayFoods = new List<FoodInformation>();
    public List<FoodInformation> _FirthDayFoods = new List<FoodInformation>();
    public List<FoodInformation> _FourDayFoods = new List<FoodInformation>();
    public List<FoodInformation> _LastFoods = new List<FoodInformation>();

    [Header("Author Food")]
    public List<FoodInformation> _MAINauthorFoods = new List<FoodInformation>();

    private void Start()
    {
        ui = GetComponent<UI>();
    }

    public void OnClickChoosePage(int Page)
    {
        PageID = Page;
        if (PageID == 4)
        {
            ChangeButtonsLabel(true);
            m_SimpleSelectPage.SetActive(true);
            m_WeekSelectPage.SetActive(false);
        }
        else if (PageID == 5) 
        {
            m_SimpleSelectPage.SetActive(false);
            m_WeekSelectPage.SetActive(true);
        }
        else if (PageID == 6)
        {
            GoToAuthor();
            SpawnList(_MAINauthorFoods);
        }
        else
        {
            m_SimpleSelectPage.SetActive(true);
            m_WeekSelectPage.SetActive(false);
            ChangeButtonsLabel(false);
        }
    }

    public void OnClickSpawnMain(int podpage)
    {
        #region GoodFoods
        if (PageID == 1 && podpage == 1)
        {
            SpawnList(_MAINgoodFoods);
        }
        else if (PageID == 1 && podpage == 2)
        {
            SpawnList(_SECONDgoodFoods);
        }
        else if (PageID == 1 && podpage == 3)
        {
            SpawnList(_DRUNKgoodFoods);
        }
        #endregion
        #region Bad Food
        if (PageID == 2 && podpage == 1)
        {
            SpawnList(_MAINbadFoods);
        }
        else if (PageID == 2 && podpage == 2)
        {
            SpawnList(_SECONDbadFoods);
        }
        else if (PageID == 2 && podpage == 3)
        {
            SpawnList(_DRUNKbadFoods);
        }
        #endregion
        #region Can Only Try Food
        if (PageID == 3 && podpage == 1)
        {
            SpawnList(_MAINcanOrTryFoods);
        }
        else if (PageID == 3 && podpage == 2)
        {
            SpawnList(_SECONDcanOrTryFoods);
        }
        else if (PageID == 3 && podpage == 3)
        {
            SpawnList(_DRUNKcanOrTryFoods);
        }
        #endregion
        #region Can Only 1 Food
        if (PageID == 4 && podpage == 1)
        {
            SpawnList(_MAINonley1DayFoods);
        }
        else if (PageID == 4 && podpage == 2)
        {
            SpawnList(_SECONDonley1DayFoods);
        }
        else if (PageID == 4 && podpage == 3)
        {
            SpawnList(_DRUNKonley1DayFoods);
        }
        #endregion
        #region Week Food
        if (PageID == 5 && podpage == 1)
        {
            SpawnList(_MondayFoods);
        }
        else if (PageID == 5 && podpage == 2)
        {
            SpawnList(_ThusdayFoods);
        }
        else if (PageID == 5 && podpage == 3)
        {
            SpawnList(_FirthDayFoods);
        }
        else if (PageID == 5 && podpage == 4)
        {
            SpawnList(_FourDayFoods);
        }
        else if (PageID == 5 && podpage == 5)
        {
            SpawnList(_LastFoods);
        }
        #endregion
        #region Author Foods
        #endregion
    }

    private void GoToAuthor() 
    {
        ui.ActivePage = 2;
        MainFoods.SetActive(false);
        AuthorFoods.SetActive(true);
    }

    public void GoBack() 
    {
        if (PageID == 6)
        {
            ui.ActivePage = 0;
            MainFoods.SetActive(true);
            AuthorFoods.SetActive(false);
        }
    }

    private void ChangeButtonsLabel(bool change) 
    {
        if (change)
        {
            mainLabel.text = "Завтрак";
            secondLabel.text = "Обед";
            drunkLabel.text = "Ужин";
        }
        else
        {
            mainLabel.text = "Основные блюда";
            secondLabel.text = "Закуски";
            drunkLabel.text = "Напитки";
        }
    }

    private void SpawnList(List<FoodInformation> foodInformations) 
    {
        foreach (Transform trans in SpawnPosition)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < foodInformations.Count; i++)
        {
            FoodContent food = Instantiate(_foodContent, SpawnPosition);
            food.Food = foodInformations[i];
        }
    }
}
