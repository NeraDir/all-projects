using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "create new Level Data")]
public class LevelDatas : ScriptableObject
{
    public List<string> levelPattern = new List<string>();
}


