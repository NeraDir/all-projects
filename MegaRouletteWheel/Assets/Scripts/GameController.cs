using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public static List<RouletteInfinityJellyComponent> currentBlocks = new List<RouletteInfinityJellyComponent>();
    public static int Level;
    public static List<Transform> StandPositions = new List<Transform>();
    public static int DestroyedCount;
    public static Action ShowBadResult;

    public static int MaxReachedLevel
    {
        get => PlayerPrefs.GetInt("MaxReachedLevelSaveKey", 0);
        set => PlayerPrefs.SetInt("MaxReachedLevelSaveKey", value);
    }

    public static Action checkingEnd;

    [SerializeField] private Slider _levelProgressBar;
    [SerializeField] private RouletteInfinityJellyComponent _blockPrefab;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private LevelDatas _levelData;
    [SerializeField] private GameObject _nextBtn;
    [SerializeField] private GameObject _restartBtn;

    [SerializeField] private GameObject _resultsPage;
    [SerializeField] private TMP_Text[] _levelsTxt;
    [SerializeField] private TMP_Text _resultsTxt;


    [SerializeField] private RouletteinfitityShooterBox _shooterBoxPrefab;
    [SerializeField] private Transform[] _shooterSpawnPoints;

    [SerializeField] private Transform[] _standPositions;

    private int MaxSpawnedBlocks;

    private void Awake()
    {
        DestroyedCount = 0;
         MaxSpawnedBlocks = 0;
        StandPositions = _standPositions.ToList();
        float yOffset = 0f;
        for (int i = 0; i < _levelData.levels[Level].lineDatas.Length; i++)
        {
            SpawnLine(_levelData.levels[Level].lineDatas[i], yOffset);
            yOffset += 1.5f * _levelData.levels[Level].lineDatas[i].lineCount;
        }
        MaxSpawnedBlocks = currentBlocks.Count;
        SpawnShooters(_levelData.levels[Level].shootCount);

        
        foreach (var item in _levelsTxt)
        {
            item.text = "LEVEL " + (Level + 1).ToString();
        }
        checkingEnd += OnChecking;
        ShowBadResult += OnShowBadresults;
    }

    private void OnDestroy()
    {
        checkingEnd -= OnChecking;
        ShowBadResult -= OnShowBadresults;
    }

    private void OnChecking()
    {
        if (DestroyedCount >= MaxSpawnedBlocks)
        {
            Invoke(nameof(ShowResults),1);
        }
    }

    private void LateUpdate()
    {
        _levelProgressBar.value = Mathf.Lerp(_levelProgressBar.value, ((float)DestroyedCount / MaxSpawnedBlocks), 10 * Time.deltaTime);
    }

    private void OnShowBadresults()
    {
        _resultsPage.SetActive(true);
        _resultsTxt.text = "NOT COMPLETED";
        _nextBtn.SetActive(false);
        _restartBtn.SetActive(true);
    }

    private void ShowResults()
    {
        _resultsPage.SetActive(true);
        _resultsTxt.text = "COMPLETED";
        _nextBtn.SetActive(true);
        _restartBtn.SetActive(false);
    }

    private void SpawnLine(LineData lineData, float yOffset)
    {
        float xOffset = 0;

        for (int i = 0; i < lineData.lineCount; i++)
        {
            foreach (var item in lineData.type)
            {
                for (int j = 0; j < (int)(10 / lineData.type.Length); j++)
                {
                    RouletteInfinityJellyComponent newBlock = Instantiate(_blockPrefab,
                        _spawnPosition.position + new Vector3(xOffset, 0, yOffset),
                        Quaternion.Euler(-90, 0, 0));

                    newBlock.jellyType = item;
                    newBlock.Init();
                    currentBlocks.Add(newBlock);

                    xOffset += 1.05f;
                }
            }
            xOffset = 0;
            yOffset += 1.5f;
        }
    }

    private void SpawnShooters(int totalShoots)
    {
        int totalBlocks = currentBlocks.Count;
        int requiredShooters = Mathf.CeilToInt((float)totalBlocks / totalShoots);

        int indexPos = 0;

        foreach (var kvp in _levelData.levels[Level].attackBlockTypes)
        {
            JellyType shooterType = kvp;
            int shooterCount = totalShoots;

            int spawnIndex = indexPos % _shooterSpawnPoints.Length;
            Transform spawnPoint = _shooterSpawnPoints[spawnIndex];

            RouletteinfitityShooterBox newShooter = Instantiate(_shooterBoxPrefab, spawnPoint);
            newShooter.jellyVariant = shooterType;
            newShooter.ammoCount = totalShoots;
            newShooter.Init();

            indexPos = (indexPos + 1) % _shooterSpawnPoints.Length;
        }
    }

    public void OnClickNext()
    {
        Level += 1;
        if (Level > MaxReachedLevel)
        {
            MaxReachedLevel = Level;
        }
        SceneManager.LoadScene("Game");
    }

    public void OnClickRestart()
    {
        if (Level > MaxReachedLevel)
        {
            MaxReachedLevel = Level;
        }
        SceneManager.LoadScene("Game");
    }

    public void OnClickMenu()
    {
        if (Level > MaxReachedLevel)
        {
            MaxReachedLevel = Level;
        }
        SceneManager.LoadScene("Menu");
    }
}
