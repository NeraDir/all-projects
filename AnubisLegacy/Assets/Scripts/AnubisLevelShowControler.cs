using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnubisLevelShowControler : MonoBehaviour
{
    [SerializeField]
    private AnubisLevelContent _contentPrefab;

    [SerializeField]
    private Transform _contentParent;

    [SerializeField]
    private int _levelsCount;

    private void Start()
    {
        SetLevels();
    }

    private void SetLevels()
    {
        for (int i = 0; i < _levelsCount; i++)
        {
            AnubisLevelContent newContent = Instantiate(_contentPrefab, _contentParent);
            newContent.Init(i);
        }
    }
}
