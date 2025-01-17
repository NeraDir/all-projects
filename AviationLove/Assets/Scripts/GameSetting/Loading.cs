using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public float time;
    public string Scene;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(Scene);
    }
}
