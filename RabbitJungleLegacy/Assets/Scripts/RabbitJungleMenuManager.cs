using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RabbitJungleMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _jungleGameInfoScreen;

    [SerializeField]
    private Text _jungleBestScoreShow;

    [SerializeField]
    private RabbitJungleBuyComponent[] _buyComponents;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("RabbitJungleHowToPlayScreenDisplayedSaveKey"))
        {
            _jungleGameInfoScreen.SetActive(true);
            PlayerPrefs.SetInt("RabbitJungleSkinState" + 0 + "SaveKey", 1);
            PlayerPrefs.SetInt("RabbitJungleHowToPlayScreenDisplayedSaveKey", 1);
        }
        foreach (var item in _buyComponents)
        {
            item.Init();
        }
       
    }

    private void LateUpdate()
    {
        _jungleBestScoreShow.text = RabbitJungleGameManager.rabbitJungleBestRecord.ToString() + "G";
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnClickExit() 
    {
        Application.Quit();
    }
}
