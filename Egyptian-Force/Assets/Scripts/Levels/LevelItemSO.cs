using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class LevelItemSO : ScriptableObject
{
    public List<Level> Levels = new();
}

[System.Serializable]
public struct Level
{
    public int FieldSize;
    public int CellSize;
    public int WinScore;

    public string LevelName;
}
