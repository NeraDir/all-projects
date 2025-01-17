using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static int currentLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("CowboySlotWildCurrentLevelSaveKey"))
                return PlayerPrefs.GetInt("CowboySlotWildCurrentLevelSaveKey");
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("CowboySlotWildCurrentLevelSaveKey", value);
        }
    }

    public static int wildwestgamemanagercanvasmarginValue
    {
        get
        {
            if (PlayerPrefs.HasKey("wildwestgamemanagercanvasmarginValueKey"))
            {
                return PlayerPrefs.GetInt("wildwestgamemanagercanvasmarginValueKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("wildwestgamemanagercanvasmarginValueKey", value);
        }
    }

    public static string gamemanagercanvasnamestringKey;

    public static int wildwestgamemanagerActiveTollBarValue
    {
        get
        {
            if (PlayerPrefs.HasKey("wildwestgamemanagerActiveTollBarValueKey"))
            {
                return PlayerPrefs.GetInt("wildwestgamemanagerActiveTollBarValueKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("wildwestgamemanagerActiveTollBarValueKey", value);
        }
    }

    public static int currentCoins
    {
        get
        {
            if (PlayerPrefs.HasKey("CowboySlotWildCurrentCoinsSaveKey"))
                return PlayerPrefs.GetInt("CowboySlotWildCurrentCoinsSaveKey");
            return 100;
        }
        set
        {
            PlayerPrefs.SetInt("CowboySlotWildCurrentCoinsSaveKey", value);
        }
    }

    public static UnityEvent isBeginSlotStart = new UnityEvent();

    public static UnityEvent playerDeath = new UnityEvent();

    public static UnityEvent playerReachedFinish = new UnityEvent();

    public static int xValue = 0;

    public static bool canGO;

    [SerializeField]
    private GameObject _howToPlayScreen;

    [SerializeField]
    private TableController _tableController;

    [SerializeField]
    private Text[] _showCurrentLevel;

    [SerializeField]
    private Text _showXResult;

    [SerializeField]
    private Text[] _showCurrentCoins;

    [SerializeField]
    private GameObject _gameScreen;

    [SerializeField]
    private GameObject _resultScreen;

    [SerializeField]
    private GameObject _rulleteScreen;

    [SerializeField]
    private GameObject _preGameScreen;

    [SerializeField]
    private GameObject _preSaluneScreen;

    private int isNext 
    {
        get 
        {
            if (PlayerPrefs.HasKey("CowboySlotWildIsNextSaveKey"))
                return PlayerPrefs.GetInt("CowboySlotWildIsNextSaveKey");
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("CowboySlotWildIsNextSaveKey",value);
        }
    }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("CowboySlotWildHowToPlaySeeSaveKey"))
        {
            _howToPlayScreen.SetActive(true);
            PlayerPrefs.SetInt("CowboySlotWildHowToPlaySeeSaveKey", 1);
        }
        xValue = 0;
        CerclerComponentersf.cerclerEnd.AddListener(OnStartOnEnd);
        EnemieController.canShoot = false;
        playerReachedFinish.AddListener(OnFinish);
        playerDeath.AddListener(OnEnd);
        canGO = false;
        if (isNext == 0)
        {
            _preGameScreen.SetActive(false);
            _rulleteScreen.SetActive(true);
        }
        else
        {
            _preGameScreen.SetActive(true);
            _rulleteScreen.SetActive(false);
        }
    }

    private void OnFinish() 
    {
        canGO = false;
        _rulleteScreen.SetActive(true);
    }

    private void OnEnd()
    {
        canGO = false;
        _resultScreen.SetActive(true);
    }

    public void OnClickEnd() 
    {
        _resultScreen.SetActive(true);
    }

    private void OnStartOnEnd() 
    {
        _gameScreen.SetActive(true);
        EnemieController.canShoot = true;
        canGO = true;
    }

    private void OnApplicationQuit()
    {
        isNext = 0;
        currentLevel = 1;
    }

    public void OnClickRestart() 
    {
        currentLevel = 1;
        isNext = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickMenu() 
    {
        currentLevel = 1;
        isNext =  0; 
        SceneManager.LoadScene("Menu");
    }

    public void OnClickNext() 
    {
        currentLevel += 1;
        isNext = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDestroy()
    {
        CerclerComponentersf.cerclerEnd.RemoveAllListeners();
    }

    public void OnClickSlotting() 
    {
        if (TableController.cantClick)
            return;
        TableController.cantClick = true;
        _tableController.OnEndRotate();
        Invoke(nameof(Wait), 0.1f);
    }

    private void LateUpdate()
    {
        _showXResult.text = "x" + xValue.ToString();
        foreach (var item in _showCurrentCoins)
        {
            item.text = "x" +  currentCoins.ToString();
        }
        foreach (var item in _showCurrentLevel)
        {
            item.text = "LVL " + currentLevel.ToString();
        }
    }

    private void Wait() 
    {
        isBeginSlotStart?.Invoke();
    }
}
