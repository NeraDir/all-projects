using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovementConfig : MonoBehaviour
{
    public string planeSpeed;

    public List<string> planerSkinsName;
    public string planehealth;
    public string planeNem;


    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
