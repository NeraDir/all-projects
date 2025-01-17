using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinPanel : MonoBehaviour
{
    public TMP_Text TimeFF;

    public void Init(float _time)
    {
        TimeFF.text = "Time: " + _time;
    }
}
