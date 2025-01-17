using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CrystallsConfig",menuName ="Create new Crystalls Config",order = 1)]
public class CrystallsParamConfig : ScriptableObject
{
    public List<CrystallData> CrystallsDatas = new ();
}

[Serializable]
public class CrystallData 
{
    public string Name;
    public int Index;
    public Sprite Sprite;
    public float Scale;
    public int Score;
}
