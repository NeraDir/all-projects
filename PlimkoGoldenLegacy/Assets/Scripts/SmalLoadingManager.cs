using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SmalLoadingManager : MonoBehaviour
{
    [SerializeField]
    private float smallLoadTime;

    [SerializeField]
    private string smallLoadSceneName;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(smallLoadTime);
        SceneManager.LoadScene(smallLoadSceneName);
    }
}
