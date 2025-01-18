using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillTheLevelsCollection : MonoBehaviour
{
    public List<LevelSO> levls = new();
    public LevelItemUI prefab;
    public Transform Content;

    private void Start()
    {
        foreach (LevelSO level in levls)
        {
            LevelItemUI buff = Instantiate(prefab, Content);
            buff.Init(level);
        }
    }
}
