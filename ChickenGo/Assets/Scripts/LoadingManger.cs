using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManger : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Menu");
    }
}
