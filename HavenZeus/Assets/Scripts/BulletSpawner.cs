using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform _bulletSpawnPosition;
    [SerializeField]
    private GameObject[] _bullets;

    private int _currentBulletIndex;

    public void SpawnBullet()
    {
        _bullets[_currentBulletIndex].transform.position = _bulletSpawnPosition.position;

        _bullets[_currentBulletIndex].transform.rotation = Quaternion.LookRotation(new Vector3(EnemyDetector._nearestEnemy.transform.position.x, _bullets[_currentBulletIndex].transform.position.y - 1, EnemyDetector._nearestEnemy.transform.position.z) - _bullets[_currentBulletIndex].transform.position);

        _bullets[_currentBulletIndex].SetActive(true);

        _currentBulletIndex++;

        if(_currentBulletIndex == _bullets.Length)
        {
            _currentBulletIndex = 0;
        }
    } 
}
