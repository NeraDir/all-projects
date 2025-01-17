using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loading : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene("menu");
    }
}
