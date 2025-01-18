using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreeGammaPanel : MonoBehaviour
{
    [SerializeField]
    private AbstractPrefab prefab;





    public void ShowPageItems()
    {
        prefab.ShowPageItems();
        gameObject.SetActive(false);
    }
}
