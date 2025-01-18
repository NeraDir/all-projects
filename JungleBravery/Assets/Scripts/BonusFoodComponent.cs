using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusFoodComponent : MonoBehaviour
{
    private bool _isClicked;

    [SerializeField]
    private MeshRenderer _rendere;

    [SerializeField]
    private MeshFilter _filter;

    [SerializeField]
    private Mesh[] _meshes;

    [SerializeField]
    private Material[] _materials;

    public int index;

    private void Start()
    {
        index = Random.Range(0, _meshes.Length);
        _filter.mesh = _meshes[index];
        _rendere.material = _materials[index];
    }

    private void LateUpdate()
    {
        if (BonusGame.bonusEnded)
            return;
        transform.position += new Vector3(0, -1, 0) * BonusGame.fallSpeed * Time.deltaTime;
    }

    private void OnMouseDown() 
    {
        if (_isClicked)
            return;
        if (BonusGame.bonusEnded)
            return;
        _isClicked = true;
        transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => { GameManager.score += (Random.Range(5, 10) * 2);Destroy(gameObject); });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BonusDeathLine line))
        {
            transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => { BonusGame.bonusEnded = true; Destroy(gameObject); });
        }
    }
}
