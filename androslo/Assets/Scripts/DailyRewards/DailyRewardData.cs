using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public enum RewardType {
    Coins,
}

[CreateAssetMenu(fileName = "New Daily Rewards Data",menuName = "Create New Daily Rewards Data", order = 1)]
public class DailyRewardData : ScriptableObject {
    public List<DailyReward> dailyRewardsDataList = new List<DailyReward>();
}

[System.Serializable]
public class DailyReward {
    public List<RewardDaily> rewards = new List<RewardDaily>(); 
}

[System.Serializable]
public class RewardDaily {
    public RewardType type;
    public float getAmount;
    [HideInInspector] public bool canClaim;
    [HideInInspector] public bool isClaimed;
}
