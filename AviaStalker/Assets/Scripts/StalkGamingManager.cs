using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StalkGamingManager : MonoBehaviour
{
    public static int stalkPlayerEnterTryCounts
    {
        get
        {
            if (PlayerPrefs.HasKey("stalkPlayerEnterTryCountsSavingKey"))
            {
                return PlayerPrefs.GetInt("stalkPlayerEnterTryCountsSavingKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("stalkPlayerEnterTryCountsSavingKey", value);
        }
    }

    public static string stalkPlayerFirstEnterSettingsKey;

    public static int stalkBeginEnginersCounts
    {
        get
        {
            if (PlayerPrefs.HasKey("stalkBeginEnginersCountsSavingKey"))
            {
                return PlayerPrefs.GetInt("stalkBeginEnginersCountsSavingKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("stalkBeginEnginersCountsSavingKey", value);
        }
    }

    public static int stalkPlanesRecoveredScoreBest
    {
        get
        {
            if (PlayerPrefs.HasKey("stalkPlanesRecoveredScoreBestSavingKey"))
            {
                return PlayerPrefs.GetInt("stalkPlanesRecoveredScoreBestSavingKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("stalkPlanesRecoveredScoreBestSavingKey", value);
        }
    }

    public static int recoveredPlanesCount;

    public StalkPlaneComponent stalkCurrentPlane;

    public bool stalkPlaneCanRecover;

    public bool canChangePiece;

    public Slider recovererSlider;

    public Slider planeHealthBar;

    public static float currentplaneHealth;

    public Image recovererPlace;

    public static string placeState;

    public GameObject[] recovererButtons;

    public static bool cangSelectNewPiece;

    public static int stalkPlayerHearts;

    public GameObject[] stalkPlayerHeartsImages;

    public static float sliderMoveValue;

    public TMP_Text[] showRecoveredPlanesValue;

    public GameObject stalkResultScreen;

    private void Start()
    {
        stalkPlayerHearts = 3;
        sliderMoveValue = 0.75f;
        GetNewPlane();
    }

    public void GetNewPlane() 
    {
        if (stalkPlayerHearts <= 0)
            return;
            StartCoroutine(GetNew());
    }

    private IEnumerator GetNew() 
    {
        yield return new WaitForSeconds(1);
        foreach (var item in stalkCurrentPlane.stalkPieces)
        {
            item.PlanePiece.gameObject.SetActive(false);
            item.recovered = false;
        }
        currentplaneHealth = 1;
        int rndCount = Random.Range(0, stalkCurrentPlane.stalkPieces.Length);
        for (int i = 0; i < rndCount; i++)
        {
            int rndPiece = Random.Range(0, stalkCurrentPlane.stalkPieces.Length);
            if (!stalkCurrentPlane.stalkPieces[rndPiece].recovered)
            {
                stalkCurrentPlane.stalkPieces[rndPiece].recovered = true;
            }
        }
        stalkCurrentPlane.gameObject.SetActive(true);
    }

    public void StalkSelectPlaneAddPiece(int index) 
    {
        if (cangSelectNewPiece)
            return;
        if (canChangePiece)
            return;
        canChangePiece = true;
        cangSelectNewPiece = true;
        StartCoroutine(MoveSlider(1, 0, index));
    }

    private void LateUpdate()
    {
        for (int i = 0; i < stalkPlayerHeartsImages.Length; i++)
        {
            if (i >= stalkPlayerHearts)
            {
                stalkPlayerHeartsImages[i].gameObject.SetActive(false);
            }
        }
        Debug.Log(stalkPlayerHearts);
        if (stalkPlayerHearts <= 0) { 
            stalkResultScreen.SetActive(true);
            return;
        }

        if (Input.GetMouseButtonDown(0) && cangSelectNewPiece)
            canChangePiece = false;
        planeHealthBar.value = currentplaneHealth;

        foreach (var item in showRecoveredPlanesValue)
        {
            item.text = recoveredPlanesCount.ToString();
        }
    }

    public void OnMenuPressed() 
    {
        if (recoveredPlanesCount > stalkPlanesRecoveredScoreBest)
        {
            stalkPlanesRecoveredScoreBest = recoveredPlanesCount;
        }
        SceneManager.LoadScene("StalkMenuScene");
    }

    public void OnAgainPressed() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator MoveSlider(float firstValue,float secondValue,int index) 
    {
        recovererSlider.gameObject.SetActive(true);
        bool goUp = false;
        recovererPlace.transform.localPosition = new Vector3(Random.Range(-367, 367), 0, 0);
        recovererSlider.minValue = 0;
        recovererSlider.maxValue = 1;
        while (canChangePiece)
        {
            if (goUp)
            {
                recovererSlider.value = Mathf.MoveTowards(recovererSlider.value, firstValue, sliderMoveValue * Time.deltaTime);
                if (recovererSlider.value >= firstValue)
                {
                    goUp = false;
                }
            }
            else
            {
                recovererSlider.value = Mathf.MoveTowards(recovererSlider.value, secondValue, sliderMoveValue * Time.deltaTime);
                if (recovererSlider.value <= secondValue)
                {
                    goUp = true;
                }
            }
            yield return null;
        }
        float value = 0;
        float[] badValues = { 3f, 0.3f,-3f,-0.3f};
        switch (placeState)
        {
            case "Normal":
                value = Random.Range(-0.3f, 0.3f);
                break;
            case "Good":
                value = Random.Range(-0.15f, 0.15f);
                break;
            case "Amazing":
                value = 0;
                break;
            case "Break":
                value = Random.Range(0, 2) != 0 ? Random.Range(badValues[0], badValues[1]) : Random.Range(badValues[2], badValues[3]);
                break;
        }
        stalkCurrentPlane.MoveSelectedPiece(index, value);
    }
}
