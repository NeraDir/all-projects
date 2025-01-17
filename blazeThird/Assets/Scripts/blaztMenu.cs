using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class blaztMenu : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _bestRecord;

    [SerializeField]
    private GameObject _howtoplayPage;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("blaztFusionhowtoplaypagesaves"))
        {
            _howtoplayPage.SetActive(true);
            PlayerPrefs.SetInt("blaztFusionhowtoplaypagesaves", 1);
        }
        _bestRecord.text = blaztGame.blaztfusionBestRecordCount.ToString();
    }

    public void OnClickPaly()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
