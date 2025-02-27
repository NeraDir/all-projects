using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BettysSkinChanger : MonoBehaviour
{
    [SerializeField] private Material[] _skins;
    [SerializeField] private SkinnedMeshRenderer _skinsRenderer;

    [SerializeField] private int _maxAnimateIndex;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        
    }

    private void OnEnable()
    {
        if (_animator != null)
        {
            int value = 0;
            value = Random.Range(0, _maxAnimateIndex);
            _animator.SetInteger("Dancer", value);
        }
    }

    private void LateUpdate()
    {
        _skinsRenderer.material = _skins[ProfileData.BettysSkinIndex];
    }
}
