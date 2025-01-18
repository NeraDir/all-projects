using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class LoadingScript : MonoBehaviour
{
    public string SceneIndex;

    public float timeLoad;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(timeLoad);
        SceneManager.LoadScene(SceneIndex);
    }
}
