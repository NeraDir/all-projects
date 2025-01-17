using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AviaPlaneController : MonoBehaviour
{
    [SerializeField]
    private Joystick _joystick;

    [SerializeField]
    private Image[] _heartImages;

    [SerializeField]
    private GameObject _moveEffect;
    [SerializeField]
    private GameObject _effectBoom;
    public static float maxLivingTime
    {
        get
        {
            if (PlayerPrefs.HasKey("avia_skies_runners_max_living_time"))
                return PlayerPrefs.GetFloat("avia_skies_runners_max_living_time");
            return 0;
        }
        set
        {
            PlayerPrefs.SetFloat("avia_skies_runners_max_living_time", value);
        }
    }

    private float _currentLivingTime;
    public static bool isEnd;

    private int _heartCount;
    private float _moveSpeed = 0.15f;
    private Quaternion _lastRotation;

    [SerializeField]
    private TMP_Text[] _showLivingTime;

    [SerializeField]
    private GameObject _resultScreen;

    private void Awake()
    {
        isEnd = false;
        _heartCount = 3;
        StartCoroutine(Effect());
    }

    private void LateUpdate()
    {
        if (isEnd)
            return;

        _currentLivingTime += Time.deltaTime;
        if (_currentLivingTime > maxLivingTime)
            maxLivingTime = _currentLivingTime;
        foreach (var item in _showLivingTime)
            item.text = _currentLivingTime.ToString("0.0") + "s";
        for (int i = 0; i < _heartImages.Length; i++)
        {
            if (i>= _heartCount)
            {
                _heartImages[i].transform.DOScale(Vector3.zero, 0.25f);
            }
            else
            {
                _heartImages[i].transform.DOScale(Vector3.one, 0.25f);
            }
        }
        if (_heartCount <= 0)
        {
            Instantiate(_effectBoom, transform.position, Quaternion.identity);
            Destroy(gameObject);
            _resultScreen.SetActive(true);
            isEnd = true;
            return;
        }
           

        transform.position += (transform.up * _moveSpeed);
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            float needRotation = Mathf.Atan2(-_joystick.Horizontal, _joystick.Vertical) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, needRotation));
            //_planerBody.velocity = new Vector3(_joystick.Horizontal * _planerMovementSpeed, _joystick.Vertical * _planerMovementSpeed, _planerBody.velocity.z);
            _lastRotation = transform.rotation;
        }
        else
        {
            transform.rotation = _lastRotation;
        }
    }

    public void GetDamage()
    {
        _heartCount--;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AviDeadWallComponent wall))
        {
            _heartCount = 0;
        }
    }

    private IEnumerator Effect()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.02f);
            GameObject tempEffect = Instantiate(_moveEffect, _moveEffect.transform.position, _moveEffect.transform.rotation);
            tempEffect.SetActive(true);
        }
    }
}
