using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class packComponent : MonoBehaviour
{
    [SerializeField] private List<WinLineData> winLineDatas = new List<WinLineData>();

    [SerializeField] private UILineConnector _connectorPrefab;

    public bool CheckLines()
    {
        foreach (WinLineData line in winLineDatas)
        {
            UILineConnector newLineConnector = Instantiate(_connectorPrefab, transform);
            line.GetWinLine(newLineConnector);
        }
        return true;
    }

    public void DestroyLines()
    {
        foreach (WinLineData line in winLineDatas)
        {
            line.Clear();
            if(line.GetConnector() != null)
                Destroy(line.GetConnector().gameObject);
        }

    }

    public void OnSpinFinish()
    {
        ChaseSlotController.spinIsFinish?.Invoke();
    }

    public void OnSpinBeggining()
    {
        ChaseSlotController.spinIsBeggining?.Invoke();
    }

    public void OnHideEnd()
    {
        ChaseSlotController.spinHide?.Invoke();
    }
}
