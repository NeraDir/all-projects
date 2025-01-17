using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonLanEnemie : MonoBehaviour
{
    [SerializeField]
    private GameObject soulPrefab;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float shootDistance;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Transform spawnPos;

    private float health;

    private bool isDeath;

    private IEnumerator Start()
    {
        health = 1;
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (!isDeath) 
            {
                if (Vector3.Distance(transform.position, DragonLanGameController.DragonLanTransform.position) < shootDistance)
                {
                    DragonLanBullet tempBullet = Instantiate(bullet.GetComponent<DragonLanBullet>(), spawnPos.position, spawnPos.rotation);
                    tempBullet.isEnemieBullet = true;
                    tempBullet.direction = DragonLanGameController.DragonLanTransform.position - transform.position;
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (!isDeath)
        {
            if (Vector3.Distance(transform.position,DragonLanGameController.DragonLanTransform.position) < shootDistance)
            {
                transform.LookAt(DragonLanGameController.DragonLanTransform.position);
            }
        }
    }

    public void GetSoul() 
    {
        DragonLanEnemieSoul tempSoul = Instantiate(soulPrefab.GetComponent<DragonLanEnemieSoul>(),transform.position,Quaternion.identity);
        tempSoul.GoTo(DragonLanGameController.DragonLanTransform);
        transform.DOMoveY(transform.position.y - 0.5f, 1).OnComplete(() => { Destroy(gameObject); });
    }

    private void OnMouseDown()
    {
        if (isDeath)
            return;
        DragonLanController.DragonShoot?.Invoke(transform);
    }

    public void Death()
    {
        if (isDeath)
            return;
        health -= 1;
        if (health <= 0)
        {
            isDeath = true;
            animator.SetBool("Death", true);
        }
    }
}
