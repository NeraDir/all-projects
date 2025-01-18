using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LostLoading : MonoBehaviour
{
    public float load;

    public string loadname;

    private float loadtime;

    private void LateUpdate()
    {
        loadtime += Time.deltaTime;
        if (loadtime >= load)
        {
            SceneManager.LoadScene(loadname);
        }
    }
}
