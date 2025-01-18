using UnityEngine.SceneManagement;
using UnityEngine;

public class Laing : MonoBehaviour
{
    public int loadingIndex;

    private float timerg;
    private void LateUpdate()
    {
        timerg += Time.deltaTime;
        if (timerg >= 0.2f)
        {
            SceneManager.LoadScene(loadingIndex);
        }
    }
}
