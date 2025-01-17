using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource au;
    void Start()
    {
        if (staticInfo.sound)
        {
            au.Play();
            Destroy(gameObject,1f);
        }
    }
}
