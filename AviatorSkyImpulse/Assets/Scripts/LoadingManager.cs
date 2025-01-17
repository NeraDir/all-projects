using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("Menu");
    }
}
