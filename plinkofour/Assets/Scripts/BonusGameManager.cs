using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BonusGameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _resultScreen;

    [SerializeField]
    private Transform[] _ballsSpawnPositions;

    [SerializeField]
    private GameObject _ballPrefab;

    [SerializeField]
    private Material[] _skyMaterials;

    [SerializeField]
    private TMP_Text _endScoreTxt;

    public static int endScoreValue;
    
    private bool _isInitialized = false;

    private void Start()
    {
        Time.timeScale = 1;
        endScoreValue = 0;
        _isInitialized = false;
        RenderSettings.skybox = _skyMaterials[Random.Range(0, _skyMaterials.Length)];
        StartCoroutine(SpawningBalls());
    }

    private IEnumerator SpawningBalls()
    {
        int currentCount = 0;
        while (currentCount < GameSavesManager.GameCurrentBallsCount)
        {
            yield return new WaitForSeconds(0.1f);
            Instantiate(_ballPrefab, new Vector3(Random.Range(_ballsSpawnPositions[0].position.x, _ballsSpawnPositions[1].position.x), _ballsSpawnPositions[0].position.y, _ballsSpawnPositions[0].position.z), Quaternion.identity);
            currentCount +=1;
        }
        GameSavesManager.GameCurrentBallsCount = 0;
        _isInitialized = true;
    }

    private void LateUpdate()
    {
        if (!_isInitialized)
            return;
        if (FindObjectOfType<BonusBallComponent>() == null)
        {
            _resultScreen.SetActive(true);
        }
        _endScoreTxt.text = "+" + endScoreValue.ToString();
    }

    private void OnApplicationQuit()
    {
        GameSavesManager.GameCurrentLevelValue = 1;
    }

    public void OnClickContinue()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnClickMenu()
    {
        GameSavesManager.GameCurrentLevelValue = 1;
        SceneManager.LoadScene("Menu");
    }
}
