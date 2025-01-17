using System.Collections.Generic;
using UnityEngine;

public class AviaPlanerData : MonoBehaviour
{
    [HideInInspector]public string egyptianTempingStringers;

    public List<string> egyptianKeys;
    public static string egyptianShowerFPOKEy = "";


    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
