using UnityEngine;
using UnityEngine.SceneManagement;

public class BrazingMangerAdditionaler : MonoBehaviour
{
    public string sceneName1;
    public string sceneName2;

    public void LoadBrazingScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene(sceneName1);
    }
    public void LaunchBrazingStart(string lovelyMana)
    {
        FindObjectOfType<BrzingMovementer>().BrzingMovementeIdenteficator = lovelyMana;
        SceneManager.LoadScene(sceneName2);
    }
}
