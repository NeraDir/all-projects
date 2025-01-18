using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField]
    private OwnerType owner;
    [SerializeField]
    private ItemType itemType;

    [SerializeField]
    private Sprite AdditionalView;


    private Image cuuremtImageComponent;

    private Transform curretTransform;
    private float speed;
    private float direction;

    private Collider2D lastCollider2D;

    public delegate void LastItemDestroyedDelegate();
    public static event LastItemDestroyedDelegate LastItemDestroyedEvent;




    public void Init(OwnerType owner, float speed)
    {
        this.owner = owner;
        this.speed = speed;

        lastCollider2D = null;

        cuuremtImageComponent = GetComponent<Image>();
        direction = 1;

        if (this.owner == OwnerType.Enemy)
        {
            direction = -1;
            cuuremtImageComponent.sprite = AdditionalView;
        }
    }


    public void StartMovement()
    {
        StartCoroutine(startMovenet());
    }
    private IEnumerator startMovenet()
    {
        curretTransform = GetComponent<Transform>();

        while (true)
        {
            curretTransform.position += Vector3.up * speed * Time.deltaTime * direction * 1.1f;
            yield return null;
        }
    }

    public ItemType GetItemType()
    {
        return itemType;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Item item) && owner == OwnerType.Player)
        {
            if (lastCollider2D != collision && item.owner != owner)
            {
                lastCollider2D = collision;
                CheckTriggerItem(item);
                speed *= 3f;
                item.speed *= 3f;
            }
        }
        if(collision.gameObject.TryGetComponent(out BattleParticipant battleParticipant) && owner != battleParticipant.GetParticipantType())
        {
            battleParticipant.TakeDamage();
            PlayDestroyAnimation();
        }
    }


    public void CheckTriggerItem(Item triggerItem)
    {
        if (itemType == triggerItem.GetItemType())
        {
            triggerItem.PlayDestroyAnimation();
            PlayDestroyAnimation();
        }

        if (itemType == ItemType.Amulet)
        {
            if (triggerItem.GetItemType() == ItemType.Ring)
            {
                triggerItem.PlayDestroyAnimation();
            }
            else if(triggerItem.GetItemType() == ItemType.Necklace)
            {
                PlayDestroyAnimation();
            }
            else
            {
                triggerItem.PlayDestroyAnimation();
                PlayDestroyAnimation();
            } 
        }
        else if (itemType == ItemType.Necklace)
        {
            if (triggerItem.GetItemType() == ItemType.Amulet)
            {
                triggerItem.PlayDestroyAnimation();
            }
            else if (triggerItem.GetItemType() == ItemType.Sword)
            {
                PlayDestroyAnimation();
            }
            else
            {
                triggerItem.PlayDestroyAnimation();
                PlayDestroyAnimation();
            }
        }
        else if (itemType == ItemType.Ring)
        {
            if (triggerItem.GetItemType() == ItemType.Shield)
            {
                triggerItem.PlayDestroyAnimation();
            }
            else if (triggerItem.GetItemType() == ItemType.Amulet)
            {
                PlayDestroyAnimation();
            }
            else
            {
                triggerItem.PlayDestroyAnimation();
                PlayDestroyAnimation();
            }
        }
        else if (itemType == ItemType.Shield)
        {
            if (triggerItem.GetItemType() == ItemType.Sword)
            {
                triggerItem.PlayDestroyAnimation();
            }
            else if (triggerItem.GetItemType() == ItemType.Ring)
            {
                PlayDestroyAnimation();
            }
            else
            {
                triggerItem.PlayDestroyAnimation();
                PlayDestroyAnimation();
            }
        }
        else if (itemType == ItemType.Sword)
        {
            if (triggerItem.GetItemType() == ItemType.Necklace)
            {
                triggerItem.PlayDestroyAnimation();
            }
            else if (triggerItem.GetItemType() == ItemType.Shield)
            {
                PlayDestroyAnimation();
            }
            else
            {
                triggerItem.PlayDestroyAnimation();
                PlayDestroyAnimation();
            }
        }

    }

    private bool canDestroy = true;

    public void PlayDestroyAnimation()
    {
        if (canDestroy)
        {
            canDestroy = false;
            GameManager.destroyedItemsPerRound++;
            Debug.Log("GameManager.destroyedItemsPerRound: " + GameManager.destroyedItemsPerRound);

            if (GameManager.destroyedItemsPerRound == 10)
            {
                GameManager.destroyedItemsPerRound = 0;

                if (LastItemDestroyedEvent != null)
                    LastItemDestroyedEvent();
            }

            Destroy(GetComponent<BoxCollider2D>());
            GetComponent<Animator>().SetInteger("ClipIndex", 1);
        }

    }
    public void DestroyItem()
    {
        Destroy(gameObject);
    }

}

public enum ItemType
{
    Amulet,
    Ring,
    Sword,
    Necklace,
    Shield
}
public enum OwnerType
{
    Player,
    Enemy
}