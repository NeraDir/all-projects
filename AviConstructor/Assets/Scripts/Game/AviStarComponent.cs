using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AviStarComponent : MonoBehaviour, IAviTriggerComponent
{
    public void Use()
    {
        transform.DOScale(Vector2.zero, 0.25f).OnComplete(() => { AviGameComponent.currentAviStars += 1; Destroy(gameObject); });
    }

    private void LateUpdate()
    {
        if (!AviGameComponent.AviGameIsPlay)
            return;
        transform.position += new Vector3(0, -1, 0) * 300 * Time.deltaTime;
    }
}
