using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DailyRewardWindow : MonoBehaviour {
   
   [SerializeField] private Button _claimButton;
   
   [Space(10)] 
   [Header("Requirement Rewards Components")] 
   [SerializeField] private Transform[] _contentTransform;
   [SerializeField] private RewardContainer _containerPrefab;

   [SerializeField] private int _containersCountPerLine;
   
   private DailyRewardManager _dailyRewardManager;
   private DailyRewardData _dailyRewardsData;
   private List<RewardContainer> _listOfRewardContainers = new List<RewardContainer>();
   
   private void Awake() {
      _claimButton.onClick.AddListener(OnClaimButtonPressed);
      SpawnContainers();
   }

   public void Init(DailyRewardManager manager, DailyRewardData dailyRewardsData) {
      _dailyRewardManager = manager;
      _dailyRewardsData = dailyRewardsData;
      UpdateClaimButtonInteractable();
   }

   private void OnClaimButtonPressed() {
      bool claimed = false;
      foreach (var item in _listOfRewardContainers) {
         if (item.IsCanClaim()) {
            item.OnClaimButtonPressed();
            claimed = true;
         }
      }
      if (claimed) {
         UpdateClaimButtonInteractable();
      }
   }

   public void UpdateClaimButtonInteractable() {
      bool canClaimReward = _dailyRewardManager.GetDailyRewardStatus();
      _claimButton.interactable = canClaimReward;
   }
   
   private void SpawnContainers() {
      int containerIndex = 0;
      int currentClaimableDay = _dailyRewardManager.GetCurrentClaimableDay();
      for (int i = 0; i < _dailyRewardsData.dailyRewardsDataList.Count; i++) {
         if (i % _containersCountPerLine == 0 && i != 0) {
            containerIndex += 1;  
         }
         DailyReward newRewardData = _dailyRewardsData.dailyRewardsDataList[i];
         bool canClaim = (i + 1) <= currentClaimableDay;
         bool isClaimed = _dailyRewardManager.GetLastClaimedDay() >= (i + 1);
         foreach (var reward in newRewardData.rewards) {
            reward.canClaim = canClaim && !isClaimed;
            reward.isClaimed = isClaimed;
         }
         FillContainer(i,newRewardData,containerIndex);
      }
   }

   private void FillContainer(int index = 0,DailyReward reward = default, int containerIndex = 0) {
      RewardContainer newRewardContainer = Instantiate(_containerPrefab, _contentTransform[containerIndex]);
      newRewardContainer.Init(reward,index,_dailyRewardManager);
      _listOfRewardContainers.Add(newRewardContainer);
   }
}
