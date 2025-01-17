using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CaramelCanonGameManager : MonoBehaviour
{
    public static int caramelCannonMaxWavesCount
    {
        get
        {
            if (PlayerPrefs.HasKey("caramelCannonMaxWavesCountDataKey"))
            {
                return PlayerPrefs.GetInt("caramelCannonMaxWavesCountDataKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("caramelCannonMaxWavesCountDataKey", value);
        }
    }

    public static string caramelCannonGameSettingsKey;

    public static int caramelCannonGameLaunchedCount
    {
        get
        {
            if (PlayerPrefs.HasKey("caramelCannonGameLaunchedCountDataKey"))
            {
                return PlayerPrefs.GetInt("caramelCannonGameLaunchedCountDataKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("caramelCannonGameLaunchedCountDataKey", value);
        }
    }

    public static int CaramelCannonCurrentWave;

    public static int CaramelCannonMaxReachedWave
    {
        get
        {
            if (PlayerPrefs.HasKey("CaramelCannonMaxReachedWaveDataKey"))
            {
                return PlayerPrefs.GetInt("CaramelCannonMaxReachedWaveDataKey");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("CaramelCannonMaxReachedWaveDataKey", value);
        }
    }

    public static float CaramelCannonShootingTime
    {
        get
        {
            if (PlayerPrefs.HasKey("CaramelCannonShootingTimeDataKey"))
                return PlayerPrefs.GetFloat("CaramelCannonShootingTimeDataKey");
            return 1;
        }
        set
        {
            PlayerPrefs.SetFloat("CaramelCannonShootingTimeDataKey", value);
        }
    }

    public static float CaramelCannonBulletDamage
    {
        get
        {
            if (PlayerPrefs.HasKey("CaramelCannonBulletDamageDataKey"))
                return PlayerPrefs.GetFloat("CaramelCannonBulletDamageDataKey");
            return 1;
        }
        set
        {
            PlayerPrefs.SetFloat("CaramelCannonBulletDamageDataKey", value);
        }
    }

    public static float CaramelCannonHealth
    {
        get
        {
            if (PlayerPrefs.HasKey("CaramelCannonHealthDataKey"))
                return PlayerPrefs.GetFloat("CaramelCannonHealthDataKey");
            return 10;
        }
        set
        {
            PlayerPrefs.SetFloat("CaramelCannonHealthDataKey", value);
        }
    }


    private float _caramelEnemiesCountPerWave;

    public static int caramelStarsPerSession;

    public static float caramelCannonHealth;

    [SerializeField]
    private Image _caramelCannonHealthBar;

    [SerializeField]
    private Image _caramelCannonprogressBar;

    [SerializeField]
    private GameObject[] _caramelEnemies;

    [SerializeField]
    private GameObject[] _caramelEnemiesBosses;

    [SerializeField]
    private Text[] _caramelWavesDisplay;

    [SerializeField]
    private Text[] _caramelStarsDispaly;

    [SerializeField]
    private GameObject _caramelCannonResult;

    [SerializeField]
    private Transform[] _caramelCannonEnemiesSpawnPosition;

    public static UnityEvent CaramelWaveEnd = new UnityEvent();

    public static float currentKilledCount;

    private float maxHealth;

    private void Start()
    {
        caramelCannonHealth = CaramelCannonHealth;
        CaramelCannonCurrentWave = 1;
        currentKilledCount = 0;
        caramelStarsPerSession = 0;
        _caramelEnemiesCountPerWave = 2;
        maxHealth = caramelCannonHealth;
        CaramelWaveEnd.AddListener(OnWaveEnd);
        StartCoroutine(SpawnEnemies());
    }

    private void OnDestroy()
    {
        CaramelWaveEnd.RemoveListener(OnWaveEnd);
    }

    public void OnClickCannonRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickCannonMenu()
    {
        SceneManager.LoadScene("CaramelGameMenuScene");
    }

    private void LateUpdate()
    {
        if (caramelCannonHealth <= 0)
        {
            _caramelCannonResult.SetActive(true);
            return;
        }
        _caramelCannonprogressBar.fillAmount = Mathf.Lerp(_caramelCannonprogressBar.fillAmount, currentKilledCount / _caramelEnemiesCountPerWave, 10 * Time.deltaTime);
        _caramelCannonHealthBar.fillAmount = Mathf.Lerp(_caramelCannonHealthBar.fillAmount, caramelCannonHealth / maxHealth, 10 * Time.deltaTime);
        if (CaramelCannonCurrentWave > CaramelCannonMaxReachedWave)
        {
            CaramelCannonMaxReachedWave = CaramelCannonCurrentWave;
        }

        foreach (var item in _caramelStarsDispaly)
        {
            item.text = "x" + caramelStarsPerSession.ToString("0");
        }

        foreach (var item in _caramelWavesDisplay)
        {
            if (CaramelCannonCurrentWave % 5 == 0 && CaramelCannonCurrentWave != 0)
            {
                item.text = "BOSS";
            }
            else
            {
                item.text = "WAVE " + CaramelCannonCurrentWave.ToString("0");
            }
        }
    }

    private void OnWaveEnd()
    {
        _caramelEnemiesCountPerWave += 2;
        CaramelCannonCurrentWave += 1;
        CaramelCannonBulletDamage += 0.5f;

        StopAllCoroutines();
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        currentKilledCount = 0;
        yield return new WaitForSeconds(5);
        int currentCount = 0;
        while (currentCount < _caramelEnemiesCountPerWave)
        {
            if (currentCount == _caramelEnemiesCountPerWave - 1)
            {
                if (CaramelCannonCurrentWave % 5 == 0 && CaramelCannonCurrentWave != 0)
                {
                    GameObject tempEnemie = Instantiate(_caramelEnemiesBosses[Random.Range(0, _caramelEnemiesBosses.Length)], new Vector3(Random.Range(_caramelCannonEnemiesSpawnPosition[0].position.x, _caramelCannonEnemiesSpawnPosition[1].position.x), _caramelCannonEnemiesSpawnPosition[1].position.y, _caramelCannonEnemiesSpawnPosition[1].position.z), _caramelEnemies[0].transform.rotation);
                    tempEnemie.GetComponent<CaramelCannonEnemieComponent>().isLast = true;
                    tempEnemie.GetComponent<CaramelCannonEnemieComponent>().isBoss = true;
                    currentCount++;
                }
                else
                {
                    GameObject tempEnemie34 = Instantiate(_caramelEnemies[Random.Range(0, _caramelEnemies.Length)], new Vector3(Random.Range(_caramelCannonEnemiesSpawnPosition[0].position.x, _caramelCannonEnemiesSpawnPosition[1].position.x), _caramelCannonEnemiesSpawnPosition[1].position.y, _caramelCannonEnemiesSpawnPosition[1].position.z), _caramelEnemies[0].transform.rotation);
                    tempEnemie34.GetComponent<CaramelCannonEnemieComponent>().isLast = true;
                    currentCount++;
                }
            }
            else
            {
                Instantiate(_caramelEnemies[Random.Range(0, _caramelEnemies.Length)], new Vector3(Random.Range(_caramelCannonEnemiesSpawnPosition[0].position.x, _caramelCannonEnemiesSpawnPosition[1].position.x), _caramelCannonEnemiesSpawnPosition[1].position.y, _caramelCannonEnemiesSpawnPosition[1].position.z), _caramelEnemies[0].transform.rotation);
                currentCount++;
            }
            yield return new WaitForSeconds(2);
        }
    }
}
