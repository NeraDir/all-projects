using System;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static Action<int> changeHealth;
    
    [SerializeField] private HeartComponent[] _heartTransforms;

    [SerializeField] private int _currentHealth;
    
    [SerializeField] private AudioClip _damageSound;
    [SerializeField] private AudioClip _healSound;
    
    public void Init()
    {
        _currentHealth = _heartTransforms.Length;
        changeHealth += OnChangeHealth;
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnChangeHealth(+1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            OnChangeHealth(-1);
        }
    }
    
    private void OnDestroy()
    {
        changeHealth -= OnChangeHealth;
    }

    private void OnChangeHealth(int health)
    {
        if (health > 0)
        {
            SettingsManager.playSound?.Invoke(_healSound);
        }
        else if (health < 0)
        {
            SettingsManager.playSound?.Invoke(_damageSound);
        }
        _currentHealth += health;
        if (_currentHealth >= _heartTransforms.Length)
        {
            _currentHealth = _heartTransforms.Length;
        }
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            GameManager.resultShow?.Invoke(true);
            return;
        }
        VisualUpdate();
    }

    private void VisualUpdate()
    {
        for (int i = 0; i < _heartTransforms.Length; i++)
        {
            if (i < _currentHealth)
            {
                _heartTransforms[i].gameObject.SetActive(true);
            }
            else
            {
                _heartTransforms[i].DestroyMe();
                break;
            }
        }
    }
}
