using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class LoadNextScene : MonoBehaviour
{
    public string SceneName;
    public float loadWaitng;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(loadWaitng);
        SceneManager.LoadScene(SceneName);
    }
}
