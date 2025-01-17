using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private static Music instance;
    public static Music Instance => instance;
    public float Volume => musicSource.volume;

    [SerializeField] private AudioSource musicSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        if (PlayerPrefs.HasKey("volume"))
            musicSource.volume = PlayerPrefs.GetFloat("volume");
    }

    private void Update()
    {
    }

    public void SetVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("volume", volume);

    }
}
