using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RevengerGameManager : MonoBehaviour
{
    public GameObject[] spawnRevengeItems;

    public GameObject revengerResultPage;

    public AnimationCurve[] revengeSpawningXCurve;

    public AnimationCurve[] revengeSpawningYCurve;

    public TMP_Text revengeDetailsDisplay;

    public TMP_Text revengeResultsDetailsDisplay;

    public static int revengeDetailsCount = 0;

    private bool revengeGameStopped  = false;

    private GameObject currentSpawnedRevengeItem;

    private void Awake()
    {
        currentSpawnedRevengeItem = null;
        revengeDetailsCount = 0;
        RevengePlaneCOntroller.planeRevengeFuelEnoughtEvent.AddListener(OnFuelEnought);
        revengeGameStopped = false;
        revengerResultPage.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(SpawnItems());    
    }

    private void OnFuelEnought() 
    {
        revengeGameStopped = true;
    }

    private void LateUpdate()
    {
        revengeDetailsDisplay.text = "x" + revengeDetailsCount.ToString();
        revengeResultsDetailsDisplay.text = "x" + revengeDetailsCount.ToString();
        if (revengeDetailsCount > RevengerMenuScript.TotalEarnedDetailsRevenge)
        {
            RevengerMenuScript.TotalEarnedDetailsRevenge = revengeDetailsCount;
        }
    }

    public void OnClickRestart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickMenu() 
    {
        SceneManager.LoadScene("RevengerMenu");
    }

    private IEnumerator SpawnItems() 
    {
        float tempInctremente = 0;
        AnimationCurve tempXCurve = null;
        AnimationCurve tempYCurve = null;
        if (tempXCurve == null)
        {
            tempXCurve = revengeSpawningXCurve[Random.Range(0, revengeSpawningXCurve.Length)];
        }
        if (tempYCurve == null)
        {
            tempYCurve = revengeSpawningYCurve[Random.Range(0, revengeSpawningYCurve.Length)];
        }
        while (!revengeGameStopped)
        {
            if (currentSpawnedRevengeItem != null)
            {
                currentSpawnedRevengeItem = Instantiate(spawnRevengeItems[Random.Range(0, spawnRevengeItems.Length)], new Vector3(tempXCurve.Evaluate(tempInctremente), tempYCurve.Evaluate(tempInctremente), transform.position.z + 100),Quaternion.identity);
                tempInctremente += 0.15f;
            }
            else
            {
                currentSpawnedRevengeItem = Instantiate(spawnRevengeItems[Random.Range(0, spawnRevengeItems.Length)], new Vector3(tempXCurve.Evaluate(tempInctremente), tempYCurve.Evaluate(tempInctremente),transform.position.z + 100),Quaternion.identity);
                tempInctremente += 0.15f;
            }
            if (tempInctremente >= 10)
            {
                tempInctremente = 0;
                tempXCurve = revengeSpawningXCurve[Random.Range(0, revengeSpawningXCurve.Length)];
                tempYCurve = revengeSpawningYCurve[Random.Range(0, revengeSpawningYCurve.Length)];
            }
            yield return new WaitForSeconds(0.5f);
        }
        revengerResultPage.SetActive(true);
    }
}
