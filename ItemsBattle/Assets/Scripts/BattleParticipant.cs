using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(BattleParticipantAnimation), typeof(BattleParticipantEvents))]
public abstract class BattleParticipant : MonoBehaviour
{
    [SerializeField]
    private TMP_Text healthDisplay;

    [SerializeField]
    private List<Transform> itemSpawnPoints;
    private List<Item> itemPrefabList;
    private List<Item> currentItemsInScene;

    [SerializeField]
    private int healtValue;

    private BattleParticipantAnimation battleParticipantAnimation;

    private OwnerType Type;

    private float timeToChacheShowItemsAnimation;

    private float itemsSpeed;

    [HideInInspector]
    public bool isAlive;

    public virtual void Init(List<Item> itemPrefabs, OwnerType owner, float itemsSpeed)
    {
        this.itemPrefabList = itemPrefabs;
        this.Type = owner;
        this.itemsSpeed = itemsSpeed;

        isAlive = true;

        currentItemsInScene = new();

        battleParticipantAnimation = GetComponent<BattleParticipantAnimation>();
        AnimationClip[] animationClips = battleParticipantAnimation.gameObject.GetComponent<Animator>().runtimeAnimatorController.animationClips;

        foreach (var clip in animationClips)
        {
            if (clip.name == "DisableItems")
                timeToChacheShowItemsAnimation = clip.length;
        }

    }

    private void Update()
    {
        healthDisplay.text = "X" + healtValue;
    }


    public virtual void ChangeItemCollection()
    {
        if (currentItemsInScene.Count != 0)
        {
            for (int i = 0; i < currentItemsInScene.Count; i++)
            {
                Destroy(currentItemsInScene[i].gameObject);
            }

            currentItemsInScene.Clear();
        }

        for (int i = 0; i < itemSpawnPoints.Count; i++)
        {

            Item newItem = Instantiate(GetRandomItem(), itemSpawnPoints[i].position, itemSpawnPoints[i].rotation, itemSpawnPoints[i]);
            newItem.Init(Type, itemsSpeed);

            currentItemsInScene.Add(newItem);
        }

    }

    public virtual void ShowItems()
    {

        ChangeItemCollection();
        battleParticipantAnimation.PlayShowItemsAnimation();
    }


    public virtual void DisableItems()
    {
        battleParticipantAnimation.PlayDisableAnimation();
        Invoke(nameof(ShowItems), timeToChacheShowItemsAnimation + 0.5f);

    }

    public virtual void SetItemsPositionToNextRound()
    {
        battleParticipantAnimation.SetPositionToNextRoundAnimation();
    }

    public virtual void Attack()
    {
        battleParticipantAnimation.SetEmptyAnimation();

        for (int i = 0; i < itemSpawnPoints.Count; i++)
        {
            itemSpawnPoints[i].localScale = Vector3.one;
        }

       

        StartCoroutine(startAttack());
    }

    private IEnumerator startAttack()
    {
        currentItemsInScene[2].StartMovement();
        yield return new WaitForSeconds(2f);
        currentItemsInScene[1].StartMovement();
        currentItemsInScene[3].StartMovement();
        yield return new WaitForSeconds(2f);
        currentItemsInScene[0].StartMovement();
        currentItemsInScene[4].StartMovement();

        currentItemsInScene.Clear();

    }
    

    public virtual void TakeDamage()
    {
        if (healtValue - 1 >= 0)
        {
            healtValue--;

            if (healtValue == 0)
                isAlive = false;
        }
        else
        {
            isAlive = false;
        }
    }

    public virtual BattleParticipantEvents GetParticipantEvents()
    {
        BattleParticipantEvents result = GetComponent<BattleParticipantEvents>();

        if(result == null)
        {
            Debug.Log("Participant: " + gameObject.name + " does not have an event component");
            return null;
        }
        else
        {
            return result;
        }

    }

    public OwnerType GetParticipantType()
    {
        return Type;
    }

    public Item GetRandomItem()
    {
        return itemPrefabList[Random.Range(0, itemPrefabList.Count)];
    }
}
