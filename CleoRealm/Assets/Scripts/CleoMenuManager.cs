using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CleoMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _cleoHowPlayPage;

    [SerializeField]
    private Text _cleoShowBestScore;

    public static int CleoBestScoreValue 
    {
        get 
        {
            if (PlayerPrefs.HasKey("cleoBestScoreSaveKey"))
                return PlayerPrefs.GetInt("cleoBestScoreSaveKey");
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("cleoBestScoreSaveKey", value);
        }
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("cleoHowPlayShowedSaveKey"))
        {
            _cleoHowPlayPage.SetActive(true);
            PlayerPrefs.SetInt("cleoHowPlayShowedSaveKey", 1);
        }
        _cleoShowBestScore.text = CleoBestScoreValue.ToString();
    }

    public void OnClickCleoOpenGameScene() 
    {
        SceneManager.LoadScene("CleoGame");
    }

    public void OnClickCleoCloseGame() 
    {
        Application.Quit();
    }
}
