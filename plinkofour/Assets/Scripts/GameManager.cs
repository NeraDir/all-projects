using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<Material> _platformMaterials;

    [SerializeField]
    private PlatformComponent _platformPrefabs;

    [SerializeField]
    private PlatformComponent _endPlatform;

    [SerializeField]
    private PlatformComponent _startPlatform;

    [SerializeField]
    private float _platformsDistance;

    [SerializeField]
    private GameObject _endScreen;

    [SerializeField]
    private GameObject _nextButton;

    [SerializeField]
    private Material[] _skyBoxMaterials;

    [SerializeField]
    private TMP_Text _ballsCountTxt;

    [SerializeField]
    private TMP_Text _levelCountTxt;

    [SerializeField]
    private TMP_Text _scoreCountTxt;

    [SerializeField]
    private TMP_Text _levelStatusTxt;

    public static AudioSource soundEffectSource;

    private void Start()
    {
        Time.timeScale = 1;
        RenderSettings.skybox = _skyBoxMaterials[Random.Range(0,_skyBoxMaterials.Length)];
        BallComponent.endReached.AddListener(OnBallReachedFinish);
        BallComponent.dead.AddListener(OnDead);
        foreach (var item in FindObjectsOfType<AudioSource>())
        {
            if (item.loop != true)
            {
                soundEffectSource = item;
            }
        }
        GenerateLevel();
    }

    private void OnDestroy()
    {
        BallComponent.endReached.RemoveAllListeners();
    }

    private void OnDead()
    {
        Time.timeScale = 0;
        _endScreen.SetActive(true);
        _nextButton.SetActive(false);
        _levelStatusTxt.text = "LEVEL NOT COMPLETE";
    }

    private void OnBallReachedFinish()
    {
        Time.timeScale = 0;
        _endScreen.SetActive(true);
        _nextButton.SetActive(true);
        _levelStatusTxt.text = "LEVEL COMPLETE";
    }

    private void LateUpdate()
    {
        _ballsCountTxt.text = "x" + GameSavesManager.GameCurrentBallsCount.ToString();
        _levelCountTxt.text = "LVL " + GameSavesManager.GameCurrentLevelValue.ToString();
        _scoreCountTxt.text = GameSavesManager.GameCurrentScoreValue.ToString();
    }

    private void GenerateLevel()
    {
        int count = 0;
        List<Material> tempMaterials = new List<Material>();
        for (int i = 0; i < 2; i++)
        {
            int rndIndex = Random.Range(0, _platformMaterials.Count);
            tempMaterials.Add(_platformMaterials[rndIndex]);
            _platformMaterials.Remove(_platformMaterials[rndIndex]);
        }
        PlatformComponent lastPlatform = null;
        lastPlatform = Instantiate(_startPlatform, new Vector3(0, 0, 0), Quaternion.identity);
        lastPlatform._platformMaterials = tempMaterials;
        for (int i = 0; i < GameSavesManager.GameCurrentLevelValue; i++)
        {
            count += 2;
        }
        for (int i = 0; i < count; i++)
        {
            lastPlatform = Instantiate(_platformPrefabs,new Vector3(0,0,lastPlatform.transform.position.z + _platformsDistance),Quaternion.identity);
            lastPlatform._platformMaterials = tempMaterials;
        }
        lastPlatform = Instantiate(_endPlatform, new Vector3(0, 0, lastPlatform.transform.position.z + _platformsDistance), Quaternion.identity);
        lastPlatform._platformMaterials = tempMaterials;
    }

    private void OnApplicationQuit()
    {
        GameSavesManager.GameCurrentLevelValue = 1;
        GameSavesManager.GameHeartsCount = 3;
        GameSavesManager.GameCurrentScoreValue = 0;
        GameSavesManager.GameCurrentBallsCount = 0;
    }

    public void OnClickNext()
    {
        if (GameSavesManager.GameCurrentLevelValue > GameSavesManager.GameBestReachLevelValue)
        {
            GameSavesManager.GameBestReachLevelValue = GameSavesManager.GameCurrentLevelValue;
        }
        if (GameSavesManager.GameCurrentScoreValue > GameSavesManager.GameBestReachScoreValue)
        {
            GameSavesManager.GameBestReachScoreValue = GameSavesManager.GameCurrentScoreValue;
        }
        GameSavesManager.GameCurrentLevelValue += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickRestart()
    {
        if (GameSavesManager.GameCurrentLevelValue > GameSavesManager.GameBestReachLevelValue)
        {
            GameSavesManager.GameBestReachLevelValue = GameSavesManager.GameCurrentLevelValue;
        }
        if (GameSavesManager.GameCurrentScoreValue > GameSavesManager.GameBestReachScoreValue)
        {
            GameSavesManager.GameBestReachScoreValue = GameSavesManager.GameCurrentScoreValue;
        }
        GameSavesManager.GameCurrentLevelValue = 1;
        GameSavesManager.GameHeartsCount = 3;
        GameSavesManager.GameCurrentBallsCount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickMenu()
    {
        if (GameSavesManager.GameCurrentLevelValue > GameSavesManager.GameBestReachLevelValue)
        {
            GameSavesManager.GameBestReachLevelValue = GameSavesManager.GameCurrentLevelValue;
        }
        if (GameSavesManager.GameCurrentScoreValue > GameSavesManager.GameBestReachScoreValue)
        {
            GameSavesManager.GameBestReachScoreValue = GameSavesManager.GameCurrentScoreValue;
        }
        GameSavesManager.GameCurrentLevelValue = 1;
        GameSavesManager.GameHeartsCount = 3;
        GameSavesManager.GameCurrentBallsCount = 0;
        SceneManager.LoadScene("Menu");
    }

    public void OnClickBonus()
    {
        if (GameSavesManager.GameCurrentLevelValue > GameSavesManager.GameBestReachLevelValue)
        {
            GameSavesManager.GameBestReachLevelValue = GameSavesManager.GameCurrentLevelValue;
        }
        if (GameSavesManager.GameCurrentScoreValue > GameSavesManager.GameBestReachScoreValue)
        {
            GameSavesManager.GameBestReachScoreValue = GameSavesManager.GameCurrentScoreValue;
        }
        GameSavesManager.GameCurrentLevelValue += 1;
        SceneManager.LoadScene("BonusGame");
    }
}
