using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookjHandler : MonoBehaviour
{
    public string woothingKey;

    public static int wootingSavingValue
    {
        get
        {
            if (PlayerPrefs.HasKey("wootingSavingValueKey"))
            {
                return PlayerPrefs.GetInt("wootingSavingValueKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("wootingSavingValueKey", value);
        }
    }

    public static int woothingBookPagesCount
    {
        get
        {
            if (PlayerPrefs.HasKey("woothingBookPagesCountSaveKey"))
            {
                return PlayerPrefs.GetInt("woothingBookPagesCountSaveKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("woothingBookPagesCountSaveKey", value);
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
