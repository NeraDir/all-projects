using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPrefab : MonoBehaviour
{
    public abstract GameObject Object { get; set; }

    public abstract void ShowPageItems();

    public abstract void Init();
}
