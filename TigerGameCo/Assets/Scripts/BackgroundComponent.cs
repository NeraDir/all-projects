using UnityEngine;
using UnityEngine.UI;

public class BackgroundComponent : MonoBehaviour
{
    [SerializeField] private Sprite[] _backgroundSprites;
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
        _image.sprite = _backgroundSprites[TigerClawsGameData.TigerClawsSelectedBackgroundIndex];
    }

    private void LateUpdate()
    {
        if(_image != null)
            _image.sprite = _backgroundSprites[TigerClawsGameData.TigerClawsSelectedBackgroundIndex];
    }
}
