using System.Collections;
using UnityEngine;

public class jackCheckButtonComponent : MonoBehaviour
{
    public jackGameDiceComponent[] dices;

    private bool isClicked;

    private void OnMouseDown()
    {
        if (jackGameManager.BetValue == 0)
            return;
        if (jackGameManager.score == 0)
            return;
        if (isClicked)
            return;
        isClicked = true;
        jackGameManager.canRerol = true;
        StartCoroutine(Launching());
    }

    private IEnumerator Launching()
    {
        foreach (var d in dices)
        {
            d.Launch();
            dices[dices.Length - 1].isLast = true;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
