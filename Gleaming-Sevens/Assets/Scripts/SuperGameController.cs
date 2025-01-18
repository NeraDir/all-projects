using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class SuperGameController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> allMultiplierItems;

    [SerializeField]
    private List<GameObject> itemsPrefabs;
    

    [SerializeField]
    private Transform itemsPanelTranform;

    [SerializeField]
    private float itemsPanelMoveSpeed;


    [SerializeField]
    private TMP_Text multiplierValueText;

    public int multiplierValue;
    public int gainValue;

    [SerializeField]
    private GameObject gamePlayUIPage;


    [SerializeField]
    private GameObject TutorPage;
    [SerializeField]
    private GameObject GamePage;
    [SerializeField]
    private GameObject ResultPage;


    [SerializeField]
    private SuperGameResultController superGameResultController;

    [SerializeField]
    private List<GameObject> buffItems;

    private Vector3 default_itemsPanel;

    private char[,] maps = new char[,]
    {
        {
            'M','-','M','-','M',
            'M','M','M','M','M',
            'M','M','M','M','M',
            'M','M','M','M','M',
            '-','-','M','-','-',
            '-','-','-','-','-',
            '-','-','-','-','-',
        },
        {
            'M','-','-','-','M',
            'M','M','M','M','M',
            'M','-','M','-','M',
            'M','M','-','M','M',
            'M','-','M','-','M',
            'M','M','M','M','M',
            'M','-','-','-','M',
        }

    };


    private void Start()
    {
        default_itemsPanel = itemsPanelTranform.position;
       
    }

    private void OnEnable()
    {
        BagMultiplier.MultiplierHasBeenDetect += UpdateMultiplierValue;
        SuperGameTutor.TutorCompleted += StartSuperGame;


        if (!PlayerPrefs.HasKey("FirstEnterSuperGameKey"))
        {
            TutorPage.SetActive(true);
            GamePage.SetActive(false);
            PlayerPrefs.SetInt("FirstEnterSuperGameKey", 1);
        }
        else
        {
            TutorPage.SetActive(false);
            GamePage.SetActive(true);
            Debug.Log("GamePage");
            
        }

        StartSuperGame();
        ResultPage.SetActive(false);

        multiplierValue = 1;
    }

    private void OnDisable()
    {
        BagMultiplier.MultiplierHasBeenDetect -= UpdateMultiplierValue;
        SuperGameTutor.TutorCompleted -= StartSuperGame;
    }

    private void Update()
    {
        multiplierValueText.text = "X" + multiplierValue;
    }

    private void ShowMultipliers()
    {


        if (buffItems.Count != 0)
        {
            for (int i = 0; i < buffItems.Count; i++)
            {

                Destroy(buffItems[i]);
                buffItems.RemoveAt(i);
            }
        }


        int randMapIndex = Random.Range(0, 2);

        for (int i = 0; i < allMultiplierItems.Count; i++)
        {
            if (maps[randMapIndex, i] == 'M')
            {
                Transform itemSpawnPoint = allMultiplierItems[i].transform;
                allMultiplierItems[i].SetActive(true);
                buffItems.Add(Instantiate(itemsPrefabs[Random.Range(0, itemsPrefabs.Count)], itemSpawnPoint.position, itemSpawnPoint.rotation, itemSpawnPoint));
            }
            else
            {
                allMultiplierItems[i].SetActive(false);
            }
        }
    }


    private IEnumerator moveItemsPanel()
    {

        while (itemsPanelTranform.position.y > -800f)
        {
            itemsPanelTranform.position -= Vector3.up * itemsPanelMoveSpeed * Time.deltaTime;
            yield return null;
        }

        itemsPanelTranform.position = default_itemsPanel;
        ShowResultPage();
    }

    public void UpdateMultiplierValue(int value)
    {
        multiplierValue += value;
    }

    public void ShowResultPage()
    {
        GamePage.SetActive(false);
        Debug.Log("gainValue: " + gainValue);
        Debug.Log("multiplierValue: " + multiplierValue);
        superGameResultController.SetInfo(gainValue, multiplierValue);
        ResultPage.SetActive(true);
    }

    public void StartSuperGame()
    {
        ShowMultipliers();
        StartCoroutine(moveItemsPanel());
    }


    public void TapContinueButton()
    {
        ResultPage.SetActive(false);
        gameObject.SetActive(false);
    }

}
