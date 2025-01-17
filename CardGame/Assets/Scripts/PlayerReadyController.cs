using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerReadyController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameManager gameManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        gameManager.StartRotateAllCard();
        //StartCoroutine(gameManager.RotateAllCards(2f, false));
        gameObject.SetActive(false);
    }
}
