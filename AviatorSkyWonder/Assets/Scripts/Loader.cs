using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    private float _loadingValue;

    private void LateUpdate()
    {
        _loadingValue += Time.deltaTime;
        if (_loadingValue >= 4)
        {
            SceneManager.LoadScene("Menu");
            _loadingValue = 0;
        }
    }
}
