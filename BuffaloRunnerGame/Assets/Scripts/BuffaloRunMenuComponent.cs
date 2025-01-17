using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BuffaloRunMenuComponent : MonoBehaviour
{
    [SerializeField]
    private AudioSource _musicSource;

    [SerializeField]
    private AudioSource _soundSource;

    [SerializeField]
    private TMP_Text[] _start_coinsTxt;

    [SerializeField]
    private TMP_Text _bestScoreTxt;

    [SerializeField]
    private GameObject _buffaloAboutPage;

    public BuffaloRunGameBuyComponent[] _buffaloBuyComponents;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("BuffaloGameAboutShowsData"))
        {
            _buffaloAboutPage.SetActive(true);
            _buffaloBuyComponents[0].OnBuy();
            PlayerPrefs.SetInt("BuffaloGameAboutShowsData", 1);
        }
        List<AudioSource> musics = new List<AudioSource>();
        List<AudioSource> sounds = new List<AudioSource>();
        if (FindObjectsOfType<AudioSource>().Length > 2)
        {
            foreach (var item in FindObjectsOfType<AudioSource>())
            {
                if (item.name.Contains("Music"))
                {
                    musics.Add(item);
                }
                else
                {
                    sounds.Add(item);
                }
            }
            Destroy(musics[1].gameObject);
            Destroy(sounds[1].gameObject);
            _musicSource = musics[0];
            _soundSource = sounds[0];
        }
        else
        {
            DontDestroyOnLoad(_musicSource.gameObject);
            DontDestroyOnLoad(_soundSource.gameObject);
        }
        _musicSource.volume = BuffaloRunGameController.BuffaloMusicVolume;
        _soundSource.volume = BuffaloRunGameController.BuffaloSoundVolume;
    }

    public void OnChangeSound(Slider slider)
    {
        BuffaloRunGameController.BuffaloSoundVolume = slider.value;
        _soundSource.volume = BuffaloRunGameController.BuffaloSoundVolume;
    }

    public void OnChangeMusic(Slider slider)
    {
        BuffaloRunGameController.BuffaloMusicVolume = slider.value;
        _musicSource.volume = BuffaloRunGameController.BuffaloMusicVolume;
    }

    private void LateUpdate()
    {
        foreach (var item in _start_coinsTxt)
        {
            item.text = "x" + BuffaloRunGameController.BuffaloCoins.ToString();
        }
        _bestScoreTxt.text = BuffaloRunGameController.BuffaloMaxScore.ToString();
    }

    public void OnClickStartGame()
    {
        SceneManager.LoadScene("BuffaloGames");
    }

    public void OnClickCloseGame()
    {
        Application.Quit();
    }
}
