using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnubisLegacyLoading : MonoBehaviour
{
    private IEnumerator Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        yield return new WaitForSeconds(3);
        Scene currentScene = SceneManager.GetActiveScene();
        Scene nextScene = SceneManager.CreateScene("AnubisLegacyMenuScene");
        SceneManager.SetActiveScene(nextScene);
        GameObject menuObject = Resources.Load("Prefabs/AnubisMenu") as GameObject;
        Instantiate(menuObject);
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
