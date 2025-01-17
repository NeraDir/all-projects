using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChaseLoadManager : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(6);
        Scene currentScene = SceneManager.GetActiveScene();
        Scene nextScene = SceneManager.CreateScene("CheaseMenuScene");
        SceneManager.SetActiveScene(nextScene);
        SceneManager.UnloadScene(currentScene);
        GameObject menuObject = Resources.Load<GameObject>("Prefabs/ChaseMenuPrefab");
        Instantiate(menuObject);
    }
}
