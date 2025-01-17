using UnityEngine.SceneManagement;
using UnityEngine;

public class Menumanager : MonoBehaviour
{

    public void onClickPlay() 
    {
        SceneManager.LoadScene("Game");
    }

    public void onClickClosegame() 
    {
        Application.Quit();
    }
}
