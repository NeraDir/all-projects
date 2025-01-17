using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIButtonsMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _shopMenu;
    [SerializeField] private GameObject _HWT;

    public int saveHWT
    {
        get
        {
            if(!PlayerPrefs.HasKey("saveHWT"))
                return 0;

            return PlayerPrefs.GetInt("saveHWT");
        }
        set
        {
            PlayerPrefs.SetInt("saveHWT", value);
        }
    }

    private void Start()
    {
        if(saveHWT == 0 && _HWT !=  null)
        {
            _HWT.SetActive(true);
            saveHWT = 1;
        }
    }

    private Text _moneyText;
    public void StartGame()
    {
        EndGame.endGame = false;
        SceneManager.LoadScene("Game");
    }

    public void OpenShop()
    {    
        _mainMenu.SetActive(false);
        _shopMenu.SetActive(true);
        _moneyText = GameObject.FindWithTag("MoneyText").GetComponent<Text>();
        _moneyText.text = CollisionController.Coin.ToString();
    }
    public void UpgradeButtonDamage()
    {
        if (CollisionController.Coin >= 100)
            Character.upgradeHP++;
        UpdateCoin();        
    }
    
    public void UpgradeButtonHealth()
    {
        if (CollisionController.Coin >= 100)
            Character.upgradeStartHP++;
        UpdateCoin();
        
    }

    private void UpdateCoin()
    {
        _moneyText = GameObject.FindWithTag("MoneyText").GetComponent<Text>();
        if (CollisionController.Coin >= 100)
        {
            _moneyText.text = (CollisionController.Coin - 100).ToString();
            CollisionController.Coin -= 100;
        }
    }

    public void ExitButtonInMenu()
    {
        _shopMenu.SetActive(false);
        _mainMenu.SetActive(true);        
    }

    public void ExitButtonWithGame()
    {
        Application.Quit();
    }
}
