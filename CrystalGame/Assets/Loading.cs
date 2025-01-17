using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [SerializeField]
    private string nameer;

    [SerializeField]
    private float timer;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene(nameer);
    }
}
