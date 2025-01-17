using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrazingMoverManager : MonoBehaviour
{
    public string brazingstring;

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
