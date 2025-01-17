using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loading : MonoBehaviour
{
    public float time;

    public string txt;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(txt);
    }
}
