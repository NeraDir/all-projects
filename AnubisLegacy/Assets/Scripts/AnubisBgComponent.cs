using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnubisBgComponent : MonoBehaviour
{
    private Image _iamge;

    private void Start()
    {
        _iamge = GetComponent<Image>();
    }

    private void LateUpdate()
    {
        if (_iamge == null)
            return;
        if (_iamge.sprite == null)
        {
            Sprite sprite = Resources.Load<Sprite>($"AnubisBg/{AnubisUserData.CurrentBackgroundName}");
            _iamge.sprite = sprite;
        }
        else
        {
            if (_iamge.sprite.name != AnubisUserData.CurrentBackgroundName)
            {
                Sprite sprite = Resources.Load<Sprite>($"AnubisBg/{AnubisUserData.CurrentBackgroundName}");
                _iamge.sprite = sprite;
            }
        }
    }
}
