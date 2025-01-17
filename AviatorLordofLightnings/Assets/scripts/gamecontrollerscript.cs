using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gamecontrollerscript : MonoBehaviour
{
    public static float maxTimelife
    {
        get
        {
            if (PlayerPrefs.HasKey("maxTimeAvidestinyLifeDataInfo"))
            {
                return PlayerPrefs.GetFloat("maxTimeAvidestinyLifeDataInfo");
            }
            return 0.0f;
        }
        set
        {
            PlayerPrefs.SetFloat("maxTimeAvidestinyLifeDataInfo", value);
        }
    }

    public static int aviplanestartspeedvalue
    {
        get
        {
            if (PlayerPrefs.HasKey("aviplanestartspeedvalue"))
            {
                return PlayerPrefs.GetInt("aviplanestartspeedvalue");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("aviplanestartspeedvalue", value);
        }
    }

    public static string aviplanegamename;

    public static int aviplanegamelaunchcount
    {
        get
        {
            if (PlayerPrefs.HasKey("aviplanegamelaunchcount"))
            {
                return PlayerPrefs.GetInt("aviplanegamelaunchcount");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("aviplanegamelaunchcount", value);
        }
    }

    [SerializeField]
    private Image[] _heartImages;

    [SerializeField]
    private GameObject _enemiePrefab;

    [SerializeField]
    private Transform[] _enemiePositions;

    [SerializeField]
    private Text[] _lifeTimeTxt;

    [SerializeField]
    private GameObject _resultScreen;

    public static int heartsCount;

    private float _spawningTime;

    private float _currentLifeTime;

    private void Start()
    {
        heartsCount = 5;
        _spawningTime = 3;
        _currentLifeTime = 0;
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            GameObject tempEnemie = Instantiate(_enemiePrefab, new Vector3(_enemiePositions[0].position.x, Random.Range(_enemiePositions[0].position.y, _enemiePositions[1].position.y), _enemiePositions[0].position.z), Quaternion.identity, _enemiePositions[0].transform.parent);
            tempEnemie.transform.SetSiblingIndex(0);
            yield return new WaitForSeconds(_spawningTime);
        }
    }

    private void LateUpdate()
    {
        if (heartsCount <= 0)
        {
            _resultScreen.SetActive(true);
            return;
        }
        _spawningTime -= 0.005f;
        if (_spawningTime <= 0.5f)
        {
            _spawningTime = 0.5f;
        }
        if (_currentLifeTime > maxTimelife)
        {
            maxTimelife = _currentLifeTime;
        }
        _currentLifeTime += Time.deltaTime;
        foreach (var item in _lifeTimeTxt)
        {
            item.text = _currentLifeTime.ToString("0.0") + "s";
        }
        for (int i = 0; i < _heartImages.Length; i++)
        {
            if (i >= heartsCount)
            {
                _heartImages[i].transform.DOScale(Vector3.zero, 0.25f);
            }
        }

    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene("gamescene");
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene("menuscene");
    }
}
