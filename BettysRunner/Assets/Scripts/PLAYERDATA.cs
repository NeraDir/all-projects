using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft;
using Newtonsoft.Json;

public class PLAYERDATA : MonoBehaviour
{
    private const string COINS_SAVE_KEY = "COINSCOUNTSAVEKEY";
    private const string RECORDS_SAVE_KEY = "RECORDSLISTSAVEKEY";
    private const string FIRST_ENTRY_SAVE_KEY = "FIRSTENTRYSAVEKEY";
    private const string MUSIC_VOLUME_SAVE_KEY = "MUSICVOLUMESAVEKEY";
    private const string SOUNDS_VOLUME_SAVE_KEY = "SOUNDVOLUMESAVEKEY";
    private const string BACKGROUND_INDEX_SAVE_KEY = "BACKGROUNDINDEXSAVEKEY";

    public static int COINS
    {
        get => PlayerPrefs.GetInt(COINS_SAVE_KEY, 100);
        set => PlayerPrefs.SetInt(COINS_SAVE_KEY, value);
    }

    public static int BACKGROUNDINDEX
    {
        get => PlayerPrefs.GetInt(BACKGROUND_INDEX_SAVE_KEY, 0);
        set => PlayerPrefs.SetInt(BACKGROUND_INDEX_SAVE_KEY, value);
    }

    public static bool FIRSTENTRY
    {
        get => bool.Parse(PlayerPrefs.GetString(FIRST_ENTRY_SAVE_KEY, "false"));
        set => PlayerPrefs.SetString(FIRST_ENTRY_SAVE_KEY, value.ToString());
    }

    public static float MUSICVOLUME
    {
        get => PlayerPrefs.GetFloat(MUSIC_VOLUME_SAVE_KEY, 1);
        set => PlayerPrefs.SetFloat(MUSIC_VOLUME_SAVE_KEY, value);
    }

    public static float SOUNDVOLUME
    {
        get => PlayerPrefs.GetFloat(SOUNDS_VOLUME_SAVE_KEY, 1);
        set => PlayerPrefs.SetFloat(SOUNDS_VOLUME_SAVE_KEY, value);
    }

    public static List<string> RECORDS
    {
        get
        {
            string json = PlayerPrefs.GetString(RECORDS_SAVE_KEY, "[]");
            Debug.Log($"Загружаем данные: {json}");
            return JsonConvert.DeserializeObject<List<string>>(json) ?? new List<string>();
        }
        set
        {
            if (value != null)
            {
                string json1 = PlayerPrefs.GetString(RECORDS_SAVE_KEY, "[]");
                Debug.Log($"Загружаем данные перед обновлением: {json1}");

                List<string> newList = JsonConvert.DeserializeObject<List<string>>(json1) ?? new List<string>();
                newList.AddRange(value); 

                string json = JsonConvert.SerializeObject(newList);
                Debug.Log($"Сохраняем данные: {json}");

                PlayerPrefs.SetString(RECORDS_SAVE_KEY, json);
                PlayerPrefs.Save();
            }
        }
    }

}
