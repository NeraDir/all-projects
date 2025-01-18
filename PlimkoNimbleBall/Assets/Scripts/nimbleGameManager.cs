using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class nimbleGameManager : MonoBehaviour
{
    public static int nimbleGameLaunchNeedBallsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("nimbleGameLaunchNeedBallsCountSaveKey"))
            {
                return PlayerPrefs.GetInt("nimbleGameLaunchNeedBallsCountSaveKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("nimbleGameLaunchNeedBallsCountSaveKey", value);
        }
    }

    public static string nimbleGameSettingsDataStringKey;

    public static int nimbleGameToolsActive
    {
        get
        {
            if (PlayerPrefs.HasKey("nimbleGameToolsActiveSaveKey"))
            {
                return PlayerPrefs.GetInt("nimbleGameToolsActiveSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("nimbleGameToolsActiveSaveKey", value);
        }
    }

    public static int nimbleCurentLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("nimbleCurentLevelSaveKey"))
            {
                return PlayerPrefs.GetInt("nimbleCurentLevelSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("nimbleCurentLevelSaveKey", value);
        }
    }

    public static UnityEvent<nimbleBallGet> nimbleAddNewBallToCase = new UnityEvent<nimbleBallGet>();

    public static UnityEvent nimblePlayerFinished = new UnityEvent();

    public static UnityEvent nimblePlayerDeath = new UnityEvent();

    [SerializeField]
    private GameObject[] _nimbleRoadPrefabs;

    [SerializeField]
    private GameObject _nimbleFinishRoad;

    [SerializeField]
    private GameObject _nimbleClearRoad;

    [SerializeField]
    private Transform _nimblePlayerCase;

    [SerializeField]
    private nimbleCamComponent _nimbleCamComponent;

    [SerializeField]
    private Vector3 _nimbleCamFinishOffset;

    [SerializeField]
    private float _nimbleRoadMargins;

    [SerializeField]
    private Transform _nimblePlayerTransform;

    [SerializeField]
    private Text _nimbleScoreTxt;

    [SerializeField]
    private Text[] _nimbleCurrentLevelTxt;

    [SerializeField]
    private Slider _nimbleLevelDistanceSlider;

    [SerializeField]
    private GameObject _nimbleresultScreen;

    [SerializeField]
    private Text _nimbleResultTxt;

    [SerializeField]
    private GameObject _nimbleNextButton;

    private Vector3 _finishPos;

    private Vector3 _startPos;

    private GameObject _nimbleLastRoad;

    public List<nimbleBallGet> _currentNimbleBallsInCase = new List<nimbleBallGet>();

    private bool isFinished;

    public static int needScore;

    public static int currentScore;

    private void Awake()
    {
        currentScore = 0;
        needScore = Random.Range(40, 100) * (nimbleCurentLevel + 1);
        _startPos = _nimblePlayerTransform.position;
        nimbleAddNewBallToCase.AddListener(AddNewBall);
        nimblePlayerDeath.AddListener(OnDeath);
        nimblePlayerFinished.AddListener(OnFinish);
        SetLevel();
    }

    private void OnDestroy()
    {
        nimbleAddNewBallToCase.RemoveListener(AddNewBall);
        nimblePlayerFinished.RemoveListener(OnFinish);
        nimblePlayerDeath.RemoveListener(OnDeath);
    }

    private void OnDeath()
    {
        _nimbleNextButton.SetActive(false);
        _nimbleResultTxt.text = "LOOSE!";
        _nimbleresultScreen.SetActive(true);
    }

    private void OnFinish()
    {
        if (isFinished)
            return;
        isFinished = true;

        _nimbleCamComponent.isFinish = true;
        _nimbleCamComponent._offest = _nimbleCamFinishOffset;
        StartCoroutine(LaunchFinishGame());
    }

    private IEnumerator LaunchFinishGame()
    {
        yield return new WaitForSeconds(1);
        while (_currentNimbleBallsInCase.Count > 0)
        {
            for (int i = _currentNimbleBallsInCase.Count - 1 < 0 ? 0: _currentNimbleBallsInCase.Count - 1; i > -1; i--)
            {
                _currentNimbleBallsInCase[i].transform.parent = null;
                _currentNimbleBallsInCase[i].AddComponent<nimbleBallTest>();

                _currentNimbleBallsInCase[i].transform.DOMove(new Vector3(_nimblePlayerTransform.position.x, _nimblePlayerTransform.position.y, _nimblePlayerTransform.position.z + 6), 0.25f);
                _currentNimbleBallsInCase[i].AddComponent<Rigidbody>();
                _currentNimbleBallsInCase[i].GetComponent<Collider>().isTrigger = false;
                Destroy(_currentNimbleBallsInCase[i].GetComponent<nimbleBallGet>());
                _currentNimbleBallsInCase.Remove(_currentNimbleBallsInCase[i]);
                yield return new WaitForSeconds(1);
            }
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(7);
        if (currentScore >= needScore)
        {
            _nimbleNextButton.SetActive(true);
            _nimbleResultTxt.text = "VICTORY!";
        }
        else
        {
            _nimbleNextButton.SetActive(false);
            _nimbleResultTxt.text = "LOOSE!";
        }
        _nimbleresultScreen.SetActive(true);
    }

    private void LateUpdate()
    {
        _nimbleLevelDistanceSlider.value = Vector3.Distance(_startPos,_nimblePlayerTransform.position);
        _nimbleScoreTxt.text = currentScore.ToString() + "/" + needScore.ToString();
        foreach (var item in _nimbleCurrentLevelTxt)
        {
            item.text = "LVL " + nimbleCurentLevel.ToString();

        }
        if (nimbleCurentLevel > nimbleGameMenu.nimbleMaxLevel)
        {
            nimbleGameMenu.nimbleMaxLevel = nimbleCurentLevel;
        }
    }

    private void SetLevel()
    {
        for (int i = 0; i < nimbleCurentLevel + 4; i++)
        {
            if (_nimbleLastRoad != null)
            {
                _nimbleLastRoad = Instantiate(_nimbleRoadPrefabs[Random.Range(0, _nimbleRoadPrefabs.Length)], new Vector3(_nimbleLastRoad.transform.position.x, _nimbleLastRoad.transform.position.y, _nimbleLastRoad.transform.position.z + _nimbleRoadMargins), _nimbleRoadPrefabs[0].transform.rotation);
            }
            else
            {
                _nimbleLastRoad = Instantiate(_nimbleClearRoad, new Vector3(0, -36f, 43.3f), _nimbleRoadPrefabs[0].transform.rotation);
            }
        }
        _nimbleLastRoad = Instantiate(_nimbleFinishRoad, new Vector3(_nimbleLastRoad.transform.position.x, _nimbleLastRoad.transform.position.y, _nimbleLastRoad.transform.position.z + _nimbleRoadMargins), _nimbleRoadPrefabs[0].transform.rotation);
        _finishPos = _nimbleLastRoad.transform.position;
        _nimbleLevelDistanceSlider.maxValue = Vector3.Distance(_startPos, _finishPos);
        _nimbleLevelDistanceSlider.value = 0;
    }

    public void OnClickNext()
    {
        nimbleCurentLevel++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickRestart()
    {
        nimbleCurentLevel = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickMenu()
    {
        nimbleCurentLevel = 0;
        SceneManager.LoadScene("nimbleGameMenuScene");
    }

    private void OnApplicationQuit()
    {
        nimbleCurentLevel = 0;
    }

    private void AddNewBall(nimbleBallGet ball)
    {
        if (_currentNimbleBallsInCase.Count < 1)
            ball._target = _nimblePlayerCase;
        else
            ball._target = _currentNimbleBallsInCase[_currentNimbleBallsInCase.Count - 1].transform;
        ball.transform.parent = _nimblePlayerCase.transform; 
        _currentNimbleBallsInCase.Add(ball);
    }
}
