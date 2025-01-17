using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFruitsTrigger : MonoBehaviour
{
    private int index;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out FallFruitsComponent fruit))
        {
            index = fruit.indexOfFruit;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        index = 1000;
    }

    public int GetIndex() 
    {
        return index;
    }
}
