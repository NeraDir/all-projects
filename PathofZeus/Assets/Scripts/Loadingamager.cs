using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loadingamager : MonoBehaviour
{
    public string menuIndex;

    public float timeload;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(timeload);
        SceneManager.LoadScene(menuIndex);
    }
}
