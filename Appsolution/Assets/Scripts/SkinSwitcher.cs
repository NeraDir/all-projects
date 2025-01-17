using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSwitcher : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _skins;

    private void Start()
    {
        if (PlayerPrefs.HasKey("CurrentSkinIndex"))
        {
            _skins[PlayerPrefs.GetInt("CurrentSkinIndex")].SetActive(true);
        }
        else
        {
            _skins[0].SetActive(true);
        }
      
    }
}
