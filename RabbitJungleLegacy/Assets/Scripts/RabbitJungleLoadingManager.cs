using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RabbitJungleLoadingManager : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MenuScene");
    }
}
