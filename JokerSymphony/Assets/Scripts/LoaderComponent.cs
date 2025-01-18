using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderComponent : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(WaitAndLoad());
    }

    private IEnumerator WaitAndLoad() 
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Menu");
    }
}
