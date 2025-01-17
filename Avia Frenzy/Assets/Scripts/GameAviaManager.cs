using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameAviaManager : MonoBehaviour
{
    public Image fuelBarImage;

    public float maxFuelValue;
    public static float currentFuelValue;

    public static int score;

    public GameObject aviaElementPrefab;

    public Transform[] elementsSpawnPositions;

    public TMP_Text[] scoresDispalyers;

    public static int level = 1;

    public static int clickedCount;

    public GameObject congratulationsWindow;

    public static int MaxReachedScore 
    {
        get 
        {
            if (PlayerPrefs.HasKey("maxreachedaviascoresave"))
            {
                return PlayerPrefs.GetInt("maxreachedaviascoresave");
            }
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("maxreachedaviascoresave", value);
        }
    }

    private void Start()
    {
        level = 1;
        score = 0;
        clickedCount = 0;
        currentFuelValue = 10;
        StartCoroutine(SpawningElements());
    }

    private IEnumerator SpawningElements() 
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1,2));
            Instantiate(aviaElementPrefab,new Vector3(Random.Range(elementsSpawnPositions[0].position.x, elementsSpawnPositions[1].position.x), Random.Range(elementsSpawnPositions[0].position.y, elementsSpawnPositions[1].position.y),0),Quaternion.identity, elementsSpawnPositions[0].parent);
        }
    }

    public void ClickAddFuel() 
    {
        currentFuelValue += Random.Range(1, 3) + (level / 2);
        if (currentFuelValue >= maxFuelValue)
        {
            currentFuelValue = maxFuelValue;
        }
    }

    private void LateUpdate()
    {
        currentFuelValue -= (2f * level) * Time.deltaTime;

        if (currentFuelValue >= maxFuelValue)
            currentFuelValue = maxFuelValue;
        if (currentFuelValue < 0)
        {
            currentFuelValue = 0;
            congratulationsWindow.SetActive(true);
        }
        if (MaxReachedScore < score)
            MaxReachedScore = score;
        if (score <= 0)
            score = 0;
       
        foreach (var item in scoresDispalyers)
        {
            item.text = score.ToString();
        }
        UpdateFuelBar();
    }

    private void UpdateFuelBar() 
    {
        if (fuelBarImage != null)
            fuelBarImage.fillAmount = Mathf.MoveTowards(fuelBarImage.fillAmount, (currentFuelValue / maxFuelValue), 10 * Time.deltaTime);
    }

    public void menu() 
    {
        SceneManager.LoadScene("GameAviaMenu");
    }

    public void restart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
