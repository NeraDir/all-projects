using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class loodingManager : MonoBehaviour
{
    [SerializeField]
    private float loadingTime;

    [SerializeField]
    private string laodingScene;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(loadingTime);
        SceneManager.LoadScene(laodingScene);
    }
}
