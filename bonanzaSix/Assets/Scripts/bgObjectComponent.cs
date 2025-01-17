using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgObjectComponent : MonoBehaviour
{
    [SerializeField]
    private Material[] _bgMaterials;

    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material = _bgMaterials[Random.Range(0,_bgMaterials.Length)];
        float startY = Random.Range(-6, 6f);
        transform.position = new Vector3(transform.position.x, startY, transform.position.z);
        float rndValue = Random.Range(2.5f, 8f);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveY(-6, rndValue).OnComplete(() => transform.DOMoveY(startY, rndValue)));
        sequence.SetLoops(-1,LoopType.Yoyo);
    }
}
