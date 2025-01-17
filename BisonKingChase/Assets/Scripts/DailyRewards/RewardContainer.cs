using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardContainer : MonoBehaviour {
    [SerializeField] private GameObject _lockPanel;
    [SerializeField] private Text _nameLabel;
    [SerializeField] private Text _amountText;
    
    private List<IReward> _reward = new List<IReward>();
    private DailyReward _dailyReward = default;
    private List<RewardType> _type = new List<RewardType>();
    private DailyRewardManager _dailyRewardManager = null;
    
    private int _index;
    
    public void Init(DailyReward dailyReward = default, int index = 0,DailyRewardManager manager = null) {
        _dailyReward = dailyReward;
        _index = index;
        _dailyRewardManager = manager;
        foreach (var reward in dailyReward.rewards) {
            _type.Add(reward.type);
        }
        foreach (var rewardType in _type) {
            AddRewardComponent(rewardType.ToString());
        }
        ContainerUpdateVisual();
        DailyRewardManager.newDayReached += ContainerUpdateVisual;
    }

    private void OnDestroy() {
        DailyRewardManager.newDayReached -= ContainerUpdateVisual;
    }
    
    public void OnClaimButtonPressed() {
        if(_dailyReward.rewards[0].isClaimed)
            return;
        for (int i = 0; i < _dailyReward.rewards.Count; i++) {
            _dailyReward.rewards[i].isClaimed = true;
            _dailyReward.rewards[i].canClaim = false;
            _dailyRewardManager.SetLastClaimedDay(_index + 1);
            _reward[i].ClaimReward(_dailyReward.rewards[i].getAmount,ClaimReward);
        }
    }

    public bool IsCanClaim() {
        return _dailyReward.rewards[0].canClaim;
    }
    
    private void AddRewardComponent(string typeString) {
        if(_dailyReward.rewards[0].isClaimed)
            return;
        if(!_dailyReward.rewards[0].canClaim)
            return;
        System.Type newSkillType = System.Type.GetType(typeString);
        gameObject.AddComponent(newSkillType);
        _reward = GetComponents<IReward>().ToList();
        foreach (var reward in _reward) {
            reward.Init();
        }
    }

    private void ClaimReward() {
        ContainerUpdateVisual();
    }
    
    private void ContainerUpdateVisual() {
        _nameLabel.text = $"DAY {_index + 1}";
        _amountText.text = "+" + _dailyReward.rewards[0].getAmount.ToString();
        foreach (var dailyReward in _dailyReward.rewards) { 
            _lockPanel.SetActive(dailyReward.isClaimed);
        }
    }
}
