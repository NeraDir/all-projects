using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManagers : MonoBehaviour
{
    public string str;

    private void OnEnable()
    {
        StartCoroutine(set());
    }

    private IEnumerator set()
    {
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene(str);
    }
}
