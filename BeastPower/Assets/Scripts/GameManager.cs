using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int PantherSkinIndex
    {
        get
        {
            if (PlayerPrefs.HasKey("BeastPowerSkinIndexSaveKey"))
                return PlayerPrefs.GetInt("BeastPowerSkinIndexSaveKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("BeastPowerSkinIndexSaveKey", value);
        }
    }

    public static int BeastGameStartedCount
    {
        get
        {
            if (PlayerPrefs.HasKey("BBeastGameStartedCountSaveKey"))
                return PlayerPrefs.GetInt("BBeastGameStartedCountSaveKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("BBeastGameStartedCountSaveKey", value);
        }
    }

    public static string BeastGameKey;

    public static int BeastPowerValue
    {
        get
        {
            if (PlayerPrefs.HasKey("BeastPowerValueSaveKey"))
                return PlayerPrefs.GetInt("BeastPowerValueSaveKey");
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("BeastPowerValueSaveKey", value);
        }
    }

    public static int Coins
    {
        get
        {
            if (PlayerPrefs.HasKey("BeastPowerCoinsCountSaveKey"))
                return PlayerPrefs.GetInt("BeastPowerCoinsCountSaveKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("BeastPowerCoinsCountSaveKey", value);
        }
    }

    [SerializeField]
    private SkinnedMeshRenderer _pantherMeshRenderer;

    [SerializeField]
    private Mesh[] _skinsMeshes;

    [SerializeField]
    private Material[] _skinMaterials;

    [SerializeField]
    private Text[] _currentDistanceShow;

    [SerializeField]
    private Text _coinsShow;

    [SerializeField]
    private Transform _pantherTransform;

    [SerializeField]
    private float _roadsDistance;

    [SerializeField]
    private GameObject[] _roadPrefabs;

    [SerializeField]
    private GameObject _resultScreen;

    private Vector3 _pantherStartPosition;

    private float _currentDistance;

    public static UnityEvent<GameObject> RoadSpawn = new UnityEvent<GameObject>();

    public static UnityEvent PantherIsLoose = new UnityEvent();

    private List<GameObject> _roadsList = new List<GameObject>();

    private void Start()
    {
        Time.timeScale = 1;
        _pantherMeshRenderer.material = _skinMaterials[PantherSkinIndex];
        _pantherMeshRenderer.sharedMesh = _skinsMeshes[PantherSkinIndex];
        _currentDistance = 0;
        _pantherStartPosition = _pantherTransform.position;
        RoadSpawn.AddListener(SpawnRoad);
        PantherIsLoose.AddListener(GameEnd);
        for (int i = 0; i < 6; i++)
        {
            if (i == 0)
            {
                _roadsList.Add(Instantiate(_roadPrefabs[Random.Range(0, _roadPrefabs.Length)], Vector3.zero, Quaternion.identity));
            }
            else
            {
                _roadsList.Add(Instantiate(_roadPrefabs[Random.Range(0, _roadPrefabs.Length)], new Vector3(_roadsList[_roadsList.Count - 1].transform.position.x, _roadsList[_roadsList.Count - 1].transform.position.y, _roadsList[_roadsList.Count - 1].transform.position.z + _roadsDistance), Quaternion.identity));
            }
        }
    }

    private void OnDestroy()
    {
        RoadSpawn.RemoveAllListeners();
        PantherIsLoose.RemoveAllListeners();    
    }

    private void GameEnd() 
    {
        Time.timeScale = 0;
        _resultScreen.SetActive(true);
    }

    public void OnClickRestart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    private void SpawnRoad(GameObject destroyRoad) 
    {
        Destroy(destroyRoad,2);
        _roadsList.Remove(_roadsList[0]);
        _roadsList.Add(Instantiate(_roadPrefabs[Random.Range(0, _roadPrefabs.Length)], new Vector3(_roadsList[_roadsList.Count - 1].transform.position.x, _roadsList[_roadsList.Count - 1].transform.position.y, _roadsList[_roadsList.Count - 1].transform.position.z + _roadsDistance), Quaternion.identity));
    }

    private void LateUpdate()
    {
        _currentDistance = Vector3.Distance(_pantherStartPosition, _pantherTransform.position);
        foreach (var item in _currentDistanceShow)
        {
            item.text = _currentDistance.ToString("0.0") + "m";
        }
        if (_currentDistance > MenuManager.BestReachedDistance)
        {
            MenuManager.BestReachedDistance = _currentDistance;
        }
        _coinsShow.text = "x" + Coins.ToString();
    }
}
