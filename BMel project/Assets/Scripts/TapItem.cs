using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TapItem : MonoBehaviour, IPointerClickHandler
{


    public Image backgroundImage;
    private Image chilldImage;

    public float incrementScaleValue;
    public float dicrementScaleSpeed;


    private Coroutine DicrementScaleCor;

    private CandyItem mItem;

    public delegate void TapToCellCompleteDelegat();
    public static event TapToCellCompleteDelegat TapToCellCompleteEvent;
    public static event TapToCellCompleteDelegat TapIsZeroScaleEvent;

    public GameObject tapeffectPrefab;

    private bool canTap = true;

    public Animator parentAnimator;

    private void OnEnable()
    {
        chilldImage = GetComponent<Image>();
    }
    public void SetItem(CandyItem candyItem)
    {
        mItem = candyItem;
        backgroundImage.sprite = mItem.GetSprite();
        chilldImage.sprite = mItem.GetSprite();

        incrementScaleValue -= 0.01f;

    }

    public void StartDicrementScale()
    {

        transform.localScale = Vector3.one;

        if (DicrementScaleCor != null)
            return;

        DicrementScaleCor = StartCoroutine(DicrementScale());
    }

    private IEnumerator DicrementScale()
    {
        yield return new WaitForSeconds(1f);

        canTap = true;

        while(transform.localScale.x > 0)
        {
            transform.localScale -= Vector3.one * dicrementScaleSpeed;
            yield return null;
        }

        if (TapIsZeroScaleEvent != null)
            TapIsZeroScaleEvent();
        //Debug.Log("game over!");
    }



    public void OnPointerClick(PointerEventData eventData)
    {

        if (!canTap)
            return;

        transform.localScale += Vector3.one * incrementScaleValue;

        Debug.Log(transform.localScale.x);

        if(transform.localScale.x >= 1)
        {
            StopCoroutine(DicrementScaleCor);
            DicrementScaleCor = null;

            if (TapToCellCompleteEvent != null)
            {
                TapToCellCompleteEvent();
            }

            canTap = false;


            Debug.Log("done");
        }

        SpawnEffect();
        
    }


    private void SpawnEffect()
    {
       GameObject effect = Instantiate(tapeffectPrefab, Input.mousePosition, Quaternion.identity, transform);
        Destroy(effect, effect.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length);
    }

    public void ShowMe()
    {
        parentAnimator.SetInteger("var", 0);
    }
    public void HideMe()
    {
        parentAnimator.SetInteger("var", 1);
    }
}
