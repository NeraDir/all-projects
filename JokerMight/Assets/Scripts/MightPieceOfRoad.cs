using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MightPieceOfRoad : MonoBehaviour
{
    private bool _isRightPlace;

    public int mightRoadIndex;

    public static UnityEvent clickedRoad = new UnityEvent();

    public MightPlatformComponent mightPlatform;

    public static UnityEvent heartsMinusEvent = new UnityEvent();

    private bool donDestroy = false;

    private void Start()
    {
        Invoke(nameof(Destroyer), 10);
    }

    private void Destroyer() 
    {
        if (donDestroy)
            return;
        transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => {mightPlatform.spawnedPieces.Remove(this.gameObject); Destroy(gameObject); });
    }

    private void OnMouseDown()
    {
        if (!_isRightPlace)
            return;
        if (mightPlatform.indexOfPlatform != mightRoadIndex)
        {
            MightGameController.mightHearts--;
            heartsMinusEvent?.Invoke();
        }
        transform.parent = mightPlatform.transform;
        donDestroy = true;
        MightGameController.mightGameScore += Random.Range(5, 10);
        mightPlatform.spawnedPieces.Remove(this.gameObject);
        foreach (var item in mightPlatform.spawnedPieces)
        {
            mightPlatform.isSpawner = false;
            Destroy(item.gameObject);
        }
        clickedRoad?.Invoke();
        Destroy(this);
    }

    private void LateUpdate()
    {
        transform.position += new Vector3(0, 0, 1) * MightGameController.piecesMoveSpeed * Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out RightPlace rightPlace))
        {
            _isRightPlace = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isRightPlace = false;
    }
}
