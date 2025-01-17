using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{
    public static float BestBlaztLivingTimeValue
    {
        get
        {
            if (PlayerPrefs.HasKey("BestBlaztLivingTimeValueKey"))
                return PlayerPrefs.GetFloat("BestBlaztLivingTimeValueKey");
            return 0.0f;
        }
        set
        {
            PlayerPrefs.SetFloat("BestBlaztLivingTimeValueKey", value);
        }
    }

    [SerializeField]
    private Transform[] _spawnPositions;

    [SerializeField]
    private GameObject _knifePrefab;

    [SerializeField]
    private Image[] _hearts;

    [SerializeField]
    private TMP_Text[] _displayLivingTime;

    [SerializeField]
    private GameObject _resultScreen;

    private int _heartsCount;
    private float _livingTime;
    private bool _isEnd;

    public static UnityEvent GetDamage = new UnityEvent();

    private IEnumerator Start()
    {
        _heartsCount = 4;
        OnGetDamage();
        GetDamage.AddListener(OnGetDamage);
        while (!_isEnd)
        {
            yield return new WaitForSeconds(2);
            GameObject tempKnife = Instantiate(_knifePrefab, _spawnPositions[Random.Range(0, _spawnPositions.Length)]);
            tempKnife.transform.parent = _spawnPositions[0].parent;
        }
        _resultScreen.SetActive(true);
    }

    private void LateUpdate()
    {
        if (_isEnd)
            return;
        _livingTime += Time.deltaTime;
        if (_livingTime > BestBlaztLivingTimeValue)
        {
            BestBlaztLivingTimeValue = _livingTime;
        }
        foreach (var item in _displayLivingTime)
        {
            item.text = _livingTime.ToString("0.0") + "s";
        }
    }

    private void OnDestroy()
    {
        GetDamage.RemoveListener(OnGetDamage);
    }

    private void OnGetDamage()
    {
        _heartsCount -= 1;
        for (int i = 0; i < _hearts.Length; i++)
        {
            if (i >= _heartsCount)
            {
                _hearts[i].DOFade(0, 0.25f);
            }
            else
            {
                _hearts[i].DOFade(1, 0.25f);
            }
        }
        if (_heartsCount <= 0)
        {
            _isEnd = true;
        }
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
