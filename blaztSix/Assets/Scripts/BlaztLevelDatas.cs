using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Level Data",menuName = "create new Level Data")]
public class BlaztLevelDatas : ScriptableObject
{
   public LevelData[] levelDatas;
}

[System.Serializable]
public class LevelData
{
    public int fallFruitsCount;
    public float speedValue;
    public Sprite[] levelFruits;
    public Sprite[] levelPlacesSprites;
}
