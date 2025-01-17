using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadControl : MonoBehaviour
{

    void Start()
    {
        Time.timeScale = 1;
        Invoke("StartGame",1);
    }

    private void StartGame ()
    {
        SceneManager.LoadScene(1);
    }
}
