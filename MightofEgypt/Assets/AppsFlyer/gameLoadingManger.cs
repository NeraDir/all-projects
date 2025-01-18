using UnityEngine.SceneManagement;
using System;
using UnityEngine;

public class gameLoadingManger : MonoBehaviour
{
    [SerializeField]
    private string gameLoadingString;

    [SerializeField]
    private float gameLoadingTempValue;

    private void Start()
    {
        Invoke(nameof(Load), (float)gameLoadingTempValue);
    }

    private void Load() 
    {
        SceneManager.LoadScene(gameLoadingString);
    }
}
