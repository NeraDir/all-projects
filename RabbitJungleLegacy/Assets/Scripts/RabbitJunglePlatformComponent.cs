using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RabbitJunglePlatformComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject _rabbitJungleEggObject;

    public static UnityEvent<RabbitJunglePlatformComponent> isPlatformEnded = new UnityEvent<RabbitJunglePlatformComponent>();

    public static UnityEvent playerDeath = new UnityEvent();

    [SerializeField]
    private GameObject _bees;

    private void OnEnable()
    {
        if (Random.Range(0,2) != 0)
        {
            _rabbitJungleEggObject.SetActive(true);
            _bees.SetActive(true);
            _rabbitJungleEggObject.transform.localScale = Vector3.one;
            _bees.GetComponent<Animator>().speed = RabbitJungleGameManager.rabbitJunglePlatformAniamtorTime;
        }
    }

    public void OnUse() 
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(RabbitJungleGameManager.rabbitJunglePlatformWaittingTime);
        transform.DOShakePosition(1, 0.3f)
            .OnComplete(() =>
            {
                if (GetComponentInChildren<RabbitJungleComponent>() != null)
                {
                    RabbitJungleComponent.canDo = true;
                }
                transform.DOMoveY(transform.position.y - 30, 1)
                    .OnComplete(() =>
                    {
                        if (GetComponentInChildren<RabbitJungleComponent>() != null)
                        {
                            playerDeath?.Invoke();
                        }
                        else
                        {
                            isPlatformEnded?.Invoke(this);
                        }
                    });
            });
    }
}
