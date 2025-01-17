using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderDeathWall : MonoBehaviour, IThunderTrigger
{
    public void Use()
    {
        GameManager.isEnd = true;
    }
}
