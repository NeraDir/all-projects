using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBehavior : MonoBehaviour
{
    [SerializeField]
    private CoinTeleporter _bonusTeleporter;
    [SerializeField]
    private PlaneMovement _planeMovement;
    [SerializeField]
    private Transform _bonusParent;
    [SerializeField] 
    private BonusType _bonusType;
    [SerializeField]
    private float _bonusValue;

    private HeroHealthSystem _heroHealthSystem;
    private RectTransform _rectTransform;
    private Rigidbody2D _coinRigidbody;

    public enum BonusType { Health, Fuel }

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _coinRigidbody = GetComponent<Rigidbody2D>();
        _heroHealthSystem = GameObject.Find("Hero").GetComponent<HeroHealthSystem>();

        _bonusTeleporter.CoinTeleport(gameObject);
    }

    void FixedUpdate()
    {
        _coinRigidbody.MovePosition(transform.position - Vector3.right * CoinBehavior._enviromentSpeed * Time.deltaTime);

        if (Vector2.Distance(_bonusParent.position, new Vector2(transform.position.x, _bonusParent.position.y)) > _rectTransform.sizeDelta.x / 2 + Screen.width / 2 && transform.position.x < _bonusParent.transform.position.x)
        {
            _bonusTeleporter.CoinTeleport(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D hero)
    {
        if (hero.tag == "Player" && _bonusType == BonusType.Fuel)
        {
            _planeMovement.AddFuel(_bonusValue);
            _bonusTeleporter.CoinTeleport(gameObject);

        }
        else if (hero.tag == "Player" && _bonusType == BonusType.Health)
        {
            _heroHealthSystem.AddHealth(_bonusValue);
            _bonusTeleporter.CoinTeleport(gameObject);
        }
    }
}
