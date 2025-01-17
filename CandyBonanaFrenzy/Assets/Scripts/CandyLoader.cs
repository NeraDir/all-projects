using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CandyLoader : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene(3);
    }
}
