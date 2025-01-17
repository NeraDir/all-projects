using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Combinations : MonoBehaviour
{
    public static Combinations Instance;
    public List<Item> CombinationsList = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}

[System.Serializable]
public struct Item
{
    public int id;
    public int KnifesCount;

    public int CheckOnValid(AllVariants items)
    {
        int iterator = 0;

        foreach(var it in items.Vartiants)
        {
            bool off = id == it.id;

            if(off)
            {
                iterator++;
            }
        }

        if(iterator >= 3)
        {
            return KnifesCount;
        }

        return 0;
    }
}
