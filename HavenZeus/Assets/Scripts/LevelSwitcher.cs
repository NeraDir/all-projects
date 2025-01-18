using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour
{
    public void LoadLevel(string index)
    {
        SceneManager.LoadScene(index);
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
