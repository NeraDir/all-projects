using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieController : MonoBehaviour
{
    [SerializeField]
    private Transform _spawnPosition;

    [SerializeField]
    private BulletComponent _bulletComponent;


    public static bool canShoot;

    private void Start()
    {
        Vector3 startScale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(startScale, 0.25f);
        StartCoroutine(Shoot());
    }

    private void OnDestroy()
    {
  
    }

    private IEnumerator Shoot() 
    {
        while (true)
        {
            if (canShoot)
            {
                BulletComponent bulletTemp = Instantiate(_bulletComponent, _spawnPosition.position, _spawnPosition.rotation);
                bulletTemp.isEnemie = true;
            }
            yield return new WaitForSeconds(Random.Range(3f,6.5f));
        }
    }
}
