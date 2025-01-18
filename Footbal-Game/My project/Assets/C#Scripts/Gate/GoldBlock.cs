using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBlock : MonoBehaviour
{
    Win win;
    public int hp = 0;
    private void Start()
    {
        win = Win.instance;
        win.AddBlock();
    }
    public void SetOnScene(int HP)
    {
        hp = HP;
    }
    public void GetDmg(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            DestroyThis();
        }
    }
    private void DestroyThis()
    {
        win.DestroyBlock();
        
        Destroy(gameObject);
    }
}
