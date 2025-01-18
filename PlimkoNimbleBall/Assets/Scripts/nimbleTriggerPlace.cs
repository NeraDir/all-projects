using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class nimbleTriggerPlace : MonoBehaviour
{
    private TMP_Text _nimbleScoreTxt;

    public int nimbleScore;

    private void Start()
    {
        _nimbleScoreTxt = GetComponent<TMP_Text>();
        nimbleScore = Random.Range(5, 10) * (nimbleGameManager.nimbleCurentLevel + 1);
        _nimbleScoreTxt.text = nimbleScore.ToString();
    }
}
