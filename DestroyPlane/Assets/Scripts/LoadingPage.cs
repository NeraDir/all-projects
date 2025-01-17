using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingPage : MonoBehaviour
{
    public float time;
    public string key;

    private void OnEnable()
    {
        StartCoroutine(GoHome());
    }

    private IEnumerator GoHome()
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(key);
    }
}
