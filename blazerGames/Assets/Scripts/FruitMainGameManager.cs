using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FruitMainGameManager : MonoBehaviour
{
    public static int blazerFruitsTryCount
    {
        get
        {
            if (PlayerPrefs.HasKey("blazerFruitsTryCountKey"))
            {
                return PlayerPrefs.GetInt("blazerFruitsTryCountKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("blazerFruitsTryCountKey", value);
        }
    }

    public static string blazerFruitsName;

    public static int blazerFruitsWinsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("blazerFruitsWinsCountKey"))
            {
                return PlayerPrefs.GetInt("blazerFruitsWinsCountKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("blazerFruitsWinsCountKey", value);
        }
    }

    [SerializeField]
    private Text _blazerFruitsLevelShow;

    [SerializeField]
    private Transform _blazerFruitsKnifesSpawnPosition;

    [SerializeField]
    private GameObject _blazerFruitsKnifeObject;

    public static int BlazerFruitsLevel {
        get
        {
            if (PlayerPrefs.HasKey("BlazerFruitsLevelKey"))
            {
                return PlayerPrefs.GetInt("BlazerFruitsLevelKey");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("BlazerFruitsLevelKey", value);
        }
    }

    public int KnifeCount;

    public static int knifesCountToLevel;

    [SerializeField]
    private Color _knifeActiveColor;

    [SerializeField]
    private Color _knifeInactiveColor;

    [SerializeField]
    private GameObject[] _fruitLevelsPrefabs;

    [SerializeField]
    private GameObject _restartButton;

    [SerializeField]
    private GameObject _nextButton;

    [SerializeField]
    private Image _knifeImage;

    [SerializeField]
    private Transform _knifeTransform;

    [SerializeField]
    private GameObject _Winpage;

    [SerializeField]
    private GameObject _loosePage;

    [SerializeField]
    private Transform _spawnPosition;

    public static List<Image> _knifesImagesCountList = new List<Image>();

    public static List<FruitComponent> _fruitsComponents = new List<FruitComponent>();
    private bool _initialized = false;

    private IEnumerator Start()
    {
        _initialized = false ;
        _fruitsComponents.Clear();
        _knifesImagesCountList.Clear();
        if (BlazerFruitsLevel >= 25)
        {
            _restartButton.SetActive(false);
            _nextButton.SetActive(false);
        }
        Instantiate(_fruitLevelsPrefabs[BlazerFruitsLevel - 1], _spawnPosition);
        _blazerFruitsLevelShow.text = "LVL " + BlazerFruitsLevel.ToString();
        foreach (var item in FindObjectsOfType<FruitComponent>())
        {
            if (item.CanTrigger)
            {
                _fruitsComponents.Add(item);
                KnifeCount = item.knifesCount;
            }
        }
        knifesCountToLevel = KnifeCount;
        if (_knifesImagesCountList.Count == 0)
        {
            for (int i = 0; i < knifesCountToLevel; i++)
            {
                _knifesImagesCountList.Add(Instantiate(_knifeImage, _knifeTransform));
            }
        }

        FruitKnifeComponent.knifeRespawn.AddListener(OnRespawnKnife);
        OnRespawnKnife();
        
        yield return new WaitForSeconds(1);
        _initialized = true;
    }

    private void Update()
    {
        if (!_initialized)
            return;
        if (_fruitsComponents.Count <= 0)
        {
                _Winpage.SetActive(true);
                return;
           
        }
        else
        {
            if (knifesCountToLevel == 0 && FindObjectOfType<FruitKnifeComponent>() == null)
            {
                _loosePage.SetActive(true);
                return;
            }
        }

        for (int i = 0; i < _knifesImagesCountList.Count; i++)
        {
            if (i >= knifesCountToLevel)
            {
                _knifesImagesCountList[i].color = _knifeInactiveColor;
            }
            else
            {
                _knifesImagesCountList[i].color = _knifeActiveColor;
            }
        }
    }

    private void OnDestroy()
    {
        FruitKnifeComponent.knifeRespawn.RemoveListener(OnRespawnKnife);
    }

    private void OnApplicationQuit()
    {
        BlazerFruitsLevel = 1;
    }

    private void OnRespawnKnife()
    {
        Instantiate(_blazerFruitsKnifeObject, _blazerFruitsKnifesSpawnPosition);
    }

    public void OnClickNext()
    {
        BlazerFruitsLevel += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickMenu()
    {
        BlazerFruitsLevel = 1;
        SceneManager.LoadScene("Menu");
    }
}
