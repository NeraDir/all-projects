using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class LoopIterController : MonoBehaviour, IPointerClickHandler
{
    public SlConfig[] config;

    [SerializeField]
    private Animator myAnimator;

    public static bool isLooping;

    public static int betVar;
    public static int wonVar;

    public int betMinVar;
    public int betMaxVar;

    [SerializeField]
    private TMP_Text betText;

    [SerializeField]
    private TMP_Text wonText;

    [SerializeField]
    private TMP_Text spinCountDisplay;

    public static int default_spinCount;

    [SerializeField]
    private GameObject lowSpinsCount;

    public delegate void UpdateSuperGameButton();
    public static UpdateSuperGameButton ClickRotButton;

    public void Awake()
    {
        betVar = betMinVar;
        
    }

    private void OnEnable()
    {
        default_spinCount = 3 + LevelController.spinCount;
    }

    public void LoopWheel() 
    {
        
        if (default_spinCount != 0)
        {

            
            if (isLooping)
                return;
            if (betVar > SlotPageManager.currentMoneyValue)
                return;

            if (SlotPageManager.currentMoneyValue <= 0)
                return;

            default_spinCount--;

            for (int i = 0; i < config.Length; i++)
            {
                config[i].SetDefault();
            }
            //GamingPlayerData.playerPoints += won;
            //GamingPlayerData.playerPoints -= bet;

            
            Debug.Log("SlotPageManager.currentGainValue + " + wonVar);
            SlotPageManager.currentMoneyValue += wonVar;
            SlotPageManager.currentMoneyValue -= betVar;

            wonVar = 0;

            if (ClickRotButton != null)
            {
                ClickRotButton();
            }


            myAnimator.SetBool("ROTATE", false);
            Invoke(nameof(Loop), 0.1f);
        }
        else
        {
            lowSpinsCount.SetActive(true);
        }
    }

    private void Loop() 
    {
        myAnimator.SetBool("ROTATE", true);
        isLooping = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        LoopWheel();
    }

    public void OnClickBetUp() 
    {
        if (betVar + 5 > betMaxVar)
        {
            return;
        }
        betVar += 5;
    }

    private void LateUpdate()
    {
        betText.text = betVar.ToString("0");
        wonText.text = wonVar.ToString("0");
        spinCountDisplay.text = default_spinCount.ToString("0");
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            LoopWheel();
        }
    }

    public void IncrementBetVar() 
    {
        if (betVar - 5 < betMinVar)
        {
            return;
        }
        betVar -= 5;
    }

}
