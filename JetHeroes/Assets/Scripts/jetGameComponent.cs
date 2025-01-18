using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class jetGameComponent : MonoBehaviour
{
    public static int jetBestScoreValue
    {
        get
        {
            if (PlayerPrefs.HasKey("jetBestScoreValueSavingKey"))
            {
                return PlayerPrefs.GetInt("jetBestScoreValueSavingKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("jetBestScoreValueSavingKey", value);
        }
    }

    public static int jetStartRoatationZvalue
    {
        get
        {
            if (PlayerPrefs.HasKey("jetStartRoatationZvalueSavingKey"))
            {
                return PlayerPrefs.GetInt("jetStartRoatationZvalueSavingKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("jetStartRoatationZvalueSavingKey", value);
        }
    }

    public static string jetloadkeyvalue;

    public static int jetStartCloudCountValue
    {
        get
        {
            if (PlayerPrefs.HasKey("jetStartCloudCountValueSavingKey"))
            {
                return PlayerPrefs.GetInt("jetStartCloudCountValueSavingKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("jetStartCloudCountValueSavingKey", value);
        }
    }

    private float currentDispalyScoreValue;

    public static int score;

    public TMP_Text scoreDisplay;

    public TMP_Text scoreDisplayVictory;


    private float rotateValue;

    public Transform jetCharacter;
    public Transform jetCharacter2;
    public GameObject jetResultWindow;

    public GameObject jetPreRotatePref;

    public GameObject[] clouds;

    private float speed;

    private float maxFuelValue = 100;

    private float currentFuel = 100;

    public Image fuelBar;

    public TMP_Text fuelDisplay;

    public ParticleSystem jetParticleSystem;

    Sequence sequence;

    private void Start()
    {
        score = 0;
        speed = 1; 
        float yUpValue = jetCharacter2.transform.position.y + 0.1f;
        float yDownValue = jetCharacter2.transform.position.y - 0.1f;
        jetParticleSystem.startLifetime = 0.19f;
        sequence = DOTween.Sequence();
        sequence.Append(jetCharacter2.DOMoveY(yUpValue, 1));
        sequence.Append(jetCharacter2.DOMoveY(yDownValue, 1));
        sequence.Append(jetCharacter2.DOMoveY(yUpValue, 1));
        sequence.SetLoops(-1, LoopType.Yoyo);

        StartCoroutine(SpawnClouds());
        SpawnNewPreRotateModel();
        jetCharacterComponent.onTriggerWithModel.AddListener(OnTriggered);
    }

    private void OnTriggered(int value1, float value2) 
    {
        score += value1;
        currentFuel += value2;
        SpawnNewPreRotateModel();
    }

    public void SpawnNewPreRotateModel() 
    {
        if (currentFuel <= 0)
            return;
        GameObject tempJet = Instantiate(jetPreRotatePref, jetPreRotatePref.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        tempJet.SetActive(true);
        tempJet.GetComponent<jetObjectMovement>().moveSpeed = speed;
        speed += 0.2f;
    }

    private IEnumerator SpawnClouds() 
    {
        while (true)
        {
            GameObject tempCloud = Instantiate(clouds[0], new Vector3(Random.Range(clouds[0].transform.position.x, clouds[1].transform.position.x), clouds[0].transform.position.y + 10, 0), Quaternion.identity);
            tempCloud.SetActive(true);
            tempCloud.GetComponent<jetObjectMovement>().moveSpeed = 2;
            yield return new WaitForSeconds(2.5f);
        }
    }

    private void OnDestroy()
    {
        jetCharacterComponent.onTriggerWithModel.RemoveAllListeners();
    }

    public void OnMouseDownRotate(float rotateval)
    {
        rotateValue = rotateval;
    }

    public void OnMouseUpRotate() 
    {
        rotateValue = 0;
    }

    private void LateUpdate()
    {
        if (currentFuel <= 0)
        {
            jetResultWindow.SetActive(true);
            currentFuel = 0;
            return;
        }
        currentFuel -= 2 * Time.deltaTime;
        if (currentFuel > maxFuelValue)
        {
            currentFuel = maxFuelValue;
        }
        scoreDisplayVictory.text = score.ToString("0");
        fuelDisplay.text = currentFuel.ToString("0") +"%";
        fuelBar.fillAmount = Mathf.MoveTowards(fuelBar.fillAmount, currentFuel / maxFuelValue, 100 * Time.deltaTime);
        currentDispalyScoreValue = Mathf.MoveTowards(currentDispalyScoreValue, score, 100 * Time.deltaTime);
        scoreDisplay.text = currentDispalyScoreValue.ToString("0");
        jetCharacter.Rotate(new Vector3(0, 0, rotateValue), 120 * Time.deltaTime);
    }

    public void OnClickRestartGame()
    {
        sequence.Kill();
        jetParticleSystem.startLifetime = 0.56f;
        jetCharacter2.DOMoveY(jetCharacter2.position.y + 10, 1).OnComplete(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    public void OnClickCloseGameAndOpenMenu() 
    {
        sequence.Kill();
        jetParticleSystem.startLifetime = 0.56f;
        if (score > jetBestScoreValue)
        {
            jetBestScoreValue = score;
        }
        jetCharacter2.DOMoveY(jetCharacter2.position.y + 10, 1).OnComplete(() => SceneManager.LoadScene("loadingJet"));
        
    }
}
