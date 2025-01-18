using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimLoad : MonoBehaviour
{
    public string Name;

    public float time;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(Name);
    }
}
