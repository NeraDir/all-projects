using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class GamngSlotRotating : MonoBehaviour, IPointerClickHandler
{
    public GamingSlotConfig[] config;

    [SerializeField]
    private Animator m_slotAnimator;

    public static bool isRolling;

    public static int bet 
    {
        get 
        {
            if (PlayerPrefs.HasKey("SavingSlotterC"))
            {
                return PlayerPrefs.GetInt("SavingSlotterC");
            }
            return 100;
        }
        set 
        {
            PlayerPrefs.SetInt("SavingSlotterC",value);
        }
    }
    public static int won;

    public int minBetValue;
    public int maxBetValue;

    [SerializeField]
    private TMP_Text m_BetDispaly;

    [SerializeField]
    private TMP_Text m_WonDisplay;

    public int m_GamingMenuIndex;

    [SerializeField]
    private TMP_Text m_ShowSpinsResult;

    public static int spinsResult;

    public void Awake()
    {
        spinsResult = 0;
    }

    public void onSpiningWhell() 
    {
        if (isRolling)
            return;
        if (GamingSnakeSpawner.countOfSnakes <= 0)
            return;

        GamingSnakeSpawner.countOfSnakes--;
        for (int i = 0; i < config.Length; i++)
        {
            config[i].SetDefault();
        }
        spinsResult += won;
        won = 0;
        m_slotAnimator.SetBool("ROTATE", false);
        Invoke(nameof(Rolling),0.1f);
    }

    private void Rolling() 
    {
        m_slotAnimator.SetBool("ROTATE", true);
       
        isRolling = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onSpiningWhell();
    }

    public void OnClickBetUp() 
    {
        if (bet + 5 > maxBetValue)
        {
            return;
        }
        bet += 5;
    }

    private void LateUpdate()
    {
        m_BetDispaly.text = bet.ToString("0") + " <style=\"H3\">E</style>";
        m_WonDisplay.text = won.ToString("0") + " <style=\"H3\">E</style>";
        m_ShowSpinsResult.text = spinsResult.ToString("0");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            onSpiningWhell();
        }
    }

    public void OnClickBetMinus() 
    {
        if (bet - 5 < minBetValue)
        {
            return;
        }
        bet -= 5;
    }

    public void OnClickBackToMenu() 
    {
        SceneManager.LoadScene(m_GamingMenuIndex);
    }
}
