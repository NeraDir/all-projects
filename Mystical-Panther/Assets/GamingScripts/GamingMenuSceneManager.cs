using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GamingMenuSceneManager : MonoBehaviour
{
    [SerializeField]
    private Button m_gamingPlayButton;

    [SerializeField]
    private Button m_GamingExitButton;

    [SerializeField]
    private Image m_GamingHowToPlayPage;

    [SerializeField]
    private int m_GamingLoadingIndex;

    private int timingTime = 15;

    private int timingPrice = 100;

    public static int startingTime 
    {
        get
        {
            if (PlayerPrefs.HasKey("startingTimeUpradeVal"))
            {
                return PlayerPrefs.GetInt("startingTimeUpradeVal");
            }
            return 15;
        }
        set
        {
            PlayerPrefs.SetInt("startingTimeUpradeVal", value);
        }
    }

    [SerializeField]
    private TMP_Text timeingShower;

    private void Awake()
    {
        timingTime = startingTime;
        m_gamingPlayButton.onClick.AddListener(onClickPlay);
        m_GamingExitButton.onClick.AddListener(onClickExit);

        if (!PlayerPrefs.HasKey("GamingLoadingFirstHowToPlay"))
        {
            m_GamingHowToPlayPage.gameObject.SetActive(true);
            PlayerPrefs.SetInt("GamingLoadingFirstHowToPlay", 1);
        }
    }

    private void onClickPlay() 
    {
        GamingSnakeSpawner.timeing = timingTime;
        SceneManager.LoadScene(m_GamingLoadingIndex);
    }

    public void onSetTime()
    {
        if (GamingPlayerData.playerPoints > timingPrice)
        {
            timingTime++;
            GamingPlayerData.playerPoints -= timingPrice;
        }
    }

    private void LateUpdate()
    {
        timeingShower.text = timingTime.ToString();
    }

    private void onClickExit() 
    {
        Application.Quit();
    }
}
