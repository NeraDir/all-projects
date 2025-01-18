using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jackBetComponent : MonoBehaviour
{
    public int betValue;

    public bool isMinus;

    public Transform spawnPlace;

    public List<jackBetComponent> jackBetComponents = new List<jackBetComponent>();

    public jackBetComponent jackTempBeter;

    public static bool isClicked;

    private void Start()
    {
        Vector3 tempScale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(tempScale, 0.25f);
    }

    private void OnMouseDown()
    {
        if (jackGameManager.canChangeBet)
            return;
        if (isMinus)
        {
            jackGameManager.BetValue -= betValue;
            OnMinus();
        }
        else
        {
            if (jackGameManager.BetValue + betValue > jackLoaderDiceComponent.BestScore)
                return;
            jackGameManager.BetValue += betValue;
            OnPlus();
        }
    }

    private void OnMinus()
    {
        jackTempBeter.jackBetComponents[jackTempBeter.jackBetComponents.Count - 1].transform.DOScale(Vector3.zero, 0.25f);
        Destroy(jackTempBeter.jackBetComponents[jackTempBeter.jackBetComponents.Count - 1]);
        jackTempBeter.jackBetComponents.Remove(jackTempBeter.jackBetComponents[jackTempBeter.jackBetComponents.Count - 1]);

    }

    private void CanClicker() 
    {
        isClicked = false;
    }

    private void OnPlus() {
        jackBetComponent temp = null;
        if (jackBetComponents.Count > 0)
        {
            temp = Instantiate(gameObject.GetComponent<jackBetComponent>(), new Vector3(spawnPlace.position.x, jackBetComponents[jackBetComponents.Count - 1].transform.position.y + 0.1f, spawnPlace.position.z), transform.rotation);
            temp.jackTempBeter = this;
        }
        else
        {
            temp = Instantiate(gameObject.GetComponent<jackBetComponent>(), spawnPlace.position, transform.rotation);
            temp.jackTempBeter = this;
        }
        temp.isMinus = true;
        jackBetComponents.Add(temp);
    }
}
