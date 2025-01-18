using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadingcontroller : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Menu");
    }
}
