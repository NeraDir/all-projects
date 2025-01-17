using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoading : MonoBehaviour
{
    private void Awake()
    {
        Invoke(nameof(LoadLauncher), 0.3f);
    }
    private void LoadLauncher()
    {
        SceneManager.LoadScene("Launcher");
    }
}
