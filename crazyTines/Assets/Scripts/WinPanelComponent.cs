using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPanelComponent : MonoBehaviour
{
    public void OnEnd()
    {
        gameObject.SetActive(false);
    }
}
