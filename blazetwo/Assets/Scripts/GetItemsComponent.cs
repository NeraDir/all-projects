using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemsComponent : MonoBehaviour
{
    [SerializeField]
    private Mesh[] _getItemMeshes;

    [SerializeField]
    private Material[] _getItemMaterial;

    [SerializeField]
    private AudioClip _getItemAudioClip;

    private MeshRenderer _getItemRenderer;
    private MeshFilter _getItemMeshFilter;

    private float _topY;
    private float _bottomY;

    private void Start()
    {
        _getItemMeshFilter = GetComponent<MeshFilter>();
        _getItemRenderer = GetComponent<MeshRenderer>();
        int _getItemIndex = Random.Range(0, _getItemMeshes.Length);
        _getItemMeshFilter.mesh = _getItemMeshes[_getItemIndex];
        _getItemRenderer.material = _getItemMaterial[_getItemIndex];
        _topY = transform.position.y + 1;
        _bottomY = transform.position.y;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveY(_topY, 1f));
        sequence.Append(transform.DOMoveY(_bottomY, 2f));
        sequence.SetLoops(-1,LoopType.Restart);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out LegTrigger leg))
        {
            GameController.moneySource.PlayOneShot(_getItemAudioClip);
            transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => { GameController.CurrentScore += 1; Destroy(gameObject); });
        }
    }
}
