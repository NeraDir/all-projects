using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class SlConfig
{
    public string lineName;

    public PlaceTriggerS[] line;

    public List<PlaceTriggerS> winningPlaces = new List<PlaceTriggerS>();

    public GameObject linerShower;

    public GameObject wonShower;


    public void GetWinningLines()
    {
        for (var i = 0; i < line.Length; i++)
        {
            if (line[i].GetCurrentTriggeredSlot() != null)
            {
                if (i == 0)
                {
                    winningPlaces.Add(line[i]);
                }
                else
                {
                    if (line[i].GetCurrentTriggeredSlot().index == line[0].GetCurrentTriggeredSlot().index)
                    {
                        winningPlaces.Add(line[i]);
                    }
                }
            }
        }

        if (winningPlaces.Count >= 3)
        {
            foreach (var win in winningPlaces)
            {
                win.currentContainer.GetComponentInChildren<Animator>().SetBool("casted", true);
                SlotButtonManager.wonVar += (int)win.currentContainer.price;

                
            }

          

            wonShower.SetActive(true);
            linerShower.SetActive(true);
        }
    }

    public void SetDefault() 
    {
        foreach (var win in winningPlaces)
        {
            win.currentContainer.GetComponentInChildren<Animator>().SetBool("casted", false);
        }
        winningPlaces.Clear();
        linerShower.SetActive(false);
        wonShower.SetActive(false);
    }
}
