using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class LevelNumberUIPage : MonoBehaviour
{
    [SerializeField]
    private TMP_Text levelNumberText;
    [SerializeField]
    private GameObject nextPage;


    private void OnEnable()
    {
        levelNumberText.text = GamePlayController.levelNumber.ToString("LEVEL #");
    }
    private void OnDisable()
    {
        nextPage.SetActive(true);
    }

    public void CloseMyPage()
    {
        gameObject.SetActive(false);
    }
}
