using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum TypeOfConstruction
{
    wings,
    main,
    turrets,
}

[Serializable]
public class SpawnObjectsData
{
    public GameObject spawnPrefab;
    public float spawnRate;
}

public class AviGameComponent : MonoBehaviour
{
    public static int aviGameStarsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("aviConstructionGameStarsCountKey"))
            {
                return PlayerPrefs.GetInt("aviConstructionGameStarsCountKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("aviConstructionGameStarsCountKey", value);
        }
    }

    public static float aviGameBestReachedDistance
    {
        get
        {
            if (PlayerPrefs.HasKey("aviGameBestReachedDistanceKey"))
            {
                return PlayerPrefs.GetFloat("aviGameBestReachedDistanceKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetFloat("aviGameBestReachedDistanceKey", value);
        }
    }

    public static int aviGameLaunchedCount
    {
        get
        {
            if (PlayerPrefs.HasKey("aviGameLaunchedCountKey"))
            {
                return PlayerPrefs.GetInt("aviGameLaunchedCountKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("aviGameLaunchedCountKey", value);
        }
    }

    public static string aviConstructGameSettingKey;

    public static int aviGameUIMarginValue
    {
        get
        {
            if (PlayerPrefs.HasKey("aviGameUIMarginValueKey"))
            {
                return PlayerPrefs.GetInt("aviGameUIMarginValueKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("aviGameUIMarginValueKey", value);
        }
    }

    private float _aviCurrentReachedDistance;

    [SerializeField]
    private GameObject _aviGameResultPage;

    [SerializeField]
    private GameObject _aviMagnetObject;

    [SerializeField]
    private Image _aviConstructionPref;

    [SerializeField]
    private Transform[] _aviObjectsSpawnPositions;

    [SerializeField]
    private SpawnObjectsData[] _aviObjectsPrefabs;

    [SerializeField]
    private Image[] _aviConstructions;

    [SerializeField]
    private Transform[] _aviTransforms;

    [SerializeField]
    private TMP_Text[] _aviStarsDisplay;

    [SerializeField]
    private TMP_Text[] _aviDistanceDisplay;

    [SerializeField]
    private string[] _aviBuyKeys;

    [SerializeField]
    private GameObject _aviLaunchButton;

    [SerializeField]
    private GameObject _aviSelectpanel;

    [SerializeField]
    private Transform _aviPlaneTransform;

    [SerializeField]
    private GameObject _aviBulletPrefab;

    [SerializeField]
    private Transform[] _aviBulletsSpawnPositions;

    [SerializeField]
    private TypeOfConstruction[] _typeOfConstructions;

    [SerializeField]
    private AviaShopLine[] _aviaShopLines;

    [SerializeField]
    private Image _aviPlaneHealthBar;

    [SerializeField]
    private Image _aviPlaneFuelBar;

    public static Sprite currentAviWingsSprite;
    public static Sprite currentAviTurretsSprite;
    public static Sprite currentAviMainSprite;
    public static UnityEvent magnetEffectActivate = new UnityEvent();

    public static float currentFuelValue;
    public static float aviMaxFuelValue;
    public static float aviPlanePlayerCurrentHealth;
    public static int currentAviDamage;
    public static int currentAviStars;

    private Rigidbody2D _rigidBody2d;
    
    private float _aviPlaneMaxHealth;
    private float _aviMagnetEffectDuration;

    private bool isMagnetActive;
    private bool isLaunched;

    public static bool AviGameIsPlay;

    private void Start()
    {
        AviGameIsPlay = false;
        _aviMagnetEffectDuration = 0;
        _rigidBody2d = _aviPlaneTransform.GetComponent<Rigidbody2D>();
        aviMaxFuelValue = 0;
        currentFuelValue = 0;
        aviPlanePlayerCurrentHealth = 0;
        _aviPlaneMaxHealth = 0;
        currentAviDamage = 0;
        currentAviWingsSprite = null;
        currentAviTurretsSprite = null;
        currentAviMainSprite = null;
        isLaunched = false;
        currentAviStars = 0;
        for (int i = 0; i < _aviaShopLines.Length; i++)
        {
            for (int j = 0; j < _aviaShopLines[i].aviaShopDatas.Length; j++)
            {
                if (PlayerPrefs.GetInt(_aviBuyKeys[i] + j) != 0)
                {
                    Image tempBuyComponent = Instantiate(_aviConstructionPref, _aviTransforms[i]);
                    tempBuyComponent.sprite = _aviaShopLines[i].aviaShopDatas[j].aviUseSprite;
                    tempBuyComponent.GetComponent<AviGameConstructionSelectItemComponent>().constructionType = _typeOfConstructions[i];
                    tempBuyComponent.GetComponent<AviGameConstructionSelectItemComponent>().aviUseSprite = _aviaShopLines[i].aviaShopDatas[j].aviUseSprite;
                    tempBuyComponent.GetComponent<AviGameConstructionSelectItemComponent>().aviUseSprite2 = _aviaShopLines[i].aviaShopDatas[j].aviShopItem;
                }
            }
        }
        magnetEffectActivate.AddListener(OnActivemagnet);
    }

    private void OnActivemagnet()
    {
        if (isMagnetActive)
            return;
        isMagnetActive = true;  
    }

    private IEnumerator SpawnObjects()
    {
        yield return new WaitForSeconds(1.6f);
        while (true)
        {
            for (int i = 0; i < 5; i++)
            {
               GameObject tempObject =  Instantiate(_aviObjectsPrefabs[GetRandomObject()].spawnPrefab, new Vector3(Random.Range(_aviObjectsSpawnPositions[0].position.x, _aviObjectsSpawnPositions[1].position.x), _aviObjectsSpawnPositions[0].position.y, 0), Quaternion.identity, _aviObjectsSpawnPositions[0].parent);
                tempObject.transform.SetSiblingIndex(0);
            }
            yield return new WaitForSeconds(1);
        }
    }

    private int GetRandomObject()
    {
        float total = 0;
        float random = Random.Range(0f, 1f);
        float numberToAdding = 0;
        for (int i = 0; i < _aviObjectsPrefabs.Length; i++)
        {
            total += _aviObjectsPrefabs[i].spawnRate;
        }
        for (int i = 0; i < _aviObjectsPrefabs.Length; i++)
        {
            if (_aviObjectsPrefabs[i].spawnRate / total + numberToAdding >= random)
            {
                return i;
            }
            else
            {
                numberToAdding += _aviObjectsPrefabs[i].spawnRate / total;
            }
        }
        return 0;
    }

    private void LateUpdate()
    {


        if (_aviCurrentReachedDistance > aviGameBestReachedDistance)
        {
            aviGameBestReachedDistance = _aviCurrentReachedDistance;
        }
        foreach (var item in _aviDistanceDisplay)
        {
            item.text = _aviCurrentReachedDistance.ToString("0.0") + "s";
        }
        foreach (var item in _aviStarsDisplay)
        {
            item.text = "x" + currentAviStars.ToString();
        }
       
        if (isLaunched)
        {
            if (!AviGameIsPlay)
            {
                return;
            }
            if (isMagnetActive)
            {
                _aviMagnetEffectDuration += Time.deltaTime;
                if (_aviMagnetEffectDuration >= 5)
                {
                    isMagnetActive = false;
                    _aviMagnetEffectDuration = 0;
                }
                _aviMagnetObject.SetActive(true);
            }
            else
            {
                _aviMagnetObject.SetActive(false);
            }
            _aviCurrentReachedDistance += Time.deltaTime;
            _aviPlaneHealthBar.transform.parent.gameObject.SetActive(true);
            _aviPlaneFuelBar.transform.parent.gameObject.SetActive(true);
            currentFuelValue -= Time.deltaTime;
            if (currentFuelValue > aviMaxFuelValue)
            {
                currentFuelValue = _aviPlaneMaxHealth;
            }
            _aviPlaneHealthBar.fillAmount = Mathf.Lerp(_aviPlaneHealthBar.fillAmount, aviPlanePlayerCurrentHealth / _aviPlaneMaxHealth, 10 * Time.deltaTime);
            _aviPlaneFuelBar.fillAmount = Mathf.Lerp(_aviPlaneFuelBar.fillAmount, currentFuelValue / aviMaxFuelValue, 10 * Time.deltaTime);
            Vector3 direction = Input.mousePosition - _aviPlaneTransform.position;
            _aviPlaneTransform.position += new Vector3(direction.x, 0, 0) * 100 * Time.deltaTime;
            _aviLaunchButton.SetActive(false);
            if (aviPlanePlayerCurrentHealth <= 0)
            {
                _aviGameResultPage.SetActive(true);
                AviGameIsPlay = false;
            }
            if (currentFuelValue <= 0)
            {
                _aviGameResultPage.SetActive(true);
                AviGameIsPlay = false;
            }
            return;
        }
        _aviPlaneHealthBar.transform.parent.gameObject.SetActive(false);
        _aviPlaneFuelBar.transform.parent.gameObject.SetActive(false);
        if (currentAviWingsSprite != null && currentAviMainSprite != null)
        {
            _aviLaunchButton.SetActive(true);
        }
        else
        {
            _aviLaunchButton.SetActive(false);
        }

        if (currentAviWingsSprite != null)
        {
            _aviConstructions[0].transform.DOScale(Vector3.one, 0.25f);
            _aviConstructions[0].sprite = currentAviWingsSprite;
        }
        if (currentAviMainSprite != null)
        {
            _aviConstructions[1].transform.DOScale(Vector3.one, 0.25f);
            _aviConstructions[1].sprite = currentAviMainSprite;
        }
        if (currentAviTurretsSprite != null)
        {
            _aviConstructions[2].transform.DOScale(Vector3.one, 0.25f);
            _aviConstructions[2].sprite = currentAviTurretsSprite;
        }
    }

    public void OnClickedLaunch()
    {
        if (isLaunched)
            return;
        isLaunched = true;
        for (int i = 0; i < _aviaShopLines.Length; i++)
        {
            for (int j = 0; j < _aviaShopLines[i].aviaShopDatas.Length; j++)
            {
                if (_aviaShopLines[i].aviaShopDatas[j].aviShopItem == currentAviWingsSprite)
                {
                    _aviPlaneMaxHealth += ((j + 1) * 10);
                    aviMaxFuelValue += ((j + 1) * 10);
                }
                if (_aviaShopLines[i].aviaShopDatas[j].aviShopItem == currentAviMainSprite)
                {
                    _aviPlaneMaxHealth += ((j + 1) * 10);
                    aviMaxFuelValue += ((j + 1) * 10);
                }
                if (_aviaShopLines[i].aviaShopDatas[j].aviShopItem == currentAviTurretsSprite)
                {
                    currentAviDamage = ((j + 1) * 5);
                }
            }
        }
        if (currentAviTurretsSprite != null)
        {
            StartCoroutine(Shoot());
        }
        currentFuelValue = aviMaxFuelValue;
        aviPlanePlayerCurrentHealth = _aviPlaneMaxHealth;
        _aviSelectpanel.SetActive(false);
        StartCoroutine(SpawnObjects());
        AviGameIsPlay = true;
        _aviPlaneTransform.DOMoveY(_aviPlaneTransform.position.y - 200, 0.25f);
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            foreach (var item in _aviBulletsSpawnPositions)
            {
                GameObject tempBullet = Instantiate(_aviBulletPrefab, item.position, Quaternion.identity, item.parent.parent);
                tempBullet.transform.SetSiblingIndex(0);  
            }
            yield return new WaitForSeconds(2);
        }
    }

    public void OnClickedRestart()
    {
        aviGameStarsCount += currentAviStars;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickedMenu()
    {
        aviGameStarsCount += currentAviStars;
        SceneManager.LoadScene("GameMenuConstructScene");
    }
}
