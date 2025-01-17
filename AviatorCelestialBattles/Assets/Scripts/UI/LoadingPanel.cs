using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingPanel : MonoBehaviour
{
    public void LoadMenuscene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
