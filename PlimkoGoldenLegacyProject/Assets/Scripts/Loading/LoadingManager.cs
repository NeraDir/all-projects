using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    [SerializeField]
    private float sceneLoadTime;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(sceneLoadTime);
        SceneManager.LoadScene(sceneName);
    }
}
