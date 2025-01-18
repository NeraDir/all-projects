using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class SceneLoadHandler : MonoBehaviour
{
    [HideInInspector] public int SceneLoadDelay = 0;

    public void LoadScene(string sceneIndex) => SceneManager.LoadScene(sceneIndex);

    public void SetSceneLoadDelay(int targetDelay) => SceneLoadDelay = targetDelay;

    public void LoadSceneWithDelay(string sceneIndex) => StartCoroutine(SceneLoadWithDelay(sceneIndex));

    private IEnumerator SceneLoadWithDelay(string sceneIndex) {
        yield return new WaitForSeconds(SceneLoadDelay); LoadScene(sceneIndex);
    }

    public void OnSetOrientaitieon()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    public void OnSetOrientaitieon2()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
