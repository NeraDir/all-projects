using UnityEngine;
using UnityEngine.AI;

public class SlimeBehavior : MonoBehaviour
{
    [SerializeField]
    private float _damage;
    private NavMeshAgent _navMeshAgent;
    private GameObject _mainHero;
    private HeroHealthSystem _heroHealthSystem;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _mainHero = GameObject.Find("MainHero");

        _heroHealthSystem = _mainHero.GetComponent<HeroHealthSystem>();
    }

    private void Update()
    {
        _navMeshAgent.SetDestination(_mainHero.transform.position);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _heroHealthSystem.ApplyDamage(_damage);
        }
    }
}
