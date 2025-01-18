using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "crystall", menuName = "new crystall", order = 1)]
public class crystallspresetconfigcomponent : ScriptableObject
{
    public List<crystallParam> crystallParams = new();
}

[Serializable]
public class crystallParam
{
    public int Index;
    public Sprite Sprite;
    public float Scale;
    public int Score;
}
