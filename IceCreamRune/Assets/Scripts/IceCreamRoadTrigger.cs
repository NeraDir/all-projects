using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IceCreamRoadTrigger : MonoBehaviour
{
    public bool isLatRoad;

    private Animator animator;

    [SerializeField]
    private SpriteRenderer[] spriteRenderers;
    
    public static UnityEvent iceRusherLevelEnd = new UnityEvent();

    private void Start()
    {
        if (!isLatRoad)
            return;
        animator = GetComponentInParent<Animator>();
        animator.enabled = false;
    }

    public bool End() 
    {
        if (isLatRoad)
        {
            animator.enabled = true;
            for (int i = 0; i < IceCreamGameManager._currentContainers.Count; i++)
            {
                spriteRenderers[i].gameObject.SetActive(true);
                spriteRenderers[i].sprite = IceCreamGameManager._currentContainers[i].iceCreamSprite;
            }
            iceRusherLevelEnd?.Invoke();
            return true;
        }
        return false;
    }
}
