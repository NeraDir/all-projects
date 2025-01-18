using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Animator m_Animator;

    public static int musicOn 
    {
        get 
        {
            if (PlayerPrefs.HasKey("MUSICONSAVEKEY"))
            {
                return PlayerPrefs.GetInt("MUSICONSAVEKEY");
            }
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("MUSICONSAVEKEY", value);
        } 
    }

    public static int soundOn
    {
        get
        {
            if (PlayerPrefs.HasKey("SOUNDONSAVEKEY"))
            {
                return PlayerPrefs.GetInt("SOUNDONSAVEKEY");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("SOUNDONSAVEKEY", value);
        }
    }

    [SerializeField]
    private AudioSource[] musicsSources;

    [SerializeField]
    private AudioSource[] soundsSources;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _sound;

    [SerializeField]
    private TMP_Text _musicButtonTXT;

    [SerializeField]
    private TMP_Text _soundButtonTXT;

    private void Awake()
    {
        foreach (var item in musicsSources)
        {
            if (musicOn == 1)
            {
                item.mute = true;
                _musicButtonTXT.text = "MUSIC OFF";
            }
            else
            {
                item.mute = false;
                _musicButtonTXT.text = "MUSIC ON";
            }
        }

        foreach (var item in soundsSources)
        {
            if (soundOn == 1)
            {
                item.mute = true;
                _soundButtonTXT.text = "SOUND OFF";
            }
            else
            {
                item.mute = false;
                _soundButtonTXT.text = "SOUND ON";
            }
        }
    }

    public void Play() 
    {
        _audioSource.PlayOneShot(_sound);
        m_Animator.SetBool("UI_STATE", true);
        Invoke(nameof(LoadGame), 0.5f);
    }

    private void LoadGame() 
    {
        SceneManager.LoadScene("Game");
    }

    public void OnClickOfSounds() 
    {
        if (soundOn == 1)
        {
            soundOn = 0;
        }
        else
        {
            soundOn = 1;
        }
        foreach (var item in soundsSources)
        {
            if (soundOn == 1)
            {
                item.mute = true;
                _soundButtonTXT.text = "SOUND OFF";
            }
            else
            {
                item.mute = false;
                _soundButtonTXT.text = "SOUND ON";
            }
        }
    }

    public void OnClickOfMusics() 
    {
        if (musicOn == 1)
        {
            musicOn = 0;
        }
        else
        {
            musicOn = 1;
        }

        foreach (var item in musicsSources)
        {
            if (musicOn == 1)
            {
                item.mute = true;
                _musicButtonTXT.text = "MUSIC OFF";
            }
            else
            {
                item.mute = false;
                _musicButtonTXT.text = "MUSIC ON";
            }
        }
    }

    public void Exit() 
    {
        _audioSource.PlayOneShot(_sound);
        Application.Quit();
    }
}
