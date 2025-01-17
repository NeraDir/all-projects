using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystallRocksComponents : MonoBehaviour
{
    [SerializeField]
    private Material[] _crystallsRockMaterials;

    [SerializeField]
    private MeshRenderer _crystallsRockMesh;

    private void Start()
    {
        _crystallsRockMesh.material = _crystallsRockMaterials[Random.Range(0,_crystallsRockMaterials.Length)];
    }

    public void Use(Transform goPosition)
    {
        transform.DOMove(Camera.main.ScreenToWorldPoint(goPosition.position), 0.25f).OnComplete(() => { GameManager.crystallRocksCount++; Destroy(gameObject); });
    }
}
