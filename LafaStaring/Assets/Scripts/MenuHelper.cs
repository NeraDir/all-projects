using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHelper : MonoBehaviour
{
    [SerializeField] private TMP_Text showCurrentCoins;

    [SerializeField] private TMP_Text showDestroyedCount;
    [SerializeField] private TMP_Text coinsEnaring;

    public static int PlayerCoins
    {
        get
        {
            if (PlayerPrefs.HasKey("PlayerCoinser"))
            {
                return PlayerPrefs.GetInt("PlayerCoinser");
            }
            return 5;
        }
        set
        {
            PlayerPrefs.SetInt("PlayerCoinser", value);
        }
    }

    public static int countDestroyed;
    public static int coinsers;

    private void Start()
    {
        coinsers = 0;
        countDestroyed = 0;
    }

    private void LateUpdate()
    {
        if (showCurrentCoins != null)
        {
            showCurrentCoins.text = "BALANCE: " + PlayerCoins.ToString("0");
        }
        
        if (coinsEnaring!=null)
        {
            coinsEnaring.text = coinsers.ToString("0");
        }
        if (showDestroyedCount != null)
        {
            showDestroyedCount.text = countDestroyed.ToString("0");
        }
       
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        PlayerCoins += coinsers;
        SceneManager.LoadScene("Menu");
    }

    public void LoadCurrentLevel()
    {
        Time.timeScale = 1;
        PlayerCoins += coinsers;
        SceneManager.LoadScene("Game");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
