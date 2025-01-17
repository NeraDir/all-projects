using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blaztBerryComponent : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _berrySprites;

    private Image _berryImage;

    private void Start()
    {
        _berryImage = GetComponent<Image>();
        _berryImage.sprite = _berrySprites[Random.Range(0,_berrySprites.Length)];
    }
}
