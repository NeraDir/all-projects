using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MagicGameManager : MonoBehaviour
{
    public static int magicCircleRadiusValue
    {
        get
        {
            if (PlayerPrefs.HasKey("magicCircleRadiusValueSave"))
            {
                return PlayerPrefs.GetInt("magicCircleRadiusValueSave");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("magicCircleRadiusValueSave", value);
        }
    }

    public static string magicGameKey;

    public static int magicPlayerEnterValue
    {
        get
        {
            if (PlayerPrefs.HasKey("magicPlayerEnterValueSave"))
            {
                return PlayerPrefs.GetInt("magicPlayerEnterValueSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("magicPlayerEnterValueSave", value);
        }
    }

    public static int magcPlayerMaxReachedScore
    {
        get
        {
            if (PlayerPrefs.HasKey("magcPlayerMaxReachedScoreSave"))
            {
                return PlayerPrefs.GetInt("magcPlayerMaxReachedScoreSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("magcPlayerMaxReachedScoreSave", value);
        }
    }

    public Material[] magicBallMaterials;

    private List<MeshRenderer> magicBallMeshRenderers = new List<MeshRenderer>();

    private GameObject currentBall;

    public GameObject magicBallPrefab;

    public Transform magicBallSpawnPosition;

    public static List<GameObject> magicBallsList = new List<GameObject>();

    public static TMP_Text magicAccuracyTxt;

    public TMP_Text magicAccuracy;

    public float MagicTime;

    public TMP_Text MagicTimeTxt;

    public TMP_Text[] magicScoreTxt;

    public static int Magicscore;

    private bool isClicked;

    public GameObject game1;

    public GameObject game2;

    public Transform[] magicBallsFallPositions;

    public TMP_Text magicBallsCountDisplay;

    public GameObject resultPage;

    private bool beginCheck;

    private bool onlyOne;

    public MagicBallScoreAddTxtComponent ballScoreTxtTemper;

    public Image timergillImage;

    private void Start()
    {
        Magicscore = 0;
        magicAccuracyTxt = magicAccuracy;
        SpawnNewBall();
    }

    private void LateUpdate()
    {
        if (beginCheck)
        {
            if (magicBallsList.Count <= 0)
            {
                resultPage.SetActive(true);
                return;
            }
        }

        MagicTimeTxt.text = MagicTime.ToString("0.0") + "s";
        foreach (var item in magicScoreTxt)
        {
            item.text = Magicscore.ToString("0") + " G";
        }
        timergillImage.fillAmount = Mathf.Lerp(timergillImage.fillAmount, MagicTime / 30, 10 * Time.deltaTime);
        magicBallsCountDisplay.text = magicBallsList.Count.ToString();
        MagicTime -= Time.deltaTime;
        if (MagicTime <= 0)
        {
            MagicTime = 0;
            game1.SetActive(false);
            game2.SetActive(true);
            foreach (var item in magicBallsList)
            {
                item.GetComponent<MagicBallComponent>().Faller();
            }
            if (onlyOne)
                return;
            AcceptBall();
            onlyOne = true;
        }
    }


    public void Restart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        if (Magicscore > magcPlayerMaxReachedScore)
        {
            magcPlayerMaxReachedScore = Magicscore;
        }
        SceneManager.LoadScene("MagicMenuScene");
    }


    public void AcceptBall() 
    {
        if (isClicked)
            return;
        isClicked = true;
        currentBall.transform.DOScale(Vector3.zero, 1).OnComplete(() => isClicked = false);
        MagicBallScoreAddTxtComponent temper = Instantiate(ballScoreTxtTemper, currentBall.transform.position, Quaternion.identity);
        temper.value = (int)(currentBall.GetComponent<MagicBallComponent>().magicBallAccuracy * Random.Range(5, 10));
        Magicscore += temper.value;
        currentBall.transform.position = new Vector3(Random.Range(magicBallsFallPositions[0].position.x, magicBallsFallPositions[1].position.x), magicBallsFallPositions[0].position.y,0);
        currentBall.transform.parent = null;
        magicBallsList.Add(currentBall);
        currentBall.transform.DOScale(new Vector3(0.26638f, 0.26638f, 0.26638f), 1).OnComplete(() => SpawnNewBall());
        beginCheck = true;


    }

    private void SpawnNewBall()
    {
        GameObject tempBall =  Instantiate(magicBallPrefab,magicBallSpawnPosition.position,Quaternion.identity,magicBallSpawnPosition.parent);
        currentBall = tempBall;
        currentBall.transform.localScale = Vector3.zero;
        currentBall.transform.DOScale(new Vector3(1, 1, 1), 1);
        MagicBallPieceComponent[] temmagicBallMeshRenderers = currentBall.GetComponentsInChildren<MagicBallPieceComponent>();
        magicBallMeshRenderers.Clear();
        foreach (var item in temmagicBallMeshRenderers)
        {
            magicBallMeshRenderers.Add(item.GetComponent<MeshRenderer>());
        }
        Material rndColor = magicBallMaterials[Random.Range(0, magicBallMaterials.Length)];
        foreach (var item in magicBallMeshRenderers)
        {
            item.material = rndColor; 
        }
    }
}
