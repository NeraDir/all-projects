using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BeastEnergyGameManager : MonoBehaviour
{
    public static float beastEnergyRecordLiveTime {
        get
        {
            if(PlayerPrefs.HasKey("beastEnergyRecordLiveTimeSave"))
            {
                return PlayerPrefs.GetFloat("beastEnergyRecordLiveTimeSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetFloat("beastEnergyRecordLiveTimeSave", value);
        }
    }

    public static int beastEnergyCanvasMarginValue
    {
        get
        {
            if (PlayerPrefs.HasKey("beastEnergyCanvasMarginValueSave"))
            {
                return PlayerPrefs.GetInt("beastEnergyCanvasMarginValueSave");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("beastEnergyCanvasMarginValueSave", value);
        }
    }

    public static int beastEnergyCoinsCount 
    {
        get
        {
            if (PlayerPrefs.HasKey("beastEnergyCoinsCountSave"))
            {
                return PlayerPrefs.GetInt("beastEnergyCoinsCountSave");
            }
            return 10000;
        }
        set
        {
            PlayerPrefs.SetInt("beastEnergyCoinsCountSave", value);
        }
    }

    public static string beastEnergyGameSetting;

    public static float beastEnergyCurrentLifeTime;

    public static int beastEnergyCurrentCoins;

    public static bool beastEnergyRunLaunched;

    public static UnityEvent beastEnergyRoadTriggererd = new UnityEvent();

    public static int beastEnergyRoadZPositionValue
    {
        get
        {
            if (PlayerPrefs.HasKey("beastEnergyRoadZPositionValueSave"))
            {
                return PlayerPrefs.GetInt("beastEnergyRoadZPositionValueSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("beastEnergyRoadZPositionValueSave", value);
        }
    }

    public static int beastCurrentSkinIndex
    {
        get
        {
            if (PlayerPrefs.HasKey("beastCurrentSkinIndexSave"))
            {
                return PlayerPrefs.GetInt("beastCurrentSkinIndexSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("beastCurrentSkinIndexSave", value);
        }
    }

    [SerializeField] private TMP_Text[] _currentLifeTimeDisplayer;

    [SerializeField] private TMP_Text[] _currentCoinsCountDisplayer;

    [SerializeField] private GameObject[] _beastEnergySkins;

    [SerializeField] private GameObject[] _beastEnergyRoadPrefabs;

    [SerializeField] private GameObject _beastEnergyResultScreen;

    private GameObject _beastEnergyLastRoad;

    private float _beastEnergyLifeTime;

    private void Start()
    {
        Time.timeScale = 1;
        _beastEnergyLifeTime = 0;
           beastEnergyRunLaunched = false;
        beastEnergyCurrentLifeTime = 0;
        beastEnergyCurrentCoins = 0;
        _beastEnergySkins[beastCurrentSkinIndex].gameObject.SetActive(true);
        beastEnergyRoadTriggererd.AddListener(SpawnRoad);
        BeastEnergyPlayerControllerManager.beastEnergyDeath.AddListener(Result);
        SpawnRoad();
    }

    private void Result() 
    {
        _beastEnergyResultScreen.SetActive(true);
        Time.timeScale = 0;
    }

    private void LateUpdate()
    {
        if (_beastEnergyLifeTime > beastEnergyRecordLiveTime)
        {
            beastEnergyRecordLiveTime = _beastEnergyLifeTime;
        }
        _beastEnergyLifeTime += Time.deltaTime;
        foreach (var item in _currentCoinsCountDisplayer)
        {
            item.text = beastEnergyCoinsCount.ToString("0") + "C";
        }
        foreach (var item in _currentLifeTimeDisplayer)
        {
            item.text = _beastEnergyLifeTime.ToString("0") + "s";
        }
    }

    private void OnDestroy()
    {
        BeastEnergyPlayerControllerManager.beastEnergyDeath.RemoveAllListeners();
        beastEnergyRoadTriggererd.RemoveAllListeners();
    }

    private void SpawnRoad() 
    {
        if (beastEnergyRunLaunched)
        {
            _beastEnergyLastRoad = Instantiate(_beastEnergyRoadPrefabs[Random.Range(0, _beastEnergyRoadPrefabs.Length)], new Vector3(_beastEnergyLastRoad.gameObject.transform.position.x, _beastEnergyLastRoad.gameObject.transform.position.y, _beastEnergyLastRoad.gameObject.transform.position.z + 120), Quaternion.identity);
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                if (i == 0)
                {
                    _beastEnergyLastRoad = Instantiate(_beastEnergyRoadPrefabs[0], new Vector3(0, 0, 0), Quaternion.identity);
                }
                else
                {
                    _beastEnergyLastRoad = Instantiate(_beastEnergyRoadPrefabs[Random.Range(0, _beastEnergyRoadPrefabs.Length)], new Vector3(_beastEnergyLastRoad.gameObject.transform.position.x, _beastEnergyLastRoad.gameObject.transform.position.y, _beastEnergyLastRoad.gameObject.transform.position.z + 120), Quaternion.identity);
                }
            }
        }
    }

    public void ClickStarGame() 
    {
        beastEnergyRunLaunched = true;
    }

    public void ClickRestartBeastGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ClickLoadMenu() 
    {
        SceneManager.LoadScene("BeasrEnergyMenu");
    }
}
