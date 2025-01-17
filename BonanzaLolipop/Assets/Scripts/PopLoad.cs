using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopLoad : MonoBehaviour
{
    public void LoadMenu() 
    {
        SceneManager.LoadScene("PopMenuScene");
    }
}
