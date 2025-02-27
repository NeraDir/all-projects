using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelItemComponent : MonoBehaviour
{
    [SerializeField] private Sprite[] _gemSprites;

    public CrystallType type;
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
        type = (CrystallType)Random.Range(0, 6);
        _image.sprite = _gemSprites[(int)type];
    }
}
