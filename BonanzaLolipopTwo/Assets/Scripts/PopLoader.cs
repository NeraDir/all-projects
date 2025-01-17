using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopLoader : MonoBehaviour
{
    [SerializeField] 
    private List<string> _popingLoaderStrings;
    private string _adidString;

    private void Awake()
    {
        SceneManager.LoadScene("PopLoading");
    }
}
