using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CandysCandyComponent : MonoBehaviour, IBeginDragHandler,IEndDragHandler,IDragHandler
{
    public int index;

    private bool cantMove;
    public bool blockX, blockY, blockZ;
    private Vector2 lastMousePosition;
    private float x, y, z;

    private Vector2 beginPostion;

    [SerializeField]
    private AudioClip goodClip;

    [SerializeField]
    private AudioClip badClip;

    private float speed;

    private bool isDesad;

    private void Start()
    {
        speed = 1 * (float)CandysGameManager.candysCurrentLevel / 2;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isDesad)
            return;
        cantMove = true;
        lastMousePosition = eventData.position;
        beginPostion = transform.localPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDesad)
            return;
        PointerEventData ointerData = (PointerEventData)eventData;

        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)transform.GetComponentInParent<Canvas>().transform,
            ointerData.position,
            transform.GetComponentInParent<Canvas>().worldCamera,
            out position);

        transform.position = transform.GetComponentInParent<Canvas>().transform.TransformPoint(position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDesad)
            return;
        transform.localPosition = beginPostion;
        cantMove = false;
    }

    private void LateUpdate()
    {
        if (CandysGameManager.candysGameEnded)
            return;
        if (isDesad)
            return;
        if (cantMove)
            return;
        transform.position += new Vector3(0, -1, 0) * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDesad)
            return;
        if (other.TryGetComponent(out CandyBagComponent bag))
        {
            if (bag.bagIndex == index)
            {
                isDesad = true;
                CandysGameManager.candysCurrentScore += Random.Range(5, 10);
                CandysGameManager.candysAudioPlayer.PlayOneShot(goodClip);
                StartCoroutine(DestroySelf(bag.transform, bag.animator));
                bag.animator.enabled = false;
            }
            else
            {
                isDesad = true;
                CandysGameManager.candysHealth--;
                CandysGameManager.candysAudioPlayer.PlayOneShot(badClip);
                StartCoroutine(DestroySelf());
            }
        }
    }

    private IEnumerator DestroySelf(Transform position = default,Animator anima = default) 
    {
        while (transform.localScale != Vector3.zero)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, 10 * Time.deltaTime);
            if (position != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, position.position, 5 * Time.deltaTime);
            }
            yield return null;
        }
        if (anima != null)
        {
            anima.enabled = false;
        }
        Destroy(gameObject);
    }
}
