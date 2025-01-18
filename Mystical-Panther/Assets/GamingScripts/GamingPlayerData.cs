using TMPro;
using UnityEngine;

public class GamingPlayerData : MonoBehaviour
{
    [SerializeField]
    private TMP_Text[] m_playerPointsDisplayer;

    private string[] m_stringPieces = { "a", "b", "c", "v", "n", "m", "z", "x", "s", "d", "f", "g", "h", "j", "k", "l", "q", "w", "e", "r", "t", "y", "u", "i", "o", "p" };

    private static string m_playerName;

    private float m_PlayerFreeCoinsSeconds = 30;

    [SerializeField]
    private TMP_Text m_GamingDisplayFreeTXT;

    public static int playerPoints
    {
        get
        {
            if (PlayerPrefs.HasKey(m_playerName))
            {
                return PlayerPrefs.GetInt(m_playerName);
            }
            return 4500;
        }
        set
        {
            PlayerPrefs.SetInt(m_playerName, value);
        }
    }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("PlayerName"))
        {
            m_playerName = GetRandomPlayerName();
            PlayerPrefs.SetString("PlayerName",m_playerName);
        }
    }

    private string GetRandomPlayerName() 
    {
        string name = "";
        for (int i = 0; i < Random.Range(5, 15); i++)
        {
            name += m_stringPieces[Random.Range(0, m_stringPieces.Length)];
        }
        return name;
    }

    private void LateUpdate()
    {
        m_PlayerFreeCoinsSeconds -= Time.deltaTime;
        if (m_PlayerFreeCoinsSeconds <= 0)
        {
            playerPoints += 100;
            m_PlayerFreeCoinsSeconds = 30;
        }
        if (m_GamingDisplayFreeTXT != null)
        {
            if (m_PlayerFreeCoinsSeconds > 10)
            {
                m_GamingDisplayFreeTXT.text = $"FREE 100 <style=\"H3\">E</style> 00:{(int)m_PlayerFreeCoinsSeconds}";
            }
            else
            {
                m_GamingDisplayFreeTXT.text = $"FREE 100 <style=\"H3\">E</style> 00:0{(int)m_PlayerFreeCoinsSeconds}";
            }
        }

        foreach (var item in m_playerPointsDisplayer)
        {
            item.text = playerPoints.ToString("0") + " <style=\"H3\">E</style>";
        }
    }
}
