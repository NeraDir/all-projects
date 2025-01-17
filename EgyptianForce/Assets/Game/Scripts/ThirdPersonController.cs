using StarterAssets;
using System.Data;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    [SerializeField] private float _attackRadius;

    [SerializeField] private Joystick _joystick;
    private StarterAssetsInputs _input;
    private Animator[] _anims;

    public static readonly int AttackTrigger = Animator.StringToHash("attack");
    public static readonly int RunBool = Animator.StringToHash("isRun");

    private void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _anims = GetComponentsInChildren<Animator>();
    }

    private void Update()
    {
        Vector2 joystickInput = new Vector2(_joystick.Horizontal, _joystick.Vertical);
        AnimsSetRun(joystickInput.magnitude > 0.1f); 
        _input.MoveInput(joystickInput);
        _input.LookInput(joystickInput);
    }

    private void AnimsSetRun(bool runState)
    {
        foreach(var anim in _anims) anim.SetBool(RunBool, runState);
    }

    public void StartAttack() { foreach(var anim in _anims) anim.SetTrigger(AttackTrigger); }

    public void Attack()
    {
        RaycastHit[] cols = Physics.SphereCastAll(transform.position, _attackRadius, transform.forward);

        if(cols == null) return;
        foreach(var col in cols)
        {
            if(col.collider.TryGetComponent<Enemy>(out Enemy enemy)) enemy.Dead();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Bag>(out Bag bag)) bag.Collect();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }
}
