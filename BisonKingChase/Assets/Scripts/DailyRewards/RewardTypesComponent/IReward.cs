using System;
using UnityEngine;

public interface IReward {
    public void Init();
    
    public void ClaimReward(float amount = 0,Action action = null);
}
