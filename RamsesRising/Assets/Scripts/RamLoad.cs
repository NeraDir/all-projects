using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RamLoad : MonoBehaviour
{
    [SerializeField]
    private string scene;
    [SerializeField]
    private float time;

    private void Start()
    {
        Invoke(nameof(LoadScene),time);
    }

    private void LoadScene() 
    {
        SceneManager.LoadScene(scene);
    }
}
