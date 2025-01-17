using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jar : MonoBehaviour
{
    [SerializeField] private List<GameObject> jars;

    private void Start()
    {
        Instantiate(jars[PlayerPrefs.GetInt("Skin")], transform);
    }
}
