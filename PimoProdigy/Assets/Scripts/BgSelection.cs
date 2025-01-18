using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BgSelection : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private BgSelection[] selections;

    [SerializeField]
    private int index;

    [SerializeField]
    private Color selected;

    [SerializeField]
    private Color unSelected;

    private Image selectionImage;

    private void Start()
    {
        selectionImage = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SettingsManager.changeBg?.Invoke(index);
        foreach (var item in selections)
        {
            item.Set();
        }
    }

    public void Set()
    {
        if(SettingsManager.bgIndex != index)
        {
            selectionImage.color = unSelected;
        }
        else
        {
            selectionImage.color = selected;
        }
    }
}
