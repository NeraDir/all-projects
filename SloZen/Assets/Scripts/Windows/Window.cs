using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Window : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private const string ANIMATOR_KEY = "SloZenWindowAnimator";

    public virtual void Init()
    {

    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        _animator.SetBool(ANIMATOR_KEY, true);
    }
}
