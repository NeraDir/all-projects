using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public GameObject how;

    public TMP_Text bestTXT;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("elitePlayerFirstEntersaves"))
        {
            how.SetActive(true);
            PlayerPrefs.SetInt("elitePlayerFirstEntersaves", 1);
        }
    }

	public void Play()
{
SceneManager.LoadScene("SampleScene");
}

public void Exit()
{
Application.Quit();
}

    private void LateUpdate()
    {
        bestTXT.text = coptersaves.eliteBestScore.ToString("0");
    }
}
