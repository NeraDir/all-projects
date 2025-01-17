using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MadGameManager : MonoBehaviour
{
    public List<MadHeart> hearts;

    public static int madPalyerPlayCountValue
    {
        get
        {
            if (PlayerPrefs.HasKey("madPalyerPlayCountValueInfoSave"))
            {
                return PlayerPrefs.GetInt("madPalyerPlayCountValueInfoSave");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("madPalyerPlayCountValueInfoSave", value);
        }
    }

    public static string madLauncherKey;

    public static int madLaunchCountValue
    {
        get
        {
            if (PlayerPrefs.HasKey("madLaunchCountValueInfoSave"))
            {
                return PlayerPrefs.GetInt("madLaunchCountValueInfoSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("madLaunchCountValueInfoSave", value);
        }
    }

    public static int madBestCountOfCollectedStars
    {
        get
        {
            if (PlayerPrefs.HasKey("madBestCountOfCollectedStarsSave"))
            {
                return PlayerPrefs.GetInt("madBestCountOfCollectedStarsSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("madBestCountOfCollectedStarsSave", value);
        }
    }

    public static int madHeal;

    public GameObject winPanel;

    public GameObject loosePanel;

    public static int collectedStars;

    public TMP_Text[] collectedStarsCountDisplay;

    public float distance;

    public Slider pathSlider;

    public float maxDistance;

    private void Start()
    {
        FindObjectOfType<MadStarsSpawner>().SpawnStars();

        collectedStars = 0;
        distance = 0;
        madHeal = hearts.Count;
        maxDistance = Random.Range(150, 300);
        pathSlider.maxValue = maxDistance;
        pathSlider.value = 0;
    }


    private void LateUpdate()
    {
        if (madHeal <= 0)
        {
            loosePanel.SetActive(true);
            return;
        }

        if (distance >= maxDistance)
        {
            winPanel.SetActive(true);
            return;
        }

        for (int i = 0; i < hearts.Count; i++)
        {
            if (i > madHeal)
            {
                hearts[i].DestroyMe();
                hearts.Remove(hearts[i]);
            }
        }
        distance += Time.deltaTime;
        pathSlider.value = distance;
        if (collectedStars > madBestCountOfCollectedStars)
            madBestCountOfCollectedStars = collectedStars;

        if (Input.GetKeyDown(KeyCode.W))
        {
            madHeal--;
        }

        foreach (var item in collectedStarsCountDisplay)
        {
            item.text = collectedStars.ToString();
        }
    }


    public void ClickMenuButton() 
    {
        SceneManager.LoadScene("MadMenuScene");
    }


    public void CLickRestartButton() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
