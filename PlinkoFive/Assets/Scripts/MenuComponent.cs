using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuComponent : MonoBehaviour
{
    public GameObject howToPlayScreen;
    public GameObject menuScreen;

    public TMP_Text bestRecordTxt;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("PortalSphereHowToPlaydsgsagsatgsSave"))
        {
            howToPlayScreen.SetActive(true);
            menuScreen.SetActive(true);
            PlayerPrefs.SetInt("PortalSphereHowToPlaydsgsagsatgsSave", 1);
        }
        bestRecordTxt.text = GameCompoentn.BestRecord.ToString();
    }

    public void OnClickButton(int index)
    {
        switch (index)
        {
            case 0:
                SceneManager.LoadScene("SphereGame");
                break;
            case 1:
                Application.Quit();
                break;
        }
    }
}
