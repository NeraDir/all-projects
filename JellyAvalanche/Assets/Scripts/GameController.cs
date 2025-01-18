using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static Action<float> UpProgress;
    
    [SerializeField] private GameData _gameData;
    
    [SerializeField] private Slider _levelProgressBar;
    
    [SerializeField]
    private SpawnObjectsComponent _candiesSpawner;
    
    [SerializeField]
    private SpawnObjectsComponent _jarsSpawner;

    [SerializeField]
    private GameObject _candiesPrefab;
    
    [SerializeField]
    private GameObject _jarsPrefab;
    
    private float _levelProgress = 0f;
    private float _maxLevelProgress = 0f;

    public static int CurrentLevel;
    
    private void Awake()
    {
        _jarsSpawner.Init(_jarsPrefab);
        UpProgress += OnUpProgress;
    }

    private void OnDestroy()
    {
        UpProgress -= OnUpProgress;
    }
    
    private void OnUpProgress(float value)
    {
        _levelProgress += value;
        ProgressBarUpdate();
    }

    private void ProgressBarUpdate()
    {
        _levelProgressBar.value = Mathf.Lerp(_levelProgressBar.value, (_levelProgress / _maxLevelProgress), 11 * Time.deltaTime);
    }
    
    public void OnClickNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickRestart()
    {
        
    }

    public void OnClickMenu()
    {
        
    }
}
