using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScript : MonoBehaviour
{
    public float timer;

    public string namescene;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene(namescene);
    }
}
