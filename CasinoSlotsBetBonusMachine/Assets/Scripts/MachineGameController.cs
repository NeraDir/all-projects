using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MachineGameController : MonoBehaviour
{
    public static UnityEvent shootOnClick = new UnityEvent();

    public static UnityEvent<int> betChanged = new UnityEvent<int>();

    public static UnityEvent changeViewMode = new UnityEvent();

    public static bool canClick;

    public static bool isGameEnd;

    public static bool isGameStarted;

    public static int ValueOfBet;

    public static AudioSource staticAudioPlayer;

    public static LineRenderer staticLineRenderer;

    [SerializeField]
    private GameObject gameInstruction;

    [SerializeField]
    private TMP_Text showWhoWin;

    [SerializeField]
    private TMP_Text showBalance;

    [SerializeField]
    private TMP_Text showBet;

    [SerializeField]
    private Animator mainGameAnimator;

    [SerializeField]
    private GameObject resultGamePage;

    [SerializeField]
    private MachineBoxerAniamtionController playerAniamtor;

    [SerializeField]
    private MachineBoxerAniamtionController botAniamtor;

    [SerializeField]
    private AudioSource audioPlayer;

    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private Transform[] slots;

    [SerializeField]
    private Transform[] slots2D;

    [SerializeField]
    private Transform[] slots3D;

    private bool is2D;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        if (!PlayerPrefs.HasKey("PlayersSeeInstructionForGameBoxingSaveKey"))
        {
            gameInstruction.SetActive(true);

        }
        else
        {
            isGameStarted = true;
        }
        is2D = false;
        ValueOfBet = 10;
        staticAudioPlayer = audioPlayer;
        staticLineRenderer = lineRenderer;
        CharacterComponent.isCharacterDeath.AddListener(OnDeath);
        betChanged.AddListener(ChangeValueOfBet);
        isGameEnd = false;
        canClick = true;
        showWhoWin.text = "";
        changeViewMode.AddListener(OnChangeView);
        StartCoroutine(AddMoney());
    }

    private void OnChangeView() 
    {
        is2D = !is2D;
        if (is2D)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].DOMove(slots2D[i].position, 0.25f);
                slots[i].DORotateQuaternion(slots2D[i].rotation, 0.25f);
            }
        }
        else
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].DOMove(slots3D[i].position, 0.25f);
                slots[i].DORotateQuaternion(slots3D[i].rotation, 0.25f);
            }
        }
    }

    public void OnSettings(bool inputBool)
    {
        isGameStarted = inputBool;
    }

    private void OnDestroy()
    {
        changeViewMode.RemoveListener(OnChangeView);
        CharacterComponent.isCharacterDeath.RemoveListener(OnDeath);
        betChanged.RemoveListener(ChangeValueOfBet);
    }

    public void OnCloseGameInstruction()
    {
        gameInstruction.SetActive(false);
        isGameStarted = true;
        PlayerPrefs.SetInt("PlayersSeeInstructionForGameBoxingSaveKey", 1);
    }

    private void OnDeath(bool whoIs)
    {
        isGameEnd = true;
        if (whoIs)
        {
            showWhoWin.text = "BOT \nWIN!";
            botAniamtor.SetAniamtion(7);
            playerAniamtor.SetAniamtion(6);
        }
        else
        {
            showWhoWin.text = "YOU \nWIN!";
            botAniamtor.SetAniamtion(6);
            playerAniamtor.SetAniamtion(7);
        }
        mainGameAnimator.SetBool("PAGESSTATES", true);
        Invoke(nameof(OnResult), 0.5f);
    }

    private void ChangeValueOfBet(int multi) 
    {
        ValueOfBet += 10 * multi;
        if (ValueOfBet >= MachineGameDataSaver.MachineBoxerPlayerPlayBalance)
        {
            ValueOfBet = MachineGameDataSaver.MachineBoxerPlayerPlayBalance;
        }
        if (ValueOfBet <= 10)
        {
            ValueOfBet = 10;
        }
    }

    private void LateUpdate()
    {
        showBet.text = ValueOfBet.ToString();
        showBalance.text = MachineGameDataSaver.MachineBoxerPlayerPlayBalance.ToString();
    }

    private IEnumerator AddMoney() 
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            MachineGameDataSaver.MachineBoxerPlayerPlayBalance += 10;
        }
    }

    private void OnResult() 
    {
        resultGamePage.SetActive(true);
        mainGameAnimator.gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        if(ValueOfBet <= 0)
            return;
        if (ValueOfBet > MachineGameDataSaver.MachineBoxerPlayerPlayBalance)
            return;
        if (!isGameStarted)
            return;
        if (isGameEnd)
            return;
        if (!canClick)
            return;
        canClick = false;
        MachineGameDataSaver.MachineBoxerPlayerPlayBalance -= ValueOfBet;
        shootOnClick?.Invoke();
    }
}
