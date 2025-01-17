using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnubisShopController : MonoBehaviour
{
    [SerializeField]
    private AnubisShopContent _contentPrefab = null;

    [SerializeField]
    private Transform[] _contentParent = null;

    [SerializeField]
    private List<ShopData> _datas = new List<ShopData>();

    [SerializeField]
    private int _contentCountPerParent = 3;

    private void Start()
    {
        FillContent();
    }

    private void FillContent()
    {
        int index = 0;
        int spawnedCount = 0;
        foreach (ShopData data in _datas)
        {
            if (spawnedCount >= _contentCountPerParent)
            {
                index += 1;
                spawnedCount = 0;
            }
            AnubisShopContent newContent = Instantiate(_contentPrefab, _contentParent[index]);
            newContent.SetData(data.Sprite, data.Price);
            newContent.Init();
            spawnedCount += 1;
        }
    }
}

[System.Serializable]
public class ShopData
{
    public Sprite Sprite;
    public int Price;
}
