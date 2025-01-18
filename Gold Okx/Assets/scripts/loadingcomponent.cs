using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadingcomponent : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene("menuscene");
    }
}
