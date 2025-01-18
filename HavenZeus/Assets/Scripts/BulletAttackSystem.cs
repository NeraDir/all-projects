using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttackSystem : MonoBehaviour
{
    [SerializeField]
    private float _bulletDamage;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("MaxDamage"))
        {
            _bulletDamage = PlayerPrefs.GetFloat("MaxDamage");
        }
        else
        {
            _bulletDamage = UpgradesManager._bulletDamage;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthSystem>().ApplyDamage(_bulletDamage);
        }

        gameObject.SetActive(false);
        transform.parent.gameObject.SetActive(false);
 
    }
}
