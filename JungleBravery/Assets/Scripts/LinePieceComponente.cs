using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LinePieceComponente : MonoBehaviour
{
    [SerializeField]
    private Transform _startPos;

    public static float speed;

    [SerializeField]
    private MeshRenderer _rendere;

    [SerializeField]
    private MeshFilter _filter;

    [SerializeField]
    private Mesh[] _meshes;

    [SerializeField]
    private Material[] _materials;

    public int index;

    private bool _canTouch = false;

    private void Start()
    {
        _canTouch = false;
        index = Random.Range(0, _meshes.Length);
        _filter.mesh = _meshes[index];
        _rendere.material = _materials[index];
    }

    private void LateUpdate()
    {
        if (!GameManager.gameLaunched)
            return;
        if (_canTouch)
        {
            transform.Rotate(new Vector3(0, 1, 0), 90 * Time.deltaTime);
            return;
        }
        transform.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
    }

    public void Cutted() 
    {
        if (index == GameManager.foodIndex)
        {
            DOTween.Kill(transform);
            transform.DOScale(Vector3.zero, 1);
            transform.DOMove(GameManager.target.position, 1).OnComplete(() => { GameManager.needFoodCount--; GameManager.score += Random.Range(5, 10); });
        }
        else
        {
            GameManager.tigerHeartsCount--;
            float position = transform.position.y - 10;
            transform.DOMoveY(position, 10).OnComplete(() => Destroy(gameObject));
            transform.DOScale(Vector3.zero, 1);
        }
        _canTouch = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out LineEnd end))
        {
            Destroy(gameObject);
        }
    }
}
