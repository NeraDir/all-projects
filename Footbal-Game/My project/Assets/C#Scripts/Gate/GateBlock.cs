using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GateBlock : MonoBehaviour
{
    [SerializeField] private TextMeshPro _tmpHp;
    public int hp = 0;
    public void SetOnScene(int HP)
    {
        hp = HP;
        _tmpHp.text = hp.ToString();
    }
    public void GetDmg(int dmg)
    {
        hp -= dmg;
        if(hp<= 0)
        {
            DestroyThis();
        }
        _tmpHp.text = hp.ToString();
    }
    private void DestroyThis()
    {
        Destroy(gameObject);
    }
}
