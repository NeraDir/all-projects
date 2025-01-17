using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public float TimeSpawn = 5f;
    public EnemyManager EnemyPrefab;
    public Transform PlayerTransform;
    public int EnemiesCount;
    public float MapSizeX = 100;
    public float MapSizeY = 100;

    private float _timer = 0;
    public List<EnemyManager> _enemiesList = new();

    private void Start()
    {
        FirstEnemiesSpawn();
        InitalizeEnemies();
    }

    private void FirstEnemiesSpawn()
    {
        for (int i = 0; i < EnemiesCount; i++)
        {
            EnemyManager buff = Instantiate(EnemyPrefab);

            float width = 100;
            float length = 100;

            float XLeft = transform.position.x - width / 2;
            float XRight = transform.position.x + width / 2;

            float ZLeft = transform.position.z - length / 2;
            float ZRight = transform.position.z + length / 2;

            float randomX = Random.Range(XLeft, XRight);
            float randomZ = Random.Range(ZLeft, ZRight);

            buff.transform.position = new Vector3(randomX, transform.position.y, randomZ);

            buff.gameObject.SetActive(false);

            _enemiesList.Add(buff);
        }
    }

    private void InitalizeEnemies()
    {
        foreach (var item in _enemiesList)
            item.Init(PlayerTransform, this);
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if( _timer >= TimeSpawn )
        {
            EnemyManager buff = _enemiesList.Where(x => x.gameObject.activeInHierarchy == false).FirstOrDefault();

            if(buff != null)
            {
                float width = 100;
                float length = 100;

                float XLeft = transform.position.x - width / 2;
                float XRight = transform.position.x + width / 2;

                float ZLeft = transform.position.z - length / 2;
                float ZRight = transform.position.z + length / 2;

                float randomX = Random.Range(XLeft, XRight);
                float randomZ = Random.Range(ZLeft, ZRight);

                buff.transform.position = new Vector3(randomX, transform.position.y, randomZ);

                buff.HealthSystem.SetHP();
                buff.gameObject.SetActive(true);
            }

            _timer = 0;
        }
    }

    public void DeleteFromList(EnemyManager obj)
    {
        obj.gameObject.SetActive(false);
    }

    public List<EnemyManager> GetActiveEnemies()
    {
        return _enemiesList.Where(x => x.gameObject.activeInHierarchy).ToList();
    }
}
