using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public bool canLoad;

    public string name;

    public float time;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(time);
        if (canLoad)
        {
            SceneManager.LoadScene(name);
        }
    }


    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
