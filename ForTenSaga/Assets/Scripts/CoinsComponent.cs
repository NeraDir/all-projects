using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public enum CoinType
{
    Coin,
    Damage,
    Heart,
}

public class CoinsComponent : MonoBehaviour
{
    [SerializeField] private Material[] _coinMaterials;
    [SerializeField] private Material _damageMaterial;
    [SerializeField] private Material _heartMaterial;
    [SerializeField] private Material _coinMaterial;
    
    private MeshRenderer _meshRenderer;
    
    private CoinType _coinType;
    
    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        Material[] materials = new Material[2];
        int rnd = Random.Range(0, 100);
        if (rnd < 70)
        {
            _coinType = CoinType.Coin;
        }
        else if (rnd < 90 && rnd > 70)
        {
            _coinType = CoinType.Damage;
        }
        else if (rnd < 100 && rnd > 90)
        {
            _coinType = CoinType.Heart;
        }

        switch (_coinType)
        {
            case CoinType.Coin:
                materials = new []{_coinMaterials[Random.Range(0, _coinMaterials.Length)],_coinMaterial};
                break;
            case CoinType.Damage:
                materials = new []{_coinMaterials[Random.Range(0, _coinMaterials.Length)],_damageMaterial};
                break;
            case CoinType.Heart:
                materials = new []{_coinMaterials[Random.Range(0, _coinMaterials.Length)],_heartMaterial};
                break;
        }
        _meshRenderer.materials = materials;
    }

    public void Use()
    {
        transform.DOScale(Vector3.zero, 0.25f).OnComplete(() =>
        {
            switch (_coinType)
            {
                case CoinType.Coin:
                    GameManager.TigerCoinsCount += 1;
                    break;
                case CoinType.Damage:
                    HealthManager.changeHealth?.Invoke(-1);
                    break;
                case CoinType.Heart:
                    HealthManager.changeHealth?.Invoke(+1);
                    break;
            }
            Destroy(gameObject);
        });
    }
}
