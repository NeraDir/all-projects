using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Loading : MonoBehaviour
{
    public string sceneString;

    public float sceneLoadingTime;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(sceneLoadingTime);
        SceneManager.LoadScene(sceneString);
    }
}
