using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static float BestReachDistance
    {
        get
        {
            if (PlayerPrefs.HasKey("PiloOdesseyPlayerBestReachDistanceValue"))
            {
                return PlayerPrefs.GetFloat("PiloOdesseyPlayerBestReachDistanceValue");
            }
            return 0.0f;
        }
        set
        {
            PlayerPrefs.SetFloat("PiloOdesseyPlayerBestReachDistanceValue",value);
        }
    }
    public static int piloOddyseyTryCounts
    {
        get
        {
            if (PlayerPrefs.HasKey("piloOddyseyTryCountssaves"))
            {
                return PlayerPrefs.GetInt("piloOddyseyTryCountssaves");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("piloOddyseyTryCountssaves", value);
        }
    }

    public static string piloOdysseyInitializationKey;

    public static int piloOdysseyWinsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("piloOdysseyWinsCountSave"))
            {
                return PlayerPrefs.GetInt("piloOdysseyWinsCountSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("piloOdysseyWinsCountSave", value);
        }
    }

    public static int BestEarnStarsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("PiloOdesseyPlayerBestEarnStarsCountValue"))
            {
                return PlayerPrefs.GetInt("PiloOdesseyPlayerBestEarnStarsCountValue");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("PiloOdesseyPlayerBestEarnStarsCountValue", value);
        }
    }

    [SerializeField]
    private GameObject _partPrefab;

    [SerializeField]
    private Vector3 _partsSpawnPosition;

    [SerializeField]
    private float _distance;

    private List<GameObject> _partsSpawned = new List<GameObject>();

    private int _needReachCount;

    [SerializeField]
    private Material[] _materials;

    public static Material targetMaterial;

    [SerializeField]
    private PlayerController _playerController;

    [SerializeField]
    private Transform _targetStars;

    [SerializeField]
    private Image _starIamge;

    [SerializeField]
    private TMP_Text[] _currentDistanceDisplay;

    [SerializeField]
    private TMP_Text[] _starsCountDisplay;

    [SerializeField]
    private GameObject _resultpage;

    private int _starsCount;

    public static UnityEvent<Transform> PlayerGetStar = new();

    private float _currentDistance;

    [SerializeField] private Transform _playerTransform;
    private Vector3 _playerStartPosition;

    private void Awake()
    {
        _playerStartPosition = _playerTransform.position;
        targetMaterial = _materials[Random.Range(0, _materials.Length)];
        _needReachCount = 0;
        for (int i = 0; i < 6; i++)
        {
            SpawnLevel();
        }
        _playerController.enabled = true;
        PlayerController.PlayerReachPart.AddListener(OnReachPart);
        PlayerGetStar.AddListener(OnGetStar);
        PlayerController.PlayerIsDeath.AddListener(OnPlayerDeath);
        StartCoroutine(UpdateDistance());
    }

    private void OnGetStar(Transform starTrans)
    {
        Image temp = Instantiate(_starIamge, Camera.main.WorldToScreenPoint(starTrans.position), Quaternion.identity, _targetStars.parent);
        temp.transform.SetSiblingIndex(0);
        temp.transform.DOMove(_targetStars.position, 0.25f).OnComplete(() => temp.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => 
        {
            _starsCount++;

            Destroy(temp.gameObject); 
        }));
    }


    private void OnPlayerDeath()
    {
        _resultpage.SetActive(true);
    }

    private void OnDestroy()
    {
        PlayerController.PlayerReachPart.RemoveListener(OnReachPart);
        PlayerController.PlayerIsDeath.RemoveListener(OnPlayerDeath);
    }

    private IEnumerator UpdateDistance()
    {
        while (true)
        {
            _currentDistance = Vector3.Distance(_playerTransform.position, _playerStartPosition);
            foreach (var item in _currentDistanceDisplay)
            {
                item.text = _currentDistance.ToString("0.0") + "m";
            }
            foreach (var item in _starsCountDisplay)
            {
                item.text = "x" + _starsCount.ToString();
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnReachPart()
    {
        SpawnLevel();
        _needReachCount += 1;
        if (_needReachCount <= 1)
            return;
        _partsSpawned[0].GetComponent<PartComponent>().DestroyMe();
        _partsSpawned.Remove(_partsSpawned[0]);
    }

    private void SpawnLevel()
    {
        if (_partsSpawned.Count == 0)
        {
            _partsSpawned.Add(Instantiate(_partPrefab, _partsSpawnPosition, Quaternion.identity));
            _partsSpawned.Last().GetComponent<PartComponent>().Init();
        }
        else
        {
            _partsSpawned.Add(Instantiate(_partPrefab, new Vector3(0, 0, _partsSpawned[_partsSpawned.Count - 1].transform.position.z + _distance), Quaternion.identity));
            _partsSpawned.Last().GetComponent<PartComponent>().Init();
            _partsSpawned[_partsSpawned.Count - 1].GetComponent<PartComponent>().OnSetPlatformColors(targetMaterial);
        }
    }

    public void OnClickPlayAgain()
    {
        if (_starsCount > BestEarnStarsCount)
        {
            BestEarnStarsCount = _starsCount;
        }
        if (_currentDistance > BestReachDistance)
        {
            BestReachDistance = _currentDistance;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickLoadMenu()
    {
        if (_starsCount > BestEarnStarsCount)
        {
            BestEarnStarsCount = _starsCount;
        }
        if (_currentDistance > BestReachDistance)
        {
            BestReachDistance = _currentDistance;
        }
        SceneManager.LoadScene("Menu");
    }
}
