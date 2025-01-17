using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunsGameMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _tutorPage;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("SunsOfEgyptTutorSeeSaveKeyt"))
        {
            _tutorPage.SetActive(true);
            PlayerPrefs.SetInt("SunsOfEgyptTutorSeeSaveKeyt", 1);
        }
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
