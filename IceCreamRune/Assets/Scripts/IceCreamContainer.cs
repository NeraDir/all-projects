using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class IceCreamContainer : MonoBehaviour
{
    private Image _iceCreamImage;

    private Text _iceCreamText;

    private int iceCreamMaxCount;
    private int iceCreamCurrentCount;

    private int _iceCreamIndex;

    public bool iceCreamReady;

    public Sprite iceCreamSprite;

    public static UnityEvent<int> iceCreamContainerUpdate = new UnityEvent<int>();

    public void Init()
    {
        _iceCreamImage = GetComponent<Image>();
        _iceCreamText = GetComponentInChildren<Text>();
        iceCreamContainerUpdate.AddListener(IceUpdate);
    }

    private void IceUpdate(int index)
    {
        if (_iceCreamIndex != index)
            return;
        iceCreamCurrentCount++;
        if (iceCreamCurrentCount >= iceCreamMaxCount)
        {
            iceCreamReady = true;
            iceCreamCurrentCount = iceCreamMaxCount;
        }
        _iceCreamText.text = iceCreamCurrentCount.ToString() + "/" + iceCreamMaxCount.ToString();
    }

    public void SetData(Sprite sprite, int count,int indexw) 
    {
        _iceCreamIndex = indexw;
        iceCreamSprite = sprite;
        iceCreamMaxCount = count;
        _iceCreamImage.sprite = sprite;
        _iceCreamText.text = iceCreamCurrentCount.ToString() + "/" + iceCreamMaxCount.ToString();
    }
}
