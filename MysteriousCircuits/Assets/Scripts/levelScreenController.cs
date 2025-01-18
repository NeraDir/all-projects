using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelScreenController : MonoBehaviour
{
    [SerializeField]
    private levelComponent _levelComponentPrefab;

    [SerializeField]
    private Transform _levelsParent;

    [SerializeField]
    private int _levelsCount;

    public void Awake()
    {
        for (int i = 0; i < _levelsCount; i++)
        {
            levelComponent newLevel = Instantiate(_levelComponentPrefab, _levelsParent);
            newLevel.Init(i);
        }
    }
}
