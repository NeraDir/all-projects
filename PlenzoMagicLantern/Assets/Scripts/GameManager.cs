using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] musicsSources;

    [SerializeField]
    private AudioSource[] soundsSources;

    [SerializeField]
    private TMP_Text[] _showCurrentScore;

    [SerializeField]
    private Sprite[] spritesOfObjects;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _sound;

    public static int CurrentScore;

    [SerializeField]
    private float Width;

    [SerializeField]
    private float Height;

    [SerializeField]
    private Transform parrent;

    [SerializeField]
    private Transform spawnPosition;

    [SerializeField]
    private GameObject spawnObject;

    [SerializeField]
    private int min;

    [SerializeField]
    private int max;

    [SerializeField]
    private GameObject endPanel;

    [SerializeField]
    private Color[] colors;

    [SerializeField]
    private string[] colorsTXT;

    [SerializeField]
    private TMP_Text needColor;

    public static int needIndex;

    public static int plnezoMagicTryCounts
    {
        get
        {
            if (PlayerPrefs.HasKey("plnezoMagicTryCounts"))
            {
                return PlayerPrefs.GetInt("plnezoMagicTryCounts");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("plnezoMagicTryCounts", value);
        }
    }

    public static string plenzoMagiName;

    public static int plenzoMagicWinsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("plenzoMagicWinsCount"))
            {
                return PlayerPrefs.GetInt("plenzoMagicWinsCount");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("plenzoMagicWinsCount", value);
        }
    }

    private void Awake()
    {
        CurrentScore = 0;

        foreach (var item in musicsSources)
        {
            if (MenuController.musicOn == 1)
            {
                item.mute = true;
            }
            else
            {
                item.mute = false;
            }
        }

        foreach (var item in soundsSources)
        {
            if (MenuController.soundOn == 1)
            {
                item.mute = true;
            }
            else
            {
                item.mute = false;
            }
        }

        int RandomCount = Random.Range(min,max);
        for (int i = 0; i < RandomCount; i++)
        {
            GameObject spawnerdObject = Instantiate(spawnObject, spawnPosition);
            spawnerdObject.transform.parent = parrent;
            spawnerdObject.transform.localPosition = new Vector3(Random.Range(spawnPosition.localPosition.x, spawnPosition.localPosition.x + Width),Random.Range(spawnPosition.localPosition.y, spawnPosition.localPosition.y + Height), spawnPosition.localPosition.z);
            float RandomSize = Random.Range(0.6f, 2);
            spawnerdObject.transform.localScale = new Vector3(RandomSize, RandomSize, RandomSize);
            int RandomSprite = Random.Range(0, spritesOfObjects.Length);
            spawnerdObject.GetComponent<Image>().sprite = spritesOfObjects[RandomSprite];
            spawnerdObject.GetComponent<BallComponent>().index = RandomSprite;
            spawnerdObject.GetComponent<BallComponent>().audioSource = _audioSource;
            spawnerdObject.GetComponent<BallComponent>().clip = _sound;
        }
        
        needIndex = Random.Range(0, spritesOfObjects.Length);
        needColor.text = colorsTXT[needIndex];
        needColor.color = colors[Random.Range(0, colors.Length)];
        StartCoroutine(EndWaiting());
    }

    private void LateUpdate()
    {
        foreach (var item in _showCurrentScore)
        {
            item.text = CurrentScore.ToString();
        }
    }

    private IEnumerator EndWaiting() 
    {
        bool ended = true;
        while (ended) 
        {
            if (BallsAlive())
            {
                endPanel.SetActive(true);
                ended = false;
            }
            else
            {
                endPanel.SetActive(false);
                ended = true;
            }
            yield return new WaitForSeconds(2);
        }   

    }

    public void Restart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu() 
    {
        SceneManager.LoadScene("Menu");
    }

    private bool BallsAlive() 
    {
        BallComponent[] balls = FindObjectsOfType<BallComponent>();
        if (balls.Length > 0)
        {
            foreach (var item in balls)
            {
                if (item.index == needIndex)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
