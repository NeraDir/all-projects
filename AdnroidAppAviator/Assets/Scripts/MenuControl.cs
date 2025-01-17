using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    [SerializeField]
    private Text starsText;
    [SerializeField]
    private Text oilText;
    [SerializeField]
    private Text speedText;
    private void Start()
    {
        Time.timeScale = 1;
        if (!PlayerPrefs.HasKey("stars")) {
            PlayerPrefs.SetInt("stars", 0);
            PlayerPrefs.SetInt("speed", 5);
            PlayerPrefs.SetInt("oil", 5);
        }
        starsText.text = "Stars : " + PlayerPrefs.GetInt("stars");
        speedText.text = "+Speed \n" + "20 stars \n" + "Current : " + PlayerPrefs.GetInt("speed");
        oilText.text = "+Oil \n" + "10 stars \n" + "Current : " + PlayerPrefs.GetInt("oil");
    }

    public void PlayBttn ()
    {
        SceneManager.LoadScene(2);
    }
    public void ExitBttn ()
    {
        Application.Quit();
    }

    public void SpeedUp ()
    {
        if (PlayerPrefs.GetInt("stars")>= 20)
        {
            PlayerPrefs.SetInt("stars", PlayerPrefs.GetInt("stars") - 20);
            PlayerPrefs.SetInt("speed", PlayerPrefs.GetInt("speed") + 1);
            starsText.text = "Stars : " + PlayerPrefs.GetInt("stars");
            speedText.text = "+Speed \n" + "20 stars \n" + "Current : " + PlayerPrefs.GetInt("speed");
        }
    }
    public void OilUp ()
    {
        if (PlayerPrefs.GetInt("stars") >= 10)
        {
            PlayerPrefs.SetInt("stars", PlayerPrefs.GetInt("stars") - 10);
            PlayerPrefs.SetInt("oil", PlayerPrefs.GetInt("oil") + 1);
            starsText.text = "Stars : " + PlayerPrefs.GetInt("stars");
            oilText.text = "+Oil \n" + "10 stars \n" + "Current : " + PlayerPrefs.GetInt("oil");
        }
    }
}
