using System.Collections;
using UnityEngine;

public class jackRerolButton : MonoBehaviour
{
    public jackGameDiceComponent[] dices;

    private bool isFirst;

    private bool isClicked;

    private void OnMouseDown()
    {
        if (isFirst)
        {
            if (isClicked)
                return;
            if (jackGameManager.BetValue == 0)
                return;
            if (jackGameManager.canRerol)
                return;
            if (jackGameManager.RerolCount <= 0)
                return;
            jackGameManager.RerolCount--;
            isClicked = true;
            StartCoroutine(Launching());
        }
        else
        {
            if (isClicked)
                return;
            if (jackGameManager.BetValue == 0)
                return;
            isFirst = true;
            isClicked = true;
            jackGameManager.canChangeBet = true;
            StartCoroutine(Launcher());
        }
    }

    private IEnumerator Launcher() 
    {
        foreach (var d in dices)
        {
            d.Launch();
            yield return new WaitForSeconds(0.5f);
        }
        isClicked = false;
    }

    private IEnumerator Launching()
    {
        foreach (var d in dices)
        {
            jackGameManager.temper -= d.rotateValues;
            d.ReLaunch(true);
            yield return new WaitForSeconds(0.5f);
        }
        isClicked = false;
    }
}
