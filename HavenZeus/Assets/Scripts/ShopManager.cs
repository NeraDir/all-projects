using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _swords;
    [SerializeField]
    private GameObject[] _bows;

    public static int _currentSwordIndex;
    public static int _currentBowIndex;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("CurrentSwordIndex"))
        {
            _currentSwordIndex = PlayerPrefs.GetInt("CurrentSwordIndex");
        }
        if (PlayerPrefs.HasKey("CurrentBowIndex"))
        {
            _currentBowIndex = PlayerPrefs.GetInt("CurrentBowIndex");
        }

        _swords[_currentSwordIndex].SetActive(true);
        _bows[_currentBowIndex].SetActive(true);

    }
    
    public static void SetCurrentSword(int currentSwordIndex)
    {
        _currentSwordIndex = currentSwordIndex;
        PlayerPrefs.SetInt("CurrentSwordIndex", _currentSwordIndex);
    }

    public static void SetCurrentBow(int currentBowIndex)
    {
        _currentBowIndex = currentBowIndex;
        PlayerPrefs.SetInt("CurrentBowIndex", _currentBowIndex);
    }
}
