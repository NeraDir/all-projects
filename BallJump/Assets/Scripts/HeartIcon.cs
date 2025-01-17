using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeartIcon : MonoBehaviour
{
    [SerializeField]
    private HeartIconState iconState;

    private GameObject activeHeartIcon;

    [SerializeField]
    private GameObject buyButton;
    private TMP_Text priceText;


    [SerializeField]
    private HeartIcon nextIcon;

    [SerializeField]
    private int price;
    

    private void Start()
    {
        activeHeartIcon = transform.GetChild(0).gameObject;

        priceText = buyButton.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();

        priceText.text = price.ToString();

        iconState = HeartIconState.Active;

        ProcessState();
    }


    public void ChangeState(HeartIconState futureState)
    {
        iconState = futureState;
        ProcessState();
    }

    public void ProcessState()
    {
        if (iconState == HeartIconState.Active)
        {
            buyButton.SetActive(false);
            activeHeartIcon.SetActive(true);
        }
        else if(iconState == HeartIconState.Inactive)
        {
            buyButton.SetActive(true);
            activeHeartIcon.SetActive(false);
        }
        else
        {
            buyButton.SetActive(false);
            activeHeartIcon.SetActive(false);
        }
    }

    public void Buy()
    {
        if (BallConfigsController.coinCount - price >= 0)
        {
            BallConfigsController.coinCount -= price;
            BallConfigsController.ballHealth++;
            iconState = HeartIconState.Active;

            if (nextIcon != null)
            {

                nextIcon.ChangeState(HeartIconState.Inactive);
            } 

            ProcessState();
        }
    }
}

public enum HeartIconState
{
    Active,
    Inactive,
    Block
}
