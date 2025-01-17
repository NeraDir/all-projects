using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Data of Merge",menuName = "create new data")]
public class MergesData : ScriptableObject
{
    public List<Merger> mergers;
}

[Serializable]
public class Merger
{
    public Mesh myMesh;
    public Mesh mergeMesh;
    public Mesh holeMaker;
}
