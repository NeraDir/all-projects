using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class prodigCellComponent : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
{
    public static bool touchMoving;

    public bool isSelected;

    public Sprite sprite;

    public static bool isFirstSelected;

    public RectTransform recter;

    public Animator animator;

    public void INIT()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
        sprite = GetComponent<Image>().sprite;
    }

    private bool GetStates() 
    {
        for (int i = 0; i < prodigGameManager.currentSpritesCombination.Count; i++)
        {
            if (prodigGameManager.currentSpritesCombination[i].sprite.name != prodigGameManager.needSpritesCombination[i].name)
            {
                return false;
            }
        }
        return true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isFirstSelected)
            return;
        isFirstSelected = true;
        isSelected = true;
        animator.enabled = true;
        prodigGameManager.currentSpritesCombination.Add(this);
        prodigGameManager.currentSpritesTransforms.Add(recter);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isFirstSelected)
            return;
        if (isSelected)
            return;
        isSelected = true;
        animator.enabled = true;
        prodigGameManager.currentSpritesCombination.Add(this);
        prodigGameManager.currentSpritesTransforms.Add(recter);
        StartCoroutine(Wait());
    }

    private IEnumerator Wait() 
    {

        yield return new WaitForSeconds(0.1f);
        if (prodigGameManager.currentSpritesCombination.Count == 5)
        {
            if (GetStates())
            {
                Debug.LogError("WON");
                Vector2[] hi = prodigGameManager.liner.Points;
                prodigGameManager.liner.Points = new Vector2[hi.Length];
                foreach (var item in prodigGameManager.currentSpritesCombination)
                {
                    item.animator.enabled = false;
                    item.transform.localScale = new Vector3(1, 1, 1);
                }
                prodigGameManager.score++;
                prodigGameManager.win = true;
            }
            else
            {
                foreach (var item in prodigGameManager.currentSpritesCombination)
                {
                    item.isSelected = false;
                    touchMoving = false;
                }
                isFirstSelected = false;
                foreach (var item in prodigGameManager.currentSpritesCombination)
                {
                    item.animator.enabled = false;
                    item.transform.localScale = new Vector3(1, 1, 1);
                }
                prodigGameManager.currentSpritesCombination.Clear();
                prodigGameManager.currentSpritesTransforms.Clear() ;
                Vector2[] hi = prodigGameManager.liner.Points;
                prodigGameManager.liner.Points = new Vector2[hi.Length];

                Debug.LogError("LOOSE");
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        foreach (var item in prodigGameManager.currentSpritesCombination)
        {
            item.isSelected = false;
            touchMoving = false;
        }
        isFirstSelected = false;
        foreach (var item in prodigGameManager.currentSpritesCombination)
        {
            item.animator.enabled = false;
            item.transform.localScale = new Vector3(1, 1, 1);
        }
        prodigGameManager.currentSpritesCombination.Clear();
        prodigGameManager.currentSpritesTransforms.Clear();
        Vector2[] hi = prodigGameManager.liner.Points;
        prodigGameManager.liner.Points = new Vector2[hi.Length];

    }
}
