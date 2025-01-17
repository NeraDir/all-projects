using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyBonusManager : MonoBehaviour
{
    private DateTime? lastCheckTime
    {
        get
        {
            if (PlayerPrefs.HasKey("CrazyLastCheckTimeSaveKey"))
                return DateTime.Parse(PlayerPrefs.GetString("CrazyLastCheckTimeSaveKey"));
            return null;
        }
        set
        {
            PlayerPrefs.SetString("CrazyLastCheckTimeSaveKey", value.ToString());
        }
    }

    [SerializeField]
    private TMP_Text _currentStateOfBonusTxt;

    [SerializeField]
    private Button _spinButton;

    [SerializeField]
    private DailyAdditionalManager _dailyAdditionalManager;

    private void Start()
    {
        if (lastCheckTime == null)
        {
            _spinButton.interactable = true;
            _currentStateOfBonusTxt.text = "READY";
        }
    }

    public void OnClickSpin()
    {
        Debug.Log("Clicked");
        lastCheckTime = DateTime.UtcNow.AddHours(24);
        _dailyAdditionalManager.Launch();
        UiCustomButton._buttonIsClicked = true;
        _spinButton.interactable = false;
    }

    private void LateUpdate()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (lastCheckTime == null)
            return;
        TimeSpan? span = lastCheckTime - DateTime.UtcNow;
        if (span.Value.TotalHours <= 0 && span.Value.TotalSeconds <= 0 && span.Value.TotalMinutes <= 0 && span.Value.TotalMilliseconds <= 0)
        {
            _spinButton.interactable = true;
            _currentStateOfBonusTxt.text = "READY";
        }
        else
        {
            _currentStateOfBonusTxt.text = $"{span.Value.Hours.ToString("00")}:{span.Value.Minutes.ToString("00")}:{span.Value.Seconds.ToString("00")}";
            _spinButton.interactable = false;
        }
    }
}
