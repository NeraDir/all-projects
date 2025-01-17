using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameControl _gameControl;

    public void SpawnEnemies()
    {
        int numOfEnemies = Random.Range(spawnPoints.Count / 2, spawnPoints.Count);
        _gameControl.bagsNeed = numOfEnemies;
        _gameControl.UpdateBagsDisplay();

        var pointsLeft = spawnPoints;

        for(int i = 0; i < numOfEnemies; i++)
        {
            int randPointNum = Random.Range(0, pointsLeft.Count);
            Instantiate(_enemy, pointsLeft[randPointNum].position, Quaternion.identity);
            pointsLeft.Remove(pointsLeft[randPointNum]);
        }
    }
}
