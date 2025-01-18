using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    
    public GameObject[] Enemy; // Враги
    public Transform Player; // Позиция игрока
    public Transform PlayerHP; // Cube типа HP bar игрока
    public float totalScale; // Сумма размера Cube
    void Start()
    {
        totalScale = 6f;
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy() 
    {
        while (true)
        {
            Instantiate(Enemy[Random.Range(0, Enemy.Length)], new Vector3(2f, 0f, 45f), Quaternion.identity);
            yield return new WaitForSeconds(5);
        }
    }
}
