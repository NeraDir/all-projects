using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallDefenceGameController : MonoBehaviour
{
    public static int BallsDefenceMaxLivedWave
    {
        get
        {
            if (PlayerPrefs.HasKey("BallsDefencePimoMaxLivedWaveKey"))
            {
                return PlayerPrefs.GetInt("BallsDefencePimoMaxLivedWaveKey");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("BallsDefencePimoMaxLivedWaveKey", value);
        }
    }

    [SerializeField]
    private GameObject _enemieBallPrefab;

    [SerializeField]
    private GameObject _enemiesSpawnPosition;

    [SerializeField]
    private GameObject _resultScreen;

    [SerializeField]
    private Text[] _waveShow;

    [SerializeField]
    private Text[] _starsShow;

    private int _waveCount;

    private int _enemiesCountOfWave;

    public static int EnemiesCount;

    public static int StarsCount;

    public static bool WaveIsEnde;

    public static float EnemiesMoveSpeed;

    private bool isEnd;

    private void Start()
    {
        isEnd = false;
        WaveIsEnde = false;
        EnemiesCount = 0;
        _waveCount = 1;
        _enemiesCountOfWave = 2 + _waveCount;
        EnemiesMoveSpeed = 1 + ((float)_waveCount /2);
        BallDefenceKingManager.kingIsDead.AddListener(OnDead);
        StartCoroutine(Waves());
    }

    private void OnDead()
    {
        isEnd = true;
        _resultScreen.SetActive(true);
    }

    private IEnumerator Waves()
    {
        while (true)
        {
            if (!isEnd)
            {
                if (WaveIsEnde)
                {
                    yield return new WaitForSeconds(10);
                    _waveCount++;
                    EnemiesMoveSpeed = 1 + ((float)_waveCount / 2);
                    _enemiesCountOfWave = 2 + _waveCount;
                    WaveIsEnde = false;
                }
                else
                {
                    for (int i = 0; i < _enemiesCountOfWave; i++)
                    {
                        BallDefenceEnemieManager tempEnemie = Instantiate(_enemieBallPrefab.GetComponent<BallDefenceEnemieManager>(), _enemiesSpawnPosition.transform.position, Quaternion.identity);
                        if (i == _enemiesCountOfWave - 1)
                        {
                            tempEnemie.IsLast = true;
                        }
                        else
                        {
                            tempEnemie.IsLast = false;
                        }
                        yield return new WaitForSeconds(3);
                    }
                }
            }
            yield return null;
        }
    }

    private void LateUpdate()
    {
        foreach (var item in _waveShow)
        {
            item.text = "WAVE " + _waveCount.ToString();
        }
        foreach (var item in _starsShow)
        {
            item.text = "x" + StarsCount.ToString();
        }
    }

    public void OnPimoPausePress()
    {
        Time.timeScale = 0;
    }

    public void OnPimoUnPausePress()
    {
        Time.timeScale = 1;
    }

    public void OnPimeRestartPress()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnPimeMenuPress()
    {
        SceneManager.LoadScene("BallsDefenceMenuScene");
    }
}
