using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BlaztTriggerButtonPlace : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image _showFruit;

    private Image _placeImage;

    private BlaztFallFruitComponent _fallFruit;

    private bool _hasFruit;

    public void Init(Sprite fruit,Sprite place)
    {
        _placeImage = GetComponent<Image>();
        _showFruit.sprite = fruit;
        _placeImage.sprite = place;
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (_hasFruit)
        {
            BlaztGameManager.score += Random.Range(35, 75);
            if (_fallFruit != null)
            {
                _fallFruit.OnClickUse();
            }
        }
        else
        {
            Handheld.Vibrate();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BlaztFallFruitComponent faller))
        {
            if (faller.GetComponent<Image>().sprite == _showFruit.sprite)
            {
                _hasFruit = true;
                _fallFruit = faller;
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        _hasFruit = false;
        _fallFruit = null;
    }
}
