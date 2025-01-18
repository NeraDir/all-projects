using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingComponent : MonoBehaviour
{
    [SerializeField]
    private float LoadingValue;

    [SerializeField]
    private string LoadingLevelName;


    private float LoadingProgressValue;

    private void Start() 
    {
        LoadingProgressValue = 0;
    }

    private void LateUpdate()
    {
        LoadingProgressValue += Time.deltaTime;
        if (LoadingProgressValue >= LoadingValue)
        {
            SceneManager.LoadScene(LoadingLevelName);
        }
    }
}
