using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator m_Animator;

    private void Awake() => m_Animator = GetAnimation();

    private Animator GetAnimation() => GetComponent<Animator>() ? GetComponent<Animator>() : GetComponentInChildren<Animator>();

    public void SetAnimationState(int animationStateIndex, string animationKey) => m_Animator.SetInteger(animationKey, animationStateIndex);
}
