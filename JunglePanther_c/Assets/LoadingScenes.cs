using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScenes : MonoBehaviour
{
    [SerializeField]
    private float Time;

    [SerializeField]
    private string SceneName;

    private void OnEnable()
    {
        StartCoroutine(setTimer());
    }

    private IEnumerator setTimer()
    {
        yield return new WaitForSeconds(Time);
        SceneManager.LoadScene(SceneName);
    }
}
