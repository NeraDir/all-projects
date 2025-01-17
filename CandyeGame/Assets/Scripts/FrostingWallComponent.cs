using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FrostingWallComponent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Quaternion locked;

    [SerializeField]
    private Quaternion open;

    private bool cantRotate;

    public bool isOpen;

    private void Start()
    {
        cantRotate = false;
        transform.rotation = locked;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (cantRotate)
            return;
        cantRotate = true;
        isOpen = !isOpen;
        transform.DORotateQuaternion(isOpen == true ? open : locked,0.25f).OnComplete(() => cantRotate = false);
    }
}
