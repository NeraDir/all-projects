using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawnerComponent : MonoBehaviour
{
    public static Action playerReachedWall;
    
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private GameObject _finishWall;
    
    [SerializeField]
    private List<GameObject> _walls = new List<GameObject>();

    private int _perLevelWalls = 0;

    private GameObject _lastFinishWall;
    
    public void Init(int wallCount)
    {
        _perLevelWalls = wallCount;
        for (int i = 0; i < 2; i++)
        {
            OnWallReached();
        }
        playerReachedWall += OnWallReached;
        playerReachedWall += OnDestroyFirstWall;
    }

    private void OnDestroy()
    {
        playerReachedWall -= OnWallReached;
        playerReachedWall -= OnDestroyFirstWall;
    }

    private void OnDestroyFirstWall()
    {
        Destroy(_walls[0].gameObject);
        _walls.RemoveAt(0);
    }

    private void OnWallReached()
    {
        if (_lastFinishWall != null)
            return;
        Transform lastWall = _walls[_walls.Count - 1].transform;
        Vector3 spawnPostion = new Vector3(lastWall.position.x, lastWall.position.y + lastWall.localScale.y, lastWall.position.z);
        if (_walls.Count < _perLevelWalls)
        {
            _walls.Add(Instantiate(_wallPrefab,spawnPostion, Quaternion.identity));
        }

        if (_walls.Count >= _perLevelWalls)
        { 
            Transform lastWall2 = _walls[_walls.Count - 1].transform;
            Vector3 spawnPostion2 = new Vector3(lastWall2.position.x, lastWall2.position.y + lastWall2.localScale.y, lastWall2.position.z);
            _lastFinishWall = Instantiate(_finishWall,spawnPostion2, Quaternion.identity);
        }
    }
}
