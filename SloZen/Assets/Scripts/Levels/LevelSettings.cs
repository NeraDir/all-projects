using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create new Level Settings", fileName = "New Level Settings")]
public class LevelSettings : ScriptableObject
{
    public LevelData[] levelDatas;
}

[Serializable]
public struct LevelData
{
    public FruitType[] fruitsPerLevel;
}
