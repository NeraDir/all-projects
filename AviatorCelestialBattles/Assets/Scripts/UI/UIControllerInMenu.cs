using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControllerInMenu : MonoBehaviour
{
    public GameObject HTPPanel;

    public GameObject RewardPanel;

    public int firstEnterGame
    {
        get
        {
            if (!PlayerPrefs.HasKey("FirstEnterSave"))
                return 0;
            return 1;
        }

        set
        {
            PlayerPrefs.SetInt("FirstEnterSave", value);
        }
    }

    public int claimRewarded
    {
        get
        {
            if (!PlayerPrefs.HasKey("claimRewardedSave"))
                return 0;
            return 1;
        }

        set
        {
            PlayerPrefs.SetInt("claimRewardedSave", value);
        }
    }

    private void Awake()
    {
        if(firstEnterGame == 0)
        {
            if (HTPPanel != null)
                HTPPanel.SetActive(true);

            firstEnterGame = 1;
        }

        if (claimRewarded == 0)
            RewardPanel.SetActive(true);

#if true

#endif
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("MainGameScene");
    }

    public void ClaimReward() 
    {
        ValuteController.Instance.AddMoney(500);
        claimRewarded = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenShop()
    {
        return;
    }
}
