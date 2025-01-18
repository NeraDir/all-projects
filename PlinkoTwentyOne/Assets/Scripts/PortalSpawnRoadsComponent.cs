using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalSpawnRoadsComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject _roadPrefab;

    [SerializeField]
    private float _distance;

    [SerializeField]
    private TMP_Text[] _showScore;

    [SerializeField]
    private TMP_Text[] _showLevel;

    [SerializeField]
    private TMP_Text _resultTxt;

    [SerializeField]
    private GameObject _resultScreen;

    [SerializeField]
    private GameObject _resultNextButton;

    public static int currentScore;

    public static float currentSpeed;

    public static int MaxLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("PortalJumpsMaxLevelKey"))
                return PlayerPrefs.GetInt("PortalJumpsMaxLevelKey");
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("PortalJumpsMaxLevelKey", value);
        }
    }

    private int currentLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("PortalJumpsCurrentLevelKey"))
                return PlayerPrefs.GetInt("PortalJumpsCurrentLevelKey");
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("PortalJumpsCurrentLevelKey", value);
        }
    }

    private int _countPerLevel = 3;

    private GameObject _lastRoad;

    private void Start()
    {
        currentSpeed = 7.5f;
        for (int i = 0; i < currentLevel; i++)
        {
            currentSpeed += 2.5f;
        }
        currentScore = 0;
        for (int i = 0; i < _countPerLevel + (currentLevel * 2); i++)
        {
            SpawnNewPart(false);
        }
        SpawnNewPart(true);
        PortalJumpBallComponent.levelCompleted.AddListener(OnEnd);
    }

    private void OnDestroy()
    {
        PortalJumpBallComponent.levelCompleted.RemoveListener(OnEnd);
    }

    private void LateUpdate()
    {
        foreach (var item in _showScore)
        {
            item.text = "x" + currentScore.ToString();
        }
        foreach (var item in _showLevel)
        {
            item.text = currentLevel.ToString();
        }
    }

    private void OnEnd(bool end)
    {
        _resultScreen.SetActive(true);
        _resultNextButton.SetActive(end);
        _resultTxt.text = end == true ? "LEVEL PASSED" : "LEVEL NOT PASSED";
    }

    private void OnApplicationQuit()
    {
        currentLevel = 1;
    }

    public void OnClickNext()
    {
        currentLevel++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickRestart()
    {
        currentLevel = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickMenu()
    {
        currentLevel = 1;
        Scene nextScene = SceneManager.CreateScene("PortalJumpsMenuScene");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(nextScene);
        GameObject menuCanvas = Resources.Load("Prefabs/Menu") as GameObject;
        Instantiate(menuCanvas);
        SceneManager.UnloadScene(currentScene);
    }

    private void SpawnNewPart(bool hasLast)
    {
        if (!hasLast)
        {
            if (_lastRoad != null)
            {
                _lastRoad = Instantiate(_roadPrefab, new Vector3(0, 0, _lastRoad.transform.position.z + 23.58f), Quaternion.identity);
            }
            else
            {
                _lastRoad = Instantiate(_roadPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            }
        }
        else
        {
            _lastRoad = Instantiate(_roadPrefab, new Vector3(0, 0, _lastRoad.transform.position.z + 23.58f), Quaternion.identity);
            _lastRoad.GetComponent<PortalChangeRoad>().isLast = true;
        }
    }
}
