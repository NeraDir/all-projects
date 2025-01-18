using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StarsGameMenuComponent : MonoBehaviour
{
    [SerializeField]
    private Text _starsShowBestScore;

    [SerializeField]
    private GameObject _starsGameInstructionPage;

    private int _starsInstructionShowed
    {
        get
        {
            if (PlayerPrefs.HasKey("StarsGameInstructionShowedKey"))
                return PlayerPrefs.GetInt("StarsGameInstructionShowedKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("StarsGameInstructionShowedKey", value);
        }
    }

    public static int _starsGameBestScore
    {
        get
        {
            if (PlayerPrefs.HasKey("StarsGameBestScoreKey"))
                return PlayerPrefs.GetInt("StarsGameBestScoreKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("StarsGameBestScoreKey", value);
        }
    }

    private void Awake()
    {
        if (_starsInstructionShowed != 1)
        {
            _starsInstructionShowed = 1;
            _starsGameInstructionPage.SetActive(true);
        }
        _starsShowBestScore.text = _starsGameBestScore.ToString();
    }

    public void OnClickStarPlayOpen()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnClickStarGameQuit()
    {
        Application.Quit();
    }
}
