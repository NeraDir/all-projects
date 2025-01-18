using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MightXorOComponent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private TMP_Text showWhoTurn;

    public bool isPlayerTuned;
    public bool isBotTurned;

    

    [SerializeField]
    private Image whatIsIt;

    [SerializeField]
    private Sprite x;
    [SerializeField]
    private Sprite o;

    [SerializeField]
    private MightXorOComponent[] additionalCells;

    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private TMP_Text showWhichResult;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isPlayerTuned || isBotTurned)
            return;
        if (!MightGameController.botTurn)
        {
            isPlayerTuned = true;
            whatIsIt.sprite = x;
            MightGameController.botTurn = true;
            showWhoTurn.text = "BOT TURN";
            FindObjectOfType<MightGameController>().botCells.Remove(this);
            if (FindObjectOfType<MightGameController>().GetPlayerWon() != null)
            {
                showWhichResult.text = "YOU WON";
                Invoke(nameof(ClosePage), 2);
            }
            else
            {
                if (FindObjectOfType<MightGameController>().botCells.Count == 0)
                {
                    showWhichResult.text = "DRAW";
                    Invoke(nameof(ShowResultPage), 2);
                }
            }
        }
    }

    private void ShowResultPage() 
    {
        gameOverPanel.SetActive(true);
    }

    private void ClosePage() 
    {
        transform.parent.parent.gameObject.SetActive(false);
        MightGameController.mightHearts = 3;
    }

    public void OnClickBotTurn()
    {
        isBotTurned = true;
        whatIsIt.sprite = o;
        FindObjectOfType<MightGameController>().botCells.Remove(this);
        MightGameController.botTurn = false;
        showWhoTurn.text = "YOU TURN";
        if (FindObjectOfType<MightGameController>().GetBotWon() != null)
        {
            showWhichResult.text = "BOT WON";
            Invoke(nameof(ShowResultPage), 2);
        }
        else
        {
            if (FindObjectOfType<MightGameController>().botCells.Count == 0)
            {
                showWhichResult.text = "DRAW";
                Invoke(nameof(ShowResultPage), 2);
            }
        }
    }
}
