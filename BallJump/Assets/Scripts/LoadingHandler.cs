using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingHandler : MonoBehaviour
{

    [SerializeField]
    private float time;
    [SerializeField]
    private nextKey key;

    private void Awake()
    {
        Invoke(nameof(Load), time);
    }


    public void Load()
    {

        if (key == nextKey.Menu)
        {
            SceneManager.LoadScene("Menu");
        }
        else if (key == nextKey.AppCheck)
        {
            SceneManager.LoadScene("OracleMystery_MainHandler");
        }

       
    }
}

public enum nextKey
{
    Menu,
    AppCheck
}