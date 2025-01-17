using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemCell : MonoBehaviour, IPointerClickHandler
{
    public PlayerController controller;
    public bool IsActivated = false;

    float Timer = 0f;
    Image IMG;

    private void Start()
    {
        IMG = GetComponent<Image>();
    }

    private void Update()
    {
        if (IsActivated)
        {
            Timer += Time.deltaTime;

            if (Timer >= controller.TimeToDisactivateCell)
            {
                IMG.color = new Color(100 / 255.0f, 100 / 255.0f, 100 / 255.0f, 255);
                Timer = 0f;
                IsActivated = false;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsActivated)
        {
            IMG.color = new Color(255 / 255.0f, 255 / 255.0f, 255 / 255.0f, 255);
            IsActivated = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!IsActivated)
            {
                controller.DecreaseHealth();
            }
            else
            {

            }
        }
    }
}
