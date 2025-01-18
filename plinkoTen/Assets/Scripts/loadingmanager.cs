using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadingmanager : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("mainapge");
    }
}
