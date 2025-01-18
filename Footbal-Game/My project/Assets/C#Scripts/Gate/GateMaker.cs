using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateMaker : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int x = 5, y = 2;
    [SerializeField] private int[] _sizeGate = new int[10] { 4, 1, 2, 1, 4 , 20, 1, 2, 1, 20 };
    [SerializeField] private GameObject _prefabGateBlock;
    [SerializeField] private bool _playerGate = false;
    
    private void Start()
    {
        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                Vector2 v;
                if (_playerGate)
                {
                    v = new Vector2(transform.position.x + j, transform.position.y + i);
                }
                else
                {
                    v = new Vector2(transform.position.x + j, transform.position.y - i);
                }
                if (_sizeGate[i * x + j] == 0)
                    continue;
                
                GameObject g = Instantiate(_prefabGateBlock, transform);
                if(g.GetComponent<GateBlock>() != null)
                    g.GetComponent<GateBlock>().SetOnScene(_sizeGate[i * x + j]);
                g.transform.position = v;
                g.GetComponent<SpriteRenderer>().sprite = _sprite;
            }
        }

    }
}
