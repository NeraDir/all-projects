using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CrazyTableCellComponent : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
{
    [SerializeField]
    private Image _showCellImage;

    private Sprite _cellSprite;

    private bool _interacteble;

    public bool isSelected;

    public static bool touchMoving;

    public static bool isFirstSelected;

    public void Init(Sprite cellSprite, bool interacteble)
    {
        _cellSprite = cellSprite;
        _showCellImage.sprite = _cellSprite;
        _interacteble= interacteble;
    }

    private bool GetStates()
    {
        for (int i = 0; i < CrazyGameControllerComponent.needCombinations.Count; i++)
        {
            if (CrazyGameControllerComponent.currentCombintaions[i].name != CrazyGameControllerComponent.needCombinations[i].name)
            {
                return false;
            }
        }
        return true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_interacteble)
            return;
        if (isFirstSelected)
            return;
        isFirstSelected = true;
        isSelected = true;
        transform.localScale *= 1.2f;
        CrazyGameControllerComponent.currentCombintaions.Add(_cellSprite);
        CrazyGameControllerComponent.currentCombintaionTransforms.Add(transform);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_interacteble)
            return;
        if (!isFirstSelected)
            return;
        if (isSelected)
            return;
        isSelected = true;
        transform.localScale *= 1.2f;
        CrazyGameControllerComponent.currentCombintaions.Add(_cellSprite);
        CrazyGameControllerComponent.currentCombintaionTransforms.Add(transform);
        StartCoroutine(Waiting());
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_interacteble)
            return;
        foreach (var item in CrazyGameControllerComponent.currentCombintaionTransforms)
        {
            item.GetComponent<CrazyTableCellComponent>().isSelected = false;
            item.transform.localScale = new Vector3(1, 1, 1);
            touchMoving = false;
        }
        isFirstSelected = false;
        CrazyGameControllerComponent.currentCombintaions.Clear();
        CrazyGameControllerComponent.currentCombintaionTransforms.Clear();
    }

    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(0.1f);
        if (CrazyGameControllerComponent.needCombinations.Count == CrazyGameControllerComponent.currentCombintaions.Count)
        {
            if (GetStates())
            {
                foreach (var item in CrazyGameControllerComponent.currentCombintaionTransforms)
                {
                    item.transform.localScale = new Vector3(1, 1, 1);
                }
                CrazyGameControllerComponent.OnCombintaionGet?.Invoke(true);
            }
            else
            {
                foreach (var item in CrazyGameControllerComponent.currentCombintaionTransforms)
                {
                    item.GetComponent<CrazyTableCellComponent>().isSelected = false;
                    item.transform.localScale = new Vector3(1, 1, 1);
                    touchMoving = false;
                }
                isFirstSelected = false;
                CrazyGameControllerComponent.currentCombintaions.Clear();
                CrazyGameControllerComponent.currentCombintaionTransforms.Clear();
            }
        }
    }
}
