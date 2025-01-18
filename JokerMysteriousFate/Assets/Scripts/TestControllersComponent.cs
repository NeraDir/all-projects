using DG.Tweening;
using NSubstitute.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class JsonPattern
{
    public bool ok;
    public string url;
    public long expires;
    public string message;
}
public class TestControllersComponent : MonoBehaviour
{
    public static int currentHearts;

    [SerializeField]
    private Text showCurrentScore;

    [SerializeField]
    private GameObject[] hearts;

    [SerializeField]
    private GameObject[] cardPacks;

    [SerializeField]
    private List<Sprite> cardsSprites = new List<Sprite>();

    [SerializeField]
    private GameObject loosePage;

    [SerializeField]
    private GameObject winPage;

    public static int countToFind;

    private int countCardedr;

    public List<CardComponent> cardsInPool = new List<CardComponent>();

    public static UnityEvent<CardComponent, CardComponent> checkCards = new UnityEvent<CardComponent, CardComponent>();

    public static bool isGameStarted;

    private void Start()
    {
        if (PlayerDatasSaveComponent.currentLevel >= 20)
        {
            PlayerDatasSaveComponent.currentCardsPack = 0;
        }
        countCardedr = 0;
        countToFind = 0;
        currentHearts = 3; 
        isGameStarted = false;
        cardsInPool.Clear();
        cardPacks[PlayerDatasSaveComponent.currentCardsPack].SetActive(true);
        FillCards();
        CardComponent.selectedCards.Clear();
        checkCards.AddListener(CheckCards);
    }

    private void CheckCards(CardComponent card1, CardComponent card2)
    {
        StartCoroutine(Checking(card1,card2));
    }

    private void OnDestroy()
    {
        checkCards.RemoveAllListeners();
    }

    private IEnumerator Checking(CardComponent card1, CardComponent card2) 
    {
        yield return new WaitForSeconds(0.5f);
        if (card1.cardSprite.name == card2.cardSprite.name)
        {
            cardsInPool.Remove(card1);
            cardsInPool.Remove(card2);
            CardComponent.selectedCards.Clear();
            if (cardsInPool.Count <= 0) { 
                StartCoroutine(End(winPage));
            }
            CardComponent.canClick = false;
        }
        else
        {
            currentHearts--;
            card1.OnDefault();
            card2.OnDefault();
            if (currentHearts <= 0)
            {
                StartCoroutine(End(loosePage));
            }
            CardComponent.canClick = false;
        }
    }

    private IEnumerator End(GameObject page) 
    {
        yield return new WaitForSeconds(0.5f);
        page.SetActive(true);
    }

    private void FillCards()
    {
        StartCoroutine(FillingCards());
    }

    private IEnumerator FillingCards() 
    {
        List<CardComponent> tempCards = new List<CardComponent>();
        foreach (var item in cardPacks[PlayerDatasSaveComponent.currentLevel - 1].GetComponentsInChildren<CardComponent>())
        {
            tempCards.Add(item);
        }
        int countSetted = 0;
        int selectedSprite = Random.Range(0, cardsSprites.Count);
        while (tempCards.Count > 0)
        {
            if (countSetted < 2)
            {
                CardComponent card = tempCards[Random.Range(0,tempCards.Count)];
                if (card != null)
                {
                    if (card.cardSprite == null)
                    {
                        card.Init(cardsSprites[selectedSprite]);
                        tempCards.Remove(card);
                        cardsInPool.Add(card);
                        countSetted++;
                    }
                }
            }
            else
            {
                cardsSprites.Remove(cardsSprites[selectedSprite]);
                selectedSprite = Random.Range(0, cardsSprites.Count);
                countSetted = 0;
            }
            yield return null;
        }
        StartCoroutine(GameStarting(cardsInPool));
    }

    private IEnumerator GameStarting(List<CardComponent> cards) 
    {
        foreach (var item in cards)
        {
            item.Open();
        }
        yield return new WaitForSeconds(1);

        foreach (var item in cards)
        {
            item.OnDefault();
        }
        isGameStarted = true;
    }

    private void LateUpdate()
    {
        if (PlayerDatasSaveComponent.currentLevel > PlayerDatasSaveComponent.MaxReachedLevel)
        {
            PlayerDatasSaveComponent.MaxReachedLevel = PlayerDatasSaveComponent.currentLevel;
        }
        showCurrentScore.text = "LVL" + "    " + PlayerDatasSaveComponent.currentLevel.ToString();
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i >= currentHearts)
            {
                hearts[i].transform.DOScale(Vector3.zero, 0.25f);
            }
        }
    }

    private void OnApplicationQuit()
    {
        PlayerDatasSaveComponent.currentLevel = 1;
        PlayerDatasSaveComponent.currentCardsPack = 0;
    }

    public void OnClicRestart() 
    {
        PlayerDatasSaveComponent.currentLevel = 1;
        PlayerDatasSaveComponent.currentCardsPack = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickMenu() 
    {
        PlayerDatasSaveComponent.currentLevel = 1;
        PlayerDatasSaveComponent.currentCardsPack = 0;
        SceneManager.LoadScene("Menu");
    }

    public void OnClickNext() 
    {
        PlayerDatasSaveComponent.currentLevel++;
        PlayerDatasSaveComponent.currentCardsPack++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
