using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BettysProfileDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private TMP_Text _playerLevel;
    [SerializeField] private TMP_Text _playerMaxScore;
    [SerializeField] private TMP_Text _playerCoins;
    [SerializeField] private Slider _playerLevelProgressBar;

    private int _index = 0;

    private void LateUpdate()
    {
        _playerName.text = ProfileData.BettysPlayerName;
        _playerMaxScore.text = ProfileData.BettysMaxScore.ToString();
        _playerLevelProgressBar.value = ProfileData.BettysExptCurrentCount / ProfileData.BettysExpCounToNeed;
        _playerLevel.text = "LEVEL " + (ProfileData.BettysPlayerLevel + 1).ToString();
        _playerCoins.text = "x" + ProfileData.BettysPlayerCoins.ToString();
    }

    public void ChangeSkin(int value)
    {
        _index += value;
        _index = Mathf.Clamp(_index, 0, ProfileData.BettysPlayerSkinsBoughtList.Count - 1);
        foreach (var item in ProfileData.BettysPlayerSkinsBoughtList)
        {
            if (item == _index)
            {
                ProfileData.BettysSkinIndex = ProfileData.BettysPlayerSkinsBoughtList[_index];
            }
        }
    }
}
