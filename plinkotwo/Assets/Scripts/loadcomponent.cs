using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadcomponent : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("menuScene");
    }
}
