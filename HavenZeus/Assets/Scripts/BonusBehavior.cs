using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBehavior : MonoBehaviour
{
    [SerializeField]
    private BonusType _bonusType;
    [SerializeField]
    private int _bonusValue;

    private HeroHealthSystem _heroHealthSystem;
    private MoneyCounter _moneyCounter;
    private enum BonusType { Gold, Health}

    private void Awake()
    {
        _heroHealthSystem = GameObject.Find("MainHero").GetComponent<HeroHealthSystem>();
        _moneyCounter = GameObject.Find("MoneyCounter").GetComponent<MoneyCounter>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(_bonusType == BonusType.Health)
            {
                _heroHealthSystem.AddHealth(_bonusValue);
                Destroy(gameObject);
            }

            if (_bonusType == BonusType.Gold)
            {
                _moneyCounter.ReceiveMoney(_bonusValue);
                Destroy(gameObject);
            }
        }
    }
}
