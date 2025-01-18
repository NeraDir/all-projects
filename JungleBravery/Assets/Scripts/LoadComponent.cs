using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadComponent : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(6.7f);
        SceneManager.LoadScene("Menu");
    }
}
