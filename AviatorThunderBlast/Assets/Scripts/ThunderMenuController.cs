using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ThunderMenuController : MonoBehaviour
{
    public GameObject howTOPlayGameWindow;

    public Text showThunderBestDistance;

    public Text showThunderMaxStars;

    [Space(10)]
    [Header("Plane Buy Config")]
    public Text[] displayPlanePrices;

    public Image[] displayPlanesStates;

    public int[] planePrices;

    [Space(10)]
    [Header("Plane Buy State")]
    public Sprite starSprite;

    public Sprite selectedSprite;

    public Sprite unselectedSprite;


    private void Start()
    {
        if (!PlayerPrefs.HasKey("ThunderHowPlayGameWindowShowedSave"))
        {
            howTOPlayGameWindow.SetActive(true);
            PlayerPrefs.SetInt("ThunderHowPlayGameWindowShowedSave", 1);
        }
        PlayerPrefs.SetInt($"ThunderPlaneBuyed{0}", 1);
        showThunderBestDistance.text = GameManager.thunderBestDistanceReached.ToString("0.00") + "m";
        showThunderMaxStars.text = "X" + GameManager.thunderMaxStarsEarnedCount.ToString("0");
        CheckPlaneBuyBarState();

    }

    public void OnButtonPressed(string input) 
    {
        switch (input) 
        {
            case "Game":
                OnGameLoad();
                break;
            case "Close":
                OnGameClose();
                break;
            default:
                BuyAndEquipPlane(input);
                showThunderMaxStars.text = "X" + GameManager.thunderMaxStarsEarnedCount.ToString("0");
                break;
        }
    }

    private void BuyAndEquipPlane(string inputer) 
    {
        int index = System.Convert.ToInt32(inputer);
        if (!PlayerPrefs.HasKey($"ThunderPlaneBuyed{index}"))
        {
            if (GameManager.thunderMaxStarsEarnedCount >= planePrices[index])
            {
                GameManager.thunderMaxStarsEarnedCount -= planePrices[index];
                GameManager.thunderPlaneSelectedIndex = index;
                PlayerPrefs.SetInt($"ThunderPlaneBuyed{index}", 1);
                CheckPlaneBuyBarState();
            }
        }
        else
        {
            GameManager.thunderPlaneSelectedIndex = index;
            CheckPlaneBuyBarState();
        }
    }

    private void CheckPlaneBuyBarState() 
    {
        for (int i = 0; i < 4; i++)
        {
            if (!PlayerPrefs.HasKey($"ThunderPlaneBuyed{i}"))
            {
                displayPlanePrices[i].text = planePrices[i].ToString();
                displayPlanesStates[i].sprite = starSprite;
            }
            else
            {
                if (GameManager.thunderPlaneSelectedIndex == i)
                {
                    displayPlanePrices[i].text = "";
                    displayPlanesStates[i].sprite = selectedSprite;
                }
                else
                {
                    displayPlanePrices[i].text = "";
                    displayPlanesStates[i].sprite = unselectedSprite;
                }
            }
        }
    }

    private void OnGameLoad() 
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void OnGameClose() 
    {
        Application.Quit();
    }
}
