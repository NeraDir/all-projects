using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BettysLoadingManager : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("Menu");
    }
}
