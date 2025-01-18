using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocketComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    public int bulletDamage;

    public float shootSpeed;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootSpeed);
            BulletComponent bulletTemp = Instantiate(bulletPrefab.GetComponent<BulletComponent>(), transform.position,Quaternion.identity,transform.parent);
            bulletTemp.bulletDamage = bulletDamage;
        }

    }
}
