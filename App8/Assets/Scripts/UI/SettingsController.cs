using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsController : MonoBehaviour, IPointerClickHandler
{
    public GameObject Crest;

    public static int SoundsToggle
    {
        get
        {
            if (!PlayerPrefs.HasKey("SoundsToggle"))
                return 0;

            return PlayerPrefs.GetInt("SoundsToggle");
        }
        set
        {
            PlayerPrefs.SetInt("SoundsToggle", value);
        }
    }

    private void Start()
    {
        if(SoundsToggle == 0)
        {
            Crest.SetActive(false);
        }
        else
        {
            Crest.SetActive(true);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (SoundsToggle == 0)
        {
            SoundsToggle = 1;
            Crest.SetActive(true);
        }
        else
        {
            SoundsToggle = 0;
            Crest.SetActive(false);
        }
    }
}
