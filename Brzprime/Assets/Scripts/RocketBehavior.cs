using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehavior : MonoBehaviour
{
    [SerializeField]
    private RocketTeleporter _rocketTeleporter;
    [SerializeField]
    private Transform _rocketParent;
    [SerializeField]
    private GameObject _playerPlane;
    [SerializeField]
    private int _rocketDamage;

    private HeroHealthSystem _heroHealthSystem;
    private RectTransform _rectTransform;
    private Rigidbody2D _coinRigidbody;
    private float _currentRocketSpeed;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _coinRigidbody = GetComponent<Rigidbody2D>();
        _heroHealthSystem = _playerPlane.GetComponent<HeroHealthSystem>();

        _rocketTeleporter.RocketTeleport(gameObject);

        SpeedRandomer(CoinBehavior._enviromentSpeed * 1.1f, CoinBehavior._enviromentSpeed * 1.2f);
    }

    void FixedUpdate()
    {
        _coinRigidbody.MovePosition(transform.position - transform.right * _currentRocketSpeed * Time.deltaTime);

        if (Vector2.Distance(_rocketParent.position, new Vector2(transform.position.x, _rocketParent.position.y)) > _rectTransform.sizeDelta.x / 2 + Screen.width / 2 && transform.position.x < _rocketParent.transform.position.x)
        {
            _rocketTeleporter.RocketTeleport(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D hero)
    {
        if(hero.tag == "RocketRotator" && gameObject.tag == "Rocket")
        {
            transform.rotation = Quaternion.FromToRotation(-Vector3.right,  _playerPlane.transform.position - transform.position);
        }
        else if (hero.tag == "Player")
        {
            _heroHealthSystem.ApplyDamage(_rocketDamage);
            _rocketTeleporter.RocketTeleport(gameObject);
            SpeedRandomer(CoinBehavior._enviromentSpeed * 1.5f, CoinBehavior._enviromentSpeed * 3f);
        }
        else if(hero.tag == "Rocket" || hero.tag == "UFO")
        {
            _rocketTeleporter.RocketTeleport(gameObject);
            SpeedRandomer(CoinBehavior._enviromentSpeed * 1.5f, CoinBehavior._enviromentSpeed * 3f);
        }
    }

    public void SpeedRandomer(float minSpeed, float maxSpeed)
    {
        _currentRocketSpeed = Random.Range(minSpeed, maxSpeed);
    }
}
