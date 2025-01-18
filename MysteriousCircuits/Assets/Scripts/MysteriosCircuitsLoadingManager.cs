using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MysteriosCircuitsLoadingManager : MonoBehaviour
{
    private IEnumerator Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        yield return new WaitForSeconds(3);
        Scene nextScene = SceneManager.CreateScene("MysteriousCircuitsMenuScene");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(nextScene);
        GameObject menuCanvas = Resources.Load("Prefabs/MysteriousCircuitsMenu") as GameObject;
        Instantiate(menuCanvas);
        SceneManager.UnloadScene(currentScene);
    }
}
