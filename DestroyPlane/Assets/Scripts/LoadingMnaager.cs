using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingMnaager : MonoBehaviour
{
    public string sceneName;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("LoadingGame");
    }
}
