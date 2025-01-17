using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AviGameConstructionSelectItemComponent : MonoBehaviour, IPointerClickHandler
{
    public TypeOfConstruction constructionType;

    private Image _aviConstructionImage;

    public Sprite aviUseSprite;
    public Sprite aviUseSprite2;

    private void Start()
    {
        _aviConstructionImage = GetComponent<Image>();
        _aviConstructionImage.sprite = aviUseSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (constructionType)
        {
            case TypeOfConstruction.wings:
                AviGameComponent.currentAviWingsSprite = aviUseSprite2;
                break;
            case TypeOfConstruction.main:
                AviGameComponent.currentAviMainSprite = aviUseSprite2;
                break;
            case TypeOfConstruction.turrets:
                AviGameComponent.currentAviTurretsSprite = aviUseSprite2;
                break;
        }
    }
}
