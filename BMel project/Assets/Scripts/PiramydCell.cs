using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PiramydCell : MonoBehaviour
{

    private Image backgroundImage;
    private Image chilldImage;
    private CandyItem mItem;

    public void Init(CandyItem candyItem)
    {
        backgroundImage = GetComponent<Image>();
        chilldImage = transform.GetChild(0).gameObject.GetComponentInChildren<Image>();

        mItem = candyItem;
        backgroundImage.sprite = mItem.GetSprite();
        chilldImage.sprite = mItem.GetSprite();

        chilldImage.gameObject.SetActive(false);

    }


    public CandyItem GetCandyItem()
    {
        return mItem;
    }



    public void ActivateCell()
    {
        GetComponent<Animator>().SetInteger("var", 1);
        chilldImage.gameObject.SetActive(true);
    }


}
