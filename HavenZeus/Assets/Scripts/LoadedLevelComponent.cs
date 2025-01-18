using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadedLevelComponent : MonoBehaviour
{
    public float time;

    public string level;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(level);
    }
}
