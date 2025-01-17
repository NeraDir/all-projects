using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    public static int scoreCount;
    public static int levelNumber;
    public static float currentExp;
    public static float maxExp;

    public static int gunLevel;
    public static int activeGunIndex;

    [SerializeField]
    private GunPlatform gunPlatform;
    private ZombieSpawner zombieSpawner;

    [SerializeField]
    private UI_GamePlayPage uI_GamePlayPage;
    [SerializeField]
    private UI_UpgardeLevelPage uI_UpgardeLevelPage;
    [SerializeField]
    private UI_ResultGamePage uI_ResultGamePage;


    [SerializeField]
    private GameObject mainVirtualCamera;
    [SerializeField]
    private GameObject rotateAroundPlayerVirtualCamera;
    [SerializeField]
    private GameObject resultGameVirtualCameral;


    private void OnEnable()
    {
        GunPlatformHealth.PlayerDeadEvent += ShowResultPage;
        Zombie.ZombieDeadEvent += IncrementScore;
        UI_UpgardeLevelPage.CloseUpgradePageEvent += RotateCameraAroundPlatform;

        Init();
    }
    private void OnDisable()
    {
        GunPlatformHealth.PlayerDeadEvent -= ShowResultPage;
        Zombie.ZombieDeadEvent -= IncrementScore;
        UI_UpgardeLevelPage.CloseUpgradePageEvent -= RotateCameraAroundPlatform;
    }

    private void Init()
    {
        activeGunIndex = 0;
        scoreCount = 0;
        levelNumber = 1;
        currentExp = 0;
        maxExp = 1;

        GunPlatformHealth.maxHealth = 50;
        GunPlatformHealth.currentHealth = 50;

        gunLevel = 1;
        zombieSpawner = GetComponent<ZombieSpawner>();
        //zombieSpawner.StartZombiesSpawn();
        uI_UpgardeLevelPage.Init(this, uI_GamePlayPage);

        UpdateGun();
        RotateCameraAroundPlatform();
    }


    private void IncrementScore()
    {
        scoreCount++;

        if (currentExp + 0.1f >= maxExp)
        {
            currentExp += (maxExp - currentExp);
            LevelUp();
        }
        else
        {
            currentExp += 0.1f;
        }
    }

    private void UpdateGun()
    {
        gunPlatform.Init();
    }

    public void LevelUp()
    {
        gunPlatform.StopAttackWithGun();
        levelNumber++;
        currentExp = 0;
        maxExp += 0.5f;
        ShowUpgradePanel();

        zombieSpawner.StopZombieSpawn();
        zombieSpawner.FreezeAllZombieInScene();

    }
    private void ShowUpgradePanel()
    {
        uI_UpgardeLevelPage.gameObject.SetActive(true);
    }

    private void RotateCameraAroundPlatform()
    {
        StartCoroutine(showPlatform());
    }

    private IEnumerator showPlatform()
    {
        //zombieSpawner.StopZombieSpawn();
        //zombieSpawner.FreezeAllZombieInScene();

        mainVirtualCamera.SetActive(false);
        rotateAroundPlayerVirtualCamera.SetActive(true);

        yield return new WaitForSeconds(5f);

        mainVirtualCamera.SetActive(true);
        rotateAroundPlayerVirtualCamera.SetActive(false);

        zombieSpawner.UnFreezeAllZombieInScene();

        zombieSpawner.StartZombiesSpawn();

        gunPlatform.GetComponent<GunManager>().ContinueShooting();

        yield return null;
    }

    private void ShowResultPage()
    {
        gunPlatform.GetComponent<GunManager>().StopAttack();
        uI_ResultGamePage.gameObject.SetActive(true);
    }

}
