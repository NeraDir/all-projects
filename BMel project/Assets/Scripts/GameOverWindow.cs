using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverWindow : MonoBehaviour
{
    public TMP_Text accumulatedScoreDisplay;



    private void OnEnable()
    {
        accumulatedScoreDisplay.text = "SCORE\n" + Game.scorecount.ToString();
    }
}
