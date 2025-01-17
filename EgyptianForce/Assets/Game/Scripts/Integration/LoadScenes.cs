using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    public float WaitingTime;
    public string SceneName;

    void Start()
    {
        StartCoroutine(SceneLoader());
    }

    IEnumerator SceneLoader()
    {
        yield return new WaitForSeconds(WaitingTime);
        SceneManager.LoadScene(SceneName);
    }
}
