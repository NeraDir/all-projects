using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalSceneLoaderByEgupt : MonoBehaviour
{
    public void GlobalInThisSceneLoader()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("GameMenu");
    }
    public void EgyptAnothSceneLoad(string egyptstring)
    {
        FindObjectOfType<EgyptAspaScript>().egyptIDficator = egyptstring;
        SceneManager.LoadScene("BufferAnotherScene");
    }
}
