using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TigetManager : MonoBehaviour
{
    public static Action<Transform, Action> moveTheTiger;

    private Animator _animator;
    
    private bool _isMoving = false;

    [SerializeField] private Material[] _tigerSkins;

    [SerializeField] private Transform _tigerTransform;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private StaminaManager _staminaManager;
    
    [SerializeField] private AudioClip _jumpSound;
    
    private Action _completeAction;
    private float _staminaOfSkin;
    
    public void Init()
    {
        _staminaOfSkin = 50;
        for (int i = 0; i < (GameManager.TigerSkinIndex + 1); i++)
        {
            _staminaOfSkin += 50;
        }
        _staminaManager.Init(_staminaOfSkin);
        _skinnedMeshRenderer.material = _tigerSkins[GameManager.TigerSkinIndex];
        _animator = _tigerTransform.GetComponent<Animator>();
        moveTheTiger += OnMoveTiger;
    }

    private void OnDestroy()
    {
        moveTheTiger -= OnMoveTiger;
    }

    private void OnMoveTiger(Transform position, Action action)
    {
        if(_tigerTransform == null)
            return;
        if (Vector3.Distance(position.position,_tigerTransform.position) > 4f)
            return;
        if (_tigerTransform.position.y > position.position.y)
            return;
        if (_isMoving)
            return;
        if (_staminaManager.GetStamina() <= 25)
            return;
        _tigerTransform.parent = null;
        StaminaManager.StaminaChanged?.Invoke(-25f);
        _completeAction = action;
        _isMoving = true;
        _animator.SetBool("Jump", true);
        SettingsManager.playSound?.Invoke(_jumpSound);
        StartCoroutine(WaitAndMove(position));
    }

    private IEnumerator WaitAndMove(Transform position)
    {
        yield return new WaitForSeconds(0.2f);
        _tigerTransform.DOMove(position.position, 0.25f).OnComplete(() =>
        {
            _isMoving = false;
            _animator.SetBool("Jump", false);
            _completeAction?.Invoke();
            _tigerTransform.parent = position.parent;
        });
    }
}
