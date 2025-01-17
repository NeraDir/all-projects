using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private LineComponent _lineComponent;

    [SerializeField]
    private MergesData _mergesList;

    [SerializeField]
    private float _lineBetweenDistance;

    private holeObjectComponentn _lastHoleObject;

    [SerializeField]
    private Transform _parentOfPlayer;

    [SerializeField]
    private TMP_Text _displayLevel;

    [SerializeField]
    private GameObject _lastLinePref;

    [SerializeField]
    private objectHooleComponent _objectHole;

    public static List<holeObjectComponentn> holesSpawnedList = new List<holeObjectComponentn>();

    [SerializeField]
    private GameObject _resultScreen;

    [SerializeField]
    private GameObject _looseScreen;

    public static int MaxReachLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("candieCaptainMaxLevelSaveKey"))
                return PlayerPrefs.GetInt("candieCaptainMaxLevelSaveKey");
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("candieCaptainMaxLevelSaveKey",value);
        }
    }

    private int _currentLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("candieCaptainCurrentLevelSaveKey"))
                return PlayerPrefs.GetInt("candieCaptainCurrentLevelSaveKey");
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("candieCaptainCurrentLevelSaveKey", value);
        }
    }

    private IEnumerator Start()
    {
        holesSpawnedList.Clear();
        _displayLevel.text = $"LEVEL " + _currentLevel.ToString();
        objectHooleComponent.onMerge.AddListener(OnSetMerger);
        objectHooleComponent.lastReached.AddListener(OnWin);
        objectHooleComponent.dead.AddListener(OnDead);
        yield return new WaitForSeconds(0.35f);
            SetLevel();
    }

    private void OnDestroy()
    {
        
        objectHooleComponent.onMerge.RemoveListener(OnSetMerger);
        objectHooleComponent.lastReached.RemoveListener(OnWin);
        objectHooleComponent.dead.RemoveListener(OnDead);
    }

    private void OnWin()
    {
        _resultScreen.SetActive(true);
    }

    private void OnDead()
    {
        _looseScreen.SetActive(true);
    }

    private void OnSetMerger(Mesh mesh)
    {
        Vector3 beginScale = _parentOfPlayer.localScale;
        List<MeshFilter> meshFilters = _parentOfPlayer.GetComponentsInChildren<MeshFilter>().ToList();
        meshFilters.Remove(meshFilters[meshFilters.Count - 1]);
        foreach (var item in meshFilters)
        {
            _parentOfPlayer.transform.DOScale(Vector3.zero, 0.15f).OnComplete(() =>
            {
                item.mesh = mesh;
                item.GetComponent<MeshCollider>().sharedMesh = mesh;
                if (item.GetComponent<objectHooleComponent>()!=null)
                {
                    item.GetComponent<objectHooleComponent>()._mesh = mesh;
                }
                _parentOfPlayer.transform.DOScale(Vector3.one, 0.15f);
            });
        }
    }

    private void SetLevel()
    {
        for (int i = 0; i < _currentLevel; i++)
        {
            if (_lastHoleObject == null)
            {
                LineComponent tempLine = Instantiate(_lineComponent, new Vector3(0, 0, 0), Quaternion.Euler(-90, 0, 0));
                tempLine.Init();
                Merger tempData = _mergesList.mergers[Random.Range(0, _mergesList.mergers.Count)];
                tempLine.GetMyHole().Init(tempData.mergeMesh, tempData.myMesh, tempData.holeMaker);
                _lastHoleObject = tempLine.GetMyHole();
                Merger tempTempData = _mergesList.mergers[_mergesList.mergers.IndexOf(tempData) - 1 < 0 ? _mergesList.mergers.Count - 1 : _mergesList.mergers.IndexOf(tempData) - 1];
                OnSetMerger(tempTempData.holeMaker);
                _objectHole.Init();
                _lastHoleObject.isLast = false;
                holesSpawnedList.Add(tempLine.GetMyHole());
            }
            else
            {
                LineComponent tempLine = Instantiate(_lineComponent, new Vector3(0, 0, _lastHoleObject.transform.position.z + _lineBetweenDistance), Quaternion.Euler(-90, 0, 0));
                tempLine.Init();
                Merger tempData = _mergesList.mergers.Find(x => x.myMesh == _lastHoleObject.GetMyMesh());
                tempLine.GetMyHole().Init(tempData.mergeMesh, tempData.myMesh, tempData.holeMaker);
                _lastHoleObject = tempLine.GetMyHole();
                _lastHoleObject.isLast = false;
                holesSpawnedList.Add(tempLine.GetMyHole());
            }
            if (i >= _currentLevel-1)
            {
                _lastHoleObject.isLast = true;
            }
        }
        
        Instantiate(_lastLinePref, new Vector3(0, 0, _lastHoleObject.transform.position.z + _lineBetweenDistance), Quaternion.Euler(-90, 0, 0));
    }

    private void OnApplicationQuit()
    {
        if (_currentLevel > MaxReachLevel)
        {
            MaxReachLevel = _currentLevel;
        }
        _currentLevel = 1;
    }

    public void OnClickRestart()
    {
        if (_currentLevel > MaxReachLevel)
        {
            MaxReachLevel = _currentLevel;
        }
        _currentLevel = 1;
        LoadScene(SceneManager.GetActiveScene().name);
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnClickNext()
    {
        _currentLevel += 1;
        if (_currentLevel > MaxReachLevel)
        {
            MaxReachLevel = _currentLevel;
        }
        LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickMenu()
    {
        if (_currentLevel > MaxReachLevel)
        {
            MaxReachLevel = _currentLevel;
        }
        _currentLevel = 1;
        LoadScene("Menu");
    }
}
