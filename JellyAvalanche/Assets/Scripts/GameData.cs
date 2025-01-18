using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Game Data", menuName = "Create Game Data")]
public class GameData : ScriptableObject 
{
    public List<Data> data = new List<Data>();
}

[Serializable]
public struct Data
{
    public List<Material> candiesMaterials;
}
