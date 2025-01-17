using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;


[System.Serializable]
public class TableLines 
{
    public FallFruitsTrigger[] line;
    private List<FallFruitsTrigger> correctListOftriggers = new List<FallFruitsTrigger>();

    [SerializeField]
    private UILineRenderer _lineRenderer;

    private UILineConnector lineRenderer;

    public void GetStateOfLine() 
    {
        correctListOftriggers.Clear();
        for (int i = 0; i < line.Length; i++) 
        {
            if (i == 0)
            {
                correctListOftriggers.Add(line[i]);
            }
            else
            {
                if (line[i].GetIndex() == correctListOftriggers[0].GetIndex())
                {
                    correctListOftriggers.Add(line[i]);
                }
                else
                {
                    continue;
                }
            }
        }

        if (correctListOftriggers.Count >= 3)
        {
            if (lineRenderer != null)
            {

            }
            else
            {
                lineRenderer = GameObject.Instantiate(_lineRenderer.GetComponent<UILineConnector>(), GameObject.FindObjectOfType<TableController>().transform.parent);
            }

            foreach (var item in correctListOftriggers)
            {
                TableController.winningValue += (((item.GetIndex() + 1) * 2) * (TableController.bet / 3)) * GameController.currentLevel;
            }
            RectTransform[] victors = new RectTransform[3];
            for (int i = 0; i < victors.Length; i++)
            {
                victors[i] = correctListOftriggers[i].GetComponent<RectTransform>();
            }
            lineRenderer.transforms = victors;
        }
    }

    public void Default() 
    {
        if (lineRenderer != null)
            GameObject.Destroy(lineRenderer.gameObject);
    }
}
public class TableController : MonoBehaviour
{
    private Animator _animator;

    [SerializeField]
    private SettingRandomFruits[] _fruitsRandoms;

    [SerializeField]
    private FallFruitsComponent[] _fallFruitsComponents;

    [SerializeField]
    private TableLines[] liners;

    [SerializeField]
    private Text _showBet;

    [SerializeField]
    private Text _showWin;

    [SerializeField]
    private GameObject _resultPage;

    public static int winningValue;

    public static int bet;

    public static bool cantClick;

    private void Awake()
    {
        winningValue = 0;
        bet = 10;
        cantClick = false;
        _animator = GetComponent<Animator>();
        GameController.isBeginSlotStart.AddListener(BeginSlotting);
        foreach (var item in _fruitsRandoms)
        {
            item.SetImages();
        }
    }

    private void LateUpdate()
    {
        _showWin.text = "x" + winningValue.ToString("0");
        _showBet.text = "x" + bet.ToString("0");
    }

    private void BeginSlotting() 
    {
        if (GameController.currentCoins >= bet)
        {
            GameController.currentCoins -= bet;
            _animator.SetBool("isSlotting", true);
        }
        else
        {
            _resultPage.SetActive(true);
        }
    }

    public void OnClickChangeBet(int value) 
    {
        if (bet + (value * 10) < 10)
            return;
        if (bet +(value * 10)> GameController.currentCoins)
            return;
        bet += (value * 10);
    }

    private void OnDestroy()
    {
        GameController.isBeginSlotStart.RemoveAllListeners();
    }

    public void OnBeginRotate() 
    {
        foreach (var item in _fruitsRandoms)
        {
            item.SetNewPackOfFruits();
        }
    }

    public void ChangeState()
    {
        cantClick = false;
        foreach (var item in liners)
        {
            item.GetStateOfLine();
        }
    }

    public void OnEndRotate()
    {
        foreach (var item in liners)
        {
            item.Default();
        }
        GameController.currentCoins += winningValue;
        winningValue = 0;
        _animator.SetBool("isSlotting", false);
    }

    public void OnSetNewFallFruits() 
    {
        foreach (var item in _fallFruitsComponents)
        {
            item.Init();
        }
    }
}
