using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public float speeder;

    public string namer;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(speeder);
            SceneManager.LoadScene(namer);
        }
    }
}
