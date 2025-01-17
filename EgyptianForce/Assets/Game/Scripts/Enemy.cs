using Game.Shop;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _bagDrop;

    private Animator _anim;
    public static readonly int deadTrigger = Animator.StringToHash("dead");

    private void Awake() => _anim = GetComponent<Animator>();

    public void Dead() => _anim.SetTrigger(deadTrigger);

    public void Delete()
    {
        Instantiate(_bagDrop, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
