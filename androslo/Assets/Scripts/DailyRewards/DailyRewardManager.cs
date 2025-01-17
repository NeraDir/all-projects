using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardManager : MonoBehaviour {
    [SerializeField] private DailyRewardWindow _dailyRewardWindow;
    [SerializeField] private DailyRewardData _dailyRewardData;
 
    public static Action newDayReached;

    private void Awake() {
        _dailyRewardWindow.Init(this,_dailyRewardData);
        Init();
    }

    private void Init() {
        StartCoroutine(CheckingDailyRewardIsReady());
    }

    private IEnumerator CheckingDailyRewardIsReady() {
        while (true) {
            if (GetDailyRewardStatus()) {
               
                _dailyRewardWindow.UpdateClaimButtonInteractable(); 
            }
            yield return new WaitForSeconds(1f);
        }
    }
    
    private void OnDailyRewardButtonPressed() {
        _dailyRewardWindow.gameObject.SetActive(true);
    }

    public int GetCurrentClaimableDay() {
        return ChasePlayerDataComponent.ChaseClaimableDay;
    }
    
    public bool GetDailyRewardStatus() {
        if (ChasePlayerDataComponent.ChasePlayerLastEntryTime == null) {
            ResetDailyRewardProgress();
            return true;
        }

        DateTime currentDateTime = DateTime.UtcNow;
        
        int passedDays = 0;
        if (ChasePlayerDataComponent.ChasePlayerLastEntryTime.Value.Day != currentDateTime.Day) {
            if (currentDateTime.Month != ChasePlayerDataComponent.ChasePlayerLastEntryTime.Value.Month) {
                if (currentDateTime.Day != 1) {
                    ResetDailyRewardProgress();
                    return true;
                }
                else {
                    TimeSpan? difference = currentDateTime.Date - ChasePlayerDataComponent.ChasePlayerLastEntryTime.Value.Date;
                    passedDays = (int)difference.Value.TotalDays;
                }
            }
            else {
                passedDays = currentDateTime.Day - ChasePlayerDataComponent.ChasePlayerLastEntryTime.Value.Day;
            }
        }
        
        if (passedDays > 0) {
            if (passedDays >= 2) {
                ResetDailyRewardProgress();
                return true;
            }
            for (int i = 0; i < passedDays; i++) {
                ChasePlayerDataComponent.ChaseClaimableDay++;
            }
            ChasePlayerDataComponent.ChasePlayerLastEntryTime = currentDateTime;
            _dailyRewardWindow.gameObject.SetActive(true);
            newDayReached?.Invoke();
            return true;
        }
        return false;
    }

    private void ResetDailyRewardProgress() {
        ChasePlayerDataComponent.ChaseClaimableDay = 1;
        DateTime nowTime = DateTime.UtcNow;
        ChasePlayerDataComponent.ChasePlayerLastEntryTime = nowTime;
        _dailyRewardWindow.gameObject.SetActive(true);
        SetLastClaimedDay(0);
        newDayReached?.Invoke();
    }
    
    public void SetLastClaimedDay(int day) {
        ChasePlayerDataComponent.ChaseLastClaimedDay = day;
        CheckIfAllDailyRewardsAreClaimed();
    }

    private void CheckIfAllDailyRewardsAreClaimed() {
        if (GetLastClaimedDay() > _dailyRewardData.dailyRewardsDataList.Count) {
            ChasePlayerDataComponent.ChaseClaimableDay = 1;
            SetLastClaimedDay(0); 
        }
    }
    
    public int GetLastClaimedDay() {
        return ChasePlayerDataComponent.ChaseLastClaimedDay;
    }   
}
