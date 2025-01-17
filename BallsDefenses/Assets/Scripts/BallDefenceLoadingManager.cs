using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallDefenceLoadingManager : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("BallsDefenceMenuScene");
    }
}
