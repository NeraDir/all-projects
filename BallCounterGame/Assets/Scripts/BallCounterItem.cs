using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallCounterItem : MonoBehaviour
{
    [SerializeField]
    private ColorType colorType;
    [SerializeField]
    private TMP_Text countText;

    private int countValue;

    private Animator myAnimator;
    private int inncementValue;

    private Color defaultColor;
    private Color rightColor;
    private Color wrongColor;


    public void Init(Color defaultColor, Color rightColor, Color wrongColor)
    {
        this.defaultColor = defaultColor;
        this.rightColor = rightColor;
        this.wrongColor = wrongColor;


        myAnimator = GetComponent<Animator>();
        ResetItem();
    }

    private void OnEnable()
    {
  
    }

    public void ResetItem()
    {
        ChangeNuumberColor(defaultColor);
        countValue = 0;
        DissableTextAnimator();
    }

    // Update is called once per frame
    private void Update()
    {
        countText.text = (countValue == 0 ? "0" : countValue.ToString());
    }

    public void IncrementValue()
    {
        inncementValue = 1;
        PlayChangeNumberTextAnimation();
        //countValue++;
    }
    public void DicrementValue()
    {

        if (countValue > 0)
        {
            inncementValue = -1;
            PlayChangeNumberTextAnimation();
        }

    }

    public (ColorType, int) GetColorAndValue()
    {
        return (colorType, countValue);
    }


    public void EnableTextAnimator()
    {
        myAnimator.enabled = true;
    }
    private void DissableTextAnimator()
    {
        myAnimator.enabled = true;
    }


    public void PlayEnableTextAnimation()
    {
        myAnimator.SetInteger("parameterID", 0);
    }
    public void PlayLoopTextAnimation()
    {
        myAnimator.SetInteger("parameterID", 1);
    }
    public void PlayChangeNumberTextAnimation()
    {
        myAnimator.SetInteger("parameterID", 2);
    }
    public void PlayShowRightNumberAnimation()
    {
        myAnimator.SetInteger("parameterID", 3);
    }

    public void ChangeNumber()
    { 
        countValue += inncementValue;
    }

    public void ShowRightNumber()
    {
        if (colorType == ColorType.Red)
        {
            countValue = GamePlayController.redBallCountInScene;
        }
        else if (colorType == ColorType.Green)
        {
            countValue = GamePlayController.greenBallCountInScene;
        }
        else if (colorType == ColorType.Blue)
        {
            countValue = GamePlayController.blueBallCountInScene;
        }
    }

    public void ShowRigthBallCount()
    {
        bool numberIsRight = false;

        numberIsRight =
            (colorType == ColorType.Red && countValue == GamePlayController.redBallCountInScene ? true : false) ||
            (colorType == ColorType.Green && countValue == GamePlayController.greenBallCountInScene ? true : false) ||
            (colorType == ColorType.Blue && countValue == GamePlayController.blueBallCountInScene ? true : false);


        if (numberIsRight)
        {

            ChangeNuumberColor(rightColor);
        }
        else
        {
            GamePlayController.hasRigthAnswerByPlayer = false;
            ChangeNuumberColor(wrongColor);
        }

        PlayShowRightNumberAnimation();
    }


    private void ChangeNuumberColor(Color newColor)
    {
        countText.color = newColor;
    }

}
public enum ColorType
{
    Red,
    Green,
    Blue
}
