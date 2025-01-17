using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoOnApp : MonoBehaviour
{
    public string scene;

    void Start()
    {
        Invoke(nameof(SceneSwitch), 0.3f);
    }

    public void SceneSwitch()
    {
        SceneManager.LoadScene(scene);
    }
}
