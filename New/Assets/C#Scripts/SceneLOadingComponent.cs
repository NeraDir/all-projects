using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLOadingComponent : MonoBehaviour
{
    [SerializeField]
    private int LoadSceneIndex;

    [SerializeField]
    private float LoadingSceneTime;

    private void Start()
    {
        StartCoroutine(LoadingScene());
    }

    private IEnumerator LoadingScene() 
    {
        yield return new WaitForSeconds(LoadingSceneTime);
        SceneManager.LoadScene(LoadSceneIndex);
    }
}
