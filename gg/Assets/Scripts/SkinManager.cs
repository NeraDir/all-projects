using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    [SerializeField]
    private Image[] _planeSkins;
    [SerializeField]
    private Image _currentPlaneSkin;

    private void Awake()
    {
        SetSkin();
    }
    public void SetSkin()
    {
        if (PlayerPrefs.HasKey("CurrentSkin"))
        {
            _currentPlaneSkin.sprite = _planeSkins[PlayerPrefs.GetInt("CurrentSkin")].sprite;
        }
        else
        {
            _currentPlaneSkin.sprite = _planeSkins[0].sprite;
        } 
    }

}
