using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MathManager : MonoBehaviour
{
    public MathComponent mathComponetnPref;

    public Transform[] spawnPositions;

    public TMP_Text showMathAlgebra;

    private int firstval;

    private int lastval;

    public static int resultVal;
    
    private void Start()
    {
        SpawnNewMAths();
    }

    public void SpawnNewMAths() 
    {
        firstval = Random.Range(0, 100);
        lastval = Random.Range(0, 100);
        int i = 0;

        List<MathComponent> newMathers = new List<MathComponent>();
        foreach (var item in spawnPositions)
        {
            MathComponent tempItem = Instantiate(mathComponetnPref, item.position, item.rotation);
            tempItem.value = Random.Range(50, 200);
            tempItem.index = i;
            i++;
            tempItem.Inuit();
            newMathers.Add(tempItem);
        }

        float temperVal = firstval + lastval;
        resultVal = (int)temperVal;
        int rndMath = Random.Range(0, newMathers.Count);
        newMathers[rndMath].value = temperVal;
        newMathers[rndMath].Inuit();
        showMathAlgebra.text = $"{firstval}+{lastval}=?";
    }
}
