using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nimbleLoader : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(Load());
    }

    private IEnumerator Load()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("nimbleGameMenuScene");
    }
}
