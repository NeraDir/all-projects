using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneMovement : MonoBehaviour
{
    [SerializeField]
    private Joystick _floatingJoystick;
    [SerializeField]
    private GameResulter _gameResulter;
    [SerializeField]
    private Image _fuelImage;
    [SerializeField]
    private float _movementSpeed;
    [SerializeField] [Range(1f, 100f)]
    private float _fuelConsumptionCoefficient;


    private Rigidbody2D _planeRigidbody;
    private Vector3 _movementVector;
    private float _currentFuelCount;

    public static float _maxFuelCount = 100f;

    public float _currentPoints { get; private set; }

    private void Awake()
    {
        if (PlayerPrefs.HasKey("MaxFuel"))
        {
            _maxFuelCount = PlayerPrefs.GetFloat("MaxFuel");
        }

        _currentFuelCount = _maxFuelCount;
        _planeRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(_currentFuelCount > 0)
        {
            _movementVector = new Vector3(_floatingJoystick.Direction.x, _floatingJoystick.Direction.y, 0f);
            _planeRigidbody.MovePosition(transform.position + _movementVector * _movementSpeed * Time.deltaTime);

            _currentFuelCount -= Time.deltaTime * _fuelConsumptionCoefficient;
            _currentPoints += Time.deltaTime;
            _fuelImage.fillAmount = _currentFuelCount / _maxFuelCount;
        }
        else
        {
            _gameResulter.GameFailed();
        }
    }

    public void AddFuel(float addFuel)
    {
        _currentFuelCount += addFuel;

        if(_currentFuelCount > _maxFuelCount)
        {
            _currentFuelCount = _maxFuelCount;
        }
    }
}
