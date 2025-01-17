using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingRandomFruits : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _fruitsPack;

    private List<Image> _fruitsImages = new List<Image>();

    [ContextMenu("SetImages")]
    public void SetImages() 
    {
        _fruitsImages.Clear();
        foreach (var item in transform.GetComponentsInChildren<Image>())
        {
            _fruitsImages.Add(item);
        }
        _fruitsImages.Remove(_fruitsImages[0]);
    }

    public void SetNewPackOfFruits() 
    {
        foreach (var item in _fruitsImages)
        {
            item.sprite = _fruitsPack[Random.Range(0, _fruitsPack.Length)];
        }
    }
}
