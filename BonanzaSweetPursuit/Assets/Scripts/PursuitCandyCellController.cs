using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PursuitCandyCellController : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
{
    public bool isChoosed;

    public Sprite PursuitSprite;

    public void INIT()
    {
        PursuitSprite = GetComponent<Image>().sprite;
    }

    private bool GetPursuitCellState() 
    {
        for (int i = 0; i < PursuitGameManager.pursuitCurrentSpritesCombinationList.Count; i++)
        {
            if (PursuitGameManager.pursuitCurrentSpritesCombinationList[i].PursuitSprite.name != PursuitGameManager.pursuitNeeedSpriteCombinationList[i].name)
            {
                return false;
            }
        }
        return true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (PursuitGameManager.isFirstPursuitCandyCellChoosed)
            return;
        PursuitGameManager.isFirstPursuitCandyCellChoosed = true;
        transform.DOScale(transform.localScale /= 1.1f, 0.25f);
        isChoosed = true;
        PursuitGameManager.pursuitCurrentSpritesCombinationList.Add(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!PursuitGameManager.isFirstPursuitCandyCellChoosed)
            return;
        if (isChoosed)
            return;
        isChoosed = true;
        transform.DOScale(transform.localScale /= 1.1f, 0.25f);
        PursuitGameManager.pursuitCurrentSpritesCombinationList.Add(this);
        StartCoroutine(CheckingCombinations());
    }

    private IEnumerator CheckingCombinations()
    {
        yield return new WaitForSeconds(0.1f);
        if (PursuitGameManager.pursuitCurrentSpritesCombinationList.Count == 5)
        {
            if (GetPursuitCellState())
            {
                foreach (var item in PursuitGameManager.pursuitCurrentSpritesCombinationList)
                {
                    item.transform.DOScale(Vector3.one, 0.25f);
                }
                PursuitGameManager.gameIsEnd?.Invoke(false);
            }
            else
            {
                foreach (var item in PursuitGameManager.pursuitCurrentSpritesCombinationList)
                {
                    item.isChoosed = false;
                }
                foreach (var item in PursuitGameManager.pursuitCurrentSpritesCombinationList)
                {
                    item.transform.DOScale(Vector3.one, 0.25f);
                }
                PursuitGameManager.isFirstPursuitCandyCellChoosed = false;
                PursuitGameManager.pursuitCurrentSpritesCombinationList.Clear();
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        foreach (var item in PursuitGameManager.pursuitCurrentSpritesCombinationList)
        {
            item.isChoosed = false;
        }
        foreach (var item in PursuitGameManager.pursuitCurrentSpritesCombinationList)
        {
            item.transform.DOScale(Vector3.one, 0.25f);
        }
        PursuitGameManager.isFirstPursuitCandyCellChoosed = false;
        PursuitGameManager.pursuitCurrentSpritesCombinationList.Clear();
    }
}
