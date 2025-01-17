using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoading : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Load());
    }

    private IEnumerator Load() 
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Menu");
    }
}
