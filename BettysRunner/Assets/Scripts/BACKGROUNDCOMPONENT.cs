using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BACKGROUNDCOMPONENT : MonoBehaviour
{
    [SerializeField]
    private SHOPDATA _shopData;

    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    private void LateUpdate()
    {
        if (_image != null) 
            _image.sprite = _shopData.shopItems[PLAYERDATA.BACKGROUNDINDEX].sprite;
    }
}
