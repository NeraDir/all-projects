using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAllLevels : MonoBehaviour
{
    [SerializeField] private LevelItemSO Leee;
    [SerializeField] private LevelItemUI UIItem;

    [SerializeField] private Transform Parrent;

    private List<LevelItemUI> wow = new();

    void Start()
    {
        foreach(var item in Leee.Levels)
        {
            LevelItemUI UIItemvvvv = Instantiate(UIItem, Parrent);
            UIItemvvvv.Init(item, Leee.Levels.IndexOf(item));
            wow.Add(UIItemvvvv);
        }

        for(int i = 0; i < GlobalSave.MaxLevel; i++)
        {
            wow[i].OpenPanel();
        }
    }
}
