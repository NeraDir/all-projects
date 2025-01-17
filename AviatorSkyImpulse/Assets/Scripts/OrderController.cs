using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new order",menuName = "Create Order List",order = 1)]
public class OrderController : ScriptableObject
{
    public OrderData[] Orders;
}

[System.Serializable]
public class OrderData 
{
    public float NeedDistance;
    public float NeedTime;

    public OrderData(float value1,float value2) 
    {
        NeedDistance = value1;
        NeedTime = value2;
    }
}
