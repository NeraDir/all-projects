using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IceCreamGameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _roadPrefabs;

    [SerializeField]
    private GameObject _startRoadPrefab;

    [SerializeField]
    private GameObject _resultGameScreen;

    [SerializeField]
    private GameObject _resultNextButton;

    [SerializeField]
    private Text _resultText;

    [SerializeField]
    private Text _levelPassedText;

    [SerializeField]
    private Text _starsText;

    [SerializeField]
    private Transform _needCandiesSpawnPos;

    [SerializeField]
    private IceCreamContainer _needCandiesPref;

    [SerializeField]
    private GameObject _lastRoadPref;

    [SerializeField]
    private List<Sprite> _needCandiesSprites;

    public static List<IceCreamContainer> _currentContainers = new List<IceCreamContainer>();

    public static int iceCreamMaxReachedLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("iceCreamMaxReachedLevelDataSave"))
            {
                return PlayerPrefs.GetInt("iceCreamMaxReachedLevelDataSave");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("iceCreamMaxReachedLevelDataSave", value);
        }
    }

    public static int iceRusherGameObjectsTopMarginValue
    {
        get
        {
            if (PlayerPrefs.HasKey("iceRusherGameObjectsTopMarginValueDataSave"))
            {
                return PlayerPrefs.GetInt("iceRusherGameObjectsTopMarginValueDataSave");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("iceRusherGameObjectsTopMarginValueDataSave", value);
        }
    }

    public static string iceRushingGameKey;

    public static int iceRusherFirstRoadsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("iceRusherFirstRoadsCountDataSave"))
            {
                return PlayerPrefs.GetInt("iceRusherFirstRoadsCountDataSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("iceRusherFirstRoadsCountDataSave", value);
        }
    }

    public static int iceCreamCurrentReachedLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("iceCreamCurrentReachedLevelDataSave"))
            {
                return PlayerPrefs.GetInt("iceCreamCurrentReachedLevelDataSave");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("iceCreamCurrentReachedLevelDataSave", value);
        }
    }

    public static int iceCreamStarsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("iceCreamStarsCountDataSave"))
            {
                return PlayerPrefs.GetInt("iceCreamStarsCountDataSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("iceCreamStarsCountDataSave", value);
        }
    }

    private GameObject _lastRoad;

    private int _needCandiesCount;

    public static UnityEvent balltriggeredRoad = new UnityEvent();

    public static UnityEvent ballIsDeath = new UnityEvent();

    private bool _iceEnde;

    private bool _iceFirst;

    private bool _islastSpawned;

    private void Start()
    {
        _currentContainers.Clear();
        Time.timeScale = 1;
        _needCandiesCount = Random.Range(3, 6);
        IceCreamRoadTrigger.iceRusherLevelEnd.AddListener(OnRusherLevelpassed);
        for (int i = 0; i < _needCandiesCount; i++)
        {
            IceCreamContainer tempCntainer = Instantiate(_needCandiesPref, _needCandiesSpawnPos);
            tempCntainer.Init();
            tempCntainer.SetData(_needCandiesSprites[i], Random.Range(1, 3), i);
            _currentContainers.Add(tempCntainer);
        }
        balltriggeredRoad.AddListener(SpawnRoad);
        ballIsDeath.AddListener(OnBallIsDeath);
        SpawnRoad();
    }

    private void OnDestroy()
    {
        balltriggeredRoad.RemoveListener(SpawnRoad);
        ballIsDeath.RemoveListener(OnBallIsDeath);
    }

    private void OnRusherLevelpassed()
    {
        _resultText.text = "VICTORY!";
        StartCoroutine(Endinbg());
    }

    private IEnumerator Endinbg() 
    {
        yield return new WaitForSeconds(4);
        Time.timeScale = 0;
        _resultGameScreen.SetActive(true);
        _resultNextButton.SetActive(true);
    }

    private void OnBallIsDeath()
    {
        _resultText.text = "LOOSE!";
        _resultGameScreen.SetActive(true);
        _resultNextButton.SetActive(false);
        Time.timeScale = 0;
    }

    private void SpawnRoad()
    {
        if (_iceEnde)
        {
            SpawnLast();
            return;
        }

        if (!_iceFirst)
        {
            for (int i = 0; i < 2; i++)
            {
                if (_lastRoad == null)
                {
                    _lastRoad = Instantiate(_startRoadPrefab, new Vector3(-23.62f, 0, 0), Quaternion.identity);
                }
                else
                {
                    _lastRoad = Instantiate(_roadPrefabs[Random.Range(0, _roadPrefabs.Length)], new Vector3(_lastRoad.transform.position.x + Random.Range(9.67f, 13.5f), 0, 0), Quaternion.identity);
                }
            }
            _iceFirst = true;
        }
        else
        {
            if (_lastRoad == null)
            {
                _lastRoad = Instantiate(_startRoadPrefab, new Vector3(-23.62f, 0, 0), Quaternion.identity);
            }
            else
            {
                _lastRoad = Instantiate(_roadPrefabs[Random.Range(0, _roadPrefabs.Length)], new Vector3(_lastRoad.transform.position.x + Random.Range(9.67f, 13.5f), 0, 0), Quaternion.identity);
            }
        }
        
        foreach (var item in _currentContainers)
        {
            if (!item.iceCreamReady)
            {
                return;
            }
        }
        _iceEnde = true;
    }

    private void LateUpdate()
    {
        if (iceCreamCurrentReachedLevel > iceCreamMaxReachedLevel)
        {
            iceCreamMaxReachedLevel = iceCreamCurrentReachedLevel;
        }
        _starsText.text = "x" + iceCreamStarsCount.ToString();
        _levelPassedText.text = iceCreamCurrentReachedLevel.ToString();
    }

    private void SpawnLast()
    {
        if (_islastSpawned)
            return;
        _islastSpawned = true;
        Instantiate(_lastRoadPref, new Vector3(_lastRoad.transform.position.x + Random.Range(9.67f, 13.5f), 0, 0), Quaternion.identity);
    }

    private void OnApplicationQuit()
    {
        iceCreamCurrentReachedLevel = 1;
    }

    public void OnIceRusherNext()
    {
        iceCreamCurrentReachedLevel++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnIceRushRestartGame()
    {
        iceCreamCurrentReachedLevel = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnIceRushMenu()
    {
        iceCreamCurrentReachedLevel = 1;
        SceneManager.LoadScene("RusherMenu");
    }
}
