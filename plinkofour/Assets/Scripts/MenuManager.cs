using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _howToPlayScreen;

    [SerializeField]
    private TMP_Text _bestScoreTxt;

    [SerializeField]
    private TMP_Text _bestLevelTxt;

    [SerializeField]
    private Image _musicImage;

    [SerializeField]
    private Image _soundImage;

    [SerializeField]
    private Sprite[] _soundSprites;

    [SerializeField]
    private Sprite[] _musicSprites;

    [SerializeField]
    private AudioSource _musicSource;

    [SerializeField]
    private AudioSource _soundSource;

    private AudioSource _currentMusicSource;
    private AudioSource _currentSoundSource;

    private void Start()
    {
        if (GameSavesManager.GameHowToPlayDisplayerValue != 1)
        {
            _howToPlayScreen.SetActive(true);
            GameSavesManager.GameHowToPlayDisplayerValue = 1;
        }
        _bestLevelTxt.text = GameSavesManager.GameBestReachLevelValue.ToString();
        _bestScoreTxt.text = "x" + GameSavesManager.GameBestReachScoreValue.ToString();
        _soundImage.sprite = GameSavesManager.SoundMuteState == 0 ? _soundSprites[0] : _soundSprites[1];
        _musicImage.sprite = GameSavesManager.MusicMuteState == 0 ? _musicSprites[0] : _musicSprites[1];
        if (FindObjectOfType<AudioSource>() != null)
        {
            _currentSoundSource.mute = GameSavesManager.SoundMuteState == 1 ? false : true;
            _currentMusicSource.mute = GameSavesManager.MusicMuteState == 1 ? false : true;
            return;
        }
        _currentMusicSource = Instantiate(_musicSource);
        _currentSoundSource = Instantiate(_soundSource);
        _currentSoundSource.mute = GameSavesManager.SoundMuteState == 1 ? false : true;
        _currentMusicSource.mute = GameSavesManager.MusicMuteState == 1 ? false : true;
        DontDestroyOnLoad(_currentSoundSource);
        DontDestroyOnLoad(_currentMusicSource);
    }

    public void OnClickMuteSound()
    {
        GameSavesManager.SoundMuteState = GameSavesManager.SoundMuteState == 1 ? 0 : 1;
        _currentSoundSource.mute = GameSavesManager.SoundMuteState == 1 ? false : true;
        _soundImage.sprite = _currentSoundSource.mute == true ? _soundSprites[0] : _soundSprites[1];
    }

    public void OnClickMuteMusic()
    {
        GameSavesManager.MusicMuteState = GameSavesManager.MusicMuteState == 1 ? 0 : 1;
        _currentMusicSource.mute = GameSavesManager.MusicMuteState == 1 ? false : true;
        _musicImage.sprite = _currentMusicSource.mute == true ? _musicSprites[0] : _musicSprites[1];
    }

    public void OnClickLaunchGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnClickQuitGame()
    {
        Application.Quit();
    }
}
