using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToSomeSceneManager : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(ok), 0.4f);
    }

    private void ok()
    {
        SceneManager.LoadScene("LoadingScene");
    }
}
