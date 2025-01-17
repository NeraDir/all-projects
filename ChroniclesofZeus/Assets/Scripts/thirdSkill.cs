using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdSkill : MonoBehaviour
{
    [SerializeField]
    private FirstSkill fs;

    public float damage;

    private void Start()
    {
        for (int i = 0; i < Random.Range(1,4); i++)
        {
            FirstSkill firstSkill = Instantiate(fs, transform.position, Quaternion.identity);
            firstSkill.damage = damage;
        }

        Destroy(gameObject, 1);
    }
}
