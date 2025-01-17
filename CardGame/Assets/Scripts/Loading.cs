using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [SerializeField]
    private string nextSceneKey;
    [SerializeField]
    private float loadTime;


    private void OnEnable()
    {
        StartCoroutine(loadScene());
    }

    private IEnumerator loadScene()
    {
        yield return new WaitForSeconds(loadTime);
        SceneManager.LoadScene(nextSceneKey);
    }
}
