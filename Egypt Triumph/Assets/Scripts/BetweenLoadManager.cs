using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BetweenLoadManager : MonoBehaviour
{
    [SerializeField]
    private string nameOfScene;

    private float valueScene;
    [SerializeField]
    private float needvalue;

    private void LateUpdate() 
    {
        valueScene += Time.deltaTime;
        if (valueScene >= needvalue) 
        {
            SceneManager.LoadScene(nameOfScene);
        }
    }
}
