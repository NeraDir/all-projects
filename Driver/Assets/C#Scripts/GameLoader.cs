using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameLoader : MonoBehaviour
{
    [SerializeField]
    private float waitingTime;

    [SerializeField]
    private int loading;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(loading);
        SceneManager.LoadScene(loading);
    }
}
