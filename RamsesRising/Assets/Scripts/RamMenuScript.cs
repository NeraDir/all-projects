using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RamMenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject mainThirdPanel;

    public Animator mainFirstAnimator;

    public Sprite[] lvlsSprites;
    public Sprite[] lvlsSpritesCrystall;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("mainThirdPanelOpened"))
        {
            mainThirdPanel.SetActive(true);
            PlayerPrefs.SetInt("mainThirdPanelOpened", 1);
        }
    }

    public void ClickOpenLevel(int lvl) 
    {
        switch (lvl) 
        {
            case 0:
                RamGameManager.crystallDamage = 10;
                RamGameManager.jarHealth = 100;
                RamGameManager.crystallMovementSpeed = 70;
                RamGameManager.timeSpawnCrystall = 3;
                break;
            case 1:
                RamGameManager.crystallDamage = 8;
                RamGameManager.jarHealth = 100;
                RamGameManager.crystallMovementSpeed = 140;
                RamGameManager.timeSpawnCrystall = 2;
                break;
            case 2:
                RamGameManager.crystallDamage = 5;
                RamGameManager.jarHealth = 100;
                RamGameManager.crystallMovementSpeed = 210;
                RamGameManager.timeSpawnCrystall = 1;
                break;
            case 3:
                RamGameManager.crystallDamage = 2;
                RamGameManager.jarHealth = 100;
                RamGameManager.crystallMovementSpeed = 280;
                RamGameManager.timeSpawnCrystall = 0.5f;
                break;
        }
        int indexofJare = Random.Range(0, lvlsSprites.Length);
        RamGameManager.jarSprite = lvlsSprites[indexofJare];
        RamGameManager.needCrystallSprite = lvlsSpritesCrystall[indexofJare];
        RamGameManager.needIndexCrystall = indexofJare;
        SceneManager.LoadScene("GameScene");
    }

    public void ClickClose() 
    {
        mainFirstAnimator.SetBool("RAMANIMA", true);
        Invoke("CloseGame", 0.5f);
    }

    private void CloseGame() 
    {
        Application.Quit();
    }
}
