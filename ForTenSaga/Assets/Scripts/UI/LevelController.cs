using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private void Awake()
    {
        SetLevelVisual();
    }

    private void SetLevelVisual()
    {
        LevelElementComponent[] levelElements = transform.GetComponentsInChildren<LevelElementComponent>();
        for (int i = 0; i < levelElements.Length; i++)
        {
            levelElements[i].Init(i);
        }
    }
}
