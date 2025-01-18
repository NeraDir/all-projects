using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public float time;

    private void OnEnable()
    {
        StartCoroutine(goGameScene());
    }

    private IEnumerator goGameScene()
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Menu");
    }
}
