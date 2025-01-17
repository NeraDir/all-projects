using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsGame : MonoBehaviour
{
    [SerializeField] private GameObject panelStats;

    [SerializeField] private GameObject wheelPanel;

    [SerializeField] private TMPro.TextMeshProUGUI textMoney;
    [SerializeField] private TMPro.TextMeshProUGUI textBoxes;

    private int stats;
    public static StatisticsGame instant;

    private void Awake()
    {
        instant = this;
    }

    public void AddStats()
    {
        stats++;

        textMoney.text = stats.ToString();
        textBoxes.text = stats.ToString();
    }

    public void ReloadStats()
    {
        textMoney.text = "0";
        textBoxes.text = "0";
        stats = 0;
    }

    public void OpenStats()
    {
        panelStats.SetActive(true);
        Invoke(nameof(OpenWheel), 0.4f);
    }

    private void OpenWheel() 
    {
        wheelPanel.SetActive(true);
    }
}
