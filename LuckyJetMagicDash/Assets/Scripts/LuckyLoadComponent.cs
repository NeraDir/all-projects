using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LuckyLoadComponent : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("LuckyMenuScen");
    }
}
