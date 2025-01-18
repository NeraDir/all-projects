using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDatas",menuName ="CreateNewLevels")]
public class LeprecaountLevelDatas : ScriptableObject
{
    public LevelData[] levelDatas;
}

[System.Serializable]
public class LevelData
{
    public string Quetion;
    public List<string> Answers = new List<string>();
}
