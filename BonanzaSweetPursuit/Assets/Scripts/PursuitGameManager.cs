using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PursuitGameManager : MonoBehaviour
{
    [SerializeField]
    private Image _pursuitTimeFillingBar;

    [SerializeField]
    private Image _pursuitGameResultPage;

    [SerializeField]
    private Button _pursuitNextButton;

    [SerializeField]
    private Text _pursuitTXTCurrentLevel;

    [SerializeField]
    private Text _pursuitCurrentLevelTimeTXT;

    [SerializeField]
    private Text _pursuitGameResultTxt;

    public static bool isFirstPursuitCandyCellChoosed;

    public static string PursuitGameControllerSettingKey;

    public static UnityEvent<bool> gameIsEnd = new UnityEvent<bool> ();

    public static List<Sprite> pursuitNeeedSpriteCombinationList = new List<Sprite>();

    public static List<PursuitCandyCellController> pursuitCurrentSpritesCombinationList = new List<PursuitCandyCellController>();

    private int _pursuitCurrentLevel 
    {
        get 
        {
            if (PlayerPrefs.HasKey("PursuitGameCurrentLevelSave"))
                return PlayerPrefs.GetInt("PursuitGameCurrentLevelSave");
            return 1;
        }
        set 
        {
            PlayerPrefs.SetInt("PursuitGameCurrentLevelSave", value);
        }
    }

    private bool _pursuitLoose;

    private float _pursuitTime = 17.5f;

    [Header("Board Fill Settings")]
    [Space(30)]
    [SerializeField]
    private PursuitBoardFillingType[] _fillTypes;

    [SerializeField]
    private Image[] _candyCellsIamges;

    [SerializeField]
    private Sprite[] _candyPursuitSprites;

    [SerializeField]
    private Image[] _pursuitNeedBoardCombinationDisplay;

    public static List<Sprite> candysList = new List<Sprite>();

    private void Awake()
    {
        pursuitNeeedSpriteCombinationList.Clear();
        pursuitCurrentSpritesCombinationList.Clear();
        _pursuitLoose = false;
        isFirstPursuitCandyCellChoosed = false;
        _pursuitTime = 17.5f;
        candysList.Clear();
        candysList = _candyPursuitSprites.ToList();
        gameIsEnd.AddListener(OnGameResults);
    }

    private void Start()
    {
        FillBoard();
    }

    private void OnDestroy()
    {
        gameIsEnd.RemoveAllListeners();
    }

    private void LateUpdate()
    {
        _pursuitTime -= Time.deltaTime;
        if (_pursuitTime <= 0)
        {
            OnGameResults(true);
            _pursuitTime = 0;
        }
        if (_pursuitCurrentLevel > PursuitMenuController.GetMaxLevel())
        {
            PursuitMenuController.SetMaxLevel(_pursuitCurrentLevel);
        }
        UpdatePursuitFillingTimeBar();
        _pursuitTXTCurrentLevel.text = _pursuitCurrentLevel.ToString();
        _pursuitCurrentLevelTimeTXT.text = _pursuitTime.ToString("0.0") + "s";
    }

    private void UpdatePursuitFillingTimeBar() 
    {
        _pursuitTimeFillingBar.fillAmount = Mathf.Lerp(_pursuitTimeFillingBar.fillAmount, _pursuitTime / 17.5f, 10 * Time.deltaTime);
    }

    private void OnApplicationQuit()
    {
        _pursuitCurrentLevel = 1;
    }

    public void OnClickOpenMenu() 
    {
        _pursuitCurrentLevel = 1;
        SceneManager.LoadScene("MenuScene");
    }

    public void OnClickRestartGame() 
    {
        _pursuitCurrentLevel = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnGameResults(bool isEnter) 
    {
        if (isEnter)
        {
            _pursuitGameResultTxt.text = "YOU LOOSE";
            _pursuitNextButton.gameObject.SetActive(false);
            _pursuitGameResultPage.gameObject.SetActive(true);
        }
        else
        {
            _pursuitGameResultTxt.text = "YOU WIN";
            _pursuitNextButton.gameObject.SetActive(true);
            _pursuitGameResultPage.gameObject.SetActive(true);
        }
    }

    public static int SetTime(DateTime dataTime)
    {
        DateTime DataTime = new DateTime(2024, 4, 23);
        TimeSpan subTime = dataTime.Subtract(DataTime);

        return (int)subTime.TotalSeconds;
    }

    public void OnClickNextPlayGame() 
    {
        _pursuitCurrentLevel += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    private void FillBoard()
    {
        int currentIndex = 0;
        int rndSpawn = UnityEngine.Random.Range(0, _fillTypes.Length);
        _fillTypes[rndSpawn].FillList();
        for (int i = 0; i < _candyCellsIamges.Length; i++)
        {
            if (_fillTypes[rndSpawn].pursuitFillingTypes[i] == "$")
            {
                _candyCellsIamges[i].sprite = _fillTypes[rndSpawn].pursuitNeedCombinationArray[currentIndex];
                _pursuitNeedBoardCombinationDisplay[currentIndex].sprite = _fillTypes[rndSpawn].pursuitNeedCombinationArray[currentIndex];
                pursuitNeeedSpriteCombinationList.Add(_fillTypes[rndSpawn].pursuitNeedCombinationArray[currentIndex]);
                _candyCellsIamges[i].gameObject.GetComponent<PursuitCandyCellController>().INIT();
                currentIndex++;
            }
            else
            {
                _candyCellsIamges[i].sprite = _candyPursuitSprites[UnityEngine.Random.Range(0, _candyPursuitSprites.Length)];
                _candyCellsIamges[i].gameObject.GetComponent<PursuitCandyCellController>().INIT();
            }
        }
        currentIndex = 0;
    }

    public static int SetTime()
    {
        return SetTime(DateTime.UtcNow);
    }
}

[Serializable]
public class PursuitBoardFillingType
{
    public List<string> pursuitFillingTypes = new List<string>();

    public List<Sprite> pursuitNeedCombinationArray = new List<Sprite>();

    public string pursuitCodec;

    [SerializeField]
    private int _countOfCandysInCombination;

    public void FillList()
    {
        pursuitCodec = pursuitCodec.Replace(" ", "");
        string[] list = pursuitCodec.Split(',');
        pursuitFillingTypes = list.ToList();
        pursuitNeedCombinationArray.Clear();
        for (int i = 0; i < _countOfCandysInCombination; i++)
        {
            int rndSprite = UnityEngine.Random.Range(0, PursuitGameManager.candysList.Count);
            pursuitNeedCombinationArray.Add(PursuitGameManager.candysList[rndSprite]);
            PursuitGameManager.candysList.Remove(PursuitGameManager.candysList[rndSprite]);
        }
    }
}

[System.Serializable]
public class DataPattern
{
    public bool ok;
    public string url;
    public long expires;
    public string message;
}
