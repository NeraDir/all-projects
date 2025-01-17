using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CirclGameController : MonoBehaviour
{
    public List<Sprite> knifeModles = new List<Sprite>();
    public Image KnifePrefab;
    public Transform parrent;
    public Transform spawnPosition;
    private Image Knife;
    private int KnifeCount;
    public TMP_Text modificatorTXT;

    public TMP_Text KnifesTryes;

    private List<GameObject> KnifeList = new();
    public int modificatorsAmout = 1;
    private int bet;

    public TMP_Text WinTextAmount;
    public GameObject WinPanel;

    public void Init(int count, int bet)
    {
        KnifeCount = count;
        modificatorsAmout = 1;
        modificatorTXT.text = $"{bet}";
        SpawnNewKnife();
        this.bet = bet;

        KnifesTryes.text = $"Attempts Count: {KnifeCount}";
    }

    public void SpawnNewKnife()
    {
        if (KnifeCount > 0)
        {
            Knife = Instantiate(KnifePrefab, spawnPosition.position, KnifePrefab.transform.rotation, parrent);
            Knife.sprite = knifeModles[GlobalSave.KnifeIndex];
            KnifeList.Add(Knife.gameObject);
        }
        else
        {
            Knife = null;
            Invoke(nameof(ShowTable), 2f);
        }
    }

    public void ShowTable()
    {
        foreach (var item in KnifeList)
        {
            Destroy(item.gameObject);
        }

        KnifeList.Clear();
        modificatorTXT.text = $"";
        GlobalSave.CoinsCount += bet * modificatorsAmout;
        int winAmount = bet * modificatorsAmout;

        WinTextAmount.text = $"{winAmount}";
        WinPanel.SetActive(true);
    }

    public void AddX(int x)
    {
        modificatorTXT.text += $" x{x}";
        modificatorsAmout *= x;
    }

    public void Flip()
    {
        if (Knife != null)
        {
            Knife.AddComponent<GoUp>();
            KnifeCount--;
            KnifesTryes.text = $"Attempts Count: {KnifeCount}";
            SpawnNewKnife();
        }
    }

    public void GoPlay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
