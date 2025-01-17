using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    [SerializeField] private GameObject collectable;
    [SerializeField] private GameObject deathSound;
    [SerializeField] private SpriteRenderer _sp;
    [SerializeField] private Animator _anim;
    [SerializeField] private Sprite[] _images;

    float _speed = 2f;
    float _hp = 2f;

    public void Initialize(float _speed, float _hp) 
    {
        this._speed = _speed;
        this._hp = _hp;
        _sp.sprite = _images[Random.Range(0,_images.Length)];
    }

    void Update()
    {
        transform.position += new Vector3(0, -_speed*Time.deltaTime, 0);
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Player") 
        {
            FindObjectOfType<Player>().GetDamage(2);
            Instantiate(effect,transform.position, Quaternion.identity);
            Instantiate(deathSound);
            Destroy(gameObject);
        }
        if (col.name == "SingleProjectile") 
        {
            _anim.SetTrigger("getDamage");
            _hp--;
            Destroy(col.gameObject);
            if (_hp<=0) 
            {
                Instantiate(effect,transform.position, Quaternion.identity);
                int i = Random.Range(1,5); 
                if (i==1) 
                {
                    Instantiate(collectable,transform.position, Quaternion.identity);
                }
                Instantiate(deathSound);
                Destroy(gameObject);
            }
        }
    }
}
