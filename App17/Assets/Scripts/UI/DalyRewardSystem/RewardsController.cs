using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RewardsController : MonoBehaviour
{
    public List<RewartItem> rewartItems = new();
    public int SpawnSpeed = 0;

    public GameObject ClaimText;
    public TMP_Text TimerText;

    private void Start()
    {
        StopAllCoroutines();
        StartCoroutine(SpawnStateUpdater());
    }

    public int LastClaimedID
    {
        get
        {
            if (!PlayerPrefs.HasKey("LastClaimedIDSave"))
                return 0;

            return PlayerPrefs.GetInt("LastClaimedIDSave");
        }
        set
        {
            PlayerPrefs.SetInt("LastClaimedIDSave", value);
        }
    }

    private bool canSpawn = false;

    private DateTime? lastClaimTime
    {
        get
        {
            string data = PlayerPrefs.GetString("lastClaimedTime", null);

            if (!string.IsNullOrEmpty(data))
                return DateTime.Parse(data);

            return null;
        }
        set
        {
            if (value != null)
                PlayerPrefs.SetString("lastClaimedTime", value.ToString());
            else
                PlayerPrefs.DeleteKey("lastClaimedTime");
        }
    }

    public IEnumerator SpawnStateUpdater()
    {
        while (true)
        {
            UpdateSpawnState();

            yield return new WaitForSeconds(1);
        }
    }

    private void UpdateSpawnState()
    {
        canSpawn = true;

        if (lastClaimTime.HasValue)
        {
            var timeSpan = DateTime.UtcNow - lastClaimTime.Value;
            if (timeSpan.TotalHours >= SpawnSpeed)
            {
                lastClaimTime = null;
            }
            else if (timeSpan.TotalHours <= SpawnSpeed)
            {
                canSpawn = false;
            }
        }

        if (canSpawn)
        {
            ClaimText.SetActive(true);
            TimerText.gameObject.SetActive(false);
        }
        else
        {
            ClaimText.SetActive(false);
            TimerText.gameObject.SetActive(true);

            var nextClaimTime = lastClaimTime.Value.AddHours(SpawnSpeed);
            var currentClaimCooldown = nextClaimTime - DateTime.UtcNow;
            string _cooldown = $"{currentClaimCooldown.Hours:D2}:{currentClaimCooldown.Minutes:D2}:{currentClaimCooldown.Seconds:D2}";
            TimerText.text = _cooldown;
        }

        //CheckSpawnState();
    }

    public void CheckSpawnState()
    {
        if (canSpawn)
        {
            lastClaimTime = DateTime.UtcNow;
            canSpawn = false;

            SaverManager.Coins += rewartItems[LastClaimedID].Cost;
            UIManager.Instance.RefreshCoinsTXT();

            if (LastClaimedID >= rewartItems.Count - 1)
                LastClaimedID = 0;
            else
                LastClaimedID++;
        }
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(SpawnStateUpdater());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
