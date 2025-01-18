using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MneuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _aboutGameInfoScreen;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("PunkCrystallsGameInfoSoiosidgisddsSave"))
        {
            _aboutGameInfoScreen.SetActive(true);
            PlayerPrefs.SetInt("PunkCrystallsGameInfoSoiosidgisddsSave", 1);
        }
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("main");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
