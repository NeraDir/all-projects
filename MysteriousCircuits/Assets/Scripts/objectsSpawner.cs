using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class objectsSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] _spawnPositions;

    [SerializeField]
    private GameObject[] _objectsList;

    [SerializeField]
    private GameObject _wall;

    private int _wallsCount;

    public static List<GameObject> _walls = new List<GameObject>();

    public void Init()
    {
        for (int i = 0; i < (gameController.LevelIndex + 1); i++)
        {
            _wallsCount += 2;
        }
        StartCoroutine(LaunchSpawn());
    }

    private IEnumerator LaunchSpawn()
    {
        int currentWallsCount = 0;
        while (currentWallsCount < _wallsCount)
        {
            yield return new WaitForSeconds(0.5f);
            GameObject newObject;
            if (Random.Range(8, 100) > 10)
            {
                newObject = SpawnObjects();
                newObject.transform.SetParent(_spawnPositions[0].parent);
                newObject.transform.localScale = Vector3.one;
                newObject.transform.SetSiblingIndex(2);
            }
            else
            {

                newObject = SpawnWall();
                currentWallsCount += 1;
                newObject.transform.SetParent(_spawnPositions[0].parent);
                newObject.transform.localScale = Vector3.one;
                newObject.transform.SetSiblingIndex(2);
                _walls.Add(newObject);

            }
        }
        yield return new WaitForSeconds(5);
        if (FindObjectOfType<ballComponent>().GetBallsInMe() <= 0 && !FindObjectOfType<gameController>().IsReached())
        {
            gameController.getResult?.Invoke(false);
        }
        else
        {
            gameController.getResult?.Invoke(true);
        }
    }

    private GameObject SpawnObjects()
    {
        GameObject newObject = Instantiate(_objectsList[Random.Range(0, _objectsList.Length)],
            new Vector3(
                Random.Range(_spawnPositions[0].position.x, _spawnPositions[1].position.x),
                _spawnPositions[0].position.y,
                _spawnPositions[0].position.z),
            Quaternion.Euler(0, 0, Random.Range(-360, 360)));
        return newObject;
    }

    private GameObject SpawnWall()
    {
        GameObject newObject = Instantiate(_wall,
          new Vector3(
              (_spawnPositions[0].position.x + _spawnPositions[1].position.x) / 2,
              _spawnPositions[0].position.y,
              _spawnPositions[0].position.z),
          Quaternion.identity);
        return newObject;
    }
}
