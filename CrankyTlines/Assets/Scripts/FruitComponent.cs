using UnityEngine;
using UnityEngine.UI;

public class FruitComponent : MonoBehaviour
{
    private Image _image;

    public void SetupData(Sprite sprite)
    {
        _image = GetComponent<Image>();
        _image.sprite = sprite;
    }
}
