using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingDillivery : MonoBehaviour
{
    [SerializeField]
    private float _sceneLoadingTime;

    [SerializeField]
    private string _sceneLoadingName;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_sceneLoadingTime);
        SceneManager.LoadScene(_sceneLoadingName);
    }
}
