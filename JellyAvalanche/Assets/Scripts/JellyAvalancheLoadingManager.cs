using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JellyAvalancheLoadingManager : MonoBehaviour
{
    private IEnumerator Start()
    {
        GameObject jellyLoadingPrefab = Resources.Load("Prefabs/JellyLoading") as GameObject;
        Instantiate(jellyLoadingPrefab);
        yield return new WaitForSeconds(5f);
        Scene currentScene = SceneManager.GetActiveScene(); 
        Scene nextScene = SceneManager.CreateScene("JellyAvalacheMenu");
        SceneManager.SetActiveScene(nextScene);
        GameObject jellyMenu = Resources.Load("Prefabs/JellyMenu") as GameObject;
        Instantiate(jellyMenu);
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
