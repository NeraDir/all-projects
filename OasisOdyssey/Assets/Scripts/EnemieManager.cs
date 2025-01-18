using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemieManager : MonoBehaviour
{
    private Animator _animator;

    private bool _dead;

    private Image _image;

    public EnemieConfiguration[] enemiesConfigurations;

    public Sprite[] _enemiesSprites;

    private int health;

    private int getScore;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _image = GetComponent<Image>();
        SetEnemieConfiguration();
    }

    private void SetEnemieConfiguration() 
    {
        StartCoroutine(DieingAnim(new Vector3(1,1,1)));
        _dead = false;
        EnemieConfiguration rndConfig = enemiesConfigurations[Random.Range(0, enemiesConfigurations.Length)];
        _image.sprite = rndConfig.sprite;
        health = rndConfig.health;
        getScore = rndConfig.scorePerEnemie;
        _animator.enabled = true;
    }

    public void TakeDamage(int takeDamage) 
    {
        if (_dead) 
            return;
        StartCoroutine(DieingAnim(new Vector3(1.3f, 1.3f, 1.3f)));
        health -= takeDamage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die() 
    {
        _animator.enabled = true;
        _dead = true;
        StartCoroutine(DieingAnim(Vector3.zero));
        Invoke(nameof(SetEnemieConfiguration),2f);
    }

    private IEnumerator DieingAnim(Vector3 scaleValue) 
    {
        while (transform.localScale != scaleValue) 
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, scaleValue, 3 * Time.deltaTime);
            yield return null;
        }
        CannonController.score += getScore;
        StopAllCoroutines();
    }
}

[Serializable]
public class EnemieConfiguration 
{
    public int health;
    public Sprite sprite;
    public int scorePerEnemie;
}
