using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanerController : MonoBehaviour, IRocketDieble
{
    [SerializeField]
    private Joystick _joystick;

    [SerializeField]
    private float _planerMovementSpeed;

    private Rigidbody _planerBody;

    private Quaternion _planerLastRoatation;

    [SerializeField]
    private GameObject _rocketPrefab;

    [SerializeField]
    private GameObject _boomEffect;

    [SerializeField]
    private GameObject[] _planer;

    private List<GameObject> _hearts = new List<GameObject>();

    [SerializeField]
    private GameObject heartPrefab;

    [SerializeField]
    private Transform _heartsSpawnPosition;

    private int heartsCount;

    [SerializeField]
    private GameObject _gamePanel;

    [SerializeField]
    private GameObject _loosePanel;

    public static int _selectedPlanerIndex
    {
        get 
        {
            if (PlayerPrefs.HasKey("PlanerSelectedIndex"))
            {
                return PlayerPrefs.GetInt("PlanerSelectedIndex");
            }
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("PlanerSelectedIndex", value);
        }
    }

    private IEnumerator Start()
    {
        heartsCount = _selectedPlanerIndex + 3;

        Instantiate(_planer[_selectedPlanerIndex],transform.position,Quaternion.Euler(-90,0,0),transform);

        _planerBody = GetComponent<Rigidbody>();


        for (int i = 0; i < heartsCount; i++)
        {
            _hearts.Add(Instantiate(heartPrefab, _heartsSpawnPosition));
        }

        while (true)
        {
            yield return new WaitForSeconds(4);
            Instantiate(_rocketPrefab, transform.position, _rocketPrefab.transform.rotation);
            
        }

    }

    private void TakeDamage() 
    {
        heartsCount--;
        foreach (var item in _hearts)
        {
            Destroy(item.gameObject);
        }
        _hearts.Clear();
        for (int i = 0; i < heartsCount; i++)
        {
            _hearts.Add(Instantiate(heartPrefab, _heartsSpawnPosition));
        }
        if (heartsCount == 0)
        {
            Instantiate(_boomEffect, transform.position, transform.rotation);
            GameManager.gameStarted = false;
            GameManager.NoiseCam();
            Show();
            Destroy(gameObject);
        }
    }

    private void Show() 
    {
        _gamePanel.SetActive(false);
        _loosePanel.SetActive(true);
    }

    private void LateUpdate()
    {
        transform.position += (transform.up * _planerMovementSpeed);
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            float needRotation = Mathf.Atan2(-_joystick.Horizontal, _joystick.Vertical) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, needRotation));
            //_planerBody.velocity = new Vector3(_joystick.Horizontal * _planerMovementSpeed, _joystick.Vertical * _planerMovementSpeed, _planerBody.velocity.z);
            _planerLastRoatation = transform.rotation;
        }
        else
        {
            transform.rotation = _planerLastRoatation;
        }
    }

    public void Use()
    {
           
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out RocketScript rocket)) 
        {
            if (!rocket.isFaller)
            {
                TakeDamage();
            }
        }
        else if (other.TryGetComponent(out WaterScript water))
        {
            Instantiate(_boomEffect, transform.position, transform.rotation);
            GameManager.gameStarted = false;
            GameManager.NoiseCam();
            Show();
            Destroy(gameObject);
        }
    }
}
