using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PartComponent : MonoBehaviour
{
    private List<PartPlatformComponent> _partPlatformComponents = new List<PartPlatformComponent>();
    private StarComponent[] _stars;

    public void Init()
    {
        _partPlatformComponents = GetComponentsInChildren<PartPlatformComponent>().ToList();
        Vector3 myScale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScaleY(1, 0.05f).OnComplete(() => transform.DOScaleZ(1,0.05f).OnComplete(() => transform.DOScaleX(1,0.25f)));
        _stars = GetComponentsInChildren<StarComponent>();
        foreach (var item in _stars)
        {
            item.gameObject.SetActive(false);
        }
        _stars[Random.Range(0, _stars.Length)].gameObject.SetActive(true);
    }

    public void OnSetPlatformColors(Material targetMaterial)
    {
        foreach (var item in _partPlatformComponents)
        {
            item.Init();
        }
        int rndPlatforIndex = Random.Range(0, _partPlatformComponents.Count);
        _partPlatformComponents[rndPlatforIndex].myMaterial = targetMaterial;
        _partPlatformComponents[rndPlatforIndex].Init();
    }

    public PartPlatformComponent GetRandomPlatform()
    {
        return _partPlatformComponents[Random.Range(0, _partPlatformComponents.Count)];
    }

    public void DestroyMe()
    {
        transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(gameObject));
    }
}
