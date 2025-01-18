using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public static int maxStarsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("plimkoPolygonsMaxStarsCountSave"))
                return PlayerPrefs.GetInt("plimkoPolygonsMaxStarsCountSave");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("plimkoPolygonsMaxStarsCountSave", value);
        }
    }

    public static int gameViewCanvasMarginValue
    {
        get
        {
            if (PlayerPrefs.HasKey("gameViewCanvasMarginValueDataSave"))
            {
                return PlayerPrefs.GetInt("gameViewCanvasMarginValueDataSave");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("gameViewCanvasMarginValueDataSave", value);
        }
    }

    public static string gameSettingsKey;

    public static int gameViewToolBarActiveState
    {
        get
        {
            if (PlayerPrefs.HasKey("gameViewToolBarActiveStateDataSave"))
            {
                return PlayerPrefs.GetInt("gameViewToolBarActiveStateDataSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("gameViewToolBarActiveStateDataSave", value);
        }
    }

    public static float maxReachedDistance
    {
        get
        {
            if (PlayerPrefs.HasKey("plimkopolygonsmaxreacherdDistanceDataSave"))
            {
                return PlayerPrefs.GetFloat("plimkopolygonsmaxreacherdDistanceDataSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetFloat("plimkopolygonsmaxreacherdDistanceDataSave", value);
        }
    }

    public static int ballIndex
    {
        get
        {
            if (PlayerPrefs.HasKey("ballSelectedIndexDataSave"))
            {
                return PlayerPrefs.GetInt("ballSelectedIndexDataSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("ballSelectedIndexDataSave", value);
        }
    }

    [SerializeField]
    private GameObject[] _roadTypePrefabs;

    public static UnityEvent roadSpawn = new UnityEvent();

    private GameObject _lastRoad;

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private Vector3 _offset;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private GameObject _result;

    [SerializeField]
    private Text[] _starsTxt;

    [SerializeField]
    private Text[] _distanceTxt;

    private Vector3 _starPos;

    [SerializeField]
    private Transform _ballTrans;

    [SerializeField]
    private MeshRenderer _ballRenderer;

    [SerializeField]
    private Material[] _ballMaterials;

    public static int starsCount;

    private void Start()
    {
        Time.timeScale = 1;
        _ballRenderer.material = _ballMaterials[ballIndex];
        starsCount = 0;
        _starPos = _ballTrans.position;
        ballComponent.ballIsDeath.AddListener(OnDeath);
        roadSpawn.AddListener(Spawn);
        for (int i = 0; i < 10; i++)
        {
            Spawn();
        }
        ballComponent.ballIsDeath.AddListener(OnDeath);
    }

    private void OnDestroy()
    {
        roadSpawn.RemoveListener(Spawn);
    }

    private void OnDeath()
    {
        Time.timeScale = 0;
        _result.SetActive(true);
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position + _offset, _speed * Time.deltaTime);
        float currentDistance = Vector3.Distance(_starPos, _ballTrans.position);
        if (currentDistance > maxReachedDistance)
        {
            maxReachedDistance = currentDistance;
        }
        foreach (var item in _starsTxt)
        {
            item.text = "x" + starsCount.ToString();
        }
        foreach(var item in _distanceTxt)
        {
            item.text = currentDistance.ToString("0.0") + "m";
        }
    }

    public void OnRestart()
    {
        maxStarsCount += starsCount;
        if (Random.Range(0,2) != 0)
        {
            SceneManager.LoadScene("bonusScene");
        }
        else
        {
            SceneManager.LoadScene("gameScene");
        }
    }

    public void OnMenu()
    {
        maxStarsCount += starsCount;
        if (Random.Range(0, 2) != 0)
        {
            SceneManager.LoadScene("bonusScene");
        }
        else
        {
            SceneManager.LoadScene("menuScene");
        }
    }

    private void Spawn()
    {
        if (_lastRoad == null)
            _lastRoad = Instantiate(_roadTypePrefabs[2], new Vector3(0,0,0), Quaternion.Euler(-90, 0, -90));
        else
            _lastRoad = Instantiate(_roadTypePrefabs[Random.Range(0, _roadTypePrefabs.Length)],Random.Range(0,2) != 0 ? _lastRoad.GetComponentInChildren<spawnPlaceOfRoad>().transform.position : new Vector3(_lastRoad.GetComponentInChildren<spawnPlaceOfRoad>().transform.position.x + 4, _lastRoad.GetComponentInChildren<spawnPlaceOfRoad>().transform.position.y, _lastRoad.GetComponentInChildren<spawnPlaceOfRoad>().transform.position.z), Quaternion.Euler(-90, 0, -90));
    }

}
