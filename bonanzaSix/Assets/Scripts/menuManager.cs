using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _aboutPage;

    [SerializeField]
    private TMP_Text _showMaxReachLevel;

    private void Start()
    {
        _showMaxReachLevel.text = $"MAX REACHED LEVEL {GameManager.MaxReachLevel}";
        if (!PlayerPrefs.HasKey("candieCaptainAbousawScreen"))
        {
            _aboutPage.SetActive(true);
            PlayerPrefs.SetInt("candieCaptainAbousawScreen", 1);
        }
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnClickQui()
    {
        Application.Quit();
    }
}
