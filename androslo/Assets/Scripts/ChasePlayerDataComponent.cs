using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayerDataComponent : MonoBehaviour
{
   public static int ChasePlayerMaxReachedLevel
   {
      get => PlayerPrefs.HasKey("ChasePlayerReachedMaxLevelValueSaveKey") ? PlayerPrefs.GetInt("ChasePlayerReachedMaxLevelValueSaveKey") : 0;
      set => PlayerPrefs.SetInt("ChasePlayerReachedMaxLevelValueSaveKey", value);
   }
   
   public static int ChasePlayerCurrentLevel
   {
      get => PlayerPrefs.HasKey("ChasePlayerCurrentLevelSaveKey") ? PlayerPrefs.GetInt("ChasePlayerCurrentLevelSaveKey") : 0;
      set => PlayerPrefs.SetInt("ChasePlayerCurrentLevelSaveKey", value);
   }

   public static string ChasePlayerBackgroundSpriteName
   {
      get => PlayerPrefs.HasKey("CHasePlayerBackgroundSpriteNameSaveKey") ? PlayerPrefs.GetString("CHasePlayerBackgroundSpriteNameSaveKey") : "1";
      set => PlayerPrefs.SetString("CHasePlayerBackgroundSpriteNameSaveKey", value);
   }
   
   public static int ChasePlayerCoins
   {
      get => PlayerPrefs.HasKey("ChasePlayerCoinsValueSaveKey") ? PlayerPrefs.GetInt("ChasePlayerCoinsValueSaveKey") : 2000;
      set => PlayerPrefs.SetInt("ChasePlayerCoinsValueSaveKey", value);
   }

   public static DateTime? ChasePlayerLastEntryTime
   {
      get => PlayerPrefs.HasKey("ChasePlayerLastEntryTimeSaveKey") ? DateTime.Parse(PlayerPrefs.GetString("ChasePlayerLastEntryTimeSaveKey")) : null;
      set => PlayerPrefs.SetString("ChasePlayerLastEntryTimeSaveKey", value.ToString());
   }

   public static int ChaseClaimableDay
   {
      get => PlayerPrefs.HasKey("ChaseClaimableDaySaveKey") ? PlayerPrefs.GetInt("ChaseClaimableDaySaveKey") : 0;
      set => PlayerPrefs.SetInt("ChaseClaimableDaySaveKey", value);
   }
   
   public static int ChaseLastClaimedDay
   {
      get => PlayerPrefs.HasKey("ChaseLastClaimedDaySaveKey") ? PlayerPrefs.GetInt("ChaseLastClaimedDaySaveKey") : 0;
      set => PlayerPrefs.SetInt("ChaseLastClaimedDaySaveKey", value);
   }

    public static float ChaseMuiscVolume
    {
        get => PlayerPrefs.HasKey("ChaseMuiscVolumeSaveKey") ? PlayerPrefs.GetFloat("ChaseMuiscVolumeSaveKey") : 1;
        set => PlayerPrefs.SetFloat("ChaseMuiscVolumeSaveKey", value);
    }

    public static float ChaseSoundsVolume
    {
        get => PlayerPrefs.HasKey("ChaseSoundsVolumeSaveKey") ? PlayerPrefs.GetFloat("ChaseSoundsVolumeSaveKey") : 1;
        set => PlayerPrefs.SetFloat("ChaseSoundsVolumeSaveKey", value);
    }

    public static bool ChaseVibrationState
    {
        get => PlayerPrefs.HasKey("ChaseVibrationStateSaveKey") ? Convert.ToBoolean(PlayerPrefs.GetString("ChaseVibrationStateSaveKey")) : true;
        set => PlayerPrefs.SetString("ChaseVibrationStateSaveKey", value.ToString());
    }
}
