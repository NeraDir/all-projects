using System;
using UnityEngine;

public class Coins : MonoBehaviour, IReward {
    public void Init() {
        
    }
    
    public void ClaimReward(float amount = 0,Action action = null) {
        ChasePlayerDataComponent.ChasePlayerCoins += (int)amount;
        action?.Invoke();
    }
}
