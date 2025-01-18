using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    [SerializeField]
    private MoneyCounter _moneyCounter;
    [SerializeField]
    private CoinTeleporter _coinTeleporter;
    [SerializeField]
    private Transform _coinParent;
    [SerializeField]
    private int _coinValue;

    private RectTransform _rectTransform;
    private Rigidbody2D _coinRigidbody;

    public static float _enviromentSpeed = 300f;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _coinRigidbody = GetComponent<Rigidbody2D>();

        _coinTeleporter.CoinTeleport(gameObject);
    }

    void FixedUpdate()
    {
        _coinRigidbody.MovePosition(transform.position - Vector3.right * _enviromentSpeed * Time.deltaTime);

        if (Vector2.Distance(_coinParent.position, new Vector2(transform.position.x, _coinParent.position.y)) > _rectTransform.sizeDelta.x / 2 + Screen.width / 2 && transform.position.x < _coinParent.transform.position.x)
        {
            _coinTeleporter.CoinTeleport(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D hero)
    {
        if(hero.tag == "Player")
        {
            MoneyCounter.AddMoney(_coinValue);
            _moneyCounter.RedarawGameMoney();
            _coinTeleporter.CoinTeleport(gameObject);
        }
    }
}
