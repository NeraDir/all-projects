using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadControl : MonoBehaviour
{

    [SerializeField]
    private string sceneNAme;

    [SerializeField]
    private float loadTime;

    void Start()
    {
        Invoke("StartGame", loadTime);
    }

    private void StartGame ()
    {
        SceneManager.LoadScene(sceneNAme);
    }
}
