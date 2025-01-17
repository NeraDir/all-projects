using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Loading : MonoBehaviour
{

    private void OnEnable()
    {
        Invoke(nameof(LoadMenu), 2.5f);
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("scenes_menu");
    }
}
