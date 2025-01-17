using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public AudioSource MusicSource;
    public AudioSource SoundSource;

    public AudioClip ClickSound;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        MusicSource.volume = SettingsManager.MusicVolume;
        SoundSource.volume = SettingsManager.SoundVolume;
    }

    public void PlayClick()
    {
        SoundSource.PlayOneShot(ClickSound);
    }

    public void GoMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
