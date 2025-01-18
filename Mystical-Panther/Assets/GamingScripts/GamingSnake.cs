using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GamingSnake : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _healths;

    private int health;

    private Animator _animator;

    private float speed = 0.05f;

    private void Start()
    {
        health = _healths.Length;
        _animator= GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        transform.position += -transform.right * speed;
        for (int i = 0; i < _healths.Length; i++)
        {
            if (i < health)
            {
                _healths[i].SetActive(true);
            }
            else
            {
                _healths[i].SetActive(false);
            }
        }
    }

    public void OnTakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) 
        {
            _animator.SetBool("SnakeDiew", true);
            speed = 0;
            GamingSnakeSpawner.countOfSnakes++;
            Destroy(gameObject,1); 
        }
    }
}
