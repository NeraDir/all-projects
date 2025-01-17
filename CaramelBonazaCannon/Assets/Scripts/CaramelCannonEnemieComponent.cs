using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaramelCannonEnemieComponent : MonoBehaviour
{
    private Animator _caramelAnimator;

    private bool _death;

    private float _caramelEnemieHealth;

    public bool isLast;

    public bool isBoss;

    private Collider _collider;

    [SerializeField]
    private Image _healthBar;

    private float _maxHealth;

    private void Start()
    {
        _collider = GetComponent<Collider>();
        for (int i = 0; i < CaramelCanonGameManager.CaramelCannonCurrentWave; i++)
        {
            if (isBoss)
            {
                _caramelEnemieHealth += 4f;
            }
            else
            {
                _caramelEnemieHealth += 2f;
            }
        }
        _maxHealth = _caramelEnemieHealth;
        _caramelAnimator = GetComponent<Animator>();
    }

    public void DestroyMe()
    {
        transform.DOMoveY(transform.position.y - 5, 2f).OnComplete(() => Destroy(gameObject));
    }

    private void LateUpdate()
    {
        if (!_death)
            transform.position += new Vector3(0, 0, -1) * 5 * Time.deltaTime;
        _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount, _caramelEnemieHealth / _maxHealth, 10 * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.V))
        {
            Death(1);
        }
    }

    public void Death(float damage)
    {
        if (_death)
            return;
        _caramelEnemieHealth -= damage;
        if (_caramelEnemieHealth <= 0)
        {
            CaramelCanonGameManager.currentKilledCount++;
            _death = true;
            _caramelAnimator.SetBool("CaramelDeath",true);
            if (isBoss)
            {
                CaramelCanonGameManager.caramelStarsPerSession += Random.Range(5, 15);
            }
            else
            {
                CaramelCanonGameManager.caramelStarsPerSession += Random.Range(2, 5);
            }
            Destroy(_collider);
            if (isLast)
            {
                CaramelCanonGameManager.CaramelWaveEnd?.Invoke();
            }
        }
    }
}
