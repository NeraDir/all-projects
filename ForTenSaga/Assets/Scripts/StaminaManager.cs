using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    public static Action<float> StaminaChanged;
    
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _speed;

    [SerializeField] private Image _staminaFillBar;
    
    private float _currentStamina;
    private float _maxStamina;
    
    public void Init(float stamina)
    {
        _maxStamina = stamina;
        _currentStamina = _maxStamina;
        Debug.Log(_currentStamina);
        StaminaChanged += OnStaminaChanged;
    }

    private void OnDestroy()
    {
        StaminaChanged -= OnStaminaChanged;
    }

    public float GetStamina()
    {
        return _currentStamina;
    }
    
    private void LateUpdate()
    {
        if (_currentStamina > _maxStamina)
            _currentStamina = _maxStamina;
        _currentStamina += (GameManager.TigerSkinIndex + 5) * Time.deltaTime;
        _staminaFillBar.fillAmount = Mathf.Lerp(_staminaFillBar.fillAmount,(_currentStamina/_maxStamina), 11 * Time.deltaTime);
        if (_target == null)
            return;
        transform.position = Vector3.Lerp(transform.position, _target.position + _offset, _speed * Time.deltaTime);
    }

    private void OnStaminaChanged(float stamina)
    {
        _currentStamina += stamina;
    }
}
