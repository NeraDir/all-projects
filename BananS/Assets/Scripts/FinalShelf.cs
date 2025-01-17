using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinalShelf : MonoBehaviour
{
    [SerializeField]
    private Transform boxPoint;

    private HeadSweetie headSweetie;
    private Collider lastCollider;

    [SerializeField]
    private Santa santa;
    [SerializeField]
    private GiftBox giftBox;

    [SerializeField]
    private FollowByTarget cameraComponent;

    private float waitForCloseBoxTime = 3f;

    [SerializeField]
    private UI_LevelCompleteLayer uI_LevelCompleteLayer;


    private void OnEnable()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if(lastCollider != other)
        {
            lastCollider = other;

            if (other.gameObject.TryGetComponent(out HeadSweetie headSweetie))
            {
                headSweetie.jumpPower *= 1.05f;
                headSweetie.Jump();
               
                this.headSweetie = headSweetie;

                

                giftBox.SetOpenAnimationState();
                santa.SetRotateStateAnimation();


                Invoke(nameof(TransformHeadSweetieToBox), 1.2f);
            }
            
            if (other.gameObject.TryGetComponent(out SweetiePart sweetiePart))
            {
                waitForCloseBoxTime = 3;
            }
            


        }


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            waitForCloseBoxTime = 3;
        }
    }


    private void TransformHeadSweetieToBox()
    {
        headSweetie.StopMove();
        headSweetie.transform.DOMove(boxPoint.position, 0.6f);
        StartCoroutine(waitForCloseGiftBox());
    }

    private IEnumerator waitForCloseGiftBox()
    {
        while (waitForCloseBoxTime > 0)
        {
            waitForCloseBoxTime -= Time.deltaTime;
            yield return null;
        }

        cameraComponent.target = santa.transform;
        cameraComponent.offset.y += 5f;

        santa.SetDanceStateAnimation();
        giftBox.SetCloseAninationState();

        yield return new WaitForSeconds(3f);
        ShowLevelComplete();
    }


    public void ShowLevelComplete()
    {
        uI_LevelCompleteLayer.gameObject.SetActive(true);
    }
}
