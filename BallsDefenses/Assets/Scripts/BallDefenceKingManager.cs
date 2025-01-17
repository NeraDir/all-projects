using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BallDefenceKingManager : MonoBehaviour
{
    public static int ballsDefenceKingStartHPCount
    {
        get
        {
            if (PlayerPrefs.HasKey("ballsDefenceKingStartHPCountKey"))
            {
                return PlayerPrefs.GetInt("ballsDefenceKingStartHPCountKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("ballsDefenceKingStartHPCountKey", value);
        }
    }

    public static string ballsDefenceKingName;

    public static int ballsDefenceKingStartDefencersCount
    {
        get
        {
            if (PlayerPrefs.HasKey("ballsDefenceKingStartDefencersCountKey"))
            {
                return PlayerPrefs.GetInt("ballsDefenceKingStartDefencersCountKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("ballsDefenceKingStartDefencersCountKey", value);
        }
    }

    [SerializeField]
    private GameObject[] _hearts;

    [SerializeField]
    private GameObject _shieldMan;

    public int hearts;

    public static UnityEvent kingIsDead = new UnityEvent();

    private bool _isDead = false;

    private int _maxShieldCount = 5;

    public static int CurrentShieldCount;

    private GameObject _lastShieldMan;

    private int _addPrice;

    [SerializeField]
    private Text _priceShow;

    private void Start()
    {
        _addPrice = 10;
        hearts = _hearts.Length;
        _lastShieldMan = Instantiate(_shieldMan, new Vector3(transform.position.x + 1, transform.position.y + 10, 0), Quaternion.identity);
        CurrentShieldCount++;
    }

    public void OnPimoAddNewShieldMan()
    {
        if (CurrentShieldCount < _maxShieldCount)
        {
            if (_addPrice <= BallDefenceGameController.StarsCount)
            {
                BallDefenceGameController.StarsCount -= _addPrice;
                _addPrice += 10;
                if (_lastShieldMan != null)
                {
                    _lastShieldMan = Instantiate(_shieldMan, new Vector3(_lastShieldMan.transform.position.x + 1, _lastShieldMan.transform.position.y + 10, 0), Quaternion.identity);
                    CurrentShieldCount++;
                }
                else
                {
                    _lastShieldMan = Instantiate(_shieldMan, new Vector3(transform.position.x + 1, transform.position.y + 10, 0), Quaternion.identity);
                    CurrentShieldCount++;
                }
            }
            else
            {
                Handheld.Vibrate();
            }
        }
        else
        {
            Handheld.Vibrate();
        }
    }

    private void LateUpdate()
    {
        if (_isDead)
            return;
        _priceShow.text = "x" + _addPrice.ToString();
        for (int i = 0; i < _hearts.Length; i++)
        {
            if (i >= hearts)
            {
                _hearts[i].transform.DOScale(Vector3.zero, 0.1f);
            }
        }
        if (hearts <= 0)
        {
            kingIsDead?.Invoke();
        }
    }
}
