using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_LoadingLayer : MonoBehaviour
{
    public void SetMenuScene()
    {
        SceneManager.LoadScene("JellyPeaks_MENU_SCENE");
    }
}
