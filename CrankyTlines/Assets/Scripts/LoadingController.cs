using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Menu");
    }
}
