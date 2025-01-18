using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip clipSound;

    [SerializeField]
    private AudioSource audioSource;

    public void ClickClack() 
    {
        audioSource.PlayOneShot(clipSound);
    }

    [SerializeField]
    private float timeWait;

    [SerializeField]
    private string SceneLoadName;

    private void Start()
    {
        StartCoroutine(Loading());
    }

    private IEnumerator Loading() 
    {
        yield return new WaitForSeconds(timeWait);
        SceneManager.LoadScene(SceneLoadName);
    }
}
