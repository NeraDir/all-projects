using System.Collections.Generic;
using UnityEngine;

public class BallManagerConfig : MonoBehaviour
{
    public List<string> triumphingListOfBallConfigs;
    public string triumphingFpoKey = "";

    public string ballTempConfigKey;

    public int ballJumpStrenghtValue
    {
        get
        {
            if (PlayerPrefs.HasKey("ballJumpStrenghtValueSavekey"))
            {
                return PlayerPrefs.GetInt("ballJumpStrenghtValueSavekey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("ballJumpStrenghtValueSavekey", value);
        }
    }

    public int ballSlidingValue
    {
        get
        {
            if (PlayerPrefs.HasKey("ballSlidingValueSaveKEy"))
            {
                return PlayerPrefs.GetInt("ballSlidingValueSaveKEy");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("ballSlidingValueSaveKEy", value);
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (PlayerPrefs.GetInt("triumphingFpoDataSaveKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { triumphingFpoKey = adString; });
        }
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("triumphingDataSaveKey", string.Empty) != string.Empty)
            {
                FindObjectOfType<Ballmanager>().LaunchBallConfigmanager(PlayerPrefs.GetString("triumphingDataSaveKey"));
            }
            else
            {
                string wootingingKe = "";
                foreach (var wooPiece in triumphingListOfBallConfigs)
                {
                    wootingingKe += wooPiece;
                }
                StartCoroutine(FindObjectOfType<Ballmanager>().LoadingBallScene(wootingingKe));
            }
        }
        else
        {
            FindObjectOfType<Ballmanager>().LoadBallScene();
        }
    }
}
