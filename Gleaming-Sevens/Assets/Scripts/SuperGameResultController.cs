using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SuperGameResultController : MonoBehaviour
{

    [SerializeField]
    private TMP_Text gainText;
    [SerializeField]
    private TMP_Text multiplierText;

    [SerializeField]
    private TMP_Text currentCoinsText;


    private Animator myAnimator;


    private int gain;
    private int multtiplier;
    private int currentCoins;


    [SerializeField]
    private float valuesLerpSpeed;

    public void SetInfo(int _gain, int _multiplier)
    {
        multtiplier = _multiplier;
        currentCoins = SlotPageManager.currentMoneyValue;
        gain = _gain * multtiplier;
        currentCoinsText.text = currentCoins.ToString("#");
    }


    private void OnEnable()
    {
        gainText.text = "0";
        multiplierText.text = "0";
        myAnimator = GetComponent<Animator>();
        CalculateGain();

    }
    private void OnDisable()
    {
        StopAllCoroutines();
        SlotPageManager.currentMoneyValue += gain;
    }

    private void CalculateGain()
    {
        StartCoroutine(lerpMultiplier());
    }

    private IEnumerator lerpMultiplier()
    {
        float buffMultiplier = 0;
        while (buffMultiplier != multtiplier)
        {
            buffMultiplier = Mathf.Lerp(buffMultiplier, multtiplier, valuesLerpSpeed);
            multiplierText.text = buffMultiplier.ToString("#");
            yield return null;
        }
        StartCoroutine(lerpGainValue());
    }
    private IEnumerator lerpGainValue()
    {
        float gainBuff = 0;
        while (gainBuff != gain)
        {
            gainBuff = Mathf.Lerp(gainBuff, gain, valuesLerpSpeed);
            gainText.text = gainBuff.ToString("#");
            yield return null;
        }
        myAnimator.SetInteger("state_index", 1);
    }

    public void IncrementAllCoinPage()
    {
        StartCoroutine(lerpAllCoins());
    }
    private IEnumerator lerpAllCoins()
    {
        float allCoins_Buff = currentCoins;
        float lerpValue = allCoins_Buff + gain;

        while (allCoins_Buff != lerpValue)
        {
            allCoins_Buff = Mathf.Lerp(allCoins_Buff, lerpValue, valuesLerpSpeed);
            currentCoinsText.text = allCoins_Buff.ToString("#");
            yield return null;
        }
    }

}
