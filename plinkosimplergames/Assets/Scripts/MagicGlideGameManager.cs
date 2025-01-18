using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MagicGlideGameManager : MonoBehaviour
{
    public static int MagicGlideStarsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("MagicGlideStarsCountSaveKey"))
            {
                return PlayerPrefs.GetInt("MagicGlideStarsCountSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("MagicGlideStarsCountSaveKey", value);
        }
    }

    public static float MagicGlideLifeTimeValue
    {
        get
        {
            if (PlayerPrefs.HasKey("MagicGlideLifeTimeValueSaveKey"))
            {
                return PlayerPrefs.GetFloat("MagicGlideLifeTimeValueSaveKey");
            }
            return 0f;
        }
        set
        {
            PlayerPrefs.SetFloat("MagicGlideLifeTimeValueSaveKey", value);
        }
    }

    public static int MagicGlideTryCount
    {
        get
        {
            if (PlayerPrefs.HasKey("MagicGlideTryCountSaveKey"))
            {
                return PlayerPrefs.GetInt("MagicGlideTryCountSaveKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("MagicGlideTryCountSaveKey", value);
        }
    }

    public static string MagicGlideGameName;

    public static int MagicGlideSkinIndex
    {
        get
        {
            if (PlayerPrefs.HasKey("MagicGlideSkinIndexSaveKey"))
            {
                return PlayerPrefs.GetInt("MagicGlideSkinIndexSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("MagicGlideSkinIndexSaveKey", value);
        }
    }

    public static int MagicGlideWinsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("MagicGlideWinsCountSaveKey"))
            {
                return PlayerPrefs.GetInt("MagicGlideWinsCountSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("MagicGlideWinsCountSaveKey", value);
        }
    }

    [SerializeField] private GameObject[] _magicGladePaltformPrefabs;
    [SerializeField] private GameObject _magicGladeStartPlatform;
    [SerializeField] private GameObject _magicGladeResultScreen;
    [SerializeField] private Text[] _magicGladeLifeTimeText;
    [SerializeField] private Text[] _magicGladeStarText;

    public static int _magicGladeStars;
    private float _magicGladeLifeTime;
    public static UnityEvent MagicGladePlatformReached = new UnityEvent();
    private List<GameObject> _magicGladePlatformsList = new List<GameObject>();
    private int _magicGladePlatformsCount = 0 ;
    private GameObject _magicGladeLastPlatform;

    private bool _magicGladeIsEnd;

    private void Start()
    {
        _magicGladeIsEnd = false;
        _magicGladePlatformsList.Clear();
        _magicGladeStars = 0;
        _magicGladePlatformsCount = 0;
        MagicGladePlatformReached.AddListener(OnPaltformReach);
        _magicGladeLastPlatform = Instantiate(_magicGladeStartPlatform, new Vector3(0, 0, 0), Quaternion.identity);
        _magicGladePlatformsList.Add(_magicGladeLastPlatform);
        MagicGlideBallManager.MagicGlideBallDeath.AddListener(OnMagicBallDeath);
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                _magicGladeLastPlatform = Instantiate(_magicGladePaltformPrefabs[Random.Range(0, _magicGladePaltformPrefabs.Length)], new Vector3(0, 0, _magicGladeLastPlatform.transform.position.z + 13.41f), Quaternion.identity);
                _magicGladePlatformsList.Add(_magicGladeLastPlatform);
            }
        }
    }

    private void OnMagicBallDeath()
    {
        _magicGladeResultScreen.SetActive(true);
        _magicGladeIsEnd = true;
    }

    private void OnDestroy()
    {
        MagicGladePlatformReached.RemoveAllListeners();
        MagicGlideBallManager.MagicGlideBallDeath.RemoveAllListeners();
    }

    private void OnPaltformReach()
    {
        _magicGladePlatformsCount += 1;
        if (_magicGladePlatformsCount > 2)
        {
            Destroy(_magicGladePlatformsList[0]);
            _magicGladePlatformsList.Remove(_magicGladePlatformsList[0]);
        }
        for (int i = 0; i < 2; i++)
        {
            _magicGladeLastPlatform = Instantiate(_magicGladePaltformPrefabs[Random.Range(0, _magicGladePaltformPrefabs.Length)], new Vector3(0, 0, _magicGladeLastPlatform.transform.position.z + 13.41f), Quaternion.identity);
            _magicGladePlatformsList.Add(_magicGladeLastPlatform);
        }
    }

    private void LateUpdate()
    {
        if (_magicGladeIsEnd)
            return;

        _magicGladeLifeTime += Time.deltaTime;
        if (_magicGladeLifeTime > MagicGlideLifeTimeValue)
        {
            MagicGlideLifeTimeValue = _magicGladeLifeTime;
        }
        foreach (var item in _magicGladeLifeTimeText)
        {
            item.text = _magicGladeLifeTime.ToString("0.0") + "s";
        }
        foreach (var item in _magicGladeStarText)
        {
            item.text = "x" + _magicGladeStars.ToString("0");
        }
    }

    public void OnClickAgain()
    {
        MagicGlideStarsCount += _magicGladeStars;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickMenu()
    {
        MagicGlideStarsCount += _magicGladeStars;
        SceneManager.LoadScene("MagicGlideMenuLoaderScene");
    }
}
