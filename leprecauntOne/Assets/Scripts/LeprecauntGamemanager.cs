using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeprecauntGamemanager : MonoBehaviour
{
    public static int _currentLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("LeprecountCurrentLevelvalueSaveKey"))
                return PlayerPrefs.GetInt("LeprecountCurrentLevelvalueSaveKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("LeprecountCurrentLevelvalueSaveKey", value);
        }
    }

    public static int MaxReachLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("LeprecountMaxLevelvalueSaveKey"))
                return PlayerPrefs.GetInt("LeprecountMaxLevelvalueSaveKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("LeprecountMaxLevelvalueSaveKey", value);
        }
    }

    [SerializeField]
    private TMP_Text _levelShow;

    [SerializeField]
    private TMP_Text _timerTxt;

    [SerializeField]
    private GameObject _levelPreShow;

    [SerializeField]
    private Animator _leveAnimation;

    [SerializeField]
    private Image[] _levelStars;

    [SerializeField]
    private Image _timer;

    [SerializeField]
    private GameObject[] _levelPanels;

    private float _timerMaxValue = 30;
    private float _timerValue = 0;
    private int _starsCount = 0;
    private bool _gameIsRun;

    [SerializeField]
    private GameObject[] _goodOrBad;

    public static UnityEvent<bool> onLevelEnd = new UnityEvent<bool>();

    [SerializeField]
    private GameObject _cardsGameImage;

    [Space(10)]
    [Header("Level Data Set")]
    [SerializeField]
    private TMP_Text _showQuetion;

    [SerializeField]
    private Button[] _answerButtons;

    private LevelData _levelData;

    [SerializeField]
    private List<Sprite> cardsSprites = new List<Sprite>();

    [SerializeField]
    private GameObject[] _cardsPack;

    [SerializeField]
    private TMP_Text _cardTimerTxt;

    [SerializeField]
    private Image _cardTimerImage;

    [SerializeField]
    private GameObject _quizGameScreen;

    private float _cardGameTimer = 20;

    public static bool _cardGameRunned;

    public List<LeprechauntCardComponent> cardsInPool = new List<LeprechauntCardComponent>();

    public static UnityEvent<LeprechauntCardComponent, LeprechauntCardComponent> checkCards = new UnityEvent<LeprechauntCardComponent, LeprechauntCardComponent>();

    private bool _isClicked;
    private void Awake()
    {
        _cardGameRunned = false;
        _gameIsRun = false;
        _levelShow.text = "LEVEL " + (_currentLevel + 1).ToString();
        _levelPreShow.SetActive(true);
        for (int i = 0; i < _currentLevel; i++)
        {
            _timerMaxValue -= 1.5f;
            _cardGameTimer -= 1f;
        }
        if (_cardGameTimer <= 7.5f)
        {
            _cardGameTimer = 7.5f;
        }
        if(_timerMaxValue <= 4.5f)
        {
            _timerMaxValue = 4.5f;
        }
        _cardsPack[_currentLevel >= _cardsPack.Length - 1 ? _cardsPack.Length - 1: _currentLevel].SetActive(true);
        onLevelEnd.AddListener(OnLevelEnd);
        LeprechauntCardComponent.selectedsCardsPool.Clear();
        checkCards.AddListener(CheckCards);
    }

    private void CheckCards(LeprechauntCardComponent card1, LeprechauntCardComponent card2)
    {
        StartCoroutine(Checking(card1, card2));
    }

    private IEnumerator Checking(LeprechauntCardComponent card1, LeprechauntCardComponent card2)
    {
        yield return new WaitForSeconds(0.5f);
        if (card1.myCardSprite.name == card2.myCardSprite.name)
        {
            cardsInPool.Remove(card1);
            cardsInPool.Remove(card2);
            LeprechauntCardComponent.selectedsCardsPool.Clear();
            if (cardsInPool.Count <= 0)
            {
                StartCoroutine(End(_levelPanels[1]));
            }
            LeprechauntCardComponent.canClick = false;
        }
        else
        {
            card1.OnDefault();
            card2.OnDefault();
            LeprechauntCardComponent.canClick = false;
        }
    }
    private IEnumerator End(GameObject page)
    {
        yield return new WaitForSeconds(0.5f);
        page.SetActive(true);
    }

    private void FillActiveCards()
    {
        StartCoroutine(FillingCards());
    }

    private IEnumerator FillingCards()
    {
        List<LeprechauntCardComponent> tempCards = new List<LeprechauntCardComponent>();
        foreach (var item in _cardsPack[_currentLevel >= _cardsPack.Length - 1 ? _cardsPack.Length - 1 : _currentLevel].GetComponentsInChildren<LeprechauntCardComponent>())
        {
            tempCards.Add(item);
        }
        int countSetted = 0;
        int selectedSprite = Random.Range(0, cardsSprites.Count);
        while (tempCards.Count > 0)
        {
            if (countSetted < 2)
            {
                LeprechauntCardComponent card = tempCards[Random.Range(0, tempCards.Count)];
                if (card != null)
                {
                    if (card.myCardSprite == null)
                    {
                        card.Init(cardsSprites[selectedSprite]);
                        tempCards.Remove(card);
                        cardsInPool.Add(card);
                        countSetted++;
                    }
                }
            }
            else
            {
                cardsSprites.Remove(cardsSprites[selectedSprite]);
                selectedSprite = Random.Range(0, cardsSprites.Count);
                countSetted = 0;
            }
            yield return null;
        }
        StartCoroutine(GameStarting(cardsInPool));
    }

    private IEnumerator GameStarting(List<LeprechauntCardComponent> cards)
    {
        foreach (var item in cards)
        {
            item.Open();
        }
        yield return new WaitForSeconds(1);

        foreach (var item in cards)
        {
            item.OnDefault();
        }
    }

    private void SetLevelDatas()
    {
        _levelData = LevelDatasLoader.LevelDatas[_currentLevel];
        _showQuetion.text = _levelData.Quetion;
        for (int i = 0; i < _answerButtons.Length;i++)
        {
            if (_levelData.Answers[i].Contains("-"))
            {
                _levelData.Answers[i] = _levelData.Answers[i].Replace("-", "");
                _answerButtons[i].GetComponentInChildren<TMP_Text>().text = _levelData.Answers[i];
                _answerButtons[i].onClick.AddListener(() => OnClickAnswerButton(true));
            }
            else
            {
                _answerButtons[i].GetComponentInChildren<TMP_Text>().text = _levelData.Answers[i];
                _answerButtons[i].onClick.AddListener(() => OnClickAnswerButton(false));
            }
        }
        
    }

    private void OnClickAnswerButton(bool value)
    {
        if (_isClicked)
            return;
        _isClicked = true;
        OnLevelEnd(value);
    }

    private IEnumerator Start()
    {
        _timerValue = _timerMaxValue;
        SetLevelDatas();
        yield return new WaitForSeconds(1f);
        _levelPreShow.SetActive(false);
        _leveAnimation.enabled = true;
        yield return new WaitForSeconds(0.5f);
        _gameIsRun = true;
    }

    private void OnLevelEnd(bool value) 
    {
        _gameIsRun = false;
        if (_timerValue > _timerMaxValue * 0.8f)
        {
            _starsCount = 3;
        }
        else if(_timerValue > _timerMaxValue * 0.6f)
        {
            _starsCount = 2;
        }
        else if(_timerValue > _timerMaxValue * 0.2f)
        {
            _starsCount = 1;
        }
        else
        {
            _starsCount = 0;
        }

        for (int i = 0; i < _starsCount; i++)
        {
            _levelStars[i].color = Color.white;
        }
        if (!value)
        {
            _goodOrBad[0].SetActive(true);
        }
        else
        {
            _goodOrBad[1].SetActive(true);
        }
        StartCoroutine(Ending(value));
    }

    private IEnumerator Ending(bool value)
    {
        yield return new WaitForSeconds(1f);
        if (!value)
        {
            _levelPanels[0].SetActive(true);
        }
        else
        {
            if (_starsCount > PlayerPrefs.GetInt($"{_currentLevel}LevelStarsCountLeprecaount"))
            {
                PlayerPrefs.SetInt($"{_currentLevel}LevelStarsCountLeprecaount", _starsCount);
            }
            _cardsGameImage.SetActive(true);
            _quizGameScreen.SetActive(false);
        }
        yield return new WaitForSeconds(1);
        _cardGameRunned = true;
        FillActiveCards();
    }

    private void LateUpdate()
    {
        if(_cardGameRunned)
        {
            _cardGameTimer -= Time.deltaTime;
            _cardTimerTxt.text = _cardGameTimer.ToString("0.0") + "s";
            UpdateTimer(_cardTimerImage, _cardGameTimer, 20);
            if (_cardGameTimer <= 0)
            {
                onLevelEnd?.Invoke(false);
            }
        }
        if (!_gameIsRun)
            return;
        _timerValue -= Time.deltaTime;
        _timerTxt.text = _timerValue.ToString("0.0") + "s";
        if (_timerValue <= 0)
        {
            onLevelEnd?.Invoke(false);
        }
        UpdateTimer(_timer,_timerValue,_timerMaxValue);
    }

    private void UpdateTimer(Image filler,float value1,float value2)
    {
        if (filler != null)
            filler.fillAmount = Mathf.Lerp(filler.fillAmount, value1 / value2, 8);
    }

    public void OnClickNext()
    {
        if (_currentLevel > MaxReachLevel)
        {
            MaxReachLevel = _currentLevel;
        }
        _currentLevel += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickRestart()
    {
        if (_currentLevel > MaxReachLevel)
        {
            MaxReachLevel = _currentLevel;
        }
        _currentLevel = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickMenu()
    {
        if (_currentLevel > MaxReachLevel)
        {
            MaxReachLevel = _currentLevel;
        }
        _currentLevel = 0;
        Scene nextScene = SceneManager.CreateScene("LeprechaunsLunacyMenuScene");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(nextScene);
        GameObject menuCanvas = Resources.Load("Prefabs/Menu") as GameObject;
        Instantiate(menuCanvas);
        SceneManager.UnloadScene(currentScene);
    }
}
