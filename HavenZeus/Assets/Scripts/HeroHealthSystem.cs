using UnityEngine;
using UnityEngine.UI;

public class HeroHealthSystem : MonoBehaviour
{
    [SerializeField]
    private LevelCompleter _levelCompleter;
    [SerializeField]
    private Image _healthBar;

    public float _currentHealth { get; private set; }

    public static float _maxHealth = 100f;

    void Awake()
    {
        Time.timeScale = 1f;
        if (PlayerPrefs.HasKey("MaxHealth"))
        {
            _maxHealth = PlayerPrefs.GetFloat("MaxHealth");
        }
        _currentHealth = _maxHealth;
    }

    public void AddHealth(float _health)
    {
        _currentHealth += _health;

        if(_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }

        _healthBar.fillAmount = _currentHealth / _maxHealth;
    }

    public void ApplyDamage(float _damage)
    {
        _currentHealth -= _damage;

        _healthBar.fillAmount = _currentHealth / _maxHealth;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Dead();
        }
    }

    public void Dead()
    {
        _levelCompleter.LevelFailed();
        Time.timeScale = 0;
    }

}
