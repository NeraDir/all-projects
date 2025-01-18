using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePlayPageController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text coinCount;

    [SerializeField]
    private TMP_Text spinCount;

    [SerializeField]
    private GameObject pausePanel;


    private Animator myAnimator;

    private void OnEnable()
    {
        Box.BoxHasBeenTrigger += CloseGamePlayUIForSlot;

        myAnimator = GetComponent<Animator>();
        coinCount.text = GameData.Money.ToString();

    }
    private void OnDisable()
    {
        Box.BoxHasBeenTrigger -= CloseGamePlayUIForSlot;
    }

    private void Update()
    {
        spinCount.text = LevelController.spinCount.ToString();
        coinCount.text = GameData.Money.ToString();
    }


    public void CloseGamePlayUIForSlot()
    {
        myAnimator.SetInteger("index", 2);
    }

    public void DisabledPage()
    {
        gameObject.SetActive(false);
    }

    public void ClickPauseButton()
    {
        myAnimator.SetInteger("index", 1);
    }

    public void ShowPausePage()
    { 
        pausePanel.SetActive(true);
        DisabledPage();
    }

}
