using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MagicXPlaceComponente : MonoBehaviour
{
    private TMP_Text text;

    public int valueX;

    private Image image;

    public Sprite SmallX;
    public Sprite BigX;

    private void Start()
    {
        image = GetComponent<Image>();
        text = GetComponentInChildren<TMP_Text>();
        valueX = Random.Range(1, 30);
        if (valueX < 20)
        {
            image.sprite = SmallX;
        }
        else
        {
            image.sprite = BigX;
        }
        text.text = "x " + valueX.ToString("0");
    }
}
