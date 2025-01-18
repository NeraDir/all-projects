using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuControl : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _recordLivingTimeShower;

    [SerializeField]
    private TMP_Text _recordPassedRingsShower;

    [SerializeField]
    private TMP_Text[] _currentCoinsShower;

    [SerializeField]
    private TMP_Text _currentPlanesSpeed;

    [SerializeField]
    private TMP_Text _upgradePriceShower;

    [SerializeField]
    private GameObject _menuPage;

    [SerializeField]
    private GameObject _shopPage;

    [SerializeField]
    private GameObject _howToPlayPage;

    [SerializeField]
    private GameObject _upgradePage;

    public static float PlanesSpeedPrice
    {
        get
        {
            if (PlayerPrefs.HasKey("AircrafterPlanesSpeedPriceSaveKey"))
            {
                return PlayerPrefs.GetFloat("AircrafterPlanesSpeedPriceSaveKey");
            }
            return 20f;
        }
        set
        {
            PlayerPrefs.SetFloat("AircrafterPlanesSpeedPriceSaveKey", value);
        }
    }

    [SerializeField]
    private AviaShopConteiner container;

    private void Start()
    {
        _recordPassedRingsShower.text = "RINGS: " + GamePlayerInformation.RecordOfPassedRings.ToString();
        _recordLivingTimeShower.text = "LIFE TIME: " + GamePlayerInformation.RecordOfLivingTime.ToString("0") + "s";

        container.BuyedIndex = 1;
        container.UpdateStateOfContainer();


        if (!PlayerPrefs.HasKey("GamePlayerAirCraftSaveKEy"))
        {
            _howToPlayPage.SetActive(true);
            PlayerPrefs.SetInt("GamePlayerAirCraftSaveKEy", 1);
        }
    }

    private void LateUpdate()
    {
        foreach (var item in _currentCoinsShower)
        {
            item.text = GamePlayerInformation.GameCoins.ToString("0");
        }
        
        _currentPlanesSpeed.text = "SPEED: " + GamePlayerInformation.PlanesSpeed.ToString("0") + "km/h";
        _upgradePriceShower.text = PlanesSpeedPrice.ToString();
    }

    public void UpgradeSpeedBttn() 
    {
        if (GamePlayerInformation.GameCoins >= PlanesSpeedPrice)
        {
            GamePlayerInformation.PlanesSpeed++;
            GamePlayerInformation.GameCoins -= PlanesSpeedPrice;
            PlanesSpeedPrice *= 2f;
        }
    }

    public void PlayBttn ()
    {
        SceneManager.LoadScene("Game");
    }

    public void ShopBttn() 
    {
        _menuPage.SetActive(false);
        _shopPage.SetActive(true);
    }

    public void UpgradeBttn() 
    {
        _menuPage.SetActive(false);
        _upgradePage.SetActive(true);
    }

    public void HowToPlayBttn() 
    {
        _menuPage.SetActive(false);
        _howToPlayPage.SetActive(true);
    }

    public void ExitBttn ()
    {
        Application.Quit();
    }
}
