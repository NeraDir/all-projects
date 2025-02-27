using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CustomizeManager : MonoBehaviour
{
    [SerializeField] private Text _buttonTxt;
    [SerializeField] private int _index;

    [SerializeField] private CustomizeManager[] _managers;

    private AudioClip _clip;

    private void Start()
    {
        _clip = Resources.Load("Audio/click") as AudioClip;
    }
    public void VisualUpdate()
    {
        _buttonTxt.text = MenuManager.CurrentSkinIndex == _index ? "EQUIPPED" : "EQUIP";
    }
    public void OnEquip()
    {
        MenuManager.CurrentSkinIndex = _index;
        foreach (var item in _managers)
        {
            item.VisualUpdate();
        }
        SettingsManager.instance.onPlaySound?.Invoke(_clip);
    }
}
