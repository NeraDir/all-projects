using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BerryComponent : MonoBehaviour, IPointerClickHandler
{
    private int clickCount;

    private Vector3 _addScale;

    private void Start()
    {
        clickCount = 0;
        transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        for (int i = 0; i < GameManager.CurrentLevel; i++)
        {
            if (i % 4 == 0)
            {
                clickCount += 1;
            }
        }
        Debug.Log(clickCount);
        if (clickCount > 1)
        {
            clickCount -= 1;
        }
        float value = (0.7f/ (float)clickCount);
        _addScale = new Vector3(value, value, value);
    }

    private void LateUpdate()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.DOScale(transform.localScale + _addScale, 0.025f).OnComplete(() => {
            if (transform.localScale.x >= 1)
            {
                transform.DOMove(GameManager.BasketImage.transform.position, 0.1f).OnComplete(() => transform.DOScale(Vector3.zero, 0.1f).OnComplete(() =>
                {
                    if (GameManager._isSimpleMode)
                    {
                        GameManager.CurrentCountFruits += 1;
                        Destroy(gameObject);
                    }
                    else
                    {
                        if (transform.GetComponent<Image>().sprite == GameManager.TargetFruit)
                        {
                            GameManager.CurrentCountFruits += 1;
                            Destroy(gameObject);
                        }
                        else
                        {
                            Destroy(gameObject);
                        }
                    }
                }));
            }
        });
    }
}
