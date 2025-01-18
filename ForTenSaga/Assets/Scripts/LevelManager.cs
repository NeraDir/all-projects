using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int _wallsCountPerLevel = 1;
    
    [SerializeField] private TMP_Text[] _levelTexts;

    [SerializeField] private WallSpawnerComponent _wallSpawnerComponent;
    [SerializeField] private FallTrapsSpawner _fallTrapsSpawner;
    
    public void Init()
    {
        _wallsCountPerLevel = 1;
        for (int i = 0; i < GameManager.TigerCurrentLevel; i++)
        {
            _wallsCountPerLevel += 2;
        }
        _wallSpawnerComponent.Init(_wallsCountPerLevel);
        _fallTrapsSpawner.Init();
        foreach (var item in _levelTexts)
        {
         item.text = "LEVEL " + GameManager.TigerCurrentLevel.ToString();   
        }
    }
}
