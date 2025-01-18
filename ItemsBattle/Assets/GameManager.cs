using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System.Runtime.ConstrainedExecution;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject PreBattlePage;
    [SerializeField]
    private GameObject RoundNumberDisplayPage;
    [SerializeField]
    private GameObject GameLayerPage;
    [SerializeField]
    private PreparationforBattlePage preparationforBattlePage;
    [SerializeField]
    private GameObject WinPage;
    [SerializeField]
    private GameObject LosePage;
    [SerializeField]
    private GameObject PausePage;


    [SerializeField]
    private BattleParticipant player;
    [SerializeField]
    private BattleParticipant enemy;

    [SerializeField]
    private TMP_Text timerDisplay;
    [SerializeField]
    private TMP_Text roundDisplay;


    [SerializeField]
    private List<Item> ItemsPrefabs;

    [SerializeField]
    private float itemsSpeed;

    [SerializeField]
    private float timeToStartBattle;
    private int roundNumber;

    public static int destroyedItemsPerRound;




    private void OnEnable()
    {
        Item.LastItemDestroyedEvent += ChangeRound;

        Init();
        StartCoroutine(ShowFirstPages());
        //ShowPreeBattlePage();
    }
    private void OnDisable()
    {
        Item.LastItemDestroyedEvent -= ChangeRound;
    }

    private void Init()
    {
        roundNumber = 1;
        player.Init(ItemsPrefabs, OwnerType.Player, itemsSpeed);
        enemy.Init(ItemsPrefabs, OwnerType.Enemy, itemsSpeed);
    }


    private void ShowPreeBattlePage()
    {
        PreBattlePage.SetActive(true);

        float waintTime = PreBattlePage.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;
        Invoke(nameof(ShowRoundNumberPage), waintTime);
    }
    private void ShowRoundNumberPage()
    {
        PreBattlePage.SetActive(false);
        RoundNumberDisplayPage.SetActive(true);

        float waitTime = RoundNumberDisplayPage.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;
        Invoke(nameof(ShowGameLayerPage), waitTime);

    }
    private void ShowGameLayerPage()
    {
        RoundNumberDisplayPage.SetActive(false);
        GameLayerPage.SetActive(true);

        float waitTime = GameLayerPage.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;
        Invoke(nameof(ShowItems), waitTime);
    }

    private void ShowItems()
    {
        StartCoroutine(showItems());
    }
    private IEnumerator showItems()
    {
        enemy.ShowItems();
        yield return new WaitForSeconds(2.5f);
        player.ShowItems();
    }

    private IEnumerator ShowFirstPages()
    {
        float waitTime = 0;

        PreBattlePage.SetActive(true);
        waitTime = PreBattlePage.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;

        yield return new WaitForSeconds(waitTime);


        PreBattlePage.SetActive(false);
        RoundNumberDisplayPage.SetActive(true);
        waitTime = RoundNumberDisplayPage.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;

        yield return new WaitForSeconds(waitTime);


        RoundNumberDisplayPage.SetActive(false);
        GameLayerPage.SetActive(true);

        waitTime = GameLayerPage.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;

        yield return new WaitForSeconds(waitTime);

        enemy.ShowItems();
        yield return new WaitForSeconds(2.5f);
        player.ShowItems();
        yield return new WaitForSeconds(2f);
        preparationforBattlePage.gameObject.SetActive(true);

        StartCoroutine(startBattle());
        yield return null;

    }

    private IEnumerator startBattle()
    {
        while (timeToStartBattle > 0)
        {
            timeToStartBattle -= Time.deltaTime;
            timerDisplay.text = timeToStartBattle.ToString("#s");
            yield return null;
        }

        preparationforBattlePage.PlayCloseAnimation();

        yield return new WaitForSeconds(1f);

        enemy.Attack();
        player.Attack();

    }

    public void ChangeRound()
    {
        if (!player.isAlive && !enemy.isAlive)
        {
            ShowLose();
        }
        else if (player.isAlive && !enemy.isAlive)
        {
            ShowWin();
        }
        else if (!player.isAlive && enemy.isAlive)
        {
            ShowLose();
        }
        else
        {
            StartCoroutine(changeRound());
        }
    }

    private IEnumerator changeRound()
    {
        yield return new WaitForSeconds(2f);

        enemy.SetItemsPositionToNextRound();
        player.SetItemsPositionToNextRound();

        timeToStartBattle = 10;
        float waitTime = 0;
        roundNumber++;

        roundDisplay.text = "ROUND " + roundNumber;
        RoundNumberDisplayPage.SetActive(true);
        waitTime = RoundNumberDisplayPage.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;

        yield return new WaitForSeconds(waitTime);

        RoundNumberDisplayPage.SetActive(false);


        enemy.ShowItems();
        yield return new WaitForSeconds(1f);
        player.ShowItems();
        yield return new WaitForSeconds(1f);

        preparationforBattlePage.gameObject.SetActive(true);

        StartCoroutine(startBattle());
        yield return null;
    }



    public void ShowWin()
    {
        WinPage.SetActive(true);
        LosePage.SetActive(false);
    }
    public void ShowLose()
    {
        LosePage.SetActive(true);
        WinPage.SetActive(false);
    }
    public void ShowPause()
    {
        PausePage.SetActive(true);
    }
}
