using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CaramelCannonMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _caramelCannonGameInfoScreen;

    [SerializeField]
    private Text _caramelCannonWaveTxt;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("CaramelCannonGameInfoDataKey"))
        {
            _caramelCannonGameInfoScreen.SetActive(true);
            PlayerPrefs.SetInt("CaramelCannonGameInfoDataKey", 1);
        }
        _caramelCannonWaveTxt.text = CaramelCanonGameManager.CaramelCannonMaxReachedWave.ToString("0");
    }

    public void OnClickCannonPaly()
    {
        SceneManager.LoadScene("CaramelGame");
    }

    public void OnClickCannonExit()
    {
        Application.Quit();
    }
}
