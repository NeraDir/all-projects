using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CandysGameLoading : MonoBehaviour
{
    [SerializeField]
    private string sceneIndex;

    [SerializeField]
    private float sceneLoadingTime;

    private void Start()
    {
        StartCoroutine(StartLoading());
    }

    private IEnumerator StartLoading() 
    {
        yield return new WaitForSeconds(sceneLoadingTime);
        SceneManager.LoadScene(sceneIndex);
    }
}
