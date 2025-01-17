using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuffaloRunGameController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _trapsTransforms;

    [SerializeField]
    private Transform _roadsSpawnPos;

    [SerializeField]
    private Transform[] _trapsSpawnPos;

    [SerializeField]
    private TMP_Text[] _scoreTxt;

    private int _spawnCount = 40;

    private int _currentSpawnCount = 0;

    public static int xValue;

    [SerializeField]
    private GameObject _bufafloResultPage;

    [SerializeField]
    private TMP_Text[] _starsTxt;

    [SerializeField]
    private Material[] _buffaloMaterials;

    [SerializeField]
    private SkinnedMeshRenderer _buffaloSkinRenderer;

    public static float BuffaloMusicVolume
    {
        get
        {
            if (PlayerPrefs.HasKey("BuffaloMusicVolumeData"))
            {
                return PlayerPrefs.GetFloat("BuffaloMusicVolumeData");
            }
            return 0.5f;
        }
        set
        {
            PlayerPrefs.SetFloat("BuffaloMusicVolumeData", value);
        }
    }

    public static float BuffaloSoundVolume
    {
        get
        {
            if (PlayerPrefs.HasKey("BuffaloSoundVolumeData"))
            {
                return PlayerPrefs.GetFloat("BuffaloSoundVolumeData");
            }
            return 0.5f;
        }
        set
        {
            PlayerPrefs.SetFloat("BuffaloSoundVolumeData", value);
        }
    }

    public static int BuffaloSkinIndex {
        get
        {
            if (PlayerPrefs.HasKey("BuffaloSkinIndexData"))
            {
                return PlayerPrefs.GetInt("BuffaloSkinIndexData");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("BuffaloSkinIndexData", value);
        }
    }

    public static int BuffaloMaxScore 
    {
        get
        {
            if (PlayerPrefs.HasKey("BuffaloMaxScoreData"))
            {
                return PlayerPrefs.GetInt("BuffaloMaxScoreData");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("BuffaloMaxScoreData", value);
        }
    }

    public static int buffaloTrapsDamageValue
    {
        get
        {
            if (PlayerPrefs.HasKey("buffaloTrapsDamageValueData"))
            {
                return PlayerPrefs.GetInt("buffaloTrapsDamageValueData");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("buffaloTrapsDamageValueData", value);
        }
    }

    public static string buffaloRunGameControllerSettingsKey;

    public static int buffaloTrapsSpawnTimeValue
    {
        get
        {
            if (PlayerPrefs.HasKey("buffaloTrapsSpawnTimeValueData"))
            {
                return PlayerPrefs.GetInt("buffaloTrapsSpawnTimeValueData");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("buffaloTrapsSpawnTimeValueData", value);
        }
    }

    public static int currentScore;

    public static int currentStars;

    public static int BuffaloCoins
    {
        get
        {
            if (PlayerPrefs.HasKey("BuffaloCoinsData"))
            {
                return PlayerPrefs.GetInt("BuffaloCoinsData");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("BuffaloCoinsData", value);
        }
    }

    private void Start()
    {
        BuffaloRunOwlComponent.isStop = false;
        BuffaloWallComponent.mainXValue = 0;
        _buffaloSkinRenderer.material = _buffaloMaterials[BuffaloSkinIndex];
        xValue = 1;
        currentStars = 0;
        currentScore = 0;
        StartCoroutine(SpawningTraps());
    }

    private IEnumerator SpawningTraps()
    {
        while (!BuffaloRunOwlComponent.isStop)
        {
            if (_currentSpawnCount < _spawnCount)
            {
                if (Random.Range(0,2) != 0)
                {
                    foreach (var item in _trapsSpawnPos)
                    {
                            GameObject tempObject = Instantiate(_trapsTransforms[Random.Range(0, 3)], item.position, item.rotation);
                            if (tempObject.name.ToLower().Contains("coin"))
                            {
                                tempObject.transform.position = new Vector3(tempObject.transform.position.x, tempObject.transform.position.y - 0.5f, tempObject.transform.position.z);
                            }
                    }
                    yield return new WaitForSeconds(0.25f);
                }
                else
                {
                    if (Random.Range(0, 2) != 0)
                    {
                        Instantiate(_trapsTransforms[3], _roadsSpawnPos.position, _roadsSpawnPos.rotation);
                    }
                    yield return new WaitForSeconds(1.5f);
                }
            }
            else
            {
                Instantiate(_trapsTransforms[4], _roadsSpawnPos.position,Quaternion.identity);
                yield return new WaitForSeconds(1.5f);
            }
            _currentSpawnCount++;
            yield return new WaitForSeconds(0.25f);
        }
        _bufafloResultPage.SetActive(true);
    }

    private void LateUpdate()
    {
        foreach (var item in _scoreTxt)
        {
            item.text = (currentScore * xValue).ToString();
        }
        foreach (var item in _starsTxt)
        {
            item.text = "x" + (currentStars * xValue).ToString();
        }
        if (currentScore * xValue > BuffaloMaxScore)
        {
            BuffaloMaxScore = (currentScore * xValue);
        }
    }

    public void OnClickRestart()
    {
        BuffaloCoins += (currentStars * xValue);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickMenu()
    {
        BuffaloCoins += (currentStars * xValue);
        SceneManager.LoadScene("BuffaloMenus");
    }
}
