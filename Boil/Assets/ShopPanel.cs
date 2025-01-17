using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ShopPanel : MonoBehaviour
{
    public List<ShopItem> items;

    public Transform activeBallIndificator;

    public MenuPage menuPage;

    private Animator mAnimator;

    public TMP_Text coinCountDisplay;


    private void OnEnable()
    {
        ShopItem.ItemEquptedEvent += ShowActiveItem;

        for(int i = 0; i < items.Count; i++)
        {
            items[i].LoadItemData();
        }
        coinCountDisplay.text = Configs.allCoinsCount.ToString();
        SetActiveIndificator();
    }
    private void OnDisable()
    {
        ShopItem.ItemEquptedEvent -= ShowActiveItem;
    }

    private void Start()
    {
        mAnimator = GetComponent<Animator>();
    }




    public void ShowActiveItem(int newActiveItemIndex)
    {
        for(int i = 0; i < items.Count; i++)
        {
            if(newActiveItemIndex != items[i].index && items[i].stateIndex == 2)
            {
                items[i].ChangeState(1);
            }

        }
        Configs.ballSkinIndex = newActiveItemIndex;
        coinCountDisplay.text = Configs.allCoinsCount.ToString();
        SetActiveIndificator();

        coinCountDisplay.text = Configs.allCoinsCount.ToString();
    }

    private void SetActiveIndificator()
    {
        for(int i = 0; i < items.Count; i++)
        {
            if (items[i].stateIndex == 2)
            {
                activeBallIndificator.position = items[i].transform.position;
                return;
            }
        }
    }



    public void CloseMe()
    {
        PlayCloseAnimation();
        StartCoroutine(CloseAndOpenMenu());

    }



    public void PlayOpenAnimation()
    {
        mAnimator.SetInteger("key", 0);
    }
    public void PlayCloseAnimation()
    {
        mAnimator.SetInteger("key", 1);
    }


    private IEnumerator CloseAndOpenMenu()
    {

        float waitTime = mAnimator.runtimeAnimatorController.animationClips[1].length;


        yield return new WaitForSeconds(waitTime);

        gameObject.SetActive(false);
        menuPage.gameObject.SetActive(true);
    }
}
