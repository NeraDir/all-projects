using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChillBaseBallGameController : MonoBehaviour
{
    [SerializeField]
    private Transform[] _chillSpawnPositions;

    [SerializeField]
    private GameObject _chillBallPrefab;

    [SerializeField]
    private Transform[] _chillHearts;

    [SerializeField]
    private GameObject _chillResultScreen;

    [SerializeField]
    private Text[] _chillScoreTxt;

    public static int chillHearts;

    public static float chillOtherBallsMove;

    public static int chillScore;

    private IEnumerator Start()
    {
        chillScore = 0;
        chillOtherBallsMove = 0.25f;
        chillHearts = 3;
        while (true)
        {
            Instantiate(_chillBallPrefab, _chillSpawnPositions[Random.Range(0, _chillSpawnPositions.Length)].position, Quaternion.identity);
            chillOtherBallsMove += 0.00005f;
            yield return new WaitForSeconds(6);
        }
    }

    private void LateUpdate()
    {
        foreach (var item in _chillScoreTxt)
        {
            item.text = chillScore.ToString("0");
        }
        if (chillScore > ChillGameController.ChillBaseMaxDistanceReached)
        {
            ChillGameController.ChillBaseMaxDistanceReached = chillScore;
        }
        if (chillHearts <= 0)
        {
            _chillResultScreen.SetActive(true);
            return;
        }
        for (int i = 0; i < _chillHearts.Length; ++i)
        {
            if (i >= chillHearts)
            {
                _chillHearts[i].DOScale(Vector3.zero,0.25f);
            }
        }
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene("ChillBaseMenu");
    }
}
