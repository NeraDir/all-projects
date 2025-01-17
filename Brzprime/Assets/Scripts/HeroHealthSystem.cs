using UnityEngine;
using UnityEngine.UI;

public class HeroHealthSystem : MonoBehaviour
{
    [SerializeField]
    private GameResulter _gameResulter;
    [SerializeField]
    private Image _healthBar;

    private float _currentHealth;

    public static float _maxHealth = 100;

    public static float _maxArmour = 1f;

    public void Awake()
    {
        if (PlayerPrefs.HasKey("MaxHealth"))
        {
            _maxHealth = PlayerPrefs.GetFloat("MaxHealth");
        }

        if (PlayerPrefs.HasKey("MaxArmour"))
        {
            _maxArmour = PlayerPrefs.GetFloat("MaxArmour");
        }

        _currentHealth = _maxHealth;
    }

    public void ApplyDamage(float damage)
    {
        _currentHealth -= (damage / _maxArmour);

        if(_currentHealth <= 0)
        {
            _currentHealth = 0;
            _gameResulter.GameFailed();
        }

        _healthBar.fillAmount = _currentHealth / _maxHealth;
    }

    public void AddHealth(float addHealth)
    {
        _currentHealth += addHealth;

        if(_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }

        _healthBar.fillAmount = _currentHealth / _maxHealth;
    }
}
