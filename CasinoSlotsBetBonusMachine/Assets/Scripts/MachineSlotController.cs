using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SlotLines
{
    public machineSlottriggerer[] currentLine;
    private List<machineSlottriggerer> succesLine = new List<machineSlottriggerer>();

    private LineRenderer lineRenderer;

    [SerializeField]
    private MachineBoxerAniamtionController myAniamtion;

    [SerializeField]
    private MachineSlotController mySlotController;

    public void GetWinLine()
    {
        succesLine.Clear();
        for (int i = 0; i < currentLine.Length; i++)
        {
            if (i == 0)
            {
                succesLine.Add(currentLine[i]);
            }
            else
            {
                if (currentLine[i].GetIndex() == succesLine[0].GetIndex())
                {
                    succesLine.Add(currentLine[i]);
                }
                else
                {
                    continue;
                }
            }
        }

        if (succesLine.Count >= 3)
        {
            int valueOFWin = 0;
            List<Vector3> listOfPos = new List<Vector3>();
            foreach (var item in succesLine)
            {
                listOfPos.Add(item.transform.position);
            }
            lineRenderer = GameObject.Instantiate(MachineGameController.staticLineRenderer, Vector3.zero, Quaternion.identity);
            lineRenderer.SetPositions(listOfPos.ToArray());
            myAniamtion.SetAniamtion(succesLine[0].GetIndex() + 1);
            valueOFWin += ((succesLine[0].GetIndex() + 1) * MachineGameController.ValueOfBet) /3;
            foreach (var item in myAniamtion.GetComponentsInChildren<MachineBoxerTrigger>())
            {
                item.Damage += (10 * (succesLine[0].GetIndex() + 1))/3;
            }
            mySlotController.onWin?.Invoke(valueOFWin);
        }
    }

    public void Clear()
    {
        if (lineRenderer != null)
            GameObject.Destroy(lineRenderer.gameObject);
        foreach (var item in myAniamtion.GetComponentsInChildren<MachineBoxerTrigger>())
        {
            item.Damage = 0;
        }
    }
}

public class MachineSlotController : MonoBehaviour
{
    private Animator animator;

    private MeshRenderer[] slotItemsRenderers;

    private MeshFilter[] slotItemsFilters;

    [SerializeField]
    private TMP_Text showWin;

    [SerializeField]
    private Mesh[] itemMeshes;

    [SerializeField]
    private Material[] itemMaterials;

    [SerializeField]
    private Transform[] linesVisual;

    [SerializeField]
    private MachineSlotItem[] slotItemsResults;

    [SerializeField]
    private SlotLines[] lines;

    public UnityEvent<int> onWin = new UnityEvent<int>();

    public static bool cantClick;

    public bool isPlayerMachine;

    private int winvalue;

    private void Awake()
    {
        cantClick = false;
        animator = GetComponent<Animator>();
        foreach (var item in linesVisual)
        {
            slotItemsFilters = item.GetComponentsInChildren<MeshFilter>();
            slotItemsRenderers = item.GetComponentsInChildren<MeshRenderer>();
        }
        
        
        for (int i = 0; i < slotItemsFilters.Length; i++)
        {
            int rndIndex = Random.Range(0, itemMeshes.Length);
            slotItemsFilters[i].mesh = itemMeshes[rndIndex];
            slotItemsRenderers[i].material = itemMaterials[rndIndex];
        }
        showWin.text = "0";
        onWin.AddListener(OnShowWin);
        MachineGameController.shootOnClick.AddListener(LaunchSlot);
    }

    private void OnShowWin(int value) 
    {
        winvalue += value;
        showWin.text = winvalue.ToString();
    }

    private void LaunchSlot()
    {
        if (isPlayerMachine)
        {
            MachineGameDataSaver.MachineBoxerPlayerPlayBalance += winvalue;
        }
        showWin.text = "0";
        winvalue = 0;
        animator.SetBool("isSlotting", true);
    }

    private void OnDestroy()
    {
        MachineGameController.shootOnClick.RemoveListener(LaunchSlot);
        onWin.RemoveListener(OnShowWin);
    }

    public void OnStartItems()
    {
        for (int i = 0; i < slotItemsFilters.Length; i++)
        {
            int rndIndex = Random.Range(0, itemMeshes.Length);
            slotItemsFilters[i].mesh = itemMeshes[rndIndex];
            slotItemsRenderers[i].material = itemMaterials[rndIndex];
            slotItemsFilters[i].GetComponent<MaskingComponent>().UpdateVisual();
        }
    }

    public void OnSlotEnd()
    {
        cantClick = false;
        foreach (var item in lines)
        {
            item.GetWinLine();
        }
        Invoke(nameof(SetDefaults), 2);
    }

    public void SetDefaults()
    {
        foreach (var item in lines)
        {
            item.Clear();
        }
        animator.SetBool("isSlotting", false);
        MachineGameController.canClick = true;
    }

    public void OnSetSlotItems()
    {
        foreach (var item in slotItemsResults)
        {
            item.Init();
        }
    }
}
