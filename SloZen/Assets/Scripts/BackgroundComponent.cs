using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundComponent : MonoBehaviour
{
   public static string CurrentBackgroundName
    {
        get => PlayerPrefs.GetString("SloZenCurrentBackgroundNameSaveKey", "1");
        set => PlayerPrefs.SetString("SloZenCurrentBackgroundNameSaveKey", value);
    }

    [SerializeField] private Sprite[] _backgroundSprites;

    private Image _backgroundImage;

    private void Awake()
    {
        _backgroundImage = GetComponent<Image>();
    }

    private void LateUpdate()
    {
        if (_backgroundImage != null)
        {
            if (_backgroundImage.sprite.name != CurrentBackgroundName)
            {
                foreach (var sprite in _backgroundSprites)
                {
                    if (sprite.name == CurrentBackgroundName)
                    {
                        _backgroundImage.sprite = sprite;
                        break;
                    }
                }
            }
        }
    }
}
