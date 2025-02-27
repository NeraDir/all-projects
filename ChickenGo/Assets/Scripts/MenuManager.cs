using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _howToPlay;
    [SerializeField] private Text _maxReachedLevel;

    public static int maxReachedLevel
    {
        get => PlayerPrefs.GetInt("MaxreachedLevel", 0);
        set => PlayerPrefs.SetInt("MaxreachedLevel", value);
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("ChickodsdgHowsdsgs"))
        {
            _howToPlay.SetActive(true);
            PlayerPrefs.SetInt("ChickodsdgHowsdsgs", 1);
        }
        _maxReachedLevel.text = "LEVEL " + maxReachedLevel.ToString();
    }

    public void OnClickPlay()
    {
        GameManager.level = 1;
        SceneManager.LoadScene("Game");
    }
}
