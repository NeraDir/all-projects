using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxColliderAdaptive : MonoBehaviour
{
    [SerializeField] public int imageNumber;
    [SerializeField] public List<Image> images = new List<Image>();
    [SerializeField] public GameManager gameManager;
    void Start()
    {
        Vector2 sizeBoxCollider2D = new Vector2(gameObject.GetComponent<RectTransform>().rect.width,
            gameObject.GetComponent<RectTransform>().rect.height);
        gameObject.GetComponent<BoxCollider2D>().size = sizeBoxCollider2D;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                var p = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                var hit = Physics2D.Raycast(p, Vector2.zero);

                if (hit.collider != null)
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        GameManager.scoreToComplete--;
                        
                        gameManager.ChangeState();
                        
                        if (imageNumber == 1)
                        {
                            images[0].gameObject.SetActive(false);
                            images[1].gameObject.SetActive(true);
                            
                        }
                        else
                        {
                            images[0].gameObject.SetActive(true);
                            images[1].gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }
}