using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackComponent : MonoBehaviour
{
    public FruitType fruitType;

    public Transform[] _fruitPositions;

    public List<FruitItemComponent> _fruitsList = new List<FruitItemComponent>();

    public void AddFruit(FruitItemComponent fruit)
    {
        if (_fruitsList.Count >= 3)
        {
            return;
        }
        _fruitsList.Add(fruit);
        float y = fruit.transform.position.y;
        fruit.transform.DOMoveY(y + 5, 0.125f).OnComplete(() =>
        {
            fruit.transform.DOMove(new Vector3(_fruitPositions[_fruitsList.Count - 1].transform.position.x, _fruitPositions[_fruitsList.Count - 1].transform.position.y, _fruitPositions[_fruitsList.Count - 1].transform.position.z), 0.25f).OnComplete(() =>
            {
                fruit.transform.parent = _fruitPositions[_fruitsList.Count - 1].transform;
            });
        });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out TupeComponent tube))
        {
            transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(gameObject));
        }
    }
}
