using UnityEngine;
using UnityEngine.SceneManagement;

namespace MenuLevels
{
    public class MenuHelper : MonoBehaviour
    {
        public void StartGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Game");
        }

        public void OpenMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu");
        }

        public void OpenShop()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Shop");
        }

        public void Restart()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


    public void ExitGame() => Application.Quit();
    }
}