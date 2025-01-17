using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_UpgardeLevelPage : MonoBehaviour
{
    [SerializeField]
    private GameObject GunAndHealthWindow;
    [SerializeField]
    private GameObject OnlyHealthWindow;


    [SerializeField]
    private Image leftGunImage;
    [SerializeField]
    private Image rightGunImage;

    [SerializeField]
    private List<Sprite> gunSprites;

    private int nextLeftGunIndex;

    public delegate void UpgradeGunDelegate();
    public static event UpgradeGunDelegate UpgradeGunEvent;

    private GameSceneController gameSceneController;
    private UI_GamePlayPage uI_GamePlayPage;

    public delegate void CloseUpgradePageDelegate();
    public static event CloseUpgradePageDelegate CloseUpgradePageEvent;


    public void Init(GameSceneController gameSceneController, UI_GamePlayPage uI_GamePlayPage)
    {
        this.gameSceneController = gameSceneController;
        this.uI_GamePlayPage = uI_GamePlayPage;
    }

    private void OnEnable()
    {
        uI_GamePlayPage.gameObject.SetActive(false);

        nextLeftGunIndex = 0;

        if (GameSceneController.gunLevel > 5)
        {
            OnlyHealthWindow.SetActive(true);
            GunAndHealthWindow.SetActive(false);

        }
        else
        {
            OnlyHealthWindow.SetActive(false);
            GunAndHealthWindow.SetActive(true);

            nextLeftGunIndex = 2 * (GameSceneController.gunLevel - 1);

            leftGunImage.sprite = gunSprites[nextLeftGunIndex];
            rightGunImage.sprite = gunSprites[nextLeftGunIndex + 1];

        }
    }
    private void OnDisable()
    {
        uI_GamePlayPage.gameObject.SetActive(true);
    }


    public void UpdradeHealth()
    {
        GunPlatformHealth.maxHealth += 2;
        GunPlatformHealth.currentHealth = GunPlatformHealth.maxHealth;
        CloseUpgradePage();
    }

    public void ChooseLeftGun()
    {
        GameSceneController.gunLevel++;
        GameSceneController.activeGunIndex = nextLeftGunIndex + 1;

        if (UpgradeGunEvent != null)
            UpgradeGunEvent();

        //CloseUpgradePage();
        PlayClosePageAnimation();
    }
    public void ChooseRightGun()
    {
        GameSceneController.gunLevel++;
        GameSceneController.activeGunIndex = nextLeftGunIndex + 2;

        if (UpgradeGunEvent != null)
            UpgradeGunEvent();

        PlayClosePageAnimation();
        //CloseUpgradePage();
    }

    public void CloseUpgradePage()
    {
        if (CloseUpgradePageEvent != null)
            CloseUpgradePageEvent();

        gameObject.SetActive(false);
    }

    private void PlayClosePageAnimation()
    {
        GetComponent<Animator>().SetInteger("stateID", 1);
    }
}
