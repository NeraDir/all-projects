using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileData : MonoBehaviour
{
    public static float BettersMusicVolume
    {
        get => PlayerPrefs.GetFloat("BettersMusicVolumeSaveKey", 1);
        set => PlayerPrefs.SetFloat("BettersMusicVolumeSaveKey", value);
    }

    public static float BettersSoundVolume
    {
        get => PlayerPrefs.GetFloat("BettersSoundVolumeSaveKey", 1);
        set => PlayerPrefs.SetFloat("BettersSoundVolumeSaveKey", value);
    }

    public static bool BettersPlayerFirstEntry
    {
        get => bool.Parse(PlayerPrefs.GetString("BettersPlayerFirstEntrySaveKey", "false"));
        set => PlayerPrefs.SetString("BettersPlayerFirstEntrySaveKey", value.ToString());
    }

    public static int BettysPlayerLevel
    {
        get => PlayerPrefs.GetInt("BettersPlayerlevelSaveKey", 0);
        set => PlayerPrefs.SetInt("BettersPlayerlevelSaveKey", value);
    }

    public static int BettysSkinIndex
    {
        get => PlayerPrefs.GetInt("BettysSkinIndexSaveKey", 0);
        set => PlayerPrefs.SetInt("BettysSkinIndexSaveKey", value);
    }

    public static int BettysMaxScore
    {
        get => PlayerPrefs.GetInt("BettysMaxScoreSaveKey", 0);
        set => PlayerPrefs.SetInt("BettysMaxScoreSaveKey", value);
    }

    public static float BettysExpCounToNeed
    {
        get => PlayerPrefs.GetFloat("BettysExpCounToNeedSaveKey", 10);
        set => PlayerPrefs.SetFloat("BettysExpCounToNeedSaveKey", value);
    }

    public static float BettysExptCurrentCount
    {
        get => PlayerPrefs.GetFloat("BettysExptCurrentCountSaveKey", 0);
        set => PlayerPrefs.SetFloat("BettysExptCurrentCountSaveKey", value);
    }

    public static int BettysPlayerCoins
    {
        get => PlayerPrefs.GetInt("BettysPlayerCoinsSaveKey", 0);
        set => PlayerPrefs.SetInt("BettysPlayerCoinsSaveKey", value);
    }

    public static string BettysPlayerName
    {
        get => PlayerPrefs.GetString("BettysPlayerCoinsSaveKey", "");
        set => PlayerPrefs.SetString("BettysPlayerCoinsSaveKey", value);
    }

    public static int BettysPlayerCurrentLevel
    {
        get => PlayerPrefs.GetInt("BettysPlayerCurrentLevelSaveKey", 0);
        set => PlayerPrefs.SetInt("BettysPlayerCurrentLevelSaveKey", value);
    }

    public static int BettysPlayerMaxLevel
    {
        get => PlayerPrefs.GetInt("BettysPlayerMaxLevelSaveKey", 0);
        set => PlayerPrefs.SetInt("BettysPlayerMaxLevelSaveKey", value);
    }

    public static List<int> BettysPlayerSkinsBoughtList
    {
        get
        {
            var json = PlayerPrefs.GetString("BettysPlayerBoughtSknsListSaveKey", null);
            if (string.IsNullOrEmpty(json))
            {
                return new List<int>();
            }
            try
            {
                return JsonConvert.DeserializeObject<List<int>>(json) ?? new List<int>();
            }
            catch (Exception ex)
            {
                Debug.LogError("Error deserializing BettysPlayerSkinsBoughtList: " + ex.Message);
                return new List<int>();
            }
        }
    }

    public static void AddSkin(int index)
    {
        var list = BettysPlayerSkinsBoughtList;
        list.Add(index);
        PlayerPrefs.SetString("BettysPlayerBoughtSknsListSaveKey", JsonConvert.SerializeObject(list));
        PlayerPrefs.Save();
    }
}
