using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiddayLoadControllerManager : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene("MiddayMenuScene");
    }
}
