using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelWindow : Window
{
    [SerializeField] private LevelSettings _settings;

    [SerializeField] private Window _closeWindow;

    [SerializeField] private LevelItemComponent _itemPrefab;
    [SerializeField] private Transform _spawnPosition;

    public override void Init()
    {
        for (int i = 0; i < _settings.levelDatas.Length; i++)
        {
            LevelItemComponent newItem = Instantiate(_itemPrefab, _spawnPosition);
            newItem.Init(i,_closeWindow);
        }
        base.Init();
    }
}
