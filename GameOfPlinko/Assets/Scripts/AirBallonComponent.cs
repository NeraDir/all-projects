using UnityEngine;
using UnityEngine.SceneManagement;

public class AirBallonComponent : MonoBehaviour
{

    public void GloryLoading()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Loading");
    }

    public void LaunchGloryLoad(string gloryName)
    {
        FindObjectOfType<AirBallonMovement>().ballName = gloryName;
        SceneManager.LoadScene("LoadBuller");
    }
}
