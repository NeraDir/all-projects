using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LosePanel : MonoBehaviour
{
    public TMP_Text score1;

    public void SetScore(int score)
    {
        score1.text = score.ToString();
    }
}
