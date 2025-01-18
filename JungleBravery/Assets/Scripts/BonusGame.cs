using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class BonusGame : MonoBehaviour
{
    [SerializeField]
    private Transform[] _spawnPositions;

    [SerializeField]
    private GameObject _beatPrefab;

    [SerializeField]
    private GameObject _bonusGameResult;

    [SerializeField]
    private TMP_Text[] _scoreTxt;

    public static bool bonusEnded;

    public static float fallSpeed;

    private IEnumerator Start()
    {
        fallSpeed = 1;
        bonusEnded = false;
        while (!bonusEnded)
        {
            Instantiate(_beatPrefab, new Vector3(Random.Range(_spawnPositions[0].position.x, _spawnPositions[1].position.x), _spawnPositions[0].position.y + 5, 0), _beatPrefab.transform.rotation);
            yield return new WaitForSeconds(0.4f);
        }
        _bonusGameResult.SetActive(true);
    }

    private void LateUpdate()
    {
        if (bonusEnded)
            return;
        fallSpeed += (Time.deltaTime /2);
        if (GameManager.score > GameManager.tigerBestScore)
        {
            GameManager.tigerBestScore = GameManager.score;
        }
        foreach (var item in _scoreTxt)
        {
            item.text = GameManager.score.ToString("0");
        }
    }

    public void OnMenuPressed() 
    {
        GameManager.score = 0;
        GameManager.tigerCurrentLevel = 1;
        SceneManager.LoadScene("Menu");
    }
}
