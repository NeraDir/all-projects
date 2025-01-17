using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static Card firstRotatedCard;
    public static Card secondRotatedCard;

    public static bool canChooseCard;

    [SerializeField]
    private List<Card> cardPrefabs;
    [SerializeField]
    private Transform spawnCardParent;
    [SerializeField]
    private List<Transform> cardSpawnPoints;

    [SerializeField]
    private GameObject resultGamePanel;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private GameObject pausePanel;


    private List<Card> allCardsInScene;


    [SerializeField]
    private TMP_Text timeDisplay;
    public static float elapsedTime;

    public static int curentPlayerTry;
    public static int maxPLayerTry;

    public static int currectTryCount;

    public static string tempCardsCount;

    public static int cardCOunt 
    {
        get
        {
            if (PlayerPrefs.HasKey("cardCOuntSave"))
            {
                return PlayerPrefs.GetInt("cardCOuntSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("cardCOuntSave", value);
        }
    }

    public static int cardTrueCount 
    {
        get
        {
            if (PlayerPrefs.HasKey("cardTrueCountSave"))
            {
                return PlayerPrefs.GetInt("cardTrueCountSave");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("cardTrueCountSave", value);
        }
    }

    [SerializeField]
    private PlayerReadyController playerReadyController;

    private void OnEnable()
    {
        Card.SecondCardFoundEvent += CompareCards;

        firstRotatedCard = null;
        secondRotatedCard = null;

        canChooseCard = false;

        allCardsInScene = new();

        elapsedTime = 0;

        maxPLayerTry = 3;
        curentPlayerTry = maxPLayerTry;

        currectTryCount = 0;

        timeDisplay.gameObject.SetActive(false);

        StartCoroutine(SpawnCards());
        
    }
    private void OnDisable()
    {
        Card.SecondCardFoundEvent -= CompareCards;
    }


    private void CompareCards()
    {
        canChooseCard = false;

        if (firstRotatedCard == null && secondRotatedCard == null)
            return;

        if (firstRotatedCard.GetCardIndex() != secondRotatedCard.GetCardIndex())
        {
            Debug.Log("gameOver!");


            StartCoroutine(ShowBackSideToSecondCard());

            //gameOverPanel.SetActive(true);
        }
        else
        {

            Debug.Log("firstCardParent: " + firstRotatedCard.transform.parent.gameObject.name);
            Debug.Log("secondCardParent: " + secondRotatedCard.transform.parent.gameObject.name);

            //firstRotatedCard.GetComponent<CardAnimationController>().PlayDestroyAnimation();
            //secondRotatedCard.gameObject.GetComponent<CardAnimationController>().PlayDestroyAnimation();

            //CardAnimationController buff = firstRotatedCard.GetComponent<CardAnimationController>();

            //buff.PlayDestroyAnimation();

            //buff = secondRotatedCard.gameObject.GetComponent<CardAnimationController>();

            //buff.PlayDestroyAnimation();

            currectTryCount++;

            if (currectTryCount == 8)
            {
                resultGamePanel.SetActive(true);
            }


            firstRotatedCard = secondRotatedCard = null;
        }

        canChooseCard = true;

    }

    private IEnumerator ShowBackSideToSecondCard()
    {
        canChooseCard = false;
        yield return new WaitForSeconds(1f);
        secondRotatedCard.GetComponent<CardAnimationController>().PlayRotateToBackSideAnimation();
        canChooseCard = true;
        secondRotatedCard = null;
        

        if (curentPlayerTry - 1 > 0)
        {
            curentPlayerTry--;
            Debug.Log(curentPlayerTry);
        }
        else
        {
            gameOverPanel.SetActive(true);
        }
    }


    private IEnumerator SpawnCards()
    {
        for (int i = 0; i < cardSpawnPoints.Count; i++)
        {
            cardSpawnPoints[i].gameObject.SetActive(false);
            yield return null;
        }

        
        for (int i = 0; i < cardPrefabs.Count; i++)
        {
            Card newCard = cardPrefabs[i];

            Transform newCardSpawnPoint = GetEmptySpawnPoint();
            Instantiate(newCard, newCardSpawnPoint.position, newCardSpawnPoint.rotation, newCardSpawnPoint);

            yield return null;

            newCardSpawnPoint = GetEmptySpawnPoint();
            Instantiate(newCard, newCardSpawnPoint.position, newCardSpawnPoint.rotation, newCardSpawnPoint);

            yield return null;

        }


        for (int i = 0; i < cardSpawnPoints.Count; i++)
        {
            allCardsInScene.Add(cardSpawnPoints[i].GetComponentInChildren<Card>());
            //cardSpawnPoints[i].GetComponentInChildren<Card>().Init();
            yield return null;
        }

        for (int i = 0; i < allCardsInScene.Count; i++)
        {
            allCardsInScene[i].Init();
            yield return null;
        }



        ShowAllCard();

    }


    public void StartRotateAllCard()
    {
        StartCoroutine(RotateAllCards(2f, false));
    }

    public void ShowAllCard()
    {
        Animator cardParentAnimator = spawnCardParent.GetComponent<Animator>();
        cardParentAnimator.SetInteger("StateNumber", 1);
        playerReadyController.gameObject.SetActive(true);
        
    }

    public IEnumerator RotateAllCards(float waintTime, bool isFrontSide)
    {
        yield return new WaitForSeconds(waintTime);

        for (int i = 0; i < allCardsInScene.Count; i++)
        {
            if (!isFrontSide)
            {
                allCardsInScene[i].RotateCard();
                yield return new WaitForSeconds(0.1f);
            }
        }
        StartCoroutine(timer());
        canChooseCard = true;
    }


    public Transform GetEmptySpawnPoint()
    {
        Transform result = cardSpawnPoints[Random.Range(0, cardSpawnPoints.Count)];


        if (result.gameObject.activeInHierarchy)
        {
            result = cardSpawnPoints[Random.Range(0, cardSpawnPoints.Count)];
            while (result.gameObject.activeInHierarchy)
            {
                result = cardSpawnPoints[Random.Range(0, cardSpawnPoints.Count)];
            }
        }



        //Debug.Log("1");

        result.gameObject.SetActive(true);
        return result;
    }


    private IEnumerator timer()
    {
        timeDisplay.gameObject.SetActive(true);
        while (true)
        {
            elapsedTime += Time.deltaTime;
            timeDisplay.text = elapsedTime.ToString("#.#s");
            yield return null;
        }
    }

    public void TapPauseButton()
    {
        pausePanel.SetActive(true);
    }

}
