using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalJumpsMenuComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject _howToPlayScreen;

    [SerializeField]
    private TMP_Text _maxShow;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("PortalJumpsHowToPlayScreen"))
        {
            _howToPlayScreen.SetActive(true);
            PlayerPrefs.SetInt("PortalJumpsHowToPlayScreen", 1);
        }
        _maxShow.text = PortalSpawnRoadsComponent.MaxLevel.ToString();
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("PortalGameScene");
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
